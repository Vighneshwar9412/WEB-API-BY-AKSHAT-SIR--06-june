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
using System.Configuration;
using Nancy.Json;

namespace FourQT.Core
{
    public  class ClickToCallBLL
    {

        public Object TriggerCall(string mKey, int loginId, ClickCall ObjClickCall)
        {
            APIObjectResponse response = new APIObjectResponse();
            string responseString = "Call Triggered Successfully.";
            string message = "Call Triggered Successfully.";
            try
            {

                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@DocNo", Value = Convert.ToInt32(ObjClickCall.DocNo)},
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                    new SqlParameter() { ParameterName = "@Enquiry_id", Value = Convert.ToInt32(ObjClickCall.EnquiryId)},
                };

                DataSet ds = DBHelper.GetDataset(mKey, CommandType.StoredProcedure, "GenericAPI_GetAutoCallUrl", lstParam);

                if (!Object.Equals(ds, null))
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string url = ds.Tables[0].Rows[0]["URL"].ToString();
                        int asc_Id = Convert.ToInt32(ds.Tables[0].Rows[0]["ACS_Id"].ToString());
                        string callbackUrl = ds.Tables[0].Rows[0]["CallBackUrl"].ToString();

                        string apiKey = ds.Tables[0].Rows[0]["APIKey"].ToString();
                        string authorizationKey = ds.Tables[0].Rows[0]["AuthorizationKey"].ToString();
                        string k_number = ds.Tables[0].Rows[0]["K_Number"].ToString();
                        string method = ds.Tables[0].Rows[0]["METHOD"].ToString();
                        string provider = ds.Tables[0].Rows[0]["PROVIDER"].ToString();
                        string kclinumber = ds.Tables[0].Rows[0]["KCLINUMBER"].ToString();

                        int refId = 0;

                        string callId = String.Empty;

                        List<SqlParameter> lstParamone = new List<SqlParameter>
                        {
                            new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                            new SqlParameter() { ParameterName = "@Action", Value = "I"},
                            new SqlParameter() { ParameterName = "@ACS_Id", Value = asc_Id},
                            new SqlParameter() { ParameterName = "@ExecutiveNumber", Value = ObjClickCall.agentNumber},
                            new SqlParameter() { ParameterName = "@CustomerNumber", Value = ObjClickCall.customerNumber},
                            new SqlParameter() { ParameterName = "@Enquiry_Id", Value = ObjClickCall.EnquiryId},
                            new SqlParameter() { ParameterName = "@OutMsg",Value ="" },
                            new SqlParameter() { ParameterName = "@Status",Value =0 },
                        };
                        lstParamone[6].Direction = ParameterDirection.Output;
                        lstParamone[6].Size = 500;
                        lstParamone[7].Direction = ParameterDirection.Output;
                        int res = DBHelper.ExecuteNonQuery(mKey, CommandType.StoredProcedure, "GenericAPI_IU_Hits", lstParamone);

                        refId = (int)lstParamone[7].Value;

                        url = url.Replace("@ano@", ObjClickCall.agentNumber);
                        url = url.Replace("@cno@", ObjClickCall.customerNumber);
                        url = url.Replace("@refid@", refId.ToString());
                        url = url.Replace("@url@", callbackUrl);

                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        HttpWebRequest myReq;
                        StreamReader respStreamReader;

                        myReq = (HttpWebRequest)WebRequest.Create(url);

                        JavaScriptSerializer js = new JavaScriptSerializer();
                        string json = String.Empty;

                        if (String.Compare(provider, "MCUBE", true) == 0)
                        {
                            json = url;                           

                            try
                            {
                                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

                                respStreamReader = new StreamReader(myResp.GetResponseStream());
                                responseString = respStreamReader.ReadToEnd();
                                respStreamReader.Close();
                                myResp.Close();                              

                                MCubeResponse respMCube = js.Deserialize<MCubeResponse>(responseString);
                                callId = respMCube.callid;
                                message = respMCube.msg;

                                if (message.Contains("success"))
                                {
                                    message = "Call Triggered Successfully.";
                                }
                            }
                            catch (Exception ex)
                            {
                                responseString = message = ex.Message;
                            }
                        }
                        else if (String.Compare(provider, "KNOWLARITY", true) == 0)
                        {
                            myReq.Method = "POST";
                            myReq.ContentType = "application/json";
                            myReq.Headers.Add("authorization", authorizationKey);
                            myReq.Headers.Add("x-api-key", apiKey);

                            json = new JavaScriptSerializer().Serialize(new
                            {
                                k_number = k_number,
                                agent_number = "+" + ObjClickCall.agentNumber,
                                customer_number = "+" + ObjClickCall.customerNumber,
                                caller_id = "+" + ObjClickCall.CallerId
                            });
                           

                            try
                            {
                                byte[] data = Encoding.ASCII.GetBytes(json);
                                myReq.ContentLength = data.Length;

                                Stream requestStream = myReq.GetRequestStream();
                                requestStream.Write(data, 0, data.Length);
                                requestStream.Close();

                                HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

                                respStreamReader = new StreamReader(myResp.GetResponseStream());
                                responseString = respStreamReader.ReadToEnd();
                                respStreamReader.Close();
                                myResp.Close();

                                if (responseString.Contains("success"))
                                {
                                    KPostiveResponse respP = js.Deserialize<KPostiveResponse>(responseString);
                                    callId = respP.success.call_id;
                                    message = respP.success.message;
                                }
                                else
                                {
                                    KNegativeResponse respN = js.Deserialize<KNegativeResponse>(responseString);
                                    callId = "";
                                    message = respN.error.message;
                                }
                            }
                            catch (Exception ex)
                            {
                                responseString = message = callId = ex.Message;
                            }
                        }

                        List<SqlParameter> lstParamtwo = new List<SqlParameter>
                        {
                            new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                            new SqlParameter() { ParameterName = "@Action", Value = "IR"},
                            new SqlParameter() { ParameterName = "@ACS_Id", Value = ObjClickCall.DocNo},
                            new SqlParameter() { ParameterName = "@ExecutiveNumber", Value = ObjClickCall.agentNumber},
                            new SqlParameter() { ParameterName = "@CustomerNumber", Value = ObjClickCall.customerNumber},
                            new SqlParameter() { ParameterName = "@ACHit_Id", Value = refId},
                            new SqlParameter() { ParameterName = "@Response_Status", Value = responseString},
                            new SqlParameter() { ParameterName = "@CallId", Value = callId},
                            new SqlParameter() { ParameterName = "@APIHit", Value = json},
                            new SqlParameter() { ParameterName = "@Status",Value = 0 },
                            new SqlParameter() { ParameterName = "@OutMsg", Value ="",Size = 500},
                        };

                        lstParamtwo[9].Direction = ParameterDirection.Output;
                        lstParamtwo[10].Direction = ParameterDirection.Output;
                        lstParamtwo[10].Size = 500;
                        int res1 = DBHelper.ExecuteNonQuery(mKey, CommandType.StoredProcedure, "GenericAPI_IU_Hits", lstParamtwo);

                        if (lstParamtwo[9].Value.ToString() == "0")
                        {
                            response.Status = System.Net.HttpStatusCode.BadRequest;
                            response.Message = lstParamtwo[10].Value.ToString();
                            //response.Data = lstParam[8].Value.ToString();
                            response.IsSuccess = false;
                        }
                        else
                        {
                            response.Status = HttpStatusCode.OK;
                            response.Message = lstParamtwo[10].Value.ToString();
                            //response.Data = lstParam[8].Value.ToString();
                            response.IsSuccess = true;

                        }

                    }

                }
                return response;

