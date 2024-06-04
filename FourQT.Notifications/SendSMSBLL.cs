using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Policy;
using System.Net.Mail;
using static System.Net.WebRequestMethods;
using System.Net.Http;
using System.Reflection;
using static QRCoder.PayloadGenerator;

namespace FourQT.Notifications
{
    public class SendSMSBLL
    {
        public async Task<APIObjectResponse> Sendleadsms(string Key,int loginId ,SendSMS sms)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            Boolean smsSent = false, followupAdded = false;

            try
            {
                if (sms != null) { 
                
                string spName = "API_Get_SMS_Configration";
                string? uri = "", data = "";

                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                };

                DataSet ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);
                string recList = "";

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        uri = (ds.Tables[0].Rows[0]["Url"] != null ? ds.Tables[0].Rows[0]["Url"].ToString() : "");
                        if (uri != null && uri.Trim() != "")
                        {
                            uri = uri.Replace("@MText@", sms.body);

                            if (sms != null && sms.mobileNo != null && sms.mobileNo.Trim() != "")
                            {
                                string url = uri.Replace("@MN@", sms.mobileNo.Trim());
                                var webClient = new WebClient();
                                data = webClient.DownloadString(url);

                                smsSent = true;

                                recList = sms.mobileNo.Trim();
                            }

                            if (sms != null && sms.otherMobile != null && sms.otherMobile.Trim() != "")
                            {
                                String[] othermob = sms.otherMobile.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                if (othermob != null && othermob.Length > 0)
                                {
                                    foreach (string mob in othermob)
                                    {
                                        string url = uri.Replace("@MN@", mob);
                                        var webClient = new WebClient();
                                        data = await webClient.DownloadStringTaskAsync(new Uri(url));

                                        smsSent = true;
                                        recList = recList + "," + mob.Trim();
                                    }
                                }
                            }
                        }

