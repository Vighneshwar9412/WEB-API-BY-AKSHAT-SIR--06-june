using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourQT.CommonFunctions.Portal;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities.Employee;
using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using FourQT.Entities.General;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;
using System.Reflection.PortableExecutable;

namespace FourQT.Core
{
    public class ForgotPasswordDLL
    {
        public async static Task<dynamic> forgotPasword(ForgotPasswordRequest model, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int emailSent = 0, smsSent = 0, whatsAppSent = 0;

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ForgotPasword", context);

                string keycode = "";
                if (model.token != null)
                {
                    keycode = Cryptography.Decrypt(Convert.ToString(model.token));
                }

                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault();
                if (check == null)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.IsSuccess = false;
                    genResponse.Title = "Unauthorized";
                    return genResponse;
                }

                string username="", mobile = "";
                Boolean validCreds = false;

                if (model.username != null && model.username.ToString().Trim()!="")
                {
                    username = model.username.ToString().Trim();

                    if (model.mobile != null && model.mobile.ToString().Trim()!="")
                    {
                        mobile = model.mobile.ToString().Trim();
                        validCreds = true;
                    }
                }

                if (validCreds)
                {
                    string type = (model.commonLoginType != null ? model.commonLoginType.ToString() : "");
                    if(type!="C" && type != "CP")
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Title = "Invalid";
                        genResponse.Message = "Invalid login type.";
                        return genResponse;
                    }                   

                    string spName = "API_ForgotPassword";
                    List<SqlParameter> lstParam = new List<SqlParameter>
                    {
                        new SqlParameter() { ParameterName = "@Status" , Value = 0 },
                        new SqlParameter() { ParameterName = "@OutMsg" , Value = "",SqlDbType=SqlDbType.VarChar,Size=200 },
                        new SqlParameter() { ParameterName = "@Action", Value = "R"  },
                        new SqlParameter() { ParameterName = "@Type", Value = type ,SqlDbType=SqlDbType.VarChar,Size=2 },
                        new SqlParameter() { ParameterName = "@Username", Value = username ,SqlDbType=SqlDbType.VarChar,Size=100 },
                        new SqlParameter() { ParameterName = "@Mobile", Value = mobile ,SqlDbType=SqlDbType.VarChar,Size=20 }
                    };

                    lstParam[0].Direction = ParameterDirection.Output;
                    lstParam[1].Direction = ParameterDirection.Output;

                    dynamic[]? result = null;
                    if (type == "CP" || type=="C")
                    {
                        result = await DBHelper.GetDataSetCP(keycode, CommandType.StoredProcedure, spName, lstParam);
                    }

