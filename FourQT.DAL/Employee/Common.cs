using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities;
using System.Xml.Linq;
using System.Drawing.Imaging;
using System.Xml;

namespace FourQT.DAL.Employee
{
    public class Common
    {
        public static string GetJWTTokenEmployee(string dKey,int loginId)
        {
            string token = "";
            try
            {
                string spName = "API_GetSetJWTToken";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Action", Value = "G" }
                };

                DataSet dt = DBHelper.GetDatasetEmployee(dKey, CommandType.StoredProcedure, spName, lstParam);
                var apiToken = "";

                if(dt.Tables[0] !=null && dt.Tables[0].Rows.Count > 0) {
                    apiToken = dt.Tables[0].Rows[0]["APIToken"].ToString();
                }

                if (apiToken != null)
                {
                    token = apiToken;
                }
            }
            catch(Exception ex)
            {
                token = "";
            }

            return token;
        }
    }
}
