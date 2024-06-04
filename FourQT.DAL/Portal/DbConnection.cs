using System.Configuration;
using System.Data.SqlClient;
using FourQT.CommonFunctions.Portal;

namespace FourQT.DAL.Portal
{
    public class cnSetting
    {
        private static string str = PortalAppSettingMethods.GetConnectionSTringPortal(); //ConfigurationManager.ConnectionStrings["FourQTMobAppApi"].ConnectionString;

        public static string cnString_Connection
        {
            get { return str; }
        }
    }

    public class DbConnection
    {
        public static SqlConnection GetConnection(string ConnString)
        {
            SqlConnection connection = new SqlConnection(ConnString);
            return connection;
        }

        public static SqlCommand CreateCommandObject(SqlConnection connection)
        {
            SqlCommand command = new SqlCommand();
            command.Connection = connection;
            return command;
        }

    }
}
