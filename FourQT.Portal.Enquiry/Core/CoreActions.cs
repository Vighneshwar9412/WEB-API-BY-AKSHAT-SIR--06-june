using System.Data;
using FourQT.CommonFunctions.Portal;
using FourQT.Entities.Portal;
using FourQT.Utilities.Portal;
using FourQT.DAL.Portal;

namespace FourQT.Portal.Enquiry.Core
{
    public class CoreActions
    {
        public ResponseStatus<User> GetTransferUsers(string Token, int LoginID)
        {
            ResponseStatus<User> commonResult = new ResponseStatus<User>();
            commonResult.Data = null;
            commonResult.LstData = null;
            commonResult.Status = false;
            commonResult.Message = "";
            commonResult.ErrorCode = 417;

            DataSet dsUser = new DataSet();
            try
            {
                string connString = Common.CheckToken(Token);
                if (object.Equals(connString, null) == true)
                {
                    commonResult.Data = null;
                    commonResult.LstData = null;
                    commonResult.Status = false;
                    commonResult.Message = "Invalid Token";
                    commonResult.ErrorCode = 417;
                    return commonResult;
                }
                else
                {
                    dsUser = (new FourQT.DAL.Portal.DAL()).GetTransferUsers(connString, Token, LoginID);

                    if (!Object.Equals(dsUser, null))
                    {
                        if (dsUser.Tables[0].Rows.Count > 0)
                        {
                            commonResult.Data = null;

                            commonResult.Status = true;
                            commonResult.Message = "";

                            List<User> lstUser = new List<User>();
                            foreach (DataRow dr in dsUser.Tables[0].Rows)
                            {
                                User objUser = new User();

                                objUser.Login_ID = Convert.ToInt32(dr["Login_Id"].ToString());
                                objUser.User_Name = dr["User_Name"].ToString();
                                objUser.Emp_ID = Convert.ToInt32(dr["Emp_ID"].ToString());

                                lstUser.Add(objUser);
                            }

                            commonResult.LstData = lstUser;
                            return commonResult;
                        }
                    }
                }

                return commonResult;
            }
            catch (Exception ex)
            {
                commonResult.Data = null;
                commonResult.LstData = null;
                commonResult.Status = false;
                commonResult.Message = ex.Message;
                commonResult.ErrorCode = 417;

                Log.LogExceptionSubject(ex, "GetTransferUsers( Token=" + Token + ", LoginID=" + LoginID + ")", Token);

                return commonResult;
            }
        }


    }
}
