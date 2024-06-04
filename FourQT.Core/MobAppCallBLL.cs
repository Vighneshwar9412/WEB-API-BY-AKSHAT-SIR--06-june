using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourQT.DAL;
using FourQT.Entities;
using System.Net;

namespace FourQT.Core
{
    public class MobAppCallBLL
    {
        public async Task<dynamic> RecordMobAppCall(string Key, int login_Id, MobAppCall model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
           
            try
            {
                string callXML = "<root>";

                if (model != null && model.callList != null && model.callList.Count > 0) {
                    for (int i = 0; i < model.callList.Count; i++) {
                        MobAppCallModel callModel = new MobAppCallModel();
                        if (model.callList[i] != null) { callModel = model.callList[i]; }

                        callXML = callXML + "<row>";
                        callXML = callXML + "<Customer_Mobile>" + (callModel.customerMobile != null ? callModel.customerMobile.ToString().Trim() : "") + "</Customer_Mobile>";
                        callXML = callXML + "<Type>" + (callModel.type != null ? callModel.type.ToString().Trim().ToUpper() : "") + "</Type>";
                        callXML = callXML + "<Time_Start>" + (DateTime.TryParse(callModel.timeStart.ToString(), out DateTime date) ? date : null)+ "</Time_Start>";
                        callXML = callXML + "<Time_End>" + (DateTime.TryParse(callModel.timeEnd.ToString(), out date) ? date : null) + "</Time_End>";
                        callXML = callXML + "<Call_Duration>" + (Int32.TryParse(callModel.callDuration.ToString(), out int call) ? call : 0) + "</Call_Duration>";
                        callXML = callXML + "</row>";
                    }
                }

                callXML = callXML + "</root>";

                string spName = "API_MobAppCall_IUDS";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 200},
                    new SqlParameter() { ParameterName = "@Action", Value =  "I"},
                    new SqlParameter() { ParameterName = "@Login_Id", Value = login_Id},
                    new SqlParameter() { ParameterName = "@CallXML", SqlDbType = SqlDbType.Xml, Value=callXML }                   
                };
                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                await DBHelper.ExecuteNoQueryAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[0].Value != null && lstParam[0].Value.ToString() == "1")
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = (lstParam[1].Value != null ? lstParam[1].Value.ToString() : "");
                    genresponse.IsSuccess = true;
                    genresponse.Title = "Success";
                }
                else {
                    genresponse.Status = HttpStatusCode.BadRequest;
                    genresponse.Message = (lstParam[1].Value != null ? lstParam[1].Value.ToString() : "Error");
                    genresponse.IsSuccess = false;
                    genresponse.Title = "Error";
                }
            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Data = null;
                genresponse.IsSuccess = false;
                genresponse.Title = "Error";

            }
            return genresponse;
        }

        public async Task<dynamic> mobAppCallReport(string Key, int login_Id, MobAppCallReportReq model)
        {
            APIObjectResponse genresponse = new APIObjectResponse();
            MobAppCallReportWrap report = new MobAppCallReportWrap();

            try
            {  
                string spName = "API_MobAppCall_IUDS";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "", Size = 200 },
                    new SqlParameter() { ParameterName = "@Action", Value = "S" },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = login_Id },
                    new SqlParameter() { ParameterName = "@Type", Value = model.type },
                    new SqlParameter() { ParameterName = "@Customer_Mobile", Value = model.customerMobile },
                    new SqlParameter() { ParameterName = "@Time_Start", Value = (DateTime.TryParse(model.timeStart, out DateTime date) ? date : null) },
                    new SqlParameter() { ParameterName = "@Tme_End", Value = (DateTime.TryParse(model.timeEnd, out date) ? date : null) },
                    new SqlParameter() { ParameterName = "@Duration_From", Value = model.callDuration },
                    new SqlParameter() { ParameterName = "@Duration_To", Value = model.callDurationTo }
                };
                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                DataSet ds = new DataSet();
                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0) { 
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0) {

                        List<MobAppCallReport> callList = new List<MobAppCallReport>();
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            MobAppCallReport call = new MobAppCallReport();
                            call.loginId = (Int32.TryParse(dt.Rows[i]["Login_Id"].ToString(), out int id) ? id : 0);
                            call.loginName = (dt.Rows[i]["Login_Name"] != null ? dt.Rows[i]["Login_Name"].ToString() : "");
                            call.customerMobile = (dt.Rows[i]["Customer_Mobile"] != null ? dt.Rows[i]["Customer_Mobile"].ToString() : "");
                            call.type = (dt.Rows[i]["Type"] != null ? dt.Rows[i]["Type"].ToString() : "");
                            call.timeStart = (dt.Rows[i]["Time_Start"] != null ? dt.Rows[i]["Time_Start"].ToString() : "");
                            call.timeEnd = (dt.Rows[i]["Time_End"] != null ? dt.Rows[i]["Time_End"].ToString() : "");
                            call.callDuration = (dt.Rows[i]["Call_Duration"] != null ? dt.Rows[i]["Call_Duration"].ToString() : "");
                            callList.Add(call);
                        }

                        report.CallList = callList;
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Data = report;
                genresponse.IsSuccess = true;
                genresponse.Title = "Success";
            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Data = null;
                genresponse.IsSuccess = false;
                genresponse.Title = "Error";
            }

            return genresponse;
        }
    }
}
