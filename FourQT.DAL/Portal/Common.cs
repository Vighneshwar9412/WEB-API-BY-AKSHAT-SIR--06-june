using System.Data;
using System.Drawing.Imaging;
using System.Net;
using FourQT.Entities;
using FourQT.Entities.Portal;
using FourQT.Entities.Portal.Referrals;
using FourQT.Utilities;

namespace FourQT.CommonFunctions.Portal
{
    public class Common
    {     

        public static async Task<DataTable> GetdtImageURL(string Token)
        {
            DataTable dt = new DataTable();

            try
            {
                dt = await FourQT.DAL.Portal.DAL.CheckToken(Token, "S");
            }
            catch
            {
                throw;
            }

            return dt;
        }

        public static async Task<DataSet> GetPortal_HomePages(string Token, int CustomerId)
        {
            string ConnString = "";
            string DBName = "CP_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetPortal_HomePages(ConnString, Token, CustomerId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetAllEnquiryMasters(Token=" + Token + ")", Token);
                throw;
            }

            return ds;
        }

        public static async Task<List<ChangePasswordList>> ChangePassword(string Token, string username, string oldpassword, string newpassword, int customerId = 0)
        {
            string DBName = "CP_DBName";
            List<ChangePasswordList> ProcessList = new List<ChangePasswordList>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.ChangePassword(ConnString, Token, username, oldpassword, newpassword, customerId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ProcessList.Add(new ChangePasswordList
                        {

                            UserName = row["User_Name"].ToString(),
                            Password = row["Password"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.ChangePassword(Token=" + Token + ",  LoginId=" + username + ")", Token);
                throw;
            }
            return ProcessList;
        }

        public static async Task<List<Customeruserlist>> GetCustomerLogin(string Token, string username, string password, string DeviceId, int FromDevice)
        {
            string DBName = "CP_DBName";
            List<Customeruserlist> ProcessList = new List<Customeruserlist>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.CustomerLogin(ConnString, Token, username, password, DeviceId, FromDevice);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if ((Int32.TryParse(row["STATUS"].ToString(), out int id) ? id : 0) != 0)
                        {
                            ProcessList.Add(new Customeruserlist
                            {
                                CustomerId =  (Int32.TryParse(row["CUSTOMERID"].ToString(),out id) ? id : 0),
                                Type = (row["TYPE"]!= null ? row["TYPE"].ToString() : ""),
                                Status = (Int32.TryParse(row["STATUS"].ToString(), out id) ? id : 0),
                                Company_Logo = (row["Company_Logo"]!=null? row["Company_Logo"].ToString():""),
                                Profile_Photo = (row["Profile_Photo"] != null ? row["Profile_Photo"].ToString() : "")
                            });
                        }                      
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerLogin(Token=" + Token + ",  LoginId=" + username + ")", Token);
                throw;
            }
            return ProcessList;
        }

        public static async Task<string> CheckTokenCustomerLogin(string Token, string DBName)
        {
            string retVal = "";
            ClientInfo objClientInfo = new ClientInfo();
            try
            {
                DataTable dt = await FourQT.DAL.Portal.DAL.CheckToken(Token, "S");
                if (dt.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dt.Rows[0]["REMS_UserName"].ToString()) == true && string.IsNullOrEmpty(dt.Rows[0]["REMS_Password"].ToString()) == true)
                    {
                        retVal = "Data Source=" + dt.Rows[0]["REMS_IPAddress"].ToString().Trim() + ";Initial Catalog=" + dt.Rows[0]["REMS_DBName"].ToString().Trim() + ";Integrated Security=True";
                    }
                    else
                    {
                        if (DBName == "CP_DBName")
                        {
                            retVal = "Data Source=" + dt.Rows[0]["REMS_IPAddress"].ToString().Trim() + ";Initial Catalog=" + dt.Rows[0]["CP_DBName"].ToString().Trim() + ";User ID=" + dt.Rows[0]["CP_Username"].ToString().Trim() + ";password=" + dt.Rows[0]["CP_Password"].ToString().Trim() + ";Persist Security Info=True";
                        }
                        else if (DBName == "HR_DBName")
                        {
                            retVal = "Data Source=" + dt.Rows[0]["HR_IPAddress"].ToString().Trim() + ";Initial Catalog=" + dt.Rows[0]["HR_DBName"].ToString().Trim() + ";User ID=" + dt.Rows[0]["HR_Username"].ToString().Trim() + ";password=" + dt.Rows[0]["HR_Password"].ToString().Trim() + ";Persist Security Info=True";
                        }
                        else
                        {
                            retVal = "Data Source=" + dt.Rows[0]["REMS_IPAddress"].ToString().Trim() + ";Initial Catalog=" + dt.Rows[0]["REMS_DBName"].ToString().Trim() + ";User ID=" + dt.Rows[0]["REMS_Username"].ToString().Trim() + ";password=" + dt.Rows[0]["REMS_Password"].ToString().Trim() + ";Persist Security Info=True";
                        }
                    }
                    objClientInfo.ClientConnectionString = retVal;
                }
                
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.CheckTokenCustomerLogin(Token=" + Token + ")", Token);
                throw;
            }

            return retVal;
        }

        public static async Task<string> GetRegisteredMobile(string Token, string username)
        {
            string DBName = "CP_DBName";
            string? mobile = "";
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.GetRegisteredMobile(ConnString, Token, username);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    mobile = (ds.Tables[0].Rows[0]["F_R_Mobile_No"] != null ? ds.Tables[0].Rows[0]["F_R_Mobile_No"].ToString() : "");
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetRegisteredMobile(Token=" + Token + ",  LoginId=" + username + ")", Token);
                throw;
            }
            return mobile;
        }

        public static async Task<bool> SendSMS(string Token, string Mobile, string Message)
        {
            bool success = true;
            string url = "";
            string Adddigit91 = "";
            string connString = await CheckTokenCustomerLogin(Token, "RE_DBName");

            DataSet dsData = await FourQT.DAL.Portal.DAL.GetSMSAPI(connString, Token);
            if (!Object.Equals(dsData, null))
            {
                url = dsData.Tables[0].Rows[0]["URL"].ToString();
                Adddigit91 = dsData.Tables[0].Rows[0]["AddDigit91"].ToString();
                url = url.Replace("@MN@", Mobile);
                url = url.Replace("@MText@", Message);
                string status = readHtmlPage(url);
            }

            return success;
        }

        public static String readHtmlPage(string url)
        {
            String result = "";
            String sResult = "";
            String strPost = "x=1&y=2&z=YouPostedOk";
            StreamWriter myWriter = null;

            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
            objRequest.Method = "POST";
            objRequest.ContentLength = strPost.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            try
            {
                myWriter = new StreamWriter(objRequest.GetRequestStream());
                myWriter.Write(strPost);
            }
            catch
            {
                throw;

            }
            finally
            {
                myWriter.Close();
            }
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr =
            new StreamReader(objResponse.GetResponseStream()))
            {
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }

        public static async Task<List<CustomerLoginListNew>> GetCustomerUnitNumber(string Token, int CustomerId)
        {
            string DBName = "CP_DBName";
            List<CustomerLoginListNew> CustomerUnitList = new List<CustomerLoginListNew>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.GetCustomerUnitNumber(ConnString, Token, CustomerId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CustomerUnitList.Add(new CustomerLoginListNew
                        {
                            Token = Token,
                            RegistrationId = Convert.ToInt32(row["Registration_Id"]),
                            UnitAddress = row["sProperty"].ToString(),
                            unitImage = row["App_Unit_Image"].ToString(),
                            unitIcon = row["App_Unit_Icon"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerUnitNumber(Token=" + Token + ",  CustomerId=" + CustomerId + ")", Token);
                throw;
            }
            return CustomerUnitList;
        }

        public static async Task<List<CustomerNameMobile>> GetCustomerNameMobile(string Token, int PortalId)
        {
            string DBName = "REMS_DBName";
            List<CustomerNameMobile> CustomerNameMobileList = new List<CustomerNameMobile>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.GetCustomerNameMobile(ConnString, Token, PortalId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CustomerNameMobileList.Add(new CustomerNameMobile
                        {
                            ProjectType = row["ProjectType"].ToString(),
                            F_First_name = row["F_First_name"].ToString(),
                            F_R_Mobile_no = row["F_R_Mobile_no"].ToString(),
                            F_EMail_id = row["F_EMail_id"].ToString(),
                            F_H_Mobile_no = row["F_H_Mobile_no"].ToString(),
                            Registration_Id = Convert.ToInt32(row["Registration_Id"]),
                            CP_Id = Convert.ToInt32(row["CP_Id"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common. GetCustomerNameMobile(Token=" + Token + ",  PortalId=" + PortalId + ")", Token);
                throw;
            }
            return CustomerNameMobileList;
        }

        public static async Task<List<Getquery>> Getudp_getquery(string Token)
        {
            string DBName = "CP_DBName";
            List<Getquery> GetqueryList = new List<Getquery>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.Getudp_getquery(ConnString, Token);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        GetqueryList.Add(new Getquery
                        {
                            Query_Name = row["Query_Name"].ToString(),
                            Q_id = Convert.ToInt32(row["Q_id"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common. GetCustomerNameMobileNew(Token=" + Token + ")", Token);
                throw;
            }
            return GetqueryList;
        }

        public static async Task<List<InsertqueryStatus>> InsertqueryHistory(InsertqueryHistory ObjInsertqueryHistory,string dKey="", int customerId = 0)
        {
            string DBName = "CP_DBName";
            List<InsertqueryStatus> queryHistoryList = new List<InsertqueryStatus>();
            string ConnString = String.Empty,key="";
            try
            {
                if (dKey != "") { key = dKey; }
                else
                {
                    if (ObjInsertqueryHistory.Token != null) { key = Convert.ToString(ObjInsertqueryHistory.Token); }
                }
             
                ConnString = await CheckTokenCustomerLogin(key, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.InsertqueryHistory(ConnString, ObjInsertqueryHistory,key, customerId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        queryHistoryList.Add(new InsertqueryStatus { Status = Convert.ToInt32(row["Status"])  });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.InsertqueryHistory(Token=" + key + ",Queryid=" + ObjInsertqueryHistory.queryid + ",Text=" + ObjInsertqueryHistory.text + ",Createdby=" + ObjInsertqueryHistory.createdby + ",Mailstatus=" + ObjInsertqueryHistory.mailstatus + ",Regid=" + ObjInsertqueryHistory.regid + ")", key);
                throw;
            }
            return queryHistoryList;
        }

        public static async Task<DataSet> GetConstructionProject(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "CP_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetConstructionProject(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetConstructionProject(Token=" + Token + ")", Token);
                throw;
            }

            return ds;
        }

        public static async Task<DataSet> GetConstructionTower(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "CP_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetConstructionTower(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetConstructionTower(Token=" + Token + ")", Token);
                throw;
            }

            return ds;
        }

        public static async Task<List<ProjectList>> GetProjectList(DataTable dtProjectList, string Token)
        {
            List<ProjectList>? list = new List<ProjectList>();

            try
            {
                if (dtProjectList != null && dtProjectList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtProjectList.AsEnumerable())
                    {
                        ProjectList proj = await GetdtProjectListRow(dr, Token);
                        list.Add(proj);
                    }
                }

                //list = new List<ProjectList>(
                //           (from dRow in dtProjectList.AsEnumerable()
                //            select (GetdtProjectListRow(dRow, Token)))
                //           );
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "List<Construction Update> GetProjectList(DataTable =" + dtProjectList + ",Token=" + Token + ")", Token);
                throw;
            }
            return list;
        }

        public static async Task<ProjectList> GetdtProjectListRow(DataRow dr, string Token)
        {
            ProjectList objDisplay = new ProjectList();

            try
            {
                DataTable dt = await Common.GetdtImageURL(Token);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string PortalImageURL = dt.Rows[0][20].ToString();
                    objDisplay = new ProjectList();
                    var path = PortalImageURL + "/";
                    objDisplay.Images = path + dr["PortalImages"].ToString();
                    objDisplay.sContent = dr["sContent"].ToString();
                    objDisplay.Status = dr["Status"].ToString();
                    objDisplay.ApprovedDate = dr["ApprovedDate"].ToString();
                }

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetdtProjectListRow(DataRow =" + dr + ",Token=" + Token + ")", Token);
                throw;
            }
            return objDisplay;
        }

        public static async Task<List<TowerList>> GetTowerList(DataTable dtTowerList, string Token)
        {
            List<TowerList> list = new List<TowerList>();

            try
            {
                if (dtTowerList != null && dtTowerList.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtTowerList.AsEnumerable())
                    {
                        TowerList tower = await GetdtTowerListRow(dr, Token);
                        list.Add(tower);
                    }
                }

                //list = new List<TowerList>(
                //           (from dRow in dtTowerList.AsEnumerable()
                //            select (GetdtTowerListRow(dRow, Token)))
                //           );
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "List<Cunstruction> GetTowerList(DataTable =" + dtTowerList + ",Token=" + Token + ")", Token);
                throw;
            }
            return list;
        }

        public static async Task<TowerList> GetdtTowerListRow(DataRow dr, string Token)
        {
            TowerList objUnit = new TowerList();
            try
            {
                DataTable dt = await Common.GetdtImageURL(Token);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string PortalImageURL = dt.Rows[0][20].ToString();
                    var path = PortalImageURL + "/";
                    objUnit = new TowerList();
                    objUnit.Images = path + dr["PortalImages"].ToString();
                    objUnit.sContent = dr["sContent"].ToString();
                    objUnit.Status = dr["Status"].ToString();
                    objUnit.ApprovedDate = dr["ApprovedDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetdtTowerListRow(DataRow =" + dr + ",Token=" + Token + ")", Token);
                throw;
            }
            return objUnit;
        }

        public static async Task<DataSet> GetNotificationjson(string Token)
        {
            string ConnString = "";
            string DBName = "CP_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetNotification(ConnString, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetNotification(Token=" + Token + ")", Token);
                throw;
            }

            return ds;
        }

        public static string JSONWithStringBuilderNotification(DataTable table)
        {
            var result = "";
            
            if (table != null)
            {
                try
                {
                    var ContentsList = new System.Text.StringBuilder();
                    var JSONString = new System.Text.StringBuilder();
                    var FAQContentList = new System.Text.StringBuilder();
                    if (table.Rows.Count > 0)
                    {
                        JSONString.Append("[");
                        for (int i = 0; i < table.Rows.Count; i++)
                        {
                            JSONString.Append("{");
                            for (int j = 0; j < table.Columns.Count; j++)
                            {
                                string CommonColumn = table.Columns[j].ColumnName.ToString();
                                if (j < table.Columns.Count - 1)
                                {
                                    if (table.Columns[j].ColumnName.ToString() == CommonColumn)
                                    {
                                        JSONString.Append("\"" + CommonColumn + "\":" + "\"" + table.Rows[i][(j)].ToString() + "\",");
                                    }
                                }
                                else if (j == table.Columns.Count - 1)
                                {
                                    if (table.Columns[j].ColumnName.ToString() == CommonColumn)
                                    {
                                        JSONString.Append("\"" + CommonColumn + "\":" + "\"" + table.Rows[i][(j)].ToString() + "\",");
                                    }
                                }
                            }

                            if (i == table.Rows.Count - 1)
                            {
                                JSONString.Append("}");
                            }
                            else
                            {
                                JSONString.Append("},");
                            }

                        }
                        JSONString.Append("]");
                    }

                    result = JSONString.ToString();
                }
                catch
                {
                    throw;
                }
            }

            return result;
        }

        public static async Task<DataTable> GetFAQ(string Token)
        {
            string connectionstring = "";
            string DBName = "CP_DBName";

            connectionstring = await CheckTokenCustomerLogin(Token, DBName);
            DataTable dt = new DataTable();

            try
            {
                dt = await FourQT.DAL.Portal.DAL.GetFAQ(connectionstring, Token);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetFAQ( Connectionstring=" + connectionstring + ")", Token);
                throw;
            }

            return dt;
        }

        public static string JSONWithStringBuilderFAQ(DataTable table)
        {
            var JSONString = new System.Text.StringBuilder();
            try
            {
                var ContentsList = new System.Text.StringBuilder();
                var FAQContentList = new System.Text.StringBuilder();

                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        JSONString.Append("{");
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            string CommonColumn = table.Columns[j].ColumnName.ToString();
                            if (j < table.Columns.Count - 1)
                            {
                                if (table.Columns[j].ColumnName.ToString() == CommonColumn)
                                {
                                    JSONString.Append("\"" + CommonColumn + "\":" + "\"" + table.Rows[i][(j)].ToString().Replace("\r\n\r\n", "").Replace("\r\n", "").Replace("\r\n\t\t\t\t", "").Replace("\t\t\t\t\t\t\t\t\t\t\t\t\t", "").Replace("\t", "").Replace("\"", "") + "\",");
                                }
                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                if (table.Columns[j].ColumnName.ToString() == CommonColumn)
                                {
                                    JSONString.Append("\"" + CommonColumn + "\":" + "\"" + table.Rows[i][(j)].ToString().Replace("\r\n\r\n", "").Replace("\r\n", "").Replace("\r\n\t\t\t\t", "").Replace("\t\t\t\t\t\t\t\t\t\t\t\t\t", "").Replace("\t", "").Replace("\"", "") + "\",");
                                }
                            }
                        }

                        if (i == table.Rows.Count - 1)
                        {
                            JSONString.Append("}");
                        }
                        else
                        {
                            JSONString.Append("},");
                        }

                    }
                    JSONString.Append("]");
                }
            }
            catch {
                throw;
            }

            return JSONString.ToString();
        }

        public static string UploadImage(string base64String, string FilePath)
        {
            try
            {
                string Newbase64String = base64String;

                string ext = Path.GetExtension(FilePath);
                bool isImage = false;

                string[] arrImageExt = PortalAppSettingMethods.GetImagePath().Split(",".ToCharArray());// ConfigurationManager.AppSettings["IMAGEEXT"].Split(",".ToCharArray());
                foreach (string extention in arrImageExt)
                {
                    if (String.Compare(extention, ext, true) == 0)
                    {
                        isImage = true;
                        break;
                    }
                }

                if (isImage)
                {
                    byte[] bytes = Convert.FromBase64String(base64String);

                    System.Drawing.Image image;

                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        image = System.Drawing.Image.FromStream(ms);
                    }

                    using (System.Drawing.Image thumbnail = image.GetThumbnailImage(130, 130, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            thumbnail.Save(memoryStream, ImageFormat.Png);
                            bytes = new Byte[memoryStream.Length];
                            memoryStream.Position = 0;
                            memoryStream.Read(bytes, 0, (int)bytes.Length);
                        }
                    }
                    Newbase64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                }

                File.WriteAllBytes(FilePath, Convert.FromBase64String(Newbase64String));
            }
            catch
            {
                throw;
            }

            return FilePath;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }

        public static async Task<ResponseStatus_New<NoData>> SaveCustomerReferral(CustomerReferral Obj,string dKey="", int customerId = 0)
        {
            string DBName = "CP_DBName";
            ResponseStatus_New<NoData> response = new ResponseStatus_New<NoData>();
            string ConnString = String.Empty,key="";

            if (dKey != "") { key = dKey; }
            else
            {
                if (Obj.Token != null) { key = Convert.ToString(Obj.Token); }
            }

            try
            {
                ConnString = await CheckTokenCustomerLogin(key, DBName);
                response = await FourQT.DAL.Portal.DAL.SaveCustomerReferral(ConnString, Obj, customerId);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.SaveCustomerReferral(Token=" + key + ")", key);
                throw;
            }
            return response;
        }

        public static async Task<ResponseStatus_New<CustomerReferral>> GetCustomerReferralList(CustomerReferral Obj,string dKey="", int customerId=0)
        {
            string DBName = "CP_DBName";
            ResponseStatus_New<CustomerReferral> response = new ResponseStatus_New<CustomerReferral>();
            List<CustomerReferral> lstCustomerReferral = new List<CustomerReferral>();
            string ConnString = String.Empty,key="";
            DataSet dsData = new DataSet();

            try
            {
                if (dKey != "") { key = dKey; }
                else {
                    if (Obj.Token != null) { key = Convert.ToString(Obj.Token); }                   
                }

                ConnString = await CheckTokenCustomerLogin(key, DBName);
                dsData = await FourQT.DAL.Portal.DAL.GetCustomerReferralList(ConnString, Obj,customerId);

                if (dsData != null && dsData.Tables.Count > 0 && dsData.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsData.Tables[0].Rows)
                    {
                        CustomerReferral objCustomerReferral = new CustomerReferral()
                        {
                             Customer_Referral_Id = Convert.ToInt32(dr["Customer_Referral_Id"].ToString())
                            ,Referral_Name = dr["Referral_Name"].ToString()
                            ,Referral_Mobile = dr["Referral_Mobile"].ToString()
                            ,Referral_Mobile2 = dr["Referral_Mobile2"].ToString()
                            ,Referral_Email = dr["Referral_Email"].ToString()
                            ,Referral_Address = dr["Referral_Address"].ToString()
                            ,Referral_Relation_Remarks = dr["Referral_Relation_Remarks"].ToString()
                            ,Customer_Name = dr["Customer_Name"].ToString()
                            ,Project_Name = dr["Project_Name"].ToString()
                            ,Display_Create_Date = dr["Display_Create_Date"].ToString()
                        };

                        lstCustomerReferral.Add(objCustomerReferral);
                    }
                }

                response.Status = System.Net.HttpStatusCode.OK;
                response.IsSuccess = true;
                response.Title = "Success";
                response.Message = "Success";
                response.LstData = lstCustomerReferral;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerReferralList(Token=" + key + ")", key);
                response.Status = System.Net.HttpStatusCode.ExpectationFailed;
                response.IsSuccess = false;
                response.Message = ex.Message;
                response.Title = "Error";
                response.Data = null;
                response.LstData = null;
            }
            return response;
        }

        public static async Task<int> SetJWTokenPortal(string Token, int customerId,string JWTToken)
        {
            string DBName = "CP_DBName";
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                await FourQT.DAL.Portal.DAL.SetJWTTokenPortal(ConnString,Convert.ToString(customerId),JWTToken,Token);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.SetJWTokenPortal(Token=" + Token + ",  CustomerId=" + customerId + ")", Token);
                throw;
            }

            return 0;
        }

        public static async Task<string> GetJWTokenPortal(string Token, int customerId)
        {
            string DBName = "CP_DBName";
            string? ConnString = "",token="";

            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetJWTTokenPortal(ConnString, Convert.ToString(customerId), Token);

                if (ds.Tables[0]!=null && ds.Tables[0].Rows.Count > 0)
                {
                    token = Convert.ToString(ds.Tables[0].Rows[0]["Token"]);
                }
                else
                {
                    token = "";
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetJWTokenPortal(Token=" + Token + ",  CustomerId=" + customerId + ")", Token);
                token = "";
                throw;
            }

            return token;
        }



        public static async Task<List<CustomerDetail>> GetCustomerDetails(string Token, int RegistrationId)
        {
            string DBName = "REMS_DBName";
            List<CustomerDetail> CustomerDetailList = new List<CustomerDetail>();

            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetCustomerDetails(ConnString, Token, RegistrationId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        CustomerDetailList.Add(new CustomerDetail
                        {
                            PrjectName = row["Project_Name"].ToString(),
                            RegistrationId = Convert.ToInt32(row["Registration_Id"]),
                            RegistrationNo = (row["Registration_No"]!=null? row["Registration_No"].ToString():""),
                            Project_Tower_Name = row["Project_Tower_Name"].ToString(),
                            Project_Tower_Floor_Name = row["Project_Tower_Floor_Name"].ToString(),
                            location = row["location"].ToString(),
                            Address = row["Address"].ToString(),
                            Registration_Date = row["Registration_Date"].ToString(),
                            F_First_name = row["F_First_name"].ToString(),
                            ApplicantAddress = (row["ApplicantAddress"] != null ? Utility.RemoveEscapeSeqChars(row["ApplicantAddress"].ToString(), " ") : ""),
                            S_First_name = row["S_First_name"].ToString(),
                            SecondApplicantAddress = (row["SecondApplicantAddress"] != null ? Utility.RemoveEscapeSeqChars(row["SecondApplicantAddress"].ToString(), " ") : ""),
                            S_Pan_No = row["S_Pan_No"].ToString(),
                            F_Pan_No = row["F_Pan_No"].ToString(),
                            Plan_Name = row["Plan_Name"].ToString(),
                            F_Date_of_Birth = row["F_Date_of_Birth"].ToString(),
                            S_Date_of_Birth = row["S_Date_of_Birth"].ToString(),
                            LoanInfo = row["LoanInfo"].ToString(),
                            Bank_Name = row["Bank_Name"].ToString(),
                            F_Passport_No = row["F_Passport_No"].ToString(),
                            S_Passport_No = row["S_Passport_No"].ToString(),
                            F_Nationality = row["F_Nationality"].ToString(),
                            S_Nationality = row["S_Nationality"].ToString(),
                            Project_Unit_Type_Name = row["Project_Unit_Type_Name"].ToString(),
                            Area_Measurement = row["Area_Measurement"].ToString(),
                            Project_Areameasurement = row["Project_Areameasurement"].ToString(),
                            F_MobileNo = row["F_MobileNo"].ToString(),
                            S_MobileNo = row["S_MobileNo"].ToString(),
                            F_LandLineNo = row["F_LandLineNo"].ToString(),
                            S_LandLineNo = row["S_LandLineNo"].ToString(),
                            F_Email_id = row["F_Email_id"].ToString(),
                            F_R_Address = (row["F_R_Address"] != null ? Utility.RemoveEscapeSeqChars(row["F_R_Address"].ToString(), " ") : ""),
                            F_O_Address = (row["F_O_Address"] != null ? Utility.RemoveEscapeSeqChars(row["F_O_Address"].ToString(), " ") : ""),
                            F_H_Address = (row["F_H_Address"] != null ? Utility.RemoveEscapeSeqChars(row["F_H_Address"].ToString(), " ") : ""),
                            MailingAddress = row["MailingAddress"].ToString(),
                            S_R_Address = (row["S_R_Address"] != null ? Utility.RemoveEscapeSeqChars(row["S_R_Address"].ToString(), " ") : ""),
                            S_O_Address = (row["S_O_Address"] != null ? Utility.RemoveEscapeSeqChars(row["S_O_Address"].ToString(), " ") : ""),
                            S_H_Address = (row["S_H_Address"] != null ? Utility.RemoveEscapeSeqChars(row["S_H_Address"].ToString(), " ") : ""),
                            Allotment_Date = row["Allotment_Date"].ToString(),
                            MultiApplicants = row["MultiApplicants"].ToString(),
                            SecAppType = row["SecAppType"].ToString(),
                            F_S_W_D_O = row["F_S_W_D_O"].ToString(),
                            Aadhaar_No = row["Aadhaar_No"].ToString(),
                            F_Profession = row["F_Profession"].ToString(),
                            F_Company_Firm_Name = row["F_Company_Firm_Name"].ToString(),
                            S_S_W_D_O = row["S_S_W_D_O"].ToString(),
                            SecondApplicant_Aadhaar_No = row["SecondApplicant_Aadhaar_No"].ToString(),
                            S_Profession = row["S_Profession"].ToString(),
                            S_Company_Firm_Name = row["S_Company_Firm_Name"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerDetails(Token=" + Token + ",  RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            return CustomerDetailList;
        }

        public static async Task<List<MultipleApplicant>> GetUdp_MultipleApplicant(string Token, int RegistrationId)
        {
            string DBName = "REMS_DBName";
            DataTable dt = await Common.GetdtImageURL(Token);

            string ClintImageURL = "", PortalImageURL = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                ClintImageURL = dt.Rows[0][18].ToString();
                PortalImageURL = dt.Rows[0][20].ToString();
            }

            string Path = ClintImageURL + "/";
            List<MultipleApplicant> MultipleApplicantList = new List<MultipleApplicant>();

            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetUdp_MultipleApplicant(ConnString, Token, RegistrationId);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MultipleApplicantList.Add(new MultipleApplicant
                        {
                            Name = row["NAME"].ToString(),
                            MobileNo = row["MobileNo"].ToString(),
                            Photo = Path + row["PHOTO"].ToString(),
                            Address = (row["ApplicantAddress"] != null ? Utility.RemoveEscapeSeqChars(row["ApplicantAddress"].ToString(), " ") : ""),
                            EmailId = row["EmailId"].ToString(),
                            PanNo = row["PanNo"].ToString(),
                            AdharNo = row["AadhaarNo"].ToString(),
                            DOB = row["DOB"].ToString(),
                            S_W_D_O = row["S_W_D_O"].ToString(),
                            Nationality = row["Nationality"].ToString(),
                            AnniversaryDate = row["AnniversaryDate"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetUdp_MultipleApplicant(Token=" + Token + ",  RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            return MultipleApplicantList;
        }

        public static async Task<List<UnitDetailList>> GetUnitDetails(string Token, int RegistrationId)
        {
            string DBName = "REMS_DBName";
            List<UnitDetailList> UnitDetailsList = new List<UnitDetailList>();

            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);

                DataSet ds = await FourQT.DAL.Portal.DAL.GetUnitDetails(ConnString, Token, RegistrationId);
                DataTable dt = await Common.GetdtImageURL(Token);

                string ClientURL = "", PortalImageURL = "";
                if (dt != null && dt.Rows.Count > 0)
                {
                    ClientURL = dt.Rows[0][18].ToString();
                    PortalImageURL = dt.Rows[0][20].ToString();
                }

                var Path = PortalImageURL + "/";
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        UnitDetailsList.Add(new UnitDetailList
                        {
                            Registration_Id = Convert.ToInt32(row["Registration_Id"]),
                            ProjectPhoto = Path + row["ProjectPhoto"].ToString(),
                            Project_Name = row["Project Name"].ToString(),
                            Tower = row["Tower"].ToString(),
                            Floor = row["Floor"].ToString(),
                            Unit_No = row["Unit No"].ToString(),
                            Plan = row["Plan"].ToString(),
                            Unit_Type = row["Unit Type"].ToString(),

                            Plot_Area = row["Plot_Area"].ToString(),
                            Super_Area = row["Super Area"].ToString(),
                            Build_Up_Area = row["Build_Up_Area"].ToString(),
                            Carpet_Area = row["Carpet Area"].ToString(),
                            Balcony_Area = row["Balcony_Area"].ToString(),

                            Balcony_Area1 = row["Balcony_Area1"].ToString(),
                            Balcony_Area2 = row["Balcony_Area2"].ToString(),
                            Balcony_Area3 = row["Balcony_Area3"].ToString(),

                            IsShow_PlotArea = (bool)row["IsShow_PlotArea"],
                            IsShow_SuperArea = (bool)row["IsShow_SuperArea"],
                            IsShow_BuiltUpArea = (bool)row["IsShow_BuiltUpArea"],
                            IsShow_CarpetArea = (bool)row["IsShow_CarpetArea"],
                            IsShow_BalconyArea = (bool)row["IsShow_BalconyArea"],

                            IsShow_BalconyArea1 = (bool)row["IsShow_BalconyArea1"],
                            IsShow_BalconyArea2 = (bool)row["IsShow_BalconyArea2"],
                            IsShow_BalconyArea3 = (bool)row["IsShow_BalconyArea3"],

                            UOM = row["UOM"].ToString(),
                            LoanInfo = row["LoanInfo"].ToString(),
                            Bank_Name = row["Bank_Name"].ToString(),

                            IsShow_Floor = (bool)row["IsShow_Floor"],
                            TowerLabel = row["TowerLabel"].ToString(),

                            ProjectPhoto_Color = row["ProjectPhoto_Color"].ToString(),
                            Project_Name_Color = row["Project_Name_Color"].ToString(),
                            Tower_Color = row["Tower_Color"].ToString(),
                            Floor_Color = row["Floor_Color"].ToString(),
                            Unit_No_Color = row["Unit_No_Color"].ToString(),
                            Plan_Color = row["Plan_Color"].ToString(),
                            Unit_Type_Color = row["Unit_Type_Color"].ToString(),
                            LoanInfo_Color = row["LoanInfo_Color"].ToString(),
                            Bank_Name_Color = row["Bank_Name_Color"].ToString(),

                            Plot_Area_Color = row["Plot_Area_Color"].ToString(),
                            Super_Area_Color = row["Super_Area_Color"].ToString(),
                            Build_Up_Area_Color = row["Build_Up_Area_Color"].ToString(),
                            Carpet_Area_Color = row["Carpet_Area_Color"].ToString(),
                            Balcony_Area_Color = row["Balcony_Area_Color"].ToString(),

                            Balcony1_Area_Color = row["Balcony1_Area_Color"].ToString(),
                            Balcony2_Area_Color = row["Balcony2_Area_Color"].ToString(),
                            Balcony3_Area_Color = row["Balcony3_Area_Color"].ToString(),

                            Balcony1_Area_Label = row["Balcony1_Area_Label"].ToString(),
                            Balcony2_Area_Label = row["Balcony2_Area_Label"].ToString(),
                            Balcony3_Area_Label = row["Balcony3_Area_Label"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetUnitDetails(Token=" + Token + ",  CustomerId=" + RegistrationId + ")", Token);
                throw;
            }
            return UnitDetailsList;
        }

        public static async Task<DataSet> GetAccountDetails(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetAccountDetails(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetAccountDetails(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<DataSet> Paymentschedule(string Token, int RegistrationId, string Type = "A")
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetOutstandingDetail_Paymentschedule(ConnString, Token, RegistrationId, Type);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetOutstandingDetail_Paymentschedule(Token=" + Token + ")", Token);
                throw;
            }

            return ds;
        }

        public static string DataTableToJSONWithStringBuilderInstallment(DataTable table, DataTable totalos)
        {
            var JSONString = new System.Text.StringBuilder();
            try
            {
                var ContentsList = new System.Text.StringBuilder();               
                var ContentList = new System.Text.StringBuilder();
                int count = 0;
                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");
                    if (count < totalos.Rows.Count)
                    {
                        count = count++;
                    }

                    for (int i = 0; i < table.Rows.Count; i++)
                    {

                        string InstallmentName = table.Columns[1].ColumnName.ToString();
                        string InstallmentDate = table.Columns[2].ColumnName.ToString();
                        string Installmentamount = table.Columns[3].ColumnName.ToString();
                        string ReceiveAmount = table.Columns[4].ColumnName.ToString();
                        string TotalInstallmentOSAmount = table.Columns[5].ColumnName.ToString();
                        string InstallmentColor = table.Columns[6].ColumnName.ToString();
                        string InstallmentStyle = table.Columns[7].ColumnName.ToString();
                        string DueColor = table.Columns[8].ColumnName.ToString();
                        string DueStyle = table.Columns[9].ColumnName.ToString();
                        string InstAmountColor = table.Columns[10].ColumnName.ToString();
                        string InstAmountStyle = table.Columns[11].ColumnName.ToString();
                        string ReceivedColor = table.Columns[12].ColumnName.ToString();
                        string ReceivedStyle = table.Columns[13].ColumnName.ToString();
                        string OutstandingColor = table.Columns[14].ColumnName.ToString();
                        string OutstandingStyle = table.Columns[15].ColumnName.ToString();
                        if (count < table.Rows.Count)
                            JSONString.Append("{");
                        ContentList.Append("[");
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j == 0)
                                JSONString.Append("\"InstallmentName\":" + "\"" + table.Rows[i][(j + 1)].ToString() + "\" ," + "\"InstallmentColor\":" + "\"" + table.Rows[i][6].ToString() + "\"   ," + "\"InstallmentStyle\":" + "\"" + table.Rows[i][7].ToString() + "\",");
                            else
                            {
                                if (j < table.Columns.Count - 1)
                                {

                                    if (table.Columns[j].ColumnName.ToString() == Installmentamount)
                                    {
                                        ContentList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][10].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][11].ToString() + "\"},");
                                    }

                                    else
                                    {

                                        if (table.Columns[j].ColumnName.ToString() == InstallmentDate)
                                        {
                                            ContentList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][8].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][9].ToString() + "\"},");
                                        }
                                        else if (table.Columns[j].ColumnName.ToString() == ReceiveAmount)
                                        {
                                            ContentList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][12].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][13].ToString() + "\"},");
                                        }

                                        else if (table.Columns[j].ColumnName.ToString() == TotalInstallmentOSAmount)
                                        {
                                            ContentList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][14].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][15].ToString() + "\"},");
                                        }
                                    }

                                }
                                else if (j == table.Columns.Count - 1)
                                {

                                    if (table.Columns[j].ColumnName.ToString() == InstallmentName && table.Rows[i][j].ToString() == "")
                                    {
                                        ContentsList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"  ," + "\"TagNameColor\":" + "\"" + table.Rows[i][6].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][7].ToString() + "\"},");
                                    }
                                    if (table.Columns[j].ColumnName.ToString() == InstallmentDate && table.Rows[i][j].ToString() == "")
                                    {
                                        ContentsList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][8].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][9].ToString() + "\"},");
                                    }
                                    if (table.Columns[j].ColumnName.ToString() == Installmentamount)
                                    {
                                        ContentList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"   ," + "\"TagNameColor\":" + "\"" + table.Rows[i][10].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][11].ToString() + "\"},");
                                    }
                                    if (table.Columns[j].ColumnName.ToString() == ReceiveAmount && Convert.ToDecimal(table.Rows[i][j].ToString()) == 0)
                                    {
                                        ContentsList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"  ," + "\"TagNameColor\":" + "\"" + table.Rows[i][12].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][13].ToString() + "\"},");
                                    }
                                    if (table.Columns[j].ColumnName.ToString() == TotalInstallmentOSAmount && Convert.ToDecimal(table.Rows[i][j].ToString()) == 0)
                                    {
                                        ContentsList.Append("{\"tagName\":" + "\"" + table.Columns[j].ColumnName.ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][j].ToString() + "\"  ," + "\"TagNameColor\":" + "\"" + table.Rows[i][14].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][15].ToString() + "\"}");
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        ContentList.ToString().TrimEnd(',');
                        ContentList.Append("]");
                        JSONString.Append("ContentList:" + ContentList);
                        if (i == table.Rows.Count - 1)
                        {
                            JSONString.Append("}");
                        }
                        else
                        {
                            JSONString.Append("},");
                        }
                        ContentList = new System.Text.StringBuilder();
                    }
                    JSONString.Append("]");
                }
            }
            catch {
                throw;
            }

            return JSONString.ToString();
        }

        public static async Task<DataSet> GetReceiptDetails(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetReceiptDetails(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetReceiptDetails(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<List<LetterDetailCustomer>> GetLetterDetailCustomerPortal(string Token, int RegistrationId)
        {
            string DBName = "REMS_DBName";
            List<LetterDetailCustomer> LetterDetailCustomerList = new List<LetterDetailCustomer>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetLetterDetailCustomerPortal(ConnString, Token, RegistrationId);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        LetterDetailCustomerList.Add(new LetterDetailCustomer
                        {
                            Title = (row["Title"] !=null ? Utility.RemoveEscapeSeqChars(row["Title"].ToString()," ") : ""),
                            date = row["date"].ToString(),
                            Letter = row["Letter"].ToString().Replace("\r\n\r\n", "").Replace("\r\n", "").Replace("\r\n\t\t\t\t", "").Replace("\t\t\t\t\t\t\t\t\t\t\t\t\t", "").Replace("\t", "").Replace("\"", ""),
                            LetterLink = row["LetterLink"].ToString(),
                            Type = row["Type"].ToString(),
                            PDFLink = row["PDFLink"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common. GetLetterDetailCustomer(Token=" + Token + ",  RegistrationId=" + RegistrationId + ")", Token);
                throw;
            }
            return LetterDetailCustomerList;
        }

        public static async Task<DataSet> GetContactUS(string Token)
        {
            string DBName = "REMS_DBName";
            string ConnString = "";
            DataSet dsData = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                dsData = await FourQT.DAL.Portal.DAL.GetContactUS(ConnString, Token);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common. GetContactUS(Token=" + Token + ")", Token);
                throw;
            }
            return dsData;
        }

        public static string DataTableToJSONWithStringBuilderReciptDeatil(DataTable table)
        {
            var JSONString = new System.Text.StringBuilder();
            try
            {
                var ContentList = new System.Text.StringBuilder();

                if (table.Rows.Count > 0)
                {
                    string[,] arrColNames = new string[11, 5]
                            {
                            {"Receipt Date", "Receipt Date", "ReceiptDateColor","ReceiptDateStle","ReceiptDate_IconPath"},
                            {"Mode", "Mode", "PaymentModeColor","PaymentModeStle","PaymentMode_IconPath"},
                            {"Bank", "Bank", "BankNameColor","BankNameStle","BankName_IconPath"},
                            {"Instrument No", "Instrument No", "InstrumentColor","InstrumentStle","InstrumentNo_IconPath"},
                            {"Status", "Status", "PaymentStatusColor","PaymentStatusStle","PaymentStatus_IconPath"},

                            {"Receipt Amount", "Receipt Amount", "ReceiptAmountColor","ReceiptAmountStle","ReceiptAmount_IconPath"},
                            {"Amount", "Amount", "AmountColor","AmountStle","Amount_IconPath"},
                            {"GST", "GST", "TaxtAmtColor","TaxtStle","TaxAmt_IconPath"},
                            {"GST Rebate", "GST Rebate", "ITCAmountColor","ITCAmountStle","ITCAmount_IconPath"},

                            {"Interest", "InterestAmt", "IntrestAmtColor","IntrestAmtStle","InterestAmt_IconPath"},
                            {"Extra Charge", "ExtChargeAmt", "ExtChargeAmtColor","ExtChargetStle","ExtChargeAmt_IconPath"}
                            };

                    JSONString.Append("[");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        JSONString.Append("{");
                        ContentList.Append("[");

                        JSONString.Append("\"title\":" + "\"" + "ReceiptNo" + "\"," + "\"receiptNumber\":" + "\"" + table.Rows[i]["ReceiptNo"].ToString() + "\"  ," + "\"tagNameColor\":" + "\"" + table.Rows[i]["ReceiptNoColor"].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i]["ReceiptStle"].ToString() + "\"," + "\"ReceiptLink\":" + "\"" + table.Rows[i]["ReceiptLink"].ToString() + "\",");

                        for (int iCol = 0; iCol < 11; iCol++)
                        {
                            if (iCol <= 5 || table.Rows[i][arrColNames[iCol, 1]].ToString() != "0.00")
                            {
                                ContentList.Append("{\"tagName\":" + "\"" + arrColNames[iCol, 0] + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][arrColNames[iCol, 1]].ToString() + "\"  ," + "\"tagNameColor\":" + "\"" + table.Rows[i][arrColNames[iCol, 2]].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][arrColNames[iCol, 3]].ToString() + "\" ," + "\"tagIconPath\":" + "\"" + table.Rows[i][arrColNames[iCol, 4]].ToString() + "\"},");
                            }
                        }

                        ContentList.ToString().TrimEnd(',');
                        ContentList.Append("]");
                        JSONString.Append("ContentList:" + ContentList);
                        if (i == table.Rows.Count - 1)
                        {
                            JSONString.Append("}");
                        }
                        else
                        {
                            JSONString.Append("},");
                        }
                        ContentList = new System.Text.StringBuilder();
                    }
                    JSONString.Append("]");
                }
            }
            catch {
                throw;
            }
            return JSONString.ToString();
        }

        public static async Task<ViewProjectDocuments> GetProjectDocument(string portalKey,int regId)
        {
            string DBName = "REMS_DBName";
            ViewProjectDocuments projectDocumentList = new ViewProjectDocuments();

            string ConnString = "";
            string Doctype = "";

            try
            {
                ConnString = await CheckTokenCustomerLogin(portalKey, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetProjectDocumentLink(ConnString, regId,portalKey);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[1].Rows)
                    {
                        Doctype = r["Doc_Type"].ToString();


                        DataTable documentdt = ds.Tables[0];

                        EnumerableRowCollection<DataRow> query = from document in documentdt.AsEnumerable()
                                                                 where document.Field<string>("Doc_Type") == Doctype
                                                                 select document;
                        DataView view = query.AsDataView();
                        DataSet ds1 = new DataSet();
                        ds1.Tables.Add(view.ToTable());

                        List<CommonDocumentList> objcommon = new List<CommonDocumentList>();
                        foreach (DataRow row in ds1.Tables[0].Rows)
                        {
                            objcommon.Add(new CommonDocumentList
                            {
                                ProjectId = Convert.ToInt32(row["Project_Id"].ToString()),
                                DocumentId = Convert.ToInt32(row["Doc_Id"].ToString()),
                                DocumentName = row["Document_name"].ToString(),
                                FileName = row["FileName"].ToString(),
                                FileLink = row["FileLink"].ToString(),
                                FileType = row["FileType"].ToString(),
                                FullFilePath = row["FullFilePath"].ToString(),
                                PortalType = row["PortalType"].ToString()
                            });
                        }
                        if (Doctype.ToUpper() == "BROCHURE")
                        {
                            projectDocumentList.BrochureList = objcommon;
                        }
                        else if (Doctype.ToUpper() == "LEGAL")
                        {
                            projectDocumentList.LegalList = objcommon;
                        }
                        else
                        {
                            projectDocumentList.PermissionList = objcommon;
                        }
                    }

                    DataRow drLabels = ds.Tables[2].Rows[0];

                    List<Tabs> lstTabs = new List<Tabs>();

                    lstTabs.Add(new Tabs() { TabName = "Brochure", TabLabel = drLabels["BrochureLabel"].ToString() });
                    lstTabs.Add(new Tabs() { TabName = "Legal", TabLabel = drLabels["LegalLabel"].ToString() });
                    lstTabs.Add(new Tabs() { TabName = "Permission", TabLabel = drLabels["PermissionLabel"].ToString() });

                    projectDocumentList.TabsList = lstTabs;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetConfiguration(Token=" + portalKey + ",  Reg_No=" + regId + ")", portalKey);
                throw;
            }
            return projectDocumentList;
        }

        public static async Task<List<ViewCustomerDocument>> GetCustomerDocument(string portalKey, CustomerDoc objDocument)
        {
            string DBName = "REMS_DBName";
            List<ViewCustomerDocument> customerDocumentList = new List<ViewCustomerDocument>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(portalKey, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetCustomerDocumentLink(portalKey,ConnString, objDocument);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        customerDocumentList.Add(new ViewCustomerDocument
                        {

                            DocumentId = Convert.ToInt32(row["Doc_Id"].ToString()),
                            DocumentName = row["Document_name"].ToString(),
                            PersonalDocumentId = Convert.ToInt32(row["Personal_doc_Id"].ToString()),
                            FileName = row["Image"].ToString(),
                            Type = row["Type"].ToString(),
                            FullFilePath = row["DocFullPath"].ToString()
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetConfiguration(Token=" + portalKey + ",  Reg_No=" + objDocument.CustomerId + ")", portalKey);
                throw;
            }
            return customerDocumentList;
        }

        public static async Task<int> CheckHRAttendanceLogin(string Token)
        {
            int retVal = 0;
            try
            {
                retVal = await FourQT.DAL.Portal.DAL.CheckHRAttendanceToken(Token, "S");
                return retVal;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.CheckLogin(Token=" + Token + ")", Token);
                return 2;
            }
        }

        public static async Task<DataSet> InsertAttendanceAndGetDetails(string Token, string XMLDetail)
        {
            string DBName = "HR_DBName";
            string ConnString = "";
            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                ds = await FourQT.DAL.Portal.DAL.InsertAttendanceAndGetDetails(ConnString, Token, XMLDetail);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerLogin(Token=" + Token + ",  XMLDetail=" + XMLDetail + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<int> SetJWTokenPortalLogout(string Token, int customerId)
        {
            string DBName = "CP_DBName";
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                await FourQT.DAL.Portal.DAL.SetJWTTokenPortalLogout(ConnString, Convert.ToString(customerId), Token);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.SetJWTokenPortalLogout(Token=" + Token + ",  CustomerId=" + customerId + ")", Token);
                throw;
            }

            return 0;
        }

        public static async Task<List<CustomerDuesPaid>> GetCustomerDuesPaid(string Token, int Registration_Id)
        {
            string DBName = "REMS_DBName";
            List<CustomerDuesPaid> lstCustomerDuesPaid = new List<CustomerDuesPaid>();
            string ConnString = "";
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                DataSet ds = await FourQT.DAL.Portal.DAL.GetCustomerDuesPaid(ConnString, Token, Registration_Id);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        decimal totalDuePer = 0, totalRecPer = 0, totalOSPer = 0, totalUnduePer = 0;
                        Decimal.TryParse(row["TotalDue_Per"].ToString(), out totalDuePer);
                        Decimal.TryParse(row["TotalReceived_Per"].ToString(), out totalRecPer);
                        Decimal.TryParse(row["TotalOutstanding_Per"].ToString(), out totalOSPer);
                        Decimal.TryParse(row["TotalUndue_Per"].ToString(), out totalUnduePer);

                        lstCustomerDuesPaid.Add(new CustomerDuesPaid
                        {
                            Registration_Id = Convert.ToInt32(row["Registration_Id"]),
                            TotalCost = Convert.ToString(row["TotalCost"].ToString()),
                            TotalDue = Convert.ToString(row["TotalDue"].ToString()),
                            TotalReceived = Convert.ToString(row["TotalReceived"]),
                            TotalOutstanding = Convert.ToString(row["TotalOutstanding"]),
                            TotalUndue = Convert.ToString(row["TotalUndue"]),

                            TotalDue_Per = totalDuePer,
                            TotalReceived_Per = totalRecPer,
                            TotalOutstanding_Per = totalOSPer,
                            TotalUndue_Per = totalUnduePer,

                            TotalCost_Color = Convert.ToString(row["TotalCost_Color"].ToString()),
                            TotalDue_Color = Convert.ToString(row["TotalDue_Color"].ToString()),
                            TotalReceived_Color = Convert.ToString(row["TotalReceived_Color"]),
                            TotalOutstanding_Color = Convert.ToString(row["TotalOutstanding_Color"]),
                            TotalUndue_Color = Convert.ToString(row["TotalUndue_Color"]),
                            TotalInterest = Convert.ToString(row["TotalInterest"]),

                            showInterest = (Boolean.TryParse(row["ShowInterest"].ToString(), out Boolean showInt) ? showInt : true),

                            showAddon = (Boolean.TryParse(row["ShowAddon"].ToString(), out showInt) ? showInt : true),
                            addonDueAmt = Convert.ToString(row["AddonDueAmt"]),
                            addonDueLabel = Convert.ToString(row["AddonLabel"]),
                            totalDueWithAddon = Convert.ToString(row["TotalDueWithAddon"]),
                            totalDueWithAddonLabel = Convert.ToString(row["DueWithAddonLabel"])
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetCustomerDuesPaid(Token=" + Token + ",  Registration_Id=" + Registration_Id + ")", Token);
                throw;
            }
            return lstCustomerDuesPaid;
        }

        public static async Task<ResponseStatus_New<NoData>> SaveCustomerReferral_P2(CustomerReferralRequest Obj, string dKey, int customerId = 0)
        {
            string DBName = "REMS_DBName";
            ResponseStatus_New<NoData> response = new ResponseStatus_New<NoData>();
            string ConnString = String.Empty, key = "";

            if (dKey!=null &&  dKey != "") { key = dKey; }           

            try
            {
                ConnString = await CheckTokenCustomerLogin(key, DBName);
                response = await FourQT.DAL.Portal.DAL.SaveCustomerReferral_P2(ConnString, Obj, customerId);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.SaveCustomerReferral(Token=" + key + ")", key);
                throw;
            }
            return response;
        }

        public static async Task<List<CustomerReferralDetails>> getCustomerReferralReport(CustomerCore1 Obj, string dKey, int customerId = 0)
        {
            string DBName = "REMS_DBName";
            string ConnString = String.Empty, key = "";
            DataSet ds = new DataSet();
            List<CustomerReferralDetails> refList = new List<CustomerReferralDetails>();

            if (dKey != null && dKey != "") { key = dKey; }

            try
            {
                ConnString = await CheckTokenCustomerLogin(key, DBName);
                ds = await FourQT.DAL.Portal.DAL.getCustomerReferralReport(ConnString, Obj, customerId);

                if(ds!=null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0]!=null && ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        for(int i = 0; i < dt.Rows.Count; i++)
                        {
                            CustomerReferralDetails refferal = new CustomerReferralDetails();
                            refferal.refId = (Int32.TryParse(dt.Rows[i]["Customer_Referral_Id"].ToString(), out int id) ? id : 0);
                            refferal.refFullName = (dt.Rows[i]["Referral_Name"] != null ? dt.Rows[i]["Referral_Name"].ToString() : "");
                            refferal.refContactNo= (dt.Rows[i]["Referral_Mobile"] != null ? dt.Rows[i]["Referral_Mobile"].ToString() : "");
                            refferal.refContactNo2 = (dt.Rows[i]["Referral_Mobile2"] != null ? dt.Rows[i]["Referral_Mobile2"].ToString() : "");
                            refferal.refMailId = (dt.Rows[i]["Referral_Email"] != null ? dt.Rows[i]["Referral_Email"].ToString() : "");
                            refferal.refAddress = (dt.Rows[i]["Referral_Address"] != null ? dt.Rows[i]["Referral_Address"].ToString() : "");
                            refferal.refRelation = (dt.Rows[i]["Referral_Relation_Remarks"] != null ? dt.Rows[i]["Referral_Relation_Remarks"].ToString() : "");
                            refferal.location = (dt.Rows[i]["Referral_Location"] != null ? dt.Rows[i]["Referral_Location"].ToString() : "");
                            refferal.projectName = (dt.Rows[i]["Project_Name"] != null ? dt.Rows[i]["Project_Name"].ToString() : "");
                            refferal.refDate = (dt.Rows[i]["Display_Create_Date"] != null ? dt.Rows[i]["Display_Create_Date"].ToString() : "");

                            refferal.status = (dt.Rows[i]["Status"] != null ? dt.Rows[i]["Status"].ToString() : "");
                            refferal.dumpReason = (dt.Rows[i]["DumpReason"] != null ? dt.Rows[i]["DumpReason"].ToString() : "");
                            refferal.dumpDate = (dt.Rows[i]["DumpDate"] != null ? dt.Rows[i]["DumpDate"].ToString() : "");
                            refferal.successCustomerName = (dt.Rows[i]["SuccessCustomerName"] != null ? dt.Rows[i]["SuccessCustomerName"].ToString() : "");
                            refferal.successProject = (dt.Rows[i]["SuccessProject"] != null ? dt.Rows[i]["SuccessProject"].ToString() : "");
                            refferal.successUnitNo =  (dt.Rows[i]["SuccessUnitNo"] != null ? dt.Rows[i]["SuccessUnitNo"].ToString() : "");
                            refferal.successArea = (dt.Rows[i]["SuccessArea"] != null ? dt.Rows[i]["SuccessArea"].ToString() : "");
                            refferal.successBookingDate = (dt.Rows[i]["SuccessBookingDate"] != null ? dt.Rows[i]["SuccessBookingDate"].ToString() : "");
                            refferal.allocatedAmount = (dt.Rows[i]["AllocatedAmount"] != null ? dt.Rows[i]["AllocatedAmount"].ToString() : "");
                            refferal.allocatedDate = (dt.Rows[i]["AllocatedDate"] != null ? dt.Rows[i]["AllocatedDate"].ToString() : "");
                            refferal.paidAmount = (dt.Rows[i]["PaidAmount"] != null ? dt.Rows[i]["PaidAmount"].ToString() : "");
                            refferal.paidReceiptNo = (dt.Rows[i]["PaidReceiptNo"] != null ? dt.Rows[i]["PaidReceiptNo"].ToString() : "");
                            refferal.paidProject = (dt.Rows[i]["PaidProject"] != null ? dt.Rows[i]["PaidProject"].ToString() : "");
                            refferal.paidUnitNo = (dt.Rows[i]["PaidUnitNo"] != null ? dt.Rows[i]["PaidUnitNo"].ToString() : "");
                            refferal.paidDate = (dt.Rows[i]["PaidDate"] != null ? dt.Rows[i]["PaidDate"].ToString() : "");
                            refList.Add(refferal);
                        }
                        
                    }
                }

                return refList;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.SaveCustomerReferral(Token=" + key + ")", key);
                throw;
            }
        }

        public static async Task<DataSet> GetAccountDetails_New(string Token, int RegistrationId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetAccountDetails_New(ConnString, Token, RegistrationId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetAccountDetails_New(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<DataSet> GetPortalExtraInfo(string Token, int customerId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetPortalExtraInfo(ConnString, Token, customerId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetPortalExtraInfo(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<DataSet> GetRaisedQueryDetails(QueryDetailsRequest objRequest, string Token, int customerId)
        {
            string ConnString = "";
            string DBName = "CP_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetRaisedQueryDetails(objRequest, ConnString, Token, customerId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetRaisedQueryDetails(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }

        public static async Task<DataSet> GetProjectDocumentsByType(CustomerCore5 objRequest, string Token, int customerId)
        {
            string ConnString = "";
            string DBName = "REMS_DBName";

            DataSet ds = new DataSet();
            try
            {
                ConnString = await CheckTokenCustomerLogin(Token, DBName);
                if (!object.Equals(ConnString, null) == true)
                {
                    ds = await FourQT.DAL.Portal.DAL.GetProjectDocumentsByType(objRequest, ConnString, Token, customerId);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Coustomer.GetProjectDocumentsByType(Token=" + Token + ")", Token);
                throw;
            }
            return ds;
        }
    }
}
