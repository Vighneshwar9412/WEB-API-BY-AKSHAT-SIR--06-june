using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities;
using FourQT.DAL;
using System.Net;



namespace FourQT.Core
{
    public class SaveLeadBLL
    {
        public async Task<dynamic> leadsave(string Key, LeadShort lll,int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_save_enquiry";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                new SqlParameter() { ParameterName = "@Salutation", Value = lll.salutation},

                new SqlParameter() { ParameterName = "@FirstName", Value = lll.firstName},
                new SqlParameter() { ParameterName = "@LastName", Value = lll.lastName},
                new SqlParameter() { ParameterName = "@IsdCode_MobileNo1", Value = lll.isd},
                new SqlParameter() { ParameterName = "@Mobile_No1", Value = lll.mobileNo1},
                new SqlParameter() { ParameterName = "@Email_Id1", Value = lll.emailId1},
                new SqlParameter() { ParameterName = "@Source_Id", Value = lll.sourceId},
                new SqlParameter() { ParameterName = "@EnquiryType", Value = lll.enquiryType},
                new SqlParameter() { ParameterName = "@InOut", Value = lll.callDirection},
                new SqlParameter() { ParameterName = "@Status", Value = 0},
                new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},
                new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                new SqlParameter() { ParameterName = "@Remarks", Value = lll.remarks},

                };
                lstParam[9].Direction = ParameterDirection.Output;
                lstParam[10].Direction = ParameterDirection.Output;

                int res = await DBHelper.ExecuteNonQueryAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                genresponse.Data = new { enquiryId = (int)lstParam[9].Value };

                if (lstParam[9].Value.ToString() == "0")
                {
                    genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[10].Value.ToString();
                    genresponse.IsSuccess = false;
                }
                else
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = lstParam[10].Value.ToString();
                    genresponse.IsSuccess = true;

                }
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
