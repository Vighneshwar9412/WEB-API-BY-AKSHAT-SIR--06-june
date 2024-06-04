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
    public class UpdateCustBLL
    {
        public Object updatecustomer(string Key, PartyUpdate lll,int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_update_customer";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size=500},
                    new SqlParameter() { ParameterName = "@Enquiry_Id", Value = lll.enquiryId},
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                    new SqlParameter() { ParameterName = "@Salutation", Value = lll.salutation},
                    new SqlParameter() { ParameterName = "@FirstName", Value = lll.firstName},
                    new SqlParameter() { ParameterName = "@LastName", Value = lll.lastName},
                    new SqlParameter() { ParameterName = "@Mobile_No2", Value = lll.mobileNo2},
                    new SqlParameter() { ParameterName = "@Mobile_No3", Value = lll.mobileNo3},
                    new SqlParameter() { ParameterName = "@Email_Id1", Value = lll.emailId1},
                    new SqlParameter() { ParameterName = "@Email_Id2", Value = lll.emailId2},
                    new SqlParameter() { ParameterName = "@DOB", Value = lll.dOB},
                    new SqlParameter() { ParameterName = "@DOA", Value = lll.dOA},
                };
                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[0].Value.ToString() == "1")
                {
                    genresponse.Status = System.Net.HttpStatusCode.OK;
                    genresponse.Message = "Customer Updated Successfully";
                    genresponse.IsSuccess = true;
                    genresponse.Title = "Success";
                }
                else
                {
                    genresponse.Status = HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[1].Value.ToString();
                    genresponse.IsSuccess = false;
                    genresponse.Title = "Failed";
                }
            }
            catch (Exception er)
            {
                genresponse.IsSuccess = false;
                genresponse.Status = HttpStatusCode.BadGateway;
                genresponse.Message = er.Message.ToString();
                genresponse.Title = "Error";
            }

            return genresponse;
        }


        public Object updaterquiremnt(string Key, Requirement lll,int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_Pre_Process_UpdateRequirement ";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size=500},
                    new SqlParameter() { ParameterName = "@sFollowedBy", Value = loginId},
                    new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                    new SqlParameter() { ParameterName = "@sEnqueryId", Value = lll.enquiryId},
                    new SqlParameter() { ParameterName = "@City_Id", Value = lll.cityId},
                    new SqlParameter() { ParameterName = "@UnitType_Ids", Value = lll.unitTypeId},
                    new SqlParameter() { ParameterName = "@Project_Ids", Value = lll.projectId},
                    new SqlParameter() { ParameterName = "@Budget_Min", Value = lll.budgetMin},
                    new SqlParameter() { ParameterName = "@Budget_Max", Value = lll.budgetMax},
                    new SqlParameter() { ParameterName = "@sEnqFor", Value = lll.enqFrom},
                    new SqlParameter() { ParameterName = "@Saleable_Area_Min", Value = lll.saleableAreaMin},
                    new SqlParameter() { ParameterName = "@Saleable_Area_max", Value = lll.saleableAreaMax}
                };
                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[0].Value.ToString() == "1")
                {
                    genresponse.Status = System.Net.HttpStatusCode.OK;
                    genresponse.Message = "Requirement Updated Successfully";
                    genresponse.IsSuccess = true;
                    genresponse.Title = "Success";
                }
                else
                {
                    genresponse.Status = HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[1].Value.ToString();
                    genresponse.IsSuccess = false;
                    genresponse.Title = "Fail";
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
    }
}