                    if (result != null && result.Length >= 4)
                    {  
                        if (result[1] == "1")
                        {
                            DataSet ds = result[0];
                            DataTable userDetails = new DataTable();
                            DataTable emailConfig = new DataTable();
                            DataTable smsConfig = new DataTable();
                            DataTable smsTemplate = new DataTable(); 
                            DataTable emailBody = new DataTable();
                            DataTable whatsAppConfig = new DataTable();
                            DataTable whatsAppHeaders = new DataTable();

                            if (ds != null && ds.Tables.Count > 0)
                            {
                                userDetails = ds.Tables[0];
                                if (ds.Tables.Count > 1)
                                {
                                    emailConfig = ds.Tables[1];

                                    if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0) {
                                        emailBody = ds.Tables[4];
                                    }

                                    if (emailConfig != null && emailConfig.Rows.Count > 0)
                                    {
                                        emailSent = await SendPasswordByEmail(emailConfig, userDetails, emailBody);
                                    }
                                }
                            }
                          

                            if(ds!=null && ds.Tables.Count > 3)
                            {
                                smsConfig = ds.Tables[2];
                                smsTemplate = ds.Tables[3];

                                smsSent = await SendPasswordBySMS(userDetails, smsConfig, smsTemplate);
                            }

                            if (ds != null && ds.Tables.Count > 6)
                            {
                                whatsAppConfig = ds.Tables[5];
                                whatsAppHeaders = ds.Tables[6];

                                whatsAppSent = await SendPasswordByWhatsApp(userDetails, whatsAppConfig, whatsAppHeaders);
                            }

                            if (emailSent == 1 || smsSent == 1 || whatsAppSent == 1)
                            {
                                genResponse.IsSuccess = true;
                                genResponse.Status = HttpStatusCode.OK;
                                genResponse.Title = "Success";
                                genResponse.Message = "Password reset message sent to registered Mobile and Email-Id.";
                            }
                            else
                            {
                                genResponse.IsSuccess = false;
                                genResponse.Status = HttpStatusCode.BadRequest;
                                genResponse.Title = "Failed";
                                genResponse.Message = "Password reset message could not be sent.";
                            }
                        }
                        else
                        {
                            genResponse.IsSuccess = false;
                            genResponse.Status = HttpStatusCode.BadRequest;
                            genResponse.Title = "Invalid";
                            genResponse.Message = result[2].ToString();
                        }
                        
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Invalid";
                    genResponse.Message = "Invalid credentials.";
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async static Task<int> SendPasswordByEmail(DataTable emailConfigTable,DataTable userDetails, DataTable emailBody)
        {
            try
            {
                string? emailId = "",newPassword="";
                if(userDetails!=null && userDetails.Rows.Count > 0)
                {                    
                    newPassword = (userDetails.Rows[0]["NewPassword"] != null ? userDetails.Rows[0]["NewPassword"].ToString() : "");

                    DataRow drow = emailConfigTable.Rows[0];
                    MailMessage mailmsg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(drow["SmtpServer"].ToString());

                    string? mailId = drow["EmailId"].ToString();

                    for (int i = 0; i < userDetails.Rows.Count; i++) {
                        emailId = (userDetails.Rows[i]["Email"] != null ? userDetails.Rows[i]["Email"].ToString() : "");

                        if (mailId != null && emailId != null && mailId.Trim() != "" && emailId.Trim() != "")
                        {
                            string[] emailArr = emailId.Split(',');
                            for (int j = 0; j < emailArr.Length; j++) {
                                string email = emailArr[j].ToString().Trim();
                                if (email != "") {
                                    mailmsg.From = new MailAddress(mailId);
                                    mailmsg.To.Add(emailId);
                                    mailmsg.Subject = "Password Reset";
                                    mailmsg.Body = "New password: " + newPassword;
                                    mailmsg.IsBodyHtml = true;

                                    if (emailBody != null && emailBody.Rows.Count > 0) {
                                        mailmsg.Subject = (emailBody.Rows[0]["Subject"] != null ? emailBody.Rows[0]["Subject"].ToString() : mailmsg.Subject);
                                        mailmsg.Body = (emailBody.Rows[0]["Subject_Desp"] != null ? emailBody.Rows[0]["Subject_Desp"].ToString() : mailmsg.Body);
                                    }

                                    SmtpServer.Port = Convert.ToInt32(drow["PortNo"].ToString());
                                    SmtpServer.Credentials = new System.Net.NetworkCredential(drow["EmailId"].ToString(), drow["AccountPassword"].ToString());
                                    SmtpServer.EnableSsl = drow["SslEnable"].ToString() == "N" ? false : true;
                                    await SmtpServer.SendMailAsync(mailmsg);
                                }
                            }                       
                        }
                    }

                    return 1;
                }                            
            }
            catch(Exception ex) {
                return 0;
            }

            return 0;
        }

        public async static Task<int> SendPasswordBySMS(DataTable userDetails,DataTable smsConfig,DataTable smsTemplate)
        {
            int msgSent = 0;
            try
            {
                string? mobileNo = "", newPassword = "",message="";               

                if (userDetails != null && userDetails.Rows.Count > 0)
                {                   
                    newPassword = (userDetails.Rows[0]["NewPassword"] != null ? userDetails.Rows[0]["NewPassword"].ToString() : "");

                    for (int i = 0; i < userDetails.Rows.Count; i++) {
                        mobileNo = (userDetails.Rows[i]["Mobile"] != null ? userDetails.Rows[i]["Mobile"].ToString() : "");

                        if (mobileNo != null && mobileNo.Trim() != "")
                        {
                            string[] mobs = mobileNo.Split(',');
                            for (int j = 0; j < mobs.Length; j++) {

                                string smsMob = mobs[j].ToString().Trim();
                                if (smsMob != "") {
                                    if (smsTemplate != null && smsTemplate.Rows.Count > 0)
                                    {
                                        message = smsTemplate.Rows[0]["Subject_Desp"].ToString();
                                        if (message != null)
                                        {
                                            message = message.Replace("@OTP@", newPassword);

                                            if (smsConfig != null && smsConfig.Rows.Count > 0)
                                            {
                                                int sendStatus = await SendSMS(smsConfig, message, smsMob);
                                                if (sendStatus == 1) { msgSent = 1; }
                                            }
                                        }
                                    }
                                }
                            }
                           
                        }
                    }
                }               
            }
            catch (Exception ex)
            {
                return 0;
            }

            return msgSent;
        }