                        if (smsSent)
                        {
                            try
                            {
                                followupAdded = await AddFollowup_SMS_Email(Key, "S", "SMS", uri, ds.Tables[0].Rows[0]["Sender"].ToString(), recList, "", data, loginId, sms.subjectId, sms.enquiryId);
                            }
                            catch
                            {
                                followupAdded = false;
                            }
                        }
                    }
                }

                if (smsSent)
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = "Message sent successfully." + (followupAdded ? "Followup added." : "Could not add Followup.");
                    genresponse.IsSuccess = true;
                    genresponse.Title = "Success";
                }
                else
                {
                    genresponse.Status = HttpStatusCode.BadRequest;
                    genresponse.Message = "Message could not be sent.";
                    genresponse.IsSuccess = false;
                    genresponse.Title = "Failed";
                }
            }
            catch (Exception er)
            {
                genresponse.IsSuccess = false;
                genresponse.Status = HttpStatusCode.BadGateway;
                genresponse.Message = er.ToString();
                genresponse.Title = "Error";
            }

            return genresponse;
        }


        public async Task<APIObjectResponse> Sendleademail(string Key, int loginId, SendEmail mail)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            Boolean smsSent = false, followupAdded = false;

            try
            {
                string spName = "API_GetEmail_Configuration";
                string uri = "", data = "";

                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                };

                DataSet ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) 
                {
                    DataRow drow = ds.Tables[0].Rows[0];

                    MailMessage mailmsg = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient(drow["SmtpServer"].ToString());
                    mailmsg.From = new MailAddress(drow["EmailId"].ToString());
                    mailmsg.To.Add(mail.to);
                    if (mail.cc.Trim() != "")
                        mailmsg.CC.Add(mail.cc);
                    if (mail.bcc.Trim() != "")
                        mailmsg.Bcc.Add(mail.bcc);
                    mailmsg.Subject = mail.subject;
                    mailmsg.Body = mail.body;
                    mailmsg.IsBodyHtml = true;
                    foreach (ProjectItems attach in mail.documentsList)
                    {
                        if (attach.itemPath != "")
                        {
                            var stream = new WebClient().OpenRead(attach.itemPath);
                            Attachment attaach = new Attachment(stream, attach.itemName + "." + attach.extension);
                            mailmsg.Attachments.Add(attaach);
                        }
                    }
                    SmtpServer.Port = Convert.ToInt32(drow["PortNo"].ToString());
                    SmtpServer.Credentials = new System.Net.NetworkCredential(drow["EmailId"].ToString(), drow["AccountPassword"].ToString());
                    SmtpServer.EnableSsl = drow["SslEnable"].ToString() == "N" ? false : true;
                    await SmtpServer.SendMailAsync(mailmsg);
                    smsSent = true;

                    if (smsSent)
                    {
                        try
                        {
                            followupAdded = await AddFollowup_SMS_Email(Key, "E", mail.subject, mail.body, drow["EmailId"].ToString(), mail.to, "", "", loginId, mail.subjectId, mail.enquiryId);
                        }
                        catch {
                            followupAdded = false;
                        }
                    }
                }

                if (smsSent)
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = "Email sent successfully." + (followupAdded ? "Followup added." : "Could not add Followup.");
                    genresponse.IsSuccess = true;
                    genresponse.Title = "Success";
                }
                else
                {
                    genresponse.Status = HttpStatusCode.BadRequest;
                    genresponse.Message = "Email could not be sent.";
                    genresponse.IsSuccess = false;
                    genresponse.Title = "Failed";
                }
                
            }
            catch (Exception er)
            {
                genresponse.IsSuccess = false;
                genresponse.Status = HttpStatusCode.BadGateway;
                genresponse.Message = er.ToString();
                genresponse.Title = "Error";
            }

            return genresponse;
        }


        public async Task<Boolean> AddFollowup_SMS_Email(string Key, string Type, string? Subject, string? Body, string? Sender, string Recepient, string Attachment, string Response, int loginId, int subjectId, int enquiryId)
        {
            Boolean followupAdded = false;

            try {

                string spName = "API_General_Insert_Email_Sms_History";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output, Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", SqlDbType = SqlDbType.VarChar, Size = 200, Direction = ParameterDirection.Output, Value = "" },
                    new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.VarChar, Size = 1, Value = Type },
                    new SqlParameter() { ParameterName = "@Subject", SqlDbType = SqlDbType.VarChar, Size = 200, Value = Subject },
                    new SqlParameter() { ParameterName = "@Email_Sms_Body",SqlDbType = SqlDbType.VarChar, Size = 500,  Value = Body },
                    new SqlParameter() { ParameterName = "@Sender_name", SqlDbType = SqlDbType.VarChar, Size = 50, Value = Sender },
                    new SqlParameter() { ParameterName = "@Send_To", SqlDbType = SqlDbType.VarChar, Value = Recepient },
                    new SqlParameter() { ParameterName = "@Create_By", SqlDbType = SqlDbType.Int, Value = loginId },
                    new SqlParameter() { ParameterName = "@Actual_Status", SqlDbType = SqlDbType.VarChar, Value = Response },
                    new SqlParameter() { ParameterName = "@Template_Id", SqlDbType = SqlDbType.Int, Value = subjectId },
                    new SqlParameter() { ParameterName = "@Service_Name", SqlDbType = SqlDbType.VarChar, Size = 200, Value = "" },
                    new SqlParameter() { ParameterName = "@EnquiryId", SqlDbType = SqlDbType.Int, Value = enquiryId },
                    new SqlParameter() { ParameterName = "@Attached_File", SqlDbType = SqlDbType.VarChar, Size = 400, Value = Attachment }
                };

                await DBHelper.ExecuteNoQueryAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (Int32.TryParse(lstParam[0].Value.ToString(),out int status) && status == 1)
                {
                    followupAdded = true;
                }
            }
            catch
            {
                followupAdded = false;
                throw;
            }

            return followupAdded;
        }
    }
}
