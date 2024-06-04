using System.Data;
using System.Data.SqlClient;
using FourQT.Entities.Portal;
using FourQT.Entities.Portal.Referrals;
using FourQT.CommonFunctions.Portal;
using System.Net;

namespace FourQT.DAL.Portal
{
    public class DAL
    {
        static SqlConnection? _connection;

        public static async Task<DataTable> CheckToken(string Token, string Type)
        {
            try
            {
                _connection = FourQT.DAL.Portal.DbConnection.GetConnection(cnSetting.cnString_Connection);
                SqlCommand cmd = FourQT.DAL.Portal.DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_ValidateToken";
                SqlParameter paramToken = new SqlParameter("@Token", Token);
                cmd.Parameters.Add(paramToken);
                SqlParameter paramType = new SqlParameter("@Type", Type);
                cmd.Parameters.Add(paramType);

                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                if (ds != null && ds.Tables.Count > 0) {
                    dt = ds.Tables[0];
                }

                //SqlDataAdapter sda = new SqlDataAdapter(cmd);               
                //sda.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.CheckToken( Token=" + Token + ",  Type=" + Type + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetPortal_HomePages(string ConnString, string Token, int CustomerId)
        {
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_HomePages";
                SqlParameter paramToken = new SqlParameter("@CustomerID", CustomerId);
                cmd.Parameters.Add(paramToken);

                DataSet ds = new DataSet();

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                //SqlDataAdapter sda = new SqlDataAdapter(cmd);              
                //sda.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.Portal_HomePages(ConnString=" + ConnString + ",Token=" + Token + ",CustomerId=" + CustomerId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> CustomerLogin(string ConnString, string Token, string username, string password, string DeviceId, int FromDevice)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UDP_USERLOGINV1";
                SqlParameter paramUser_Name = new SqlParameter("@UserName", username);
                SqlParameter Password = new SqlParameter("@Password", password);
                SqlParameter DeviceIds = new SqlParameter("@DeviceId", DeviceId);
                SqlParameter FromDevices = new SqlParameter("@FromDevice", FromDevice);
                cmd.Parameters.Add(paramUser_Name);
                cmd.Parameters.Add(Password);
                cmd.Parameters.Add(DeviceIds);
                cmd.Parameters.Add(FromDevices);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.CustomerLogin(ConnString=" + ConnString + ", LoginId=" + username + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> ChangePassword(string ConnString, string Token, string username, string oldpassword, string newpassword, int customerId = 0)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_udp_userchangepassword";
                SqlParameter customer = new SqlParameter("@CustomerId", customerId);
                SqlParameter paramUser_Name = new SqlParameter("@username", username);
                SqlParameter oldPassword = new SqlParameter("@Oldpassword", oldpassword);
                SqlParameter newPassword = new SqlParameter("@Newpassword", newpassword);
                cmd.Parameters.Add(customer);
                cmd.Parameters.Add(paramUser_Name);
                cmd.Parameters.Add(oldPassword);
                cmd.Parameters.Add(newPassword);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.ChangePassword(ConnString=" + ConnString + ", LoginId=" + username + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetRegisteredMobile(string ConnString, string Token, string username)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCustomerNameMobileNew";
                SqlParameter paramUser_Name = new SqlParameter("@UserName", username);
                cmd.Parameters.Add(paramUser_Name);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetRegisteredMobile(ConnString=" + ConnString + ", LoginId=" + username + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetSMSAPI(string ConnString, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_getActiveSmsSetUp";
                SqlParameter param_Name = new SqlParameter("@Subject_ID", SqlDbType.Int);
                param_Name.Value = 0;
                cmd.Parameters.Add(param_Name);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetCustomerUnitNumber(string ConnString, string Token, int CustomerId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_getCustomerRegistration";
                SqlParameter paramUser_Name = new SqlParameter("@CP_Id", CustomerId);
                cmd.Parameters.Add(paramUser_Name);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetCustomerUnitNumber(ConnString=" + ConnString + ", CustomerId=" + CustomerId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetCustomerNameMobile(string ConnString, string Token, int PortalId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCustomerNameMobileNew";
                SqlParameter cp_id = new SqlParameter("@cp_id", PortalId);
                cmd.Parameters.Add(cp_id);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetLetterDetailCustomer(ConnString=" + ConnString + ", PortalId=" + PortalId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> Getudp_getquery(string ConnString, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "udp_getquery";

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.Getudp_getquery(ConnString=" + ConnString + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }   

        public static async Task<DataSet> InsertqueryHistory(string ConnString, InsertqueryHistory ObjInsertqueryHistory,string dKey="", int customerId = 0)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "udp_InsertqueryHistoryV1";
                SqlParameter Queryid = new SqlParameter("@Queryid", ObjInsertqueryHistory.queryid);
                SqlParameter Text = new SqlParameter("@Text", ObjInsertqueryHistory.text);
                SqlParameter Createdby = new SqlParameter("@Createdby", customerId);
                SqlParameter Mailstatus = new SqlParameter("@Mailstatus", ObjInsertqueryHistory.mailstatus);
                SqlParameter Regid = new SqlParameter("@Regid", ObjInsertqueryHistory.regid);

                SqlParameter rootDomain = new SqlParameter("@RootDomain", ObjInsertqueryHistory.RootDomain);
                SqlParameter fileName = new SqlParameter("@FileName", ObjInsertqueryHistory.FileName);

                cmd.Parameters.Add(Queryid);
                cmd.Parameters.Add(Text);
                cmd.Parameters.Add(Createdby);
                cmd.Parameters.Add(Mailstatus);
                cmd.Parameters.Add(Regid);
                cmd.Parameters.Add(rootDomain);
                cmd.Parameters.Add(fileName);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                string key = "";
                if (dKey != "") { key = dKey; }
                else
                {
                    if (ObjInsertqueryHistory.Token != null) { key = Convert.ToString(ObjInsertqueryHistory.Token); }
                }

                Log.LogMessage("Source:- " + ConnString, key);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.InsertqueryHistory(ConnString=" + ConnString + ",Token=" + key + ",Queryid=" + ObjInsertqueryHistory.queryid + ",Text=" + ObjInsertqueryHistory.text + ",Createdby=" + ObjInsertqueryHistory.createdby + ",Mailstatus=" + ObjInsertqueryHistory.mailstatus + ",Regid=" + ObjInsertqueryHistory.regid + ")", key);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetConstructionProject(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_GetApproveProjectConstruction";
                SqlParameter paramToken = new SqlParameter("@Registration_Id", RegistrationId);
                cmd.Parameters.Add(paramToken);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetConstructionProject(ConnString=" + ConnString + ",Token=" + Token + ",RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetConstructionTower(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_GetApproveTowerConstruction";
                SqlParameter paramToken = new SqlParameter("@Registration_Id", RegistrationId);
                cmd.Parameters.Add(paramToken);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetConstructionTower(ConnString=" + ConnString + ",Token=" + Token + ",RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetNotification(string ConnString, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_getmessage_detail";

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetNotification(ConnString=" + ConnString + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataTable> GetFAQ(string ConnString, string Token)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_FAQ_View";

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                if (ds != null && ds.Tables.Count > 0) {
                    dt = ds.Tables[0];
                }

                return dt;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetFAQ( Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<ResponseStatus_New<NoData>> SaveCustomerReferral(string ConnString, CustomerReferral Obj, int customerId=0)
        {
            ResponseStatus_New<NoData> response = new ResponseStatus_New<NoData>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_IUS_Customer_Referral";

                cmd.Parameters.Add(new SqlParameter("Customer_Referral_Id", Obj.Customer_Referral_Id));
                cmd.Parameters.Add(new SqlParameter("Referral_Name", Obj.Referral_Name));
                cmd.Parameters.Add(new SqlParameter("Referral_Mobile", Obj.Referral_Mobile));
                cmd.Parameters.Add(new SqlParameter("Referral_Mobile2", Obj.Referral_Mobile2));
                cmd.Parameters.Add(new SqlParameter("Referral_Email", Obj.Referral_Email));
                cmd.Parameters.Add(new SqlParameter("Referral_Address", Obj.Referral_Address));
                cmd.Parameters.Add(new SqlParameter("Referral_Relation_Remarks", Obj.Referral_Relation_Remarks));
                cmd.Parameters.Add(new SqlParameter("Referral_Project_Id", Obj.Referral_Project_Id));
                cmd.Parameters.Add(new SqlParameter("Referral_Location", Obj.Referral_Location));
                cmd.Parameters.Add(new SqlParameter("CP_Id", customerId));
                cmd.Parameters.Add(new SqlParameter("Action", Obj.Action));

                SqlParameter parmStatus = new SqlParameter("Status", 0);
                parmStatus.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmStatus);

                SqlParameter parmOutMsg = new SqlParameter("OutMsg", String.Empty);
                parmOutMsg.Direction = ParameterDirection.Output;
                parmOutMsg.Size = 1000;
                cmd.Parameters.Add(parmOutMsg);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                await cmd.ExecuteNonQueryAsync();

                response.Status = HttpStatusCode.OK;
                response.Message = parmOutMsg.Value.ToString();
                response.IsSuccess = true;
                response.Title = "Success";

                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.ExpectationFailed;
                response.Message = ex.Message;
                response.IsSuccess = false;
                response.Title = "Error";

                return response;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetCustomerReferralList(string ConnString, CustomerReferral Obj, int customerId = 0)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_Get_Customer_Referral";
                cmd.Parameters.Add(new SqlParameter("CP_Id", customerId));

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<int> SetJWTTokenPortal(string ConnString, string customerId,string JWTToken,string Token)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_JWTToken_IUDS";
                cmd.Parameters.Add(new SqlParameter("CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("Token", JWTToken));
                cmd.Parameters.Add(new SqlParameter("Action", "U"));

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                await cmd.ExecuteNonQueryAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.SetJWTTokenPortal(ConnString=" + ConnString + ", CustomerId=" + customerId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetJWTTokenPortal(string ConnString, string customerId, string Token)
        {
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_JWTToken_IUDS";
                cmd.Parameters.Add(new SqlParameter("CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("Action", "S"));

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetJWTTokenPortal(ConnString=" + ConnString + ", CustomerId=" + customerId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }



        public static async Task<DataSet> GetCustomerDetails(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_CustomerDetail_Portal";
                SqlParameter paramUser_Name = new SqlParameter("@RegistrationId", RegistrationId);
                cmd.Parameters.Add(paramUser_Name);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetCustomerDetails(ConnString=" + ConnString + ", RegistrationId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetUdp_MultipleApplicant(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "Udp_MultipleApplicant";
                cmd.CommandText = "Portal_ApplicantDetails";
                SqlParameter Registration_ID = new SqlParameter("@RegistrationId", RegistrationId);
                cmd.Parameters.Add(Registration_ID);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetUdp_MultipleApplicant(ConnString=" + ConnString + ", RegistrationId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetUnitDetails(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_UnitDetails";
                SqlParameter paramUser_Name = new SqlParameter("@RegistrationId", RegistrationId);
                cmd.Parameters.Add(paramUser_Name);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetUnitDetails(ConnString=" + ConnString + ", RegistrationId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetAccountDetails(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_AccountDetails";
                SqlParameter paramToken = new SqlParameter("@RegId", RegistrationId);
                cmd.Parameters.Add(paramToken);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetAccountDetails(ConnString=" + ConnString + ",Token=" + Token + ",RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetOutstandingDetail_Paymentschedule(string ConnString, string Token, int RegistrationId, string Type = "A")
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "GetOutstandingDetail_Paymentschedule";
                cmd.CommandText = "Portal_GetOutstandingDetail_Paymentschedule";

                SqlParameter Registration_ID = new SqlParameter("@RegID", RegistrationId);
                cmd.Parameters.Add(Registration_ID);

                SqlParameter paramType = new SqlParameter("@Type", Type);
                cmd.Parameters.Add(paramType);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetOutstandingDetail_Paymentschedule(ConnString=" + ConnString + ", RegistrationId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetReceiptDetails(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                //cmd.CommandText = "GetOutstandingDetail_Paymentschedule";
                cmd.CommandText = "Portal_ReceiptDetails";
                SqlParameter Registration_ID = new SqlParameter("@RegistrationId", RegistrationId);
                cmd.Parameters.Add(Registration_ID);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetReceiptDetails(ConnString=" + ConnString + ", RegistrationId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetLetterDetailCustomerPortal(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Udp_getLetterDetailCustomer_portal";
                SqlParameter Letter_ID = new SqlParameter("@Registration_Id", RegistrationId);
                cmd.Parameters.Add(Letter_ID);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetLetterDetailCustomer(ConnString=" + ConnString + ", LetterId=" + RegistrationId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetContactUS(string ConnString, string Token)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_GetContactUsInfo";

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;

            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetLetterDetailCustomer(ConnString=" + ConnString + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetProjectDocumentLink(string ConnString, int regId,string portalKey)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_getProjectDocuments_bytype";
                SqlParameter paramUser_Name = new SqlParameter("@Registration_Id", regId);
                cmd.Parameters.Add(paramUser_Name);
                //SqlParameter paramDoc_Type = new SqlParameter("@DocType", objDocument.DocType);
                //cmd.Parameters.Add(paramDoc_Type);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, portalKey);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetProjectDocumentLink(ConnString=" + ConnString + ", RegistrationId=" + regId + ",Token=" + portalKey + ")", portalKey);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetCustomerDocumentLink(string portalKey,string ConnString, CustomerDoc objDocument)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "Portal_getdocumentfordownload";
                SqlParameter paramUser_Name = new SqlParameter("@Registration_Id", objDocument.CustomerId);
                cmd.Parameters.Add(paramUser_Name);
                SqlParameter paramDoc_Type = new SqlParameter("@DocType", objDocument.DocType);
                cmd.Parameters.Add(paramDoc_Type);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, portalKey);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetCustomerDocumentLink(ConnString=" + ConnString + ", RegistrationId=" + objDocument.CustomerId + ",Token=" + portalKey + ")", portalKey);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<int> CheckHRAttendanceToken(string Token, string Type)
        {
            int ret = 0;
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();

                _connection = DbConnection.GetConnection(cnSetting.cnString_Connection);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_Validate_HRAttendanceToken";
                SqlParameter paramToken = new SqlParameter("@Token", Token);
                cmd.Parameters.Add(paramToken);
                SqlParameter paramType = new SqlParameter("@Type", Type);
                cmd.Parameters.Add(paramType);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                if(ds!=null && ds.Tables.Count > 0)
                {
                    dt = ds.Tables[0];
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        Int32.TryParse(dt.Rows[0]["Result"].ToString(), out ret);
                    }
                }

                return ret;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.CheckHRAttendanceToken( Token=" + Token + ",  Type=" + Type + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> InsertAttendanceAndGetDetails(string ConnString, string Token, string XMLDetail)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_AddUpdateHRAttendance";
                SqlParameter EmployeeAttendanceDetails = new SqlParameter("@EmployeeAttendanceDetails", XMLDetail);
                cmd.Parameters.Add(EmployeeAttendanceDetails);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.InsertAttendanceAndGetDetails(ConnString=" + ConnString + ", XMLDetail=" + XMLDetail + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<int> SetJWTTokenPortalLogout(string ConnString, string customerId, string Token)
        {
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_JWTToken_IUDS";
                cmd.Parameters.Add(new SqlParameter("CustomerId", customerId));
                cmd.Parameters.Add(new SqlParameter("Action", "D"));

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.SetJWTTokenPortalLogout(ConnString=" + ConnString + ", CustomerId=" + customerId + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            return 0;
        }

        public static async Task<DataSet> GetCustomerDuesPaid(string ConnString, string Token, int Registration_Id)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "UDP_CustomerDemandStatusNewV1";
                SqlParameter Registration_ID = new SqlParameter("@Registration_Id", Registration_Id);
                cmd.Parameters.Add(Registration_ID);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + ConnString, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetCustomerDuesPaid(ConnString=" + ConnString + ", RegistrationId=" + Registration_Id + ",Token=" + Token + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<ResponseStatus_New<NoData>> SaveCustomerReferral_P2(string ConnString, CustomerReferralRequest Obj, int customerId = 0)
        {
            string? status = "0", outMsg = "";
            ResponseStatus_New<NoData> response = new ResponseStatus_New<NoData>();
            SqlCommand cmd = new SqlCommand();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_Portal_IUS_Customer_Referral";

                cmd.Parameters.Add(new SqlParameter("Customer_Referral_Id", 0));
                cmd.Parameters.Add(new SqlParameter("Referral_Name", Obj.refFullName));
                cmd.Parameters.Add(new SqlParameter("Referral_Mobile", Obj.refContactNo));
                cmd.Parameters.Add(new SqlParameter("Referral_Email", Obj.refMailId));
                cmd.Parameters.Add(new SqlParameter("Referral_Address", Obj.refAddress));
                cmd.Parameters.Add(new SqlParameter("Referral_Relation_Remarks", Obj.refRelation));
                cmd.Parameters.Add(new SqlParameter("Referral_Project_Id", Obj.projectId));
                cmd.Parameters.Add(new SqlParameter("Referral_Location", Obj.location));
                cmd.Parameters.Add(new SqlParameter("CP_Id", customerId));
                cmd.Parameters.Add(new SqlParameter("Action", "I"));

                SqlParameter parmStatus = new SqlParameter("Status", 0);
                parmStatus.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(parmStatus);

                SqlParameter parmOutMsg = new SqlParameter("OutMsg", String.Empty);
                parmOutMsg.Direction = ParameterDirection.Output;
                parmOutMsg.Size = 1000;
                cmd.Parameters.Add(parmOutMsg);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                await cmd.ExecuteNonQueryAsync();

                status = parmStatus.Value.ToString();
                outMsg = parmOutMsg.Value.ToString();

                if (status != null && status == "1") {
                    response.Status = HttpStatusCode.OK;                   
                    response.IsSuccess = true;
                    response.Title = "Success";
                }
                else
                {
                    response.Status = HttpStatusCode.ExpectationFailed;
                    response.IsSuccess = false;
                    response.Title = "Error";
                }

                response.Message = outMsg;

                return response;
            }
            catch (Exception ex)
            {
                response.Status = HttpStatusCode.ExpectationFailed;
                response.Message = ex.Message;
                response.IsSuccess = false;
                response.Title = "Error";

                return response;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> getCustomerReferralReport(string ConnString, CustomerCore1 Obj, int customerId = 0)
        {
            ResponseStatus<CustomerReferralDetails> response = new ResponseStatus<CustomerReferralDetails>();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_Portal_IUS_Customer_Referral";

                cmd.Parameters.Add(new SqlParameter("CP_Id", customerId));
                cmd.Parameters.Add(new SqlParameter("Action", "S"));

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }

            return ds;
        }

        public static async Task<DataSet> GetAccountDetails_New(string ConnString, string Token, int RegistrationId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_AccountDetails_New";
                SqlParameter paramToken = new SqlParameter("@RegId", RegistrationId);
                cmd.Parameters.Add(paramToken);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetAccountDetails_New(ConnString=" + ConnString + ",Token=" + Token + ",RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetPortalExtraInfo(string ConnString, string Token, int customerId)
        {
            DataSet ds = new DataSet();
            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_GetPortalExtraInfo";
                SqlParameter paramToken = new SqlParameter("@CustomerId", customerId);
                cmd.Parameters.Add(paramToken);

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetPortalExtraInfo(ConnString=" + ConnString + ",Token=" + Token + ",CustomerId=" + customerId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetRaisedQueryDetails(QueryDetailsRequest objRequest, string ConnString, string Token, int customerId)
        {
            DataSet ds = new DataSet();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_GetRaisedQueryConversation";
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
                cmd.Parameters.Add("@RegistrationId", SqlDbType.Int).Value = objRequest.RegistrationId;
                cmd.Parameters.Add("@QueryId", SqlDbType.Int).Value = objRequest.queryId;
                cmd.Parameters.Add("@Action", SqlDbType.VarChar, 1).Value = objRequest.detailType;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetRaisedQueryDetails(ConnString=" + ConnString + ",Token=" + Token + ",CustomerId=" + customerId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public static async Task<DataSet> GetProjectDocumentsByType(CustomerCore5 objRequest, string ConnString, string Token, int customerId)
        {
            DataSet ds = new DataSet();

            try
            {
                _connection = DbConnection.GetConnection(ConnString);
                SqlCommand cmd = DbConnection.CreateCommandObject(_connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "API_GetProjectDocumentsByType";
                cmd.Parameters.Add("@CustomerId", SqlDbType.Int).Value = customerId;
                cmd.Parameters.Add("@Registration_Id", SqlDbType.Int).Value = objRequest.RegistrationId;
                cmd.Parameters.Add("@DocType", SqlDbType.VarChar, 20).Value = objRequest.Type;

                if (_connection.State == ConnectionState.Closed)
                    _connection.Open();

                IDataReader reader = await cmd.ExecuteReaderAsync();
                while (!reader.IsClosed)
                {
                    ds.Tables.Add().Load(reader);
                }

                return ds;
            }
            catch (Exception ex)
            {
                Log.LogMessage("Source:- " + _connection, Token);
                Log.LogExceptionSubject(ex, "FourQT.DAL.DAL.GetProjectDocumentsByType(ConnString=" + ConnString + ",Token=" + Token + ",CustomerId=" + customerId + ")", Token);
                throw;
            }
            finally
            {
                if (_connection != null)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        _connection.Close();
                    }
                }
            }
        }

    }
}


