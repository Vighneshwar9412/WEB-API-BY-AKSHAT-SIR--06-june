using FourQT.DAL;
using FourQT.Entities;
using FourQT.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FourQT.Core
{
    public class FollowUpSaveBLL
    {
        public Object followupSave(string Key, ShortFollowUpSave lll,int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                Utility.GetDateParts(lll.nextFollowupDate, out string date, out string time, out string format);
                Utility.GetDateParts(lll.siteVisitDate, out string svDate, out string svTime, out string svFormat);
                string spName = "API_Pre_Process_FollowupOnly";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                    new SqlParameter() { ParameterName = "@Enquiry_Id", Value = lll.enquiryId},
                    new SqlParameter() { ParameterName = "@Sub_Response_Id", Value = lll.subResponseId},
                    new SqlParameter() { ParameterName = "@EnquiryType_Id", Value = lll.enquiryTypeId},
                    new SqlParameter() { ParameterName = "@NextFollowupDate", Value = date},
                    new SqlParameter() { ParameterName = "@NextFollowupTime", Value = time},
                    new SqlParameter() { ParameterName = "@NextFollowupTimeformate", Value = format},
                    new SqlParameter() { ParameterName = "@CId", Value = lll.cId},
                    new SqlParameter() { ParameterName = "@TimeframeId", Value = lll.timeFrameId},
                    new SqlParameter() { ParameterName = "@Meeting_Date", Value = svDate},
                    new SqlParameter() { ParameterName = "@Meeting_Time", Value = svTime + " " + svFormat},
                    new SqlParameter() { ParameterName = "@Meeting_Duration_Inminute", Value = lll.siteVisitDurationInMin},
                    new SqlParameter() { ParameterName = "@Meeting_Address", Value = lll.siteVisitAddress},
                    new SqlParameter() { ParameterName = "@Stage_Project_Ids", Value = lll.stageProjectId},
                    new SqlParameter() { ParameterName = "@Remarks", Value = lll.remarks},                   
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@FollowupID", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},
                    new SqlParameter() { ParameterName = "@login_id", Value = loginId},

                    new SqlParameter() { ParameterName = "@Meeting_Place", Value = lll.meetingPlace},

                };
                lstParam[15].Direction = ParameterDirection.Output;
                lstParam[16].Direction = ParameterDirection.Output;
                lstParam[17].Direction = ParameterDirection.Output;
                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[15].Value.ToString() == "0")
                {
                    genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[17].Value.ToString();
                    genresponse.Data = lstParam[16].Value.ToString();
                    genresponse.IsSuccess = false;
                }
                else
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = lstParam[17].Value.ToString();
                    genresponse.Data = lstParam[16].Value.ToString();
                    genresponse.IsSuccess = true;

                }

                //string reqContent = JsonConvert.SerializeObject(lll);
                //FourQT.CommonFunctions.Portal.Log.LogMessage(reqContent,"FollowUpSave");
            }
            catch (Exception er)
            {
                genresponse.IsSuccess = false;
                genresponse.Status = HttpStatusCode.BadGateway;
                genresponse.Message = er.ToString();
            }

            return genresponse;
        }
    }
}
