using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web;
using FourQT.Utilities;
using FourQT.DAL;
using System.Text;

namespace FourQT.Miscellaneous
{
    public static class CommonFunctions
    {
        public static bool BasicValidation(int Login_Id, string action, int PK_Id, string PK_Name, ref string Message)
        {
            bool dirty = false;
            if (Login_Id == 0)
            {
                Message = "No Logged-In User!";
                return true;
            }
            if (action == "I" || action == "U")
            {
                if (PK_Name.Trim() == string.Empty)
                {
                    Message = "Blank value!";
                    return true;
                }
            }
            if (action == "U" || action == "D")
            {
                if (PK_Id == 0)
                {
                    Message = "Blank value!";
                    return true;
                }
            }

            return dirty;
        }

        public static SqlParameter GetParameter(string Name, Object Value, SqlDbType DataType, int Size, ParameterDirection Direction)
        {
            SqlParameter objParam = new SqlParameter(Name, Value);
            objParam.SqlDbType = DataType;
            objParam.Size = Size;
            objParam.Direction = Direction;
            return objParam;
        }

        public static SqlParameter GetParameter(string Name, Object Value, SqlDbType DataType, ParameterDirection Direction)
        {
            SqlParameter objParam = new SqlParameter(Name, Value);
            objParam.SqlDbType = DataType;
            objParam.Direction = Direction;
            return objParam;
        }

        //public static void GetStatusAndOutMessage(List<SqlParameter> LstParam, out Int32 Status, out string Message)
        //{
        //    Status = 1;
        //    Message = String.Empty;

        //    SqlParameter outParamStatus = LstParam.Find(x => x.ParameterName == ApplicationConstants.PARAM_STATUS);
        //    Status = (Int32)outParamStatus.Value;

        //    SqlParameter outParamMessage = LstParam.Find(x => x.ParameterName == ApplicationConstants.PARAM_OUTMESSAGE);
        //    Message = (string)outParamMessage.Value;
        //}

        


    }
}
