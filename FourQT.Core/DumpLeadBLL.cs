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

namespace FourQT.Core
{
    public class DumpLeadBLL
    {
        public Object leaddump(string Key, Dump lll, int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_Pre_Process_Dump";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                new SqlParameter() { ParameterName = "@sEnqueryId", Value = lll.enquiryId},
                new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                new SqlParameter() { ParameterName = "@sRemarks", Value = lll.remarks},
                new SqlParameter() { ParameterName = "@DumpID", Value = lll.dumpId},
                new SqlParameter() { ParameterName = "@sCId", Value = lll.cId},
                new SqlParameter() { ParameterName = "@sFollowedBy", Value = loginId},
                new SqlParameter() { ParameterName = "@Status", Value = 0},
                new SqlParameter() { ParameterName = "@FollowupID", Value = 0},
                new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},

                };

                //var test=lstParam.Where(a => a.ParameterName == "@Status").FirstOrDefault().Direction;
                lstParam[6].Direction = ParameterDirection.Output;
                lstParam[7].Direction = ParameterDirection.Output;
                lstParam[8].Direction = ParameterDirection.Output;

                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[6].Value.ToString() == "0")
                {
                    genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[8].Value.ToString();
                    genresponse.Data = lstParam[7].Value.ToString();
                    genresponse.IsSuccess = false;
                }
                else
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = lstParam[8].Value.ToString();
                    genresponse.Data = lstParam[7].Value.ToString();
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


        public Object leadsuccess(string Key, LeadSuccess lll,int loginId)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_Pre_Process_Success";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                new SqlParameter() { ParameterName = "@Status", Value = 0},
                new SqlParameter() { ParameterName = "@FollowupID", Value = 0},
                new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},

                new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                new SqlParameter() { ParameterName = "@Enquiry_Id", Value = lll.enquiryId},             
                new SqlParameter() { ParameterName = "@Remarks", Value = lll.remarks},
                new SqlParameter() { ParameterName = "@Project_Id", Value = lll.projectId},
                new SqlParameter() { ParameterName = "@Project_UnitType_Id", Value = lll.projectUnitTypeId},
                new SqlParameter() { ParameterName = "@tower", Value = lll.tower},
                new SqlParameter() { ParameterName = "@Floor", Value = lll.floor},
                new SqlParameter() { ParameterName = "@UnitNo", Value = lll.unitNo},
                new SqlParameter() { ParameterName = "@payment_Plan_Id", Value = lll.paymentPlanId},
                new SqlParameter() { ParameterName = "@Communication_id", Value = lll.cId},
                new SqlParameter() { ParameterName = "@Area", Value = lll.area},
                new SqlParameter() { ParameterName = "@AreaUnit", Value = lll.areaUnit},
                new SqlParameter() { ParameterName = "@Price", Value = lll.price},
                new SqlParameter() { ParameterName = "@CustomerName", Value = lll.customerName},
                new SqlParameter() { ParameterName = "@CustomerMobile", Value = lll.customerMobile},
                new SqlParameter() { ParameterName = "@BookingDate", Value = lll.bookingDate}
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;
                lstParam[2].Direction = ParameterDirection.Output;

                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[0].Value.ToString() == "0")
                {
                    genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[2].Value.ToString();
                    genresponse.Data = lstParam[1].Value.ToString();
                    genresponse.IsSuccess = false;
                }
                else
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = lstParam[2].Value.ToString();
                    genresponse.Data = lstParam[1].Value.ToString();
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