        private static async Task<int> SendSMS(DataTable smsConfig, string message, string mobileNo)
        {
            try
            {
                if (smsConfig != null && smsConfig.Rows.Count > 0)
                {
                    string? Add91 = Convert.ToString(smsConfig.Rows[0]["AddDigit91"]);
                    if(Add91!="Y" && Add91 != "N") { Add91 = "N"; }

                    string? Url = Convert.ToString(smsConfig.Rows[0]["Url"]);
                    string recipient = mobileNo;
                    if (Add91 == "Y")
                    {
                        recipient = "91" + recipient;
                    }

                    if (Url != null)
                    {
                        Url = Url.Replace("@MN@", recipient);
                        Url = Url.Replace("@MText@", message);
                        return await Send_SMS(Url);
                    }                  
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

            return 0;
        }

        private static async Task<int> Send_SMS(string Url)
        {
            try
            {
                if (Url != "")
                {
                    HttpClient httpClient = new HttpClient();
                    var response = await httpClient.PostAsync(Url, new StringContent("Json string", Encoding.UTF8, "application/json"));
                    return 1;
                }
            }
            catch (Exception ex) { return 0; }

            return 0;
        }

        public async static Task<int> SendPasswordByWhatsApp(DataTable userDetails, DataTable whatsAppConfig, DataTable apiHeaders)
        {

            string? mobile = "", url = "", json = "", outMsg = "WhatsApp message snding failed.", newPassword = "", add91 = "N";
            Boolean success = false;

            try
            {
                if (whatsAppConfig != null && whatsAppConfig.Rows.Count > 0)
                {
                    url = (whatsAppConfig.Rows[0]["APIUrl"] != null ? whatsAppConfig.Rows[0]["APIUrl"].ToString() : "");
                    json = (whatsAppConfig.Rows[0]["JSONBody"] != null ? whatsAppConfig.Rows[0]["JSONBody"].ToString() : "");
                    add91 = (whatsAppConfig.Rows[0]["AddDigit91"] != null ? whatsAppConfig.Rows[0]["AddDigit91"].ToString() : "N");
                }

                if (url != null && url != "")
                {
                    if (userDetails != null && userDetails.Rows.Count > 0)
                    {
                        newPassword = (userDetails.Rows[0]["NewPassword"] != null ? userDetails.Rows[0]["NewPassword"].ToString() : "");

                        for (int i = 0; i < userDetails.Rows.Count; i++)
                        {
                            mobile = (userDetails.Rows[i]["Mobile"] != null ? userDetails.Rows[i]["Mobile"].ToString() : "");
                            if (mobile != null && mobile.Trim() != "")
                            {
                                string[] mobs = mobile.Split(',');
                                for (int j = 0; j < mobs.Length; j++)
                                {
                                    string smsMob = mobs[j].ToString().Trim();
                                    if (smsMob != "")
                                    {
                                        smsMob = (add91 == "Y" ? "91" + smsMob : smsMob);

                                        if (json != null && json != "")
                                        {
                                            string JsonNew = json.Replace("@MobileNo@", smsMob);
                                            JsonNew = JsonNew.Replace("@OTP@", newPassword);
                                            success = await Send_WhatsApp(url, (JsonNew != null ? JsonNew : ""), apiHeaders);
                                        }
                                        else
                                        {
                                            success = await Send_WhatsApp(url, (json != null ? json : ""), apiHeaders);
                                        }

                                        outMsg = (success ? "WhatsApp sent successfully." : "WhatsApp message snding failed.");
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                success = false;
                outMsg = ex.Message;
            }

            return (success ? 1 : 0);
        }

        public static HttpClient AddHeaders(DataTable headers, HttpClient httpClient)
        {
            try
            {
                if (headers != null && headers.Rows.Count > 0)
                {
                    for (int i = 0; i < headers.Rows.Count; i++)
                    {
                        httpClient.DefaultRequestHeaders.Add(headers.Rows[i]["Name"].ToString(), headers.Rows[i]["Value"].ToString());
                    }
                }
            }
            catch
            {
                throw;
            }

            return httpClient;
        }

        private static async Task<bool> Send_WhatsApp(string APIUrl, string JSONBody, DataTable headers)
        {
            bool success = false;
            try
            {
                if (APIUrl != null && APIUrl != "")
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient = AddHeaders(headers, httpClient);

                    var content = new StringContent("Json string", Encoding.UTF8, "application/json");
                    if (JSONBody != null && JSONBody != "")
                    {
                        content = new StringContent(JSONBody, Encoding.UTF8, "application/json");
                    }

                    var response = await httpClient.PostAsync(APIUrl, content);

                    success = true;
                }
            }
            catch
            {
                throw;
            }

            return success;
        }
    }
}
