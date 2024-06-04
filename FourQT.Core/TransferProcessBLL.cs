using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Text.Json;

namespace FourQT.Core
{
    public  class TransferProcessBLL
    {
        public Object transferprocess(string Key, Transfer lll,int loginId)
        {
            DataSet ds = new DataSet();

            APIObjectResponse genresponse = new APIObjectResponse();
            string spName = "API_Pre_Process_Transfer";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                new SqlParameter() { ParameterName = "@Enquiry_Id", Value = lll.enquiryId},
                new SqlParameter() { ParameterName = "@Remarks", Value = lll.remarks},
                new SqlParameter() { ParameterName = "@transfer_to", Value = lll.TransferTo},
                new SqlParameter() { ParameterName = "@Status", Value = 0},
                new SqlParameter() { ParameterName = "@FollowupID", Value = 0},
                new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},
                new SqlParameter() { ParameterName = "@ReturnTransferred_Login_Id", Value = 0},
            };

            lstParam[5].Direction = ParameterDirection.Output;
            lstParam[6].Direction = ParameterDirection.Output;
            lstParam[7].Direction = ParameterDirection.Output;
            lstParam[8].Direction = ParameterDirection.Output;

            int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

            if (lstParam[5].Value.ToString() == "0")
            {
                genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                genresponse.Message = lstParam[7].Value.ToString();
                Dictionary<string,string> arrName = new Dictionary<string,string>();
                arrName.Add("FollowupID", lstParam[6].Value.ToString());
                arrName.Add("ReturnTransferred_Login_Id", lstParam[8].Value.ToString());

                genresponse.Data = JsonSerializer.Serialize(arrName);
                genresponse.IsSuccess = false;
            }
            else
            {
                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = lstParam[7].Value.ToString();
                Dictionary<string, string> arrName = new Dictionary<string, string>();
                arrName.Add("FollowupID", lstParam[6].Value.ToString());
                arrName.Add("ReturnTransferred_Login_Id", lstParam[8].Value.ToString());

                genresponse.Data = JsonSerializer.Serialize(arrName);
                genresponse.IsSuccess = true;

            }
            return genresponse;

        }


        public Object BulkTransferProcess(string Key, Transfer ObjTransfer, int loginId)
        {
            DataSet ds = new DataSet();

            APIObjectResponse genresponse = new APIObjectResponse();
            string spName = "api_bulktransferenquiry";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                new SqlParameter() { ParameterName = "@Channel_Id", Value = ObjTransfer.channelId},
                new SqlParameter() { ParameterName = "@Transfer_To", Value = ObjTransfer.TransferTo},
                new SqlParameter() { ParameterName = "@Enquiry_Ids", Value = ObjTransfer.commaSeparatedEnquiryIds},
                new SqlParameter() { ParameterName = "@Remarks", Value = ObjTransfer.remarks},
                new SqlParameter() { ParameterName = "@OutMessage", Value = "",Size= 8000},
                new SqlParameter() { ParameterName = "@Status", Value = 0}
            };

            lstParam[5].Direction = ParameterDirection.Output;
            lstParam[6].Direction = ParameterDirection.Output;

            int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

            genresponse.Message = lstParam[5].Value.ToString();

            if (lstParam[6].Value.ToString() == "0")
            {
                genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                genresponse.IsSuccess = false;
            }
            else
            {
                genresponse.Status = HttpStatusCode.OK;                
                genresponse.IsSuccess = true;
            }
            return genresponse;

        }
    }
}
