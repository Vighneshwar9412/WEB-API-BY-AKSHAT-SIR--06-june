using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities.Employee;
using FourQT.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;
using FourQT.Entities.ChannelPartner;
using Microsoft.AspNetCore.Http;
using Microsoft.Win32;
using static System.Net.WebRequestMethods;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;
using FourQT.Core.General;
using FourQT.Entities.General;
using System.Net.Http.Headers;
using System.Reflection.PortableExecutable;
using System.Net.Http;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FourQT.Core.ChannelPartner
{
    public class ChannelPartnerLeadBLL
    {
        public async Task<dynamic> getChannelPartnerHomepage(HttpRequest req)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            List<CPHome> homePage = new List<CPHome>();

            try
            {

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string spName = "API_GetCPBookingAggregations";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Broker_Id", Value = brokerId },
                };

                DataSet? result = null;
                dynamic[] data = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);

                if (data != null && data.Length > 0)
                {
                    result = data[0];
                }

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPHome unit = new CPHome();
                            unit.projectName = (row["Project_Name"] != null ? row["Project_Name"].ToString() : "");
                            unit.area = (row["SoldArea"] != null ? row["SoldArea"].ToString() : "");
                            unit.unit = (Int32.TryParse(row["NoOfTotalApprovedUnits"].ToString(), out int i) ? i : 0);
                            unit.totalSaleValue = (row["TotalSaleValue"] != null ? row["TotalSaleValue"].ToString() : "");
                            unit.approved = (Int32.TryParse(row["NoOfApprovedUnits"].ToString(), out i) ? i : 0);
                            unit.pending = (Int32.TryParse(row["NoOfPendingUnits"].ToString(), out i) ? i : 0);
                            unit.rejected = (Int32.TryParse(row["NoOfRejectedUnits"].ToString(), out i) ? i : 0);
                            unit.projectId = (Int32.TryParse(row["Project_Id"].ToString(), out i) ? i : 0);
                            homePage.Add(unit);
                        }
                    }
                }

                genResponse.Data = homePage;
                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
                genResponse.Title= "Success";

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> getChannelPartnerMasters(InventoryRequestShort model,HttpRequest req)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            CPMasters masters = new CPMasters();

            try
            {

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string spName = "API_GetCPMasters";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Broker_Id", Value = brokerId },
                    new SqlParameter() { ParameterName = "@Project_Id", Value = model.projectId }
                };

                DataSet? result = null;
                dynamic[] data = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);

                if (data != null && data.Length > 0)
                {
                    result = data[0];
                }

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        List<CPMasterListSelect> masterList = new List<CPMasterListSelect>();
                        DataTable dt = result.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterListSelect unit = new CPMasterListSelect();
                            unit.name = (row["Project_Name"] != null ? row["Project_Name"].ToString() : "");
                            unit.id = (Int32.TryParse(row["Project_Id"].ToString(), out int i) ? i : 0);
                            unit.selected = (row["Selected"] == null ? 0 : Convert.ToInt32(row["Selected"].ToString()));
                            masterList.Add(unit);
                        }
                        masters.project = masterList;
                    }
                }

                if (result != null && result.Tables.Count > 1)
                {
                    if (result.Tables[1] != null && result.Tables[1].Rows.Count > 0)
                    {
                        List<CPMasterList> masterList = new List<CPMasterList>();
                        DataTable dt = result.Tables[1];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterList unit = new CPMasterList();
                            unit.name = (row["Salutation"] != null ? row["Salutation"].ToString() : "");
                            unit.id = (Int32.TryParse(row["Id"].ToString(), out int i) ? i : 0);
                            masterList.Add(unit);
                        }
                        masters.salutation = masterList;
                    }
                }

                if (result != null && result.Tables.Count > 2)
                {
                    if (result.Tables[2] != null && result.Tables[2].Rows.Count > 0)
                    {
                        List<CPMasterList> masterList = new List<CPMasterList>();
                        DataTable dt = result.Tables[2];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterList unit = new CPMasterList();
                            unit.name = (row["Project_Tower_Name"] != null ? row["Project_Tower_Name"].ToString() : "");
                            unit.id = (Int32.TryParse(row["Project_Tower_Id"].ToString(), out int i) ? i : 0);
                            masterList.Add(unit);
                        }
                        masters.tower = masterList;
                    }
                }

                if (result != null && result.Tables.Count > 3)
                {
                    if (result.Tables[3] != null && result.Tables[3].Rows.Count > 0)
                    {
                        List<CPMasterList> masterList = new List<CPMasterList>();
                        DataTable dt = result.Tables[3];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterList unit = new CPMasterList();
                            unit.name = (row["Project_Tower_Floor_Name"] != null ? row["Project_Tower_Floor_Name"].ToString() : "");
                            unit.id = (Int32.TryParse(row["Project_Tower_Floor_Id"].ToString(), out int i) ? i : 0);
                            masterList.Add(unit);
                        }
                        masters.baseFloor = masterList;
                    }
                }

                if (result != null && result.Tables.Count > 4)
                {
                    if (result.Tables[4] != null && result.Tables[4].Rows.Count > 0)
                    {
                        List<CPMasterListEmp> masterList = new List<CPMasterListEmp>();
                        DataTable dt = result.Tables[4];
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterListEmp unit = new CPMasterListEmp();
                            unit.name = (row["Name"] != null ? row["Name"].ToString() : "");
                            unit.id = (Int32.TryParse(row["Id"].ToString(), out int i) ? i : 0);
                            unit.empMobile = (row["Mobile"] != null ? row["Mobile"].ToString() : "");
                            masterList.Add(unit);
                        }
                        masters.salesEmpList = masterList;
                    }
                }

                genResponse.Data = masters;
                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
                genResponse.Title = "Success";

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> changePasswordCP(ChangePasswordEmployeeRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ChangePasswordCP", context);
                
                string oldPass = "", newPass = "", confirm = "";
                genResponse.IsSuccess = false;
                genResponse.Title = "Failed";
                genResponse.Status = HttpStatusCode.BadRequest;

                if (model.oldPassword != null && model.newPassword != null && model.confirmNewPassword != null)
                {
                    oldPass = model.oldPassword.Trim();
                    newPass = model.newPassword.Trim();
                    confirm = model.confirmNewPassword.Trim();

                    if (newPass.Length < 6)
                    {
                        genResponse.Message = "Invalid new password.";
                        return genResponse;
                    }

                    if (newPass == oldPass)
                    {
                        genResponse.Message = "New password cannot be same as old password.";
                        return genResponse;
                    }

                    if (newPass != confirm)
                    {
                        genResponse.Message = "Confirm new password does not match new password.";
                        return genResponse;
                    }
                }
                else
                {
                    genResponse.Message = "Password fields invalid.";
                    return genResponse;
                }

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);


                string spName = "API_ChangePasswordCP";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status" , Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg" , Value = "",SqlDbType=SqlDbType.VarChar,Size=200 },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId  },
                    new SqlParameter() { ParameterName = "@OldPassword", Value = model.oldPassword ,SqlDbType=SqlDbType.VarChar,Size=100 },
                    new SqlParameter() { ParameterName = "@NewPassword", Value = model.newPassword ,SqlDbType=SqlDbType.VarChar,Size=100 },
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                if (result != null && result.Length >= 2)
                {
                    genResponse.Message = result[1];

                    if (result[0] == "1")
                    {
                        genResponse.Title = "Success";
                        genResponse.Status = HttpStatusCode.OK;
                        genResponse.IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> registerChannelPartnerLead(RegisterLeadRequest model,HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ChannelPartner_RegisterLead_1", context);

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                int sendSMS = 0;
                if (model.sendOtpForLeadRegister == "Y") { sendSMS = 1; }

                int uploadPhoto = 0;
                if (model.uploadCustomerPhoto == "Y") { 
                    uploadPhoto = 1;

                    ServerResponse fileUploadresponse = new ServerResponse();
                    fileUploadresponse = await uploadCustomerPhoto(model, req);
                    if(fileUploadresponse!=null && fileUploadresponse.isSuccess)
                    {
                        FileUploadResponse? upFiles = fileUploadresponse.uploadedFiles;
                        if (upFiles != null && upFiles.files != null && upFiles.files.Count >= 1)
                        {
                            string? custPhotoServerName = upFiles.files[0].fileNameOnServer;
                            if (custPhotoServerName != null)
                            {
                                model.customerPhoto = custPhotoServerName;
                            }
                            else
                            {
                                genResponse.IsSuccess = false;
                                genResponse.Data = null;
                                genResponse.Status = HttpStatusCode.BadRequest;
                                genResponse.Message = "Customer photo could not be uploaded.";
                                genResponse.Title = "Error";
                                return genResponse;
                            }
                        }
                    }
                }
                else
                {
                    model.customerPhoto = "";
                }

                int sendWhatsApp = 0;
                if (model.sendWhatsAppForLeadRegister == "Y") { sendWhatsApp = 1; }

                string spName = "API_ChannelPartnerLeadRegistrationProcess";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",SqlDbType=SqlDbType.VarChar,Size=100},
                    new SqlParameter() { ParameterName = "@ProcessAction", Value = "S" },
                    new SqlParameter() { ParameterName = "@BrokerId", Value = brokerId},
                    new SqlParameter() { ParameterName = "@CustName", Value = (model.customerName!=null ? model.customerName : "")},
                    new SqlParameter() { ParameterName = "@CustMobile", Value = (model.customerMobile!=null ? model.customerMobile : "") },
                    new SqlParameter() { ParameterName = "@CustEmail", Value = (model.customerEmail!=null ? model.customerEmail : "")},
                    new SqlParameter() { ParameterName = "@CustRemarks", Value = (model.customerRemarks!=null ? model.customerRemarks : "") },
                    new SqlParameter() { ParameterName = "@ProjectId", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0)},
                    new SqlParameter() { ParameterName = "@CPEmployeeName", Value = (model.cpEmployeeName!=null ? model.cpEmployeeName : "") },
                    new SqlParameter() { ParameterName = "@CPEmployeeMobile", Value = (model.cpEmployeeMobile!=null ? model.cpEmployeeMobile : "")},
                    new SqlParameter() { ParameterName = "@IpAddress", Value = (model.ipAddress!=null ? model.ipAddress : "") },
                    new SqlParameter() { ParameterName = "@SendSMS", Value = sendSMS},
                    new SqlParameter() { ParameterName = "@UploadPhoto", Value = uploadPhoto},
                    new SqlParameter() { ParameterName = "@CustPhoto", Value = (model.customerPhoto!=null ? model.customerPhoto : "")},
                    new SqlParameter() { ParameterName = "@Salutation", Value = (model.salutation!=null ? model.salutation : "")},
                    new SqlParameter() { ParameterName = "@SendWhatsApp", Value = sendWhatsApp},
                    new SqlParameter() { ParameterName = "@SalesEmployeeId", Value = model.salesEmpId }
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                string regStatus = "0";string outMsg = "";

                dynamic[] smsConfig = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);
                if(smsConfig!=null && smsConfig.Length > 2)
                {
                    ds = smsConfig[0];
                    regStatus = smsConfig[1];
                    outMsg = smsConfig[2];
                }

                RegisterLeadResponse response = new RegisterLeadResponse();
                response.leadRegStatus = (Int32.TryParse(regStatus, out int st) ? st : 0);
                response.outMsg = (outMsg!=null ? outMsg : "");

                if (response.leadRegStatus == 1 && ds!=null) { 
                    genResponse.IsSuccess = true;
                    genResponse.Message = outMsg;
                    genResponse.Title= "Success";
                    genResponse.Status = HttpStatusCode.OK;

                    int otpsSent = 0;

                    if (sendSMS == 1)
                    {
                        string[] sms = { "0", "Error sending SMS." };
                        if (Int32.TryParse(outMsg, out int a)) {
                            sms = await SendOTP(ds, (outMsg != null ? outMsg : ""));
                        }

                        if (sms != null && sms.Length > 1)
                        {
                            response.leadRegStatus = (Int32.TryParse(sms[0], out int sms_st) ? sms_st : 0);
                            response.outMsg = (sms[1] != null ? sms[1] : "");
                            genResponse.Message = response.outMsg;
                            if (sms_st == 1) { otpsSent++; }
                        }
                    }
                    else { otpsSent++; }

                    if (sendWhatsApp == 1)
                    {
                        dynamic[] whatsApp = { false, "Error sending WhatsApp." };
                        if (Int32.TryParse(outMsg, out int a))
                        {
                            whatsApp = await SendOTPWhatsApp(ds, (outMsg != null ? outMsg : ""));
                        }

                        if (whatsApp != null && whatsApp.Length > 1)
                        {
                            response.leadRegStatus = (whatsApp[0] ? 1 : 0);
                            response.outMsg = (whatsApp[1] != null ? whatsApp[1].ToString() : "");
                            genResponse.Message = response.outMsg;
                            if (whatsApp[0]) { otpsSent++; }
                        }
                    }
                    else { otpsSent++; }

                    if (sendSMS == 1 || sendWhatsApp == 1) {
                        if (otpsSent > 0)
                        {
                            genResponse.Message = "OTP sent to customer mobile.";
                            response.outMsg = "OTP sent to customer mobile.";
                        }
                        else {
                            genResponse.Message = "Error sending OTP.";
                            response.outMsg = "Error sending OTP.";
                        }
                    }
                }               
                else { 
                    genResponse.IsSuccess = false;
                    genResponse.Message = response.outMsg;
                    genResponse.Title = "Failed";
                    genResponse.Status = HttpStatusCode.BadRequest;
                }
                                               
                genResponse.Data = response;

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
                genResponse.Title = "Error";
            }

            return genResponse;
        }       

        public async Task<dynamic> registerChannelPartnerLead_2(RegisterLeadRequest_2 model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ChannelPartner_RegisterLead_2", context);

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string spName = "API_ChannelPartnerLeadRegistrationProcess";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",SqlDbType=SqlDbType.VarChar,Size=100},
                    new SqlParameter() { ParameterName = "@ProcessAction", Value = "R" },
                    new SqlParameter() { ParameterName = "@BrokerId", Value = brokerId},
                    new SqlParameter() { ParameterName = "@OTP", Value = (model.otp!=null ? model.otp : "") },
                    new SqlParameter() { ParameterName = "@CustMobile", Value = (model.customerMobile!=null ? model.customerMobile : "") }
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                await DBHelper.ExecuteNonQueryCP(token, CommandType.StoredProcedure, spName, lstParam);

                RegisterLeadResponse response = new RegisterLeadResponse();
                response.leadRegStatus = (Int32.TryParse(lstParam[0].Value.ToString(), out int st) ? st : 0);
                response.outMsg = lstParam[1].Value.ToString();

                if (response.leadRegStatus == 1) { 
                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Title = "Success";
                }
                else { 
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Failed";
                }

                genResponse.Message = response.outMsg;               
                genResponse.Data = response;

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
                genResponse.Title = "Error";
            }

            return genResponse;
        }

        public async Task<dynamic> SendOTP(DataSet ds,string otp) {

            string mobile = "", message = "", outMsg = "", success = "0";
            
            try
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Mobile_No"] != null)
                {
                    mobile = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                }
                if (mobile != null && mobile.Trim() != "")
                {
                    if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                    {
                        message = ds.Tables[2].Rows[0]["Subject_Desp"].ToString();
                        message = message.Replace("@OTP@", otp);
                        outMsg = "OTP could not be sent.";
                        success = "0";
                    }

                    bool smsSent = false;
                    if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        smsSent= await SendSMS(ds.Tables[1], message, mobile);
                    }

                    if (smsSent)
                    {
                        success = "1";
                        outMsg = "OTP sent successfully.";
                    }
                    else
                    {
                        outMsg = "OTP could not be sent.";
                        success = "0";
                    }
                }
                else
                {
                    success = "0";
                    outMsg = "Invalid mobile number.";
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            return new string[] { success, outMsg };
        }

        private static async Task<bool> SendSMS(DataTable dt, string Subject, string Recipients)
        {
            bool success = false;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    string Add91 = Convert.ToString(dt.Rows[0]["AddDigit91"]);
                    string[] arrRecipients = Recipients.Split(',');
                    foreach (string rec in arrRecipients)
                    {
                        string Url = Convert.ToString(dt.Rows[0]["Url"]);
                        string recipient = rec;
                        if (Add91 == "Y")
                        {
                            recipient = "91" + recipient;
                        }
                        Url = Url.Replace("@MN@", recipient);
                        Url = Url.Replace("@MText@", Subject);
                        success=await Send_SMS(Url);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return success;
        }

        private static async Task<bool> Send_SMS(string Url)
        {
            bool success = false;
            try
            {
                if (Url != "")
                {
                    HttpClient httpClient = new HttpClient();
                    var response = await httpClient.PostAsync(Url, new StringContent("Json string", Encoding.UTF8, "application/json"));

                    success = true;
                }
            }
            catch (Exception ex) { throw; }

            return success;
        }

        public async Task<dynamic> SendOTPWhatsApp(DataSet ds, string otp)
        {

            string mobile = "", url = "", json = "", outMsg = "WhatsApp message snding failed.";
            Boolean success = false;

            try
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["Mobile_No"] != null)
                {
                    mobile = ds.Tables[0].Rows[0]["Mobile_No"].ToString();
                }
                if (mobile != null && mobile.Trim() != "")
                {
                    if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                    {
                        url = (ds.Tables[3].Rows[0]["APIUrl"] != null ? ds.Tables[3].Rows[0]["APIUrl"].ToString() : "");
                        json = (ds.Tables[3].Rows[0]["JSONBody"] != null ? ds.Tables[3].Rows[0]["JSONBody"].ToString() : "");
                    }

                    DataTable headers = new DataTable();
                    if (ds.Tables[4] != null) {
                        headers = ds.Tables[4];
                    }

                    if(url!=null && url != "")
                    {
                        string[] arrRecipients = mobile.Split(',');
                        foreach (string rec in arrRecipients)
                        {
                            if (json != null && json != "")
                            {
                                string JsonNew = json.Replace("@MobileNo@", rec);
                                JsonNew = JsonNew.Replace("@OTPTime@", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                                JsonNew = JsonNew.Replace("@OTP@", otp);
                                success = await Send_WhatsApp(url, (JsonNew != null ? JsonNew : ""), headers);
                            }
                            else {
                                success = await Send_WhatsApp(url, (json != null ? json : ""), headers);
                            }
                                                      
                            outMsg = (success ? "WhatsApp sent successfully." : "WhatsApp message snding failed.");
                        }
                    }                   
                }
                else
                {
                    success = false;
                    outMsg = "Invalid customer mobile no.";
                }
            }
            catch (Exception ex)
            {
                success = false;
                outMsg = ex.Message;
            }

            return new dynamic[] { success, outMsg };
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

        private static async Task<bool> Send_WhatsApp(string APIUrl,string JSONBody,DataTable headers)
        {
            bool success = false;
            try
            {
                if (APIUrl!=null && APIUrl != "")
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient = AddHeaders(headers, httpClient);

                    var content = new StringContent("Json string", Encoding.UTF8, "application/json");
                    if (JSONBody != null && JSONBody != "") {
                        content = new StringContent(JSONBody, Encoding.UTF8, "application/json");
                    }
                    
                    var response = await httpClient.PostAsync(APIUrl, content);

                    success = true;
                }
            }
            catch { 
                throw;
            }

            return success;
        }

        public async Task<dynamic> listChannelPartnerLeads(LeadListRequest model,HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            CPLeadListing listing = new CPLeadListing();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ListChannelPartnerLeads", context);

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                DateTime fromDate = new DateTime(1900, 1, 1);
                DateTime toDate = DateTime.Now;

                if (model.fromDate != null) { DateTime.TryParse(model.fromDate.ToString(), out fromDate); }
                if (model.toDate != null) { DateTime.TryParse(model.toDate.ToString(), out toDate); }

                string spName = "API_ListChannelPartnerLeads";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",SqlDbType=SqlDbType.VarChar,Size=200},
                    new SqlParameter() { ParameterName = "@TotalRecords", Value = 0 },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = brokerId},
                    new SqlParameter() { ParameterName = "@Page_Index", Value = (Int32.TryParse(model.pageNo.ToString(),out int id) ? id : 0)},
                    new SqlParameter() { ParameterName = "@Page_Size", Value = (Int32.TryParse(model.pageSize.ToString(),out  id) ? id : 0)},
                    new SqlParameter() { ParameterName = "@From_Date", Value = fromDate},
                    new SqlParameter() { ParameterName = "@To_Date", Value = toDate},
                    new SqlParameter() { ParameterName = "@SearchText",SqlDbType=SqlDbType.VarChar,Size=200,Value=(model.searchText!=null?model.searchText.ToString().Trim() : "")}
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;
                lstParam[2].Direction = ParameterDirection.Output;

                dynamic[] result = new dynamic[4];
                string status = "0", outMsg = "";
                int totalRecords = 0;
                result = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);

                if (result != null)
                {
                    ds = result[0];
                    status = result[1];
                    outMsg = result[2];
                    totalRecords = result[3];
                }

                if(ds!=null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];                  
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        List<CPLead> leadList = new List<CPLead>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            CPLead lead = new CPLead();
                            lead.enquiryId = (Int32.TryParse(dt.Rows[i]["EnquiryId"].ToString(), out id) ? id : 0);
                            lead.enquiryDate = (dt.Rows[i]["Enquiry_date"] != null ? dt.Rows[i]["Enquiry_date"].ToString() : "");
                            lead.projectName = (dt.Rows[i]["Project_Name"] != null ? dt.Rows[i]["Project_Name"].ToString() : "");
                            lead.customerName = (dt.Rows[i]["Name"] != null ? dt.Rows[i]["Name"].ToString() : "");
                            lead.customerMobile = (dt.Rows[i]["Mobile"] != null ? dt.Rows[i]["Mobile"].ToString() : "");
                            lead.customerEmail = (dt.Rows[i]["Email"] != null ? dt.Rows[i]["Email"].ToString() : "");
                            lead.customerRemarks = (dt.Rows[i]["Remarks"] != null ? dt.Rows[i]["Remarks"].ToString() : "");
                            lead.cpEmployeeName = (dt.Rows[i]["CPEmployeeName"] != null ? dt.Rows[i]["CPEmployeeName"].ToString() : "");
                            lead.cpEmployeeMobile = (dt.Rows[i]["CPEmployeeMobile"] != null ? dt.Rows[i]["CPEmployeeMobile"].ToString() : "");
                            lead.salesEmployeeName = (dt.Rows[i]["SalesPerson"] != null ? dt.Rows[i]["SalesPerson"].ToString() : "");
                            lead.salesEmployeeMobile = (dt.Rows[i]["SalesPersonMobile"] != null ? dt.Rows[i]["SalesPersonMobile"].ToString() : "");
                            lead.source = (dt.Rows[i]["Source"] != null ? dt.Rows[i]["Source"].ToString() : "");
                            leadList.Add(lead);
                        }

                        listing.lead = leadList;
                    }
                }

                listing.totalRecords = totalRecords;

                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Data = listing;
                genResponse.Title= "Success";
                genResponse.IsSuccess = true;

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> listSoldBookedUnitsCP(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            InventoryPageSoldBooked inventoryPage = new InventoryPageSoldBooked();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ListSoldBookedUnitsCP", context);

                if (model.type != "S" && model.type != "B" && model.type != "A")
                {
                    model.type = "A";
                }

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string spName = "API_SoldBookedDetail_CP";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@TotalRecords", Value = 0 },
                    new SqlParameter() { ParameterName = "@ProjectId", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@TowerId", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@FloorId", Value = (Int32.TryParse(model.floorId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Location_Id", Value = model.locationId },
                    new SqlParameter() { ParameterName = "@Type", Value = ((model.type!="") ? model.type : null) },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = (Int32.TryParse(brokerId.ToString(),out id) ? id : 0)  },
                    new SqlParameter() { ParameterName = "@AreaMin", Value = (Decimal.TryParse(model.areaMin.ToString(),out decimal d) ? d : null) },
                    new SqlParameter() { ParameterName = "@AreaMax", Value = (Decimal.TryParse(model.areaMax.ToString(),out d) ? d : null)},
                    new SqlParameter() { ParameterName = "@UnitNo", Value = model.unitNo },
                    new SqlParameter() { ParameterName = "@PageNo", Value = (Int32.TryParse(model.pageNo.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@PageSize", Value = (Int32.TryParse(model.pageSize.ToString(),out id) ? id : 0) },
                };

                if (model.bookingDateFrom != null && DateTime.TryParseExact(model.bookingDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bookFrom) && bookFrom.Year > 1900)
                {
                    lstParam.Add(new SqlParameter() { ParameterName = "@BookingDateFrom", Value = bookFrom });
                }
                if (model.bookingDateTo != null && DateTime.TryParseExact(model.bookingDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bookTo) && bookTo.Year > 1900)
                {
                    lstParam.Add(new SqlParameter() { ParameterName = "@BookingDateTo", Value = bookTo });
                }

                lstParam[0].Direction = ParameterDirection.Output;

                DataSet? result = null;
                int totalRecords = 0;
                dynamic[] data = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);
                if (data != null && data.Length > 0)
                {
                    result = data[0];
                    totalRecords = data[3];
                }

                inventoryPage.totalRecords = totalRecords;

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[0];
                        List<InventoryDetailsResponseSoldBooked> inventory = new List<InventoryDetailsResponseSoldBooked>();
                        foreach (DataRow row in dt.Rows)
                        {
                            InventoryDetailsResponseSoldBooked unit = new InventoryDetailsResponseSoldBooked();
                            unit.registrationId = (Int32.TryParse(row["Registration_Id"].ToString(), out int i) ? i : 0);
                            unit.registrationNo = (row["Registration_No"] != null ? row["Registration_No"].ToString() : "");
                            unit.unitId = (Int32.TryParse(row["Address_Id"].ToString(), out i) ? i : 0);
                            unit.unitNo = (row["UnitNo"] != null ? row["UnitNo"].ToString() : "");
                            unit.unitType = (row["Project_Unit_Type_Name"] != null ? row["Project_Unit_Type_Name"].ToString() : "");
                            unit.tower = (row["Tower"] != null ? row["Tower"].ToString() : "");
                            unit.floor = (row["Floor"] != null ? row["Floor"].ToString() : "");
                            unit.superArea = (row["SuperArea"] != null ? row["SuperArea"].ToString() : "");
                            unit.carpetArea = (row["CarpetArea"] != null ? row["CarpetArea"].ToString() : "");
                            unit.builtUpArea = (row["BuildupArea"] != null ? row["BuildupArea"].ToString() : "");
                            unit.customerName = (row["CustomerName"] != null ? row["CustomerName"].ToString() : "");
                            unit.customerPhoto = (row["CustomerPhoto"] != null ? row["CustomerPhoto"].ToString() : "");
                            unit.customerMobile = (row["MobileNo"] != null ? row["MobileNo"].ToString() : "");
                            unit.bookingAmount = (Decimal.TryParse(row["BookingAmount"].ToString(), out d) ? d : 0);
                            unit.chequeCopy = (row["ChequeCopy"] != null ? row["ChequeCopy"].ToString() : "");
                            unit.bookingDate = (row["BookingDate"] != null ? row["BookingDate"].ToString() : "");
                            unit.soldBy = (row["SoldBy"] != null ? row["SoldBy"].ToString() : "");
                            unit.type = (row["Type"] != null ? row["Type"].ToString() : "");
                            unit.colorCode = (row["ColorCode"] != null ? row["ColorCode"].ToString() : "");
                            unit.projectId = (Int32.TryParse(row["Project_Id"].ToString(), out i) ? i : 0);
                            unit.superAreaLabel = (row["SuperAreaLbl"] != null ? row["SuperAreaLbl"].ToString() : "");
                            unit.carpetAreaLabel = (row["Carpet_AreaLbl"] != null ? row["Carpet_AreaLbl"].ToString() : "");
                            unit.builtUpAreaLabel = (row["Build_Up_AreaLbl"] != null ? row["Build_Up_AreaLbl"].ToString() : "");
                            unit.superAreaDisplay = (row["SuperAreaDisp"].ToString() != "0" ? true : false);
                            unit.carpetAreaDisplay = (row["Carpet_AreaDisp"].ToString() != "0" ? true : false);
                            unit.builtUpAreaDisplay = (row["Build_Up_AreaDisp"].ToString() != "0" ? true : false);
                            inventory.Add(unit);
                        }

                        inventoryPage.unit = inventory.OrderBy(o => o.unitId).ToList();
                    }
                }

                genResponse.Data = inventoryPage;
                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> listFilterTowerFloorList(FilterDropdownRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            TowerFloorListModel listModel = new TowerFloorListModel();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ListFilterTowerFloorListCP", context);

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string spName = "API_GetTowerFloorList_CP";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@BrokerId", Value = brokerId },
                    new SqlParameter() { ParameterName = "@ProjectId", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@TowerId", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) }
                };

                DataSet? result = null;
                dynamic[] data = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);
                if (data != null && data.Length > 0)
                {
                    result = data[0];
                }

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[0];
                        List<CPMasterList> inventory = new List<CPMasterList>();
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterList unit = new CPMasterList();
                            unit.id = (Int32.TryParse(row["Id"].ToString(), out int i) ? i : 0);
                            unit.name = (row["Name"] != null ? row["Name"].ToString() : "");                          
                            inventory.Add(unit);
                        }
                        listModel.tower = inventory;
                    }
                }

                if (result != null && result.Tables.Count > 1)
                {
                    if (result.Tables[1] != null && result.Tables[1].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[1];
                        List<CPMasterList> inventory = new List<CPMasterList>();
                        foreach (DataRow row in dt.Rows)
                        {
                            CPMasterList unit = new CPMasterList();
                            unit.id = (Int32.TryParse(row["Id"].ToString(), out int i) ? i : 0);
                            unit.name = (row["Name"] != null ? row["Name"].ToString() : "");
                            inventory.Add(unit);
                        }
                        listModel.floor = inventory;
                    }
                }

                genResponse.Data = listModel;
                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> uploadCustomerPhoto(RegisterLeadRequest model, HttpRequest req)
        {
            FileUploadRequest fileUploadRequest = new FileUploadRequest();
            FileUploadResponse uploadedDocList = new FileUploadResponse();
            ServerResponse response = new ServerResponse();
            int noOfFilesUploaded = 0;
            string errorMsg = "";
            Boolean uploadSuccess = false;

            try
            {
                if (model != null)
                {
                    string? customerPhoto = model.customerPhoto;
                    string? custPhotoFormat = model.customerPhotoFormat;

                    List<FileUpload> fileList = new List<FileUpload>();

                    if (customerPhoto != null && customerPhoto.ToString().Trim() != "")
                    {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = customerPhoto;
                        file.action = "I";
                        file.fileFormat = (custPhotoFormat != null ? custPhotoFormat : "");
                        file.fileGroup = "E_R_CP";
                        file.fileName = "";
                        file.id = 1;

                        fileList.Add(file);
                    }

                    if (fileList != null && fileList.Count > 0)
                    {
                        fileUploadRequest.files = fileList;

                        uploadedDocList = await UploadFilesToExternalServerBLL.SendFilesToExternalServer(fileUploadRequest, req);
                        if (uploadedDocList != null && uploadedDocList.files != null && uploadedDocList.files.Count > 0)
                        {
                            for (int i = 0; i < uploadedDocList.files.Count; i++)
                            {
                                UploadedFile upFile = uploadedDocList.files[i];

                                if (upFile.fileUploaded)
                                {
                                    noOfFilesUploaded++;
                                }
                                else
                                {
                                    if (upFile.id == 1)
                                    {
                                        errorMsg = "Error uploading customer photo.";
                                    }
                                }

                            }
                        }
                    }
                }

                if (noOfFilesUploaded >= 1)
                {
                    errorMsg = "Success";
                    uploadSuccess = true;
                }
                else if (noOfFilesUploaded <= 0)
                {
                    errorMsg = "Error uploading customer photo.";
                    uploadSuccess = false;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                uploadSuccess = false;
            }

            response.isSuccess = uploadSuccess;
            response.message = errorMsg;
            response.uploadedFiles = uploadedDocList;
            return response;
        }

        public async Task<dynamic> getInventoryCP(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            InventoryPage inventoryPage = new InventoryPage();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "CP_getInventory", context);

                DataSet ds = new DataSet();

                (new JWTTokenMethods()).GetConnectionDetailsCP(req, out int brokerId, out string token);

                string type = "";
                if (model.type != null)
                {
                    string temp = model.type.ToString().Trim().ToUpper();
                    if (temp == "A" || temp == "H" || temp == "B")
                    {
                        type = temp;
                    }
                }

                string spName = "API_UnitStatus_CP";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@TotalRecords" , Value = 0 },
                    new SqlParameter() { ParameterName = "@Project_Id", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Tower_Id", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Floor_Id", Value = (Int32.TryParse(model.floorId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Pg_Id", Value = (Int32.TryParse(model.unitTypeGroupId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@UnitType_Id", Value = (Int32.TryParse(model.unitTypeId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Location_Id", Value = model.locationId },
                    new SqlParameter() { ParameterName = "@Puoid", Value = (Int32.TryParse(model.ownerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Type", Value = ((type!="") ? type : null) },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = brokerId  },
                    new SqlParameter() { ParameterName = "@AreaMin", Value = (Decimal.TryParse(model.areaMin.ToString(),out decimal d) ? d : null) },
                    new SqlParameter() { ParameterName = "@AreaMax", Value = (Decimal.TryParse(model.areaMax.ToString(),out d) ? d : null)},
                    new SqlParameter() { ParameterName = "@Unit_No", Value = model.unitNo },
                    new SqlParameter() { ParameterName = "@PageNo", Value = (Int32.TryParse(model.pageNo.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@PageSize", Value = (Int32.TryParse(model.pageSize.ToString(),out id) ? id : 0) }
                };

                lstParam[0].Direction = ParameterDirection.Output;

                dynamic[] resultAr = await DBHelper.GetDataSetCP(token, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                int totalRecords = (Int32.TryParse(lstParam[0].Value.ToString(), out int total) ? total : 0);
                inventoryPage.totalRecords = totalRecords;

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<InventoryUnitDetails> unitLst = new List<InventoryUnitDetails>();
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        InventoryUnitDetails unit = new InventoryUnitDetails();
                        unit.project = (rows["Project_Name"] == null ? "" : rows["Project_Name"].ToString());
                        unit.tower = (rows["Project_Tower_Name"] == null ? "" : rows["Project_Tower_Name"].ToString());
                        unit.floor = (rows["Project_Tower_Floor_Name"] == null ? "" : rows["Project_Tower_Floor_Name"].ToString());
                        unit.unitNo = (rows["UnitNo"] == null ? "" : rows["UnitNo"].ToString());
                        unit.unitGroup = (rows["UnitType_GroupName"] == null ? "" : rows["UnitType_GroupName"].ToString());
                        unit.unitType = (rows["Project_Unit_Type_Name"] == null ? "" : rows["Project_Unit_Type_Name"].ToString());
                        unit.superArea = (rows["SuperArea"] == null ? "" : rows["SuperArea"].ToString());
                        unit.carpetArea = (rows["Carpet_Area"] == null ? "" : rows["Carpet_Area"].ToString());
                        unit.buildupArea = (rows["Build_Up_Area"] == null ? "" : rows["Build_Up_Area"].ToString());
                        unit.location = (rows["Unit_Location"] == null ? "" : rows["Unit_Location"].ToString());
                        unit.unitPlan = (rows["UnitPlan"] == null ? "" : rows["UnitPlan"].ToString());
                        unit.floorPlan = (rows["FloorPlan"] == null ? "" : rows["FloorPlan"].ToString());
                        unit.status = (rows["Status"] == null ? "" : rows["Status"].ToString());
                        unit.unitId = (Int32.TryParse(rows["Address_Id"].ToString(), out int unitId) ? unitId : 0);
                        unit.cpName = (rows["ChannelPartner"] == null ? "" : rows["ChannelPartner"].ToString());
                        unit.holdDate = (rows["HoldDate"] == null ? "" : rows["HoldDate"].ToString());
                        unit.holdByEmployee = (rows["HoldBy"] == null ? "" : rows["HoldBy"].ToString());
                        unit.remarks = (rows["HoldRemark"] == null ? "" : rows["HoldRemark"].ToString());
                        unit.colorCode = (rows["ColorCode"] != null ? rows["ColorCode"].ToString() : "");
                        unit.customerName = (rows["CustomerName"] != null ? rows["CustomerName"].ToString() : "");
                        unit.customerMobile = (rows["CustomerMobile"] != null ? rows["CustomerMobile"].ToString() : "");
                        unit.superAreaLabel = (rows["SuperAreaLbl"] != null ? rows["SuperAreaLbl"].ToString() : "");
                        unit.carpetAreaLabel = (rows["Carpet_AreaLbl"] != null ? rows["Carpet_AreaLbl"].ToString() : "");
                        unit.buildupAreaLabel = (rows["Build_Up_AreaLbl"] != null ? rows["Build_Up_AreaLbl"].ToString() : "");
                        unit.superAreaDisplay = (rows["SuperAreaDisp"].ToString() != "0" ? true : false);
                        unit.carpetAreaDisplay = (rows["Carpet_AreaDisp"].ToString() != "0" ? true : false);
                        unit.buildupAreaDisplay = (rows["Build_Up_AreaDisp"].ToString() != "0" ? true : false);

                        unitLst.Add(unit);
                    }

                    inventoryPage.unit = unitLst.OrderBy(o => o.unitId).ToList();
                }

                genResponse.Data = inventoryPage;
                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }
    }

}