                //string url = "192.168.1.42/apps/appsHandler_test.php?transaction_id=CTI_DIAL&agent_id=3001&ip=&phone_num=@cn@&resFormat=3";
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.BadRequest;
                response.Message = ex.Message;

                response.IsSuccess = true;
            }

            return response;
        }



        public static void Add_Message(string message)
        {
            try
            {
                string logDirectory = Path.GetDirectoryName(ConfigurationManager.AppSettings["logfilepath"]);

                if (!Directory.Exists(logDirectory))
                    Directory.CreateDirectory(logDirectory);

                string fileName = "Log" + "_" + DateTime.Today.Year.ToString() + "_" + DateTime.Today.Month.ToString() + "_" + DateTime.Today.Day.ToString() + ".txt";
                string fileNameWithPath = logDirectory + @"\" + fileName;

                TextWriter Tex = new StreamWriter(fileNameWithPath, true);

                Tex.Write(Tex.NewLine);
                Tex.Write("-----------------------------------------------------------------------------------------");
                Tex.Write("Logged at -" + DateTime.Now.ToString());
                Tex.Write(Tex.NewLine);
                Tex.Write("------------------------------------------------------------------------------------");
                Tex.Write(message);
                Tex.Write(Tex.NewLine);
                Tex.Write("------------------------------------------------------------------------------------");
                Tex.Flush();
                Tex.Close();

            }
            catch {
                throw;
            }
        }

    }
}
