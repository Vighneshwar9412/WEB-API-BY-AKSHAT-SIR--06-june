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
    public class SaveSVLocationBLL
    {
        public APIObjectResponse saveSVlocation(string Key, SVLocationShort lll, int Login_Id)
        {

            APIObjectResponse genresponse = new APIObjectResponse();
            DataSet ds = new DataSet();
            try
            {
                string spName = "API_Pre_Process_SaveSVlocation";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id},
                    new SqlParameter() { ParameterName = "@Channel_Id", Value = lll.channelId},
                    new SqlParameter() { ParameterName = "@Enquiry_Id", Value = lll.enquiryId},
                    new SqlParameter() { ParameterName = "@GeoLocation", Value = lll.geolocation},
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},
                };
                lstParam[4].Direction = ParameterDirection.Output;
                lstParam[5].Direction = ParameterDirection.Output;

                int res = DBHelper.ExecuteNonQuery(Key, CommandType.StoredProcedure, spName, lstParam);

                if (lstParam[4].Value.ToString() == "0")
                {
                    genresponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genresponse.Message = lstParam[5].Value.ToString();
                    genresponse.IsSuccess = false;
                }
                else
                {
                    genresponse.Status = HttpStatusCode.OK;
                    genresponse.Message = lstParam[5].Value.ToString();
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
