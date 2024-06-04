using System.Data;
using FourQT.CommonFunctions.Portal;
using FourQT.Utilities.Portal;

namespace FourQT.Portal.RealEasy
{
    public class RealEasyDAL
    {
        public static DataSet RealEasyLogin(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";
            try
            {
                ConnString = Common.CheckTokenCustomerLogin(Token, DBName);
                if (object.Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetAccountDetails(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetAccountDetails(Token=" + Token + ")", Token);
                return null;
            }
        }

        public static List<RealEasyMenu> GetMenuData(string Token, int RoleId, int Fstatus)
        {
            string DBName = "REMS_DBName";
            List<RealEasyMenu> MenuList = new List<RealEasyMenu>();
            string ConnString = "";
            try
            {
                ConnString = Common.CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = FourQT.DAL.Portal.DAL.GetMenuData(ConnString, Token, RoleId, Fstatus);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MenuList.Add(new RealEasyMenu
                        {
                            MenuID = row["Date"].ToString(),

                            Url = row["Url"].ToString(),
                            Name = row["Name"].ToString(),
                            ParentID = row["ParentID"].ToString(),
                            sPosition = row["sPosition"].ToString()

                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common. GetMenuData(Token=" + Token + ")", Token);
                return null;
            }
            return MenuList;
        }
        public static DataTable GetMenu(string Token, int RoleId, int Fstatus)
        {
            string connectionstring = "";
            string DBName = "REMS_DBName";

            connectionstring = Common.CheckTokenCustomerLogin(Token, DBName);

            try
            {
                return FourQT.DAL.Portal.DAL.GetMenu(connectionstring, Token, RoleId, Fstatus);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetMenu( Connectionstring=" + connectionstring + ")", Token);
                return null;
            }
        }



    }
}
