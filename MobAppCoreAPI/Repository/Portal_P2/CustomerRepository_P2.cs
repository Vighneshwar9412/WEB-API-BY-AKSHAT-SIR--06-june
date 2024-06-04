using FourQT.Entities.Portal.Referrals;
using FourQT.Entities.Portal;
using System.Data;
using MobAppCoreAPI.Interfaces.Portal_P2;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;
using FourQT.CommonFunctions;
using FourQT.Entities;
using Nancy;
using Newtonsoft.Json.Linq;
using FourQT.Core.General;
using FourQT.Entities.Employee;
using FourQT.Entities.General;
using System.Net;
using Nancy.Diagnostics;

namespace MobAppCoreAPI.Repository.Portal
{
    public class CustomerRepository_P2 : IPortalCustomer_P2
    {
        public async Task<ResponseStatus_New<HomePagesNew>> getPortal_HomePages(CustomerCore1 objuser, HttpRequest request, HttpContext context)
        {
            HomePagesNew objEnquiryMasters;
            ResponseStatus_New<HomePagesNew> objStatus = new ResponseStatus_New<HomePagesNew>();

            string portalKey = "NoToken";
            try
            {
                string message = JsonConvert.SerializeObject(objuser);
                Log.LogPayloadDateWise(message, "Customer_getPortal_HomePages", context);

                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objEnquiryMasters = new HomePagesNew();
                DataSet dsHomePage = await Common.GetPortal_HomePages(portalKey, customerId);
                DataTable dt = await Common.GetdtImageURL(portalKey);

                string? ClintImageURL = "", PortalImageURL = "";
                if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count >= 20)
                {
                    ClintImageURL = (dt.Rows[0][18] != null ? dt.Rows[0][18].ToString() : "");
                    PortalImageURL = (dt.Rows[0][20] != null ? dt.Rows[0][20].ToString() : "");
                }
            
                if (objuser.CustomerId >= 0)
                {
                    objEnquiryMasters.DisplayList = getHomePagesList1(dsHomePage.Tables[0], portalKey, (PortalImageURL!=null? PortalImageURL:""));
                    objEnquiryMasters.UnitList = getHomePagesList2(dsHomePage.Tables[1], portalKey);
                    objEnquiryMasters.IconList = getHomePagesList3(dsHomePage.Tables[2], portalKey, (PortalImageURL != null ? PortalImageURL : ""));

                    List<CustomerMessage> lstMessage = new List<CustomerMessage>();
                    foreach (DataRow dr in dsHomePage.Tables[3].Rows)
                    {
                        lstMessage.Add(new CustomerMessage()
                        {
                            M_Id = Convert.ToInt32(dr["M_Id"].ToString()),
                            Message = (dr["Message"]!=null?dr["Message"].ToString():"")
                        });
                    }

                    objEnquiryMasters.MessageList = lstMessage;

                    List<CustomerNameWithHeader> lstCName = new List<CustomerNameWithHeader>();
                    foreach (DataRow dr in dsHomePage.Tables[4].Rows)
                    {
                        lstCName.Add(new CustomerNameWithHeader()
                        {
                            CustomerName = (dr["CustomerName"] != null ? dr["CustomerName"].ToString() : "")
                        });
                    }
                    objEnquiryMasters.CustomerNameList = lstCName;
                    objStatus.Data = objEnquiryMasters;
                }
                if (dsHomePage.Tables[0].Rows.Count > 0 || dsHomePage.Tables[1].Rows.Count > 0 || dsHomePage.Tables[2].Rows.Count > 0)
                {
                    objStatus.Status = System.Net.HttpStatusCode.OK;
                    objStatus.IsSuccess = true;
                    objStatus.Title = "Success";
                    objStatus.LstData = null;
                    objStatus.Message = MessageClass.sRecordFound;
                }
                else
                {
                    objStatus.Status =  System.Net.HttpStatusCode.NotFound;
                    objStatus.IsSuccess = true;
                    objStatus.Title = "Success";
                    objStatus.LstData = null;
                    objStatus.Message = MessageClass.sRecordNotFound;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " Customer GetPortal_HomePages( Token=" + portalKey + ",CustomerId=" + objuser.CustomerId + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
                objStatus.Data = null;
                objStatus.LstData = null;
                return objStatus;
            }
            return objStatus;
        }
     
        public async Task<ResponseStatus_New<CustomerLoginListNew>> getCustomerUnitNumber(CustomerCore1 objuser, HttpRequest request, HttpContext context)
        {
            ResponseStatus_New<CustomerLoginListNew> CustomerUnit = new ResponseStatus_New<CustomerLoginListNew>();

            string portalKey = "NoToken";
            try
            {
                string message = JsonConvert.SerializeObject(objuser);
                Log.LogPayloadDateWise(message, "Customer_getCustomerUnitNumber", context);

                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<CustomerLoginListNew> CList = new List<CustomerLoginListNew>();
                CList = await Common.GetCustomerUnitNumber(portalKey, customerId);

                if (CList.Count() > 0)
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.OK;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.Message = MessageClass.sRecordFound;
                    CustomerUnit.LstData = CList;
                }
                else
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.LstData = null;
                    CustomerUnit.Message = MessageClass.sRecordNotFound;
                    CustomerUnit.LstData = CList;
                }

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetCustomerUnitNumber( Token=" + portalKey + ",  CustomerId=" + objuser.CustomerId + ")", portalKey);
                CustomerUnit.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerUnit.IsSuccess = false;
                CustomerUnit.Message = ex.Message;
                CustomerUnit.Title = "Error";
                CustomerUnit.Data = null;
                CustomerUnit.LstData = null;
            }
            return CustomerUnit;
        }

        public async Task<ResponseStatus_New<CustomerReferral>> getCustomerReferralList(CustomerReferral Obj, HttpRequest request)
        {
            ResponseStatus_New<CustomerReferral> objResponse = new ResponseStatus_New<CustomerReferral>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objResponse = await Common.GetCustomerReferralList(Obj, portalKey, customerId);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetCustomerReferralList(Token=" + portalKey + ",  CustomerId=" + Obj.CustomerId + ")", portalKey);
                objResponse.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objResponse.IsSuccess = false;
                objResponse.Message = ex.Message;
                objResponse.Title = "Error";
                objResponse.Data = null;
                objResponse.LstData = null;
            }
            return objResponse;
        }

        public async Task<ResponseStatus_New<Getquery>> getudp_getquery(HttpRequest request)
        {
            ResponseStatus_New<Getquery> getquery = new ResponseStatus_New<Getquery>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<Getquery> getqueryList = new List<Getquery>();
                getqueryList = await Common.Getudp_getquery(portalKey);

                if (getqueryList.Count() > 0)
                {
                    getquery.Status = System.Net.HttpStatusCode.OK;
                    getquery.IsSuccess = true;
                    getquery.Title = "Success";
                    getquery.Message = MessageClass.sRecordFound;
                    getquery.LstData = getqueryList;
                }
                else
                {
                    getquery.Status = System.Net.HttpStatusCode.NotFound;
                    getquery.IsSuccess = true;
                    getquery.Title = "Success";
                    getquery.Message = MessageClass.sRecordFound;
                    getquery.LstData = getqueryList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer Getudp_getquery( Token=" + portalKey + ")", portalKey);
                getquery.Status = System.Net.HttpStatusCode.ExpectationFailed;
                getquery.IsSuccess = false;
                getquery.Message = ex.Message;
                getquery.Title = "Error";
                getquery.Data = null;
                getquery.LstData = null;
            }
            return getquery;
        }

        public async Task<ResponseStatus_New<InsertqueryStatus>> getudp_InsertqueryHistory(InsertqueryHistoryNew ObjInsertqueryHistory, HttpRequest request, HttpContext context)
        {

            ResponseStatus_New<InsertqueryStatus> InsertqueryHistory = new ResponseStatus_New<InsertqueryStatus>();
            ServerResponse serverResponse = new ServerResponse();

            string portalKey = "NoToken";
            try
            {
                string message = JsonConvert.SerializeObject(ObjInsertqueryHistory);
                Log.LogPayloadDateWise(message, "Customer_getudp_InsertqueryHistory", context);

                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                Boolean uploadDocs = false;

                if (ObjInsertqueryHistory != null && ObjInsertqueryHistory.FileContent != null && ObjInsertqueryHistory.FileName != null) {
                    if (ObjInsertqueryHistory.FileContent.Trim() != "" && ObjInsertqueryHistory.FileName.Trim() != "")
                    {
                        uploadDocs = true;
                    }
                }

                List<InsertqueryStatus> InsertqueryHistoryList = new List<InsertqueryStatus>();  
                InsertqueryHistory temp = new InsertqueryHistory();

                temp.createdby = ObjInsertqueryHistory.createdby;
                temp.mailstatus = (ObjInsertqueryHistory.mailstatus != null ? ObjInsertqueryHistory.mailstatus : "");
                temp.queryid = ObjInsertqueryHistory.queryid;
                temp.regid = ObjInsertqueryHistory.regid;
                temp.text = (ObjInsertqueryHistory.text != null ? ObjInsertqueryHistory.text : "");
                temp.Token = "";
                temp.FileName = "";
                temp.RootDomain = "";

                if (uploadDocs) {
                    serverResponse = await uploadQueryDocumentsToServer(ObjInsertqueryHistory, request);

                    if (serverResponse != null && serverResponse.isSuccess)
                    {
                        FileUploadResponse? upFiles = serverResponse.uploadedFiles;
                        if (upFiles != null && upFiles.files != null && upFiles.files.Count > 0)
                        {
                            string? docServerName = upFiles.files[0].fileNameOnServer;
                            string? docServerPath = upFiles.files[0].filePathOnServer;

                            if (docServerPath != null && docServerPath.ToString().Trim() != "")
                            {
                                docServerPath = docServerPath.ToString().Trim();
                                int index = docServerPath.LastIndexOf('/');
                                if (index >= 0)
                                {
                                    docServerPath = docServerPath.Substring(0, index + 1);
                                }
                            }

                            if (docServerName != null && docServerName.ToString().Trim() != "" && docServerPath != null && docServerPath.ToString().Trim() != "")
                            {                                                              
                                temp.FileName = docServerName;                               
                                temp.RootDomain = docServerPath;
                            }
                            else
                            {
                                InsertqueryHistory.Status = System.Net.HttpStatusCode.ExpectationFailed;
                                InsertqueryHistory.IsSuccess = false;
                                InsertqueryHistory.Message = "Error in uploading documents.";
                                InsertqueryHistory.Title = "Error";
                                InsertqueryHistory.Data = null;
                                InsertqueryHistory.LstData = null;
                                return InsertqueryHistory;
                            }
                        }
                        else
                        {
                            InsertqueryHistory.Status = System.Net.HttpStatusCode.ExpectationFailed;
                            InsertqueryHistory.IsSuccess = false;
                            InsertqueryHistory.Message = "Error in uploading documents.";
                            InsertqueryHistory.Title = "Error";
                            InsertqueryHistory.Data = null;
                            InsertqueryHistory.LstData = null;
                            return InsertqueryHistory;
                        }
                    }
                    else
                    {
                        InsertqueryHistory.Status = System.Net.HttpStatusCode.ExpectationFailed;
                        InsertqueryHistory.IsSuccess = false;
                        InsertqueryHistory.Message = "Error in uploading documents.";
                        InsertqueryHistory.Title = "Error";
                        InsertqueryHistory.Data = null;
                        InsertqueryHistory.LstData = null;
                        return InsertqueryHistory;
                    }
                }

                InsertqueryHistoryList = await Common.InsertqueryHistory(temp, portalKey, customerId);

                if (InsertqueryHistoryList.Count() > 0)
                {
                    InsertqueryHistory.Status = System.Net.HttpStatusCode.OK;
                    InsertqueryHistory.IsSuccess = true;
                    InsertqueryHistory.Title = "Success";
                    InsertqueryHistory.Message = MessageClass.sInsertqueryHistoryS;
                    InsertqueryHistory.LstData = InsertqueryHistoryList;
                }
                else
                {
                    InsertqueryHistory.Status = System.Net.HttpStatusCode.OK;
                    InsertqueryHistory.IsSuccess = true;
                    InsertqueryHistory.Title = "Success";
                    InsertqueryHistory.Message = MessageClass.sInsertqueryHistoryN;
                    InsertqueryHistory.LstData = InsertqueryHistoryList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer InsertqueryHistory( Token=" + portalKey + ",Queryid=" + ObjInsertqueryHistory.queryid + ",Text=" + ObjInsertqueryHistory.text + ",Createdby=" + ObjInsertqueryHistory.createdby + ",Mailstatus=" + ObjInsertqueryHistory.mailstatus + ",Regid=" + ObjInsertqueryHistory.regid + ")", portalKey);
                InsertqueryHistory.Status = System.Net.HttpStatusCode.ExpectationFailed;
                InsertqueryHistory.IsSuccess = false;
                InsertqueryHistory.Message = ex.Message;
                InsertqueryHistory.Title = "Error";
                InsertqueryHistory.Data = null;
                InsertqueryHistory.LstData = null;
            }
            return InsertqueryHistory;
        }

        public async Task<dynamic> uploadQueryDocumentsToServer(InsertqueryHistoryNew model, HttpRequest req)
        {
            FileUploadRequest fileUploadRequest = new FileUploadRequest();
            FileUploadResponse uploadedDocList = new FileUploadResponse();
            ServerResponse response = new ServerResponse();
            int noOfFilesUploaded = 0;
            string errorMsg = "";
            Boolean uploadSuccess = false;

            try
            {
                if (model != null)
                {
                    string? fileContent = model.FileContent;
                    string? fileName = model.FileName;
                    string? fileFormat = "";
                    if(fileName!=null && fileName.ToString().Trim() != "")
                    {
                        string[] fileNameArr = fileName.ToString().Trim().Split('.');
                        if (fileNameArr.Length >= 2) { fileFormat = fileNameArr[fileNameArr.Length - 1]; }
                    }

                    List<FileUpload> fileList = new List<FileUpload>();

                    if (fileContent != null && fileContent.ToString().Trim() != "")
                    {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = fileContent;
                        file.action = "I";
                        file.fileFormat = fileFormat;
                        file.fileGroup = "C_Q_ATT";
                        file.fileName = "";
                        file.id = 1;

                        fileList.Add(file);
                    }
                    

                    if (fileList != null && fileList.Count > 0)
                    {
                        fileUploadRequest.files = fileList;

                        uploadedDocList = await UploadFilesToExternalServerBLL.SendFilesToExternalServer(fileUploadRequest, req,"C");
                        if (uploadedDocList != null && uploadedDocList.files != null && uploadedDocList.files.Count > 0)
                        {
                            for (int i = 0; i < uploadedDocList.files.Count; i++)
                            {
                                UploadedFile upFile = uploadedDocList.files[i];

                                if (upFile.fileUploaded)
                                {
                                    noOfFilesUploaded++;
                                }
                            }
                        }
                    }
                }

                if (noOfFilesUploaded >= 1)
                {
                    errorMsg = "Success";
                    uploadSuccess = true;
                }
                else 
                {
                    errorMsg = "Error uploading document.";
                    uploadSuccess = false;
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
                uploadSuccess = false;
            }

            response.isSuccess = uploadSuccess;
            response.message = errorMsg;
            response.uploadedFiles = uploadedDocList;
            return response;
        }

        public async Task<ResponseStatus_New<FAQList>> getFAQ(HttpRequest request)
        {
            ResponseStatus_New<FAQList> CustomerUnit = new ResponseStatus_New<FAQList>();
            string portalKey = "NoToken";

            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<FAQList>? CList = new List<FAQList>();
                DataTable dtFaq = await Common.GetFAQ(portalKey);
                var Faq = Common.JSONWithStringBuilderFAQ(dtFaq);

                if (Faq != null)
                {
                    CList = JsonConvert.DeserializeObject<List<FAQList>>(Faq);
                }

                if (CList!=null && CList.Count() > 0)
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.OK;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.Message = MessageClass.sRecordFound;
                    CustomerUnit.LstData = CList;
                }
                else
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.Message = MessageClass.sRecordNotFound;
                    CustomerUnit.LstData = CList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetFAQ( Token=" + portalKey + ")", portalKey);
                CustomerUnit.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerUnit.IsSuccess = false;
                CustomerUnit.Message = ex.Message;
                CustomerUnit.Title = "Error";
                CustomerUnit.Data = null;
                CustomerUnit.LstData = null;
            }
            return CustomerUnit;
        }

        public async Task<ResponseStatus_New<NoData>> saveCustomerReferral(CustomerReferral Obj, HttpRequest request)
        {
            ResponseStatus_New<NoData> objResponse = new ResponseStatus_New<NoData>();
          
            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objResponse = await Common.SaveCustomerReferral(Obj,portalKey,customerId);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "SaveCustomerReferral( Token=" + portalKey + ",  CustomerId=" + Obj.CustomerId + ")", portalKey);
                objResponse.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objResponse.IsSuccess = false;
                objResponse.Message = ex.Message;
                objResponse.Title = "Error";
                objResponse.Data = null;
                objResponse.LstData = null;
            }
            return objResponse;
        }

        public async Task<ResponseStatus_New<ConstructionUpdate>> getConstruction(CustomerCore2 objuser, HttpRequest request)
        {
            ConstructionUpdate objConstruction;
            ResponseStatus_New<ConstructionUpdate> objStatus = new ResponseStatus_New<ConstructionUpdate>();

            string portalKey = "NoToken";

            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objConstruction = new ConstructionUpdate();
                DataSet dsProject = await Common.GetConstructionProject(portalKey, objuser.RegistrationId);
                DataSet dsTower = await Common.GetConstructionTower(portalKey, objuser.RegistrationId);

                if (dsProject.Tables[0].Rows.Count > 0 || dsTower.Tables[0].Rows.Count > 0)
                {
                    if (objuser.RegistrationId >= 0)
                    {
                        objConstruction.ProjctList = await Common.GetProjectList(dsProject.Tables[0], portalKey);
                        objConstruction.TowerList = await Common.GetTowerList(dsTower.Tables[0], portalKey);

                        objStatus.Data = objConstruction;
                    }

                    objStatus.Status = System.Net.HttpStatusCode.OK;
                    objStatus.IsSuccess = true;
                    objStatus.Title = "Success";
                    objStatus.Message = MessageClass.sRecordFound;
                    objStatus.LstData = null;
                }
                else
                {
                    objStatus.Status = System.Net.HttpStatusCode.NotFound;
                    objStatus.IsSuccess = true;
                    objStatus.Title = "Success";
                    objStatus.Message = MessageClass.sRecordNotFound;
                    objStatus.LstData = null;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " Customer GetConstruction( Token=" + portalKey + ",RegistrationId=" + objuser.RegistrationId + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
                objStatus.Data = null;
                objStatus.LstData = null;
                return objStatus;
            }
            return objStatus;
        }

        public async Task<ResponseStatus_New<Notification>> getNotification(HttpRequest request)
        {
            ResponseStatus_New<Notification> Notification = new ResponseStatus_New<Notification>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<Notification>? NotificationList = new List<Notification>();
                DataSet ds = await Common.GetNotificationjson(portalKey);
                DataTable dtnotifi = ds.Tables[0];

                if (dtnotifi != null)
                {
                    var notification = Common.JSONWithStringBuilderNotification(dtnotifi);
                    NotificationList = JsonConvert.DeserializeObject<List<Notification>>(notification);
                }
               
                if (NotificationList!=null && NotificationList.Count() > 0)
                {
                    Notification.Status = System.Net.HttpStatusCode.OK;
                    Notification.IsSuccess = true;
                    Notification.Title = "Success";
                    Notification.Message = MessageClass.sNotificationS;
                    Notification.LstData = NotificationList;
                }
                else
                {
                    Notification.Status = System.Net.HttpStatusCode.NotFound;
                    Notification.IsSuccess = true;
                    Notification.Title = "Success";
                    Notification.Message = MessageClass.sNotificationN;
                    Notification.LstData = NotificationList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetNotification( Token=" + portalKey + ")", portalKey);
                Notification.Status = System.Net.HttpStatusCode.ExpectationFailed;
                Notification.IsSuccess = false;
                Notification.Message = ex.Message;
                Notification.Title = "Error";
                Notification.Data = null;
                Notification.LstData = null;
            }
            return Notification;
        }

        public async Task<ResponseStatus_New<CustomerNameMobile>> getCustomerNameMobile(CustomerCore4 objuser, HttpRequest request)
        {
            ResponseStatus_New<CustomerNameMobile> CustomerMobile = new ResponseStatus_New<CustomerNameMobile>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<CustomerNameMobile> CustomerMobileList = new List<CustomerNameMobile>();
                CustomerMobileList = await Common.GetCustomerNameMobile(portalKey, customerId);

                if (CustomerMobileList.Count() > 0)
                {
                    CustomerMobile.Status = System.Net.HttpStatusCode.OK;
                    CustomerMobile.IsSuccess = true;
                    CustomerMobile.Title = "Success";
                    CustomerMobile.Message = MessageClass.sRecordFound;
                    CustomerMobile.LstData = CustomerMobileList;
                }
                else
                {
                    CustomerMobile.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerMobile.IsSuccess = true;
                    CustomerMobile.Title = "Success";
                    CustomerMobile.Message = MessageClass.sRecordNotFound;
                    CustomerMobile.LstData = CustomerMobileList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetCustomerNameMobile( Token=" + portalKey + ",  PortalId=" + objuser.PortalId + ")", portalKey);
                CustomerMobile.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerMobile.IsSuccess = false;
                CustomerMobile.Message = ex.Message;
                CustomerMobile.Title = "Error";
                CustomerMobile.Data = null;
                CustomerMobile.LstData = null;
            }
            return CustomerMobile;
        }



        public List<HomePageDisplay> getHomePagesList1(DataTable dtHomePagesList1, string Token, string ImageURL)
        {
            List<HomePageDisplay> list = new List<HomePageDisplay>();
            try
            {
                list = new List<HomePageDisplay>(
                           (from dRow in dtHomePagesList1.AsEnumerable()
                            select (GetdtHomePagesList1Row(dRow, Token, ImageURL)))
                           );
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList1 + ",Token=" + Token + ")", Token);              
            }
            return list;
        }

        public HomePageDisplay GetdtHomePagesList1Row(DataRow dr, string Token, string ImageURL)
        {
            HomePageDisplay objDisplay = new HomePageDisplay();
            try
            {
                objDisplay = new HomePageDisplay();
                var path = ImageURL + "/APPImages/HomePages/";
                objDisplay.Display = path + dr["Display"].ToString();
                objDisplay.Types = dr["Type"].ToString();
                objDisplay.Postion = int.Parse(dr["Postion"].ToString());
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetdtHomePagesList1Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
                throw;
            }
            return objDisplay;
        }

        public List<HomePageUnitNew> getHomePagesList2(DataTable dtHomePagesList2, string Token)
        {
            List<HomePageUnitNew> list = new List<HomePageUnitNew>();
            try
            {
                list = new List<HomePageUnitNew>(
                           (from dRow in dtHomePagesList2.AsEnumerable()
                            select (GetdtHomePagesList2Row(dRow, Token)))
                           );
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList2 + ",Token=" + Token + ")", Token);
            }
            return list;
        }

        public static HomePageUnitNew GetdtHomePagesList2Row(DataRow dr, string Token)
        {
            HomePageUnitNew objUnit = new HomePageUnitNew();
            try
            {
                objUnit = new HomePageUnitNew();
                objUnit.Registration_Id = Convert.ToInt32(dr["Registration_Id"].ToString());
                objUnit.Project = dr["Project"].ToString();
                objUnit.UnitNo = dr["Unit No"].ToString();
                objUnit.Dues = JsonConvert.DeserializeObject<string>("\"" + dr["Dues"].ToString() + "\" ");
                objUnit.unitImage = dr["App_Unit_Image"].ToString();
                objUnit.unitIcon = dr["App_Unit_Icon"].ToString();
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetdtHomePagesList2Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
                throw;
            }
            return objUnit;
        }

        public List<HomePagesIcon> getHomePagesList3(DataTable dtHomePagesList3, string Token, string ImageURL)
        {
            List<HomePagesIcon> list = new List<HomePagesIcon>();
            try
            {
                list = new List<HomePagesIcon>(
                           (from dRow in dtHomePagesList3.AsEnumerable()
                            select (GetdtHomePagesList3Row(dRow, Token, ImageURL)))
                           );
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList3 + ",Token=" + Token + ")", Token);
            }
            return list;
        }

        public HomePagesIcon GetdtHomePagesList3Row(DataRow dr, string Token, string ImageURL)
        {
            HomePagesIcon objIcon = new HomePagesIcon();
            var path = ImageURL + "/APPImages/Header/";

            try
            {
                objIcon = new HomePagesIcon();
                objIcon.Icon = ((dr["Icon"] != null && dr["Icon"].ToString().Trim() != "") ? (path + dr["Icon"].ToString().Trim()) : "");
                objIcon.Name = (dr["ModuleName"] != null ? dr["ModuleName"].ToString() : "");
                objIcon.Position = (Int32.TryParse(dr["Position"].ToString(), out int pos) ? pos : 0);
                objIcon.moduleType = (dr["ModuleType"] != null ? dr["ModuleType"].ToString() : "");
                objIcon.moduleSubType = (dr["SubType"] != null ? dr["SubType"].ToString() : "");
                objIcon.link = (dr["Link"] != null ? dr["Link"].ToString() : "");
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetdtHomePagesList3Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
            }
            return objIcon;
        }



        public async Task<ResponseStatus_New<CustomerDetail>> getCustomerDetails(CustomerCore2 objuser,HttpRequest request)
        {
            ResponseStatus_New<CustomerDetail> CustomerDetail = new ResponseStatus_New<CustomerDetail>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<CustomerDetail> CList = new List<CustomerDetail>();
                CList = await Common.GetCustomerDetails(portalKey, objuser.RegistrationId);

                if (CList != null && CList.Count() > 0)
                {
                    CustomerDetail.Status = System.Net.HttpStatusCode.OK;
                    CustomerDetail.IsSuccess = true;
                    CustomerDetail.Title = "Success";
                    CustomerDetail.Message = MessageClass.sRecordFound;
                    CustomerDetail.LstData = CList;
                }
                else
                {
                    CustomerDetail.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerDetail.IsSuccess = true;
                    CustomerDetail.Title = "Success";
                    CustomerDetail.Message = MessageClass.sRecordNotFound;
                    CustomerDetail.LstData = CList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetCustomerDetails( Token=" + portalKey + ",  CustomerId=" + objuser.RegistrationId + ")", portalKey);
                CustomerDetail.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerDetail.IsSuccess = false;
                CustomerDetail.Message = ex.Message;
                CustomerDetail.Title = "Error";
                CustomerDetail.Data = null;
                CustomerDetail.LstData = null;
            }
            return CustomerDetail;
        }

        public async Task<ResponseStatus_New<MultipleApplicant>> getUdp_MultipleApplicant(CustomerCore2 objuser, HttpRequest request)
        {
            ResponseStatus_New<MultipleApplicant> MlApplicant = new ResponseStatus_New<MultipleApplicant>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<MultipleApplicant> CList = new List<MultipleApplicant>();
                CList = await Common.GetUdp_MultipleApplicant(portalKey, objuser.RegistrationId);

                if (CList != null && CList.Count() > 0)
                {
                    MlApplicant.Status = System.Net.HttpStatusCode.OK;
                    MlApplicant.IsSuccess = true;
                    MlApplicant.Title = "Success";
                    MlApplicant.Message = MessageClass.sRecordFound;
                    MlApplicant.LstData = CList;
                }
                else
                {
                    MlApplicant.Status = System.Net.HttpStatusCode.NotFound;
                    MlApplicant.IsSuccess = true;
                    MlApplicant.Title = "Success";
                    MlApplicant.Message = MessageClass.sRecordNotFound;
                    MlApplicant.LstData = CList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetUdp_MultipleApplicant( Token=" + portalKey + ",  CustomerId=" + objuser.RegistrationId + ")", portalKey);
                MlApplicant.Status = System.Net.HttpStatusCode.ExpectationFailed;
                MlApplicant.IsSuccess = false;
                MlApplicant.Message = ex.Message;
                MlApplicant.Title = "Error";
                MlApplicant.Data = null;
                MlApplicant.LstData = null;
            }
            return MlApplicant;
        }

        public async Task<ResponseStatus_New<UnitDetailList>> UnitDetails(CustomerCore2 objuser, HttpRequest request)
        {
            ResponseStatus_New<UnitDetailList> CustomerUnit = new ResponseStatus_New<UnitDetailList>();

            string portalKey =  "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<UnitDetailList> CList = new List<UnitDetailList>();
                CList = await Common.GetUnitDetails(portalKey, objuser.RegistrationId);

                if (CList != null && CList.Count() > 0)
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.OK;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.Message = MessageClass.sRecordFound;
                    CustomerUnit.LstData = CList;
                }
                else
                {
                    CustomerUnit.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.Message = MessageClass.sRecordNotFound;
                    CustomerUnit.LstData = CList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetUnitDetails( Token=" + portalKey + ",  CustomerId=" + objuser.RegistrationId + ")", portalKey);
                CustomerUnit.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerUnit.IsSuccess = false;
                CustomerUnit.Message = ex.Message;
                CustomerUnit.Title = "Error";
                CustomerUnit.Data = null;
                CustomerUnit.LstData = null;
            }
            return CustomerUnit;
        }

        public async Task<ResponseStatus_New<AccountDeatailList>> getAccount(CustomerCore2 objuser, HttpRequest request)
        {
            AccountDeatailList objAccountDeatail;
            ResponseStatus_New<AccountDeatailList> objStatus = new ResponseStatus_New<AccountDeatailList>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objAccountDeatail = new AccountDeatailList();
                DataSet ds = await Common.GetAccountDetails(portalKey, objuser.RegistrationId);

                if(ds!=null && ds.Tables.Count > 2)
                {
                    DataTable dtUnitCost = ds.Tables[0];
                    DataTable dtExtraCharge = ds.Tables[1];
                    DataTable dtTotalCost = ds.Tables[2];

                    if (objuser.RegistrationId >= 0)
                    {
                        if (dtTotalCost.Rows.Count > 0)
                        {

                            objAccountDeatail.TagName = dtTotalCost.Rows[0][0].ToString();
                            objAccountDeatail.TagValue = JsonConvert.DeserializeObject<string>("\"" + dtTotalCost.Rows[0][1].ToString() + "\" ");
                            objAccountDeatail.TagColor = dtTotalCost.Rows[0][4].ToString();
                            objAccountDeatail.TagStyle = dtTotalCost.Rows[0][5].ToString();
                            objAccountDeatail.UnitCostList = GetUnitCostList(dtUnitCost, portalKey);
                            objAccountDeatail.ExtraChargeList = GetExtraCharge(dtExtraCharge, portalKey);
                            objStatus.Data = objAccountDeatail;
                            objStatus.IsSuccess = true;
                            objStatus.Status = System.Net.HttpStatusCode.OK;
                            objStatus.Message = MessageClass.sRecordFound;
                            objStatus.Title = "Success";
                            objStatus.LstData = null;
                        }
                        else
                        {
                            objStatus.IsSuccess = true;
                            objStatus.Status = System.Net.HttpStatusCode.NotFound;
                            objStatus.Message = MessageClass.sRecordNotFound;
                            objStatus.Title = "Success";
                            objStatus.LstData = null;
                        }
                    }
                }                               
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " Customer GetAccount( Token=" + portalKey + ",RegistrationId=" + objuser.RegistrationId + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
                objStatus.Data = null;
                objStatus.LstData = null;
                return objStatus;
            }
            return objStatus;
        }

        public List<UnitCost> GetUnitCostList(DataTable table, string Token)
        {
            List<UnitCost>? UnitDetailsList = new List<UnitCost>();
            try
            {
                var JSONString = new System.Text.StringBuilder();
                var ContentList = new System.Text.StringBuilder();
                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");
                    JSONString.Append("{");
                    ContentList.Append("[");
                    // int ss = table.Columns.Count;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["HeadType"].ToString() == "C")
                            JSONString.Append("\"headingName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"headingvalue\":" + "\"" + table.Rows[i][1].ToString() + "\" ," + "\"headingNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"headingNameStyle\":" + "\"" + table.Rows[i][5].ToString() + "\",");
                        //else if (table.Rows[i]["TagType"].ToString() == "1")
                        //    JSONString.Append("\"basicName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"basicvalue\":" + "\"" + table.Rows[i][1].ToString() + "\"," + "\"basicNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"basicNameStyle\":" + "\"" + table.Rows[i][5].ToString() + "\",");
                        else
                        {
                            if (table.Rows[i][1].ToString() != "0.0" && table.Rows[i][1].ToString() != "0")
                                ContentList.Append("{\"tagName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][1].ToString() + "\" ," + "\"TagNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][5].ToString() + "\"},");
                        }
                        ContentList.ToString().TrimEnd(',');
                    }
                    ContentList.Append("]");
                    JSONString.Append("ContentList:" + ContentList);
                    ContentList = new System.Text.StringBuilder();
                    JSONString.Append("}");
                    JSONString.Append("]");
                }

                var str = JSONString.ToString();
                UnitDetailsList = JsonConvert.DeserializeObject<List<UnitCost>>(str);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetUnitCost(Token=" + Token + ")", Token);
            }

            return UnitDetailsList;
        }

        public List<ExtraCharge> GetExtraCharge(DataTable table, string Token)
        {
            List<ExtraCharge>? ExtraChargeList = new List<ExtraCharge>();
            try
            {
                var JSONString = new System.Text.StringBuilder();
                var ContentList = new System.Text.StringBuilder();
                if (table.Rows.Count > 0)
                {
                    JSONString.Append("[");
                    JSONString.Append("{");
                    ContentList.Append("[");

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i]["HeadType"].ToString() == "C")
                            JSONString.Append("\"headingName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"headingvalue\":" + "\"" + table.Rows[i][1].ToString() + "\" ," + "\"headingNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"headingNameStyle\":" + "\"" + table.Rows[i][5].ToString() + "\",");
                        //else if (table.Rows[i]["TagType"].ToString() == "1")
                        //    JSONString.Append("\"basicName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"basicvalue\":" + "\"" + table.Rows[i][1].ToString() + "\"," + "\"basicNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"basicNameStyle\":" + "\"" + table.Rows[i][5].ToString() + "\",");

                        else
                        {
                            if (table.Rows[i][1].ToString() != "0.0" && table.Rows[i][1].ToString() != "0")
                                ContentList.Append("{\"tagName\":" + "\"" + table.Rows[i][0].ToString() + "\"," + "\"tagValue\":" + "\"" + table.Rows[i][1].ToString() + "\" ," + "\"TagNameColor\":" + "\"" + table.Rows[i][4].ToString() + "\"   ," + "\"Style\":" + "\"" + table.Rows[i][5].ToString() + "\"},");
                        }
                        ContentList.ToString().TrimEnd(',');
                    }
                    ContentList.Append("]");
                    JSONString.Append("ContentList:" + ContentList);
                    ContentList = new System.Text.StringBuilder();
                    JSONString.Append("}");
                    JSONString.Append("]");
                }

                var str = JSONString.ToString();

                // var strsss = JsonConvert.DeserializeObject<List<UnitCost>>(str);
                ExtraChargeList = JsonConvert.DeserializeObject<List<ExtraCharge>>(str);
                //unticost= JSONString.ToString();
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Common.Common.GetExtraCharge(Token=" + Token + ")", Token);
            }
            return ExtraChargeList;
        }

        public async Task<ResponseStatusPayment_New<PaymentscheduleListJsion>> getOutstandingDetail_Paymentschedule(CustomerCore5 objuser, HttpRequest request)
        {
            ResponseStatusPayment_New<PaymentscheduleListJsion> CustomerUnit = new ResponseStatusPayment_New<PaymentscheduleListJsion>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<PaymentscheduleListJsion>? CList = new List<PaymentscheduleListJsion>();
                DataTable dt = new DataTable();
                DataSet ds = await Common.Paymentschedule(portalKey, objuser.RegistrationId, objuser.Type);
                dt = ds.Tables[0];
                DataTable totalos = ds.Tables[1];
                var str = Common.DataTableToJSONWithStringBuilderInstallment(dt, totalos);
                CustomerUnit.TotalInstallmentTagName = totalos.Rows[0][0].ToString();
                CustomerUnit.TotalInstallmentTagValue = JsonConvert.DeserializeObject<string>("\"" + totalos.Rows[0][1].ToString() + "\" ");
                CustomerUnit.TotalInstallmentColor = JsonConvert.DeserializeObject<string>("\"" + totalos.Rows[0][2].ToString() + "\" ");
                CustomerUnit.TotalInstallmentStyle = JsonConvert.DeserializeObject<string>("\"" + totalos.Rows[0][3].ToString() + "\" ");

                CList = JsonConvert.DeserializeObject<List<PaymentscheduleListJsion>>(str);

                if (dt.Rows.Count > 0)
                {
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Status = System.Net.HttpStatusCode.OK;
                    CustomerUnit.Message = MessageClass.sRecordFound;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.LstData = CList;
                }
                else
                {
                    CustomerUnit.IsSuccess = true;
                    CustomerUnit.Status = System.Net.HttpStatusCode.NotFound;
                    CustomerUnit.Message = MessageClass.sRecordNotFound;
                    CustomerUnit.Title = "Success";
                    CustomerUnit.LstData = CList;
                }

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetInstallment( Token=" + portalKey + ",  RegistrationId=" + objuser.RegistrationId + ")", portalKey);
                CustomerUnit.Status = System.Net.HttpStatusCode.ExpectationFailed;
                CustomerUnit.IsSuccess = false;
                CustomerUnit.Message = ex.Message;
                CustomerUnit.Title = "Error";
                CustomerUnit.Data = null;
                CustomerUnit.LstData = null;
            }
            return CustomerUnit;
        }

        public async Task<ResponseStatusRecipt_New<ReceiptDetailsDataList>> getReceiptDetails(CustomerCore2 objuser, HttpRequest request)
        {
            ResponseStatusRecipt_New<ReceiptDetailsDataList> ReceiptDetails = new ResponseStatusRecipt_New<ReceiptDetailsDataList>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<ReceiptDetailsDataList>? ReList = new List<ReceiptDetailsDataList>();
                DataTable dt = new DataTable();
                DataTable dtTotalReceipt = new DataTable();
                DataSet ds = await Common.GetReceiptDetails(portalKey, objuser.RegistrationId);
                dt = ds.Tables[0];
                dtTotalReceipt = ds.Tables[1];

                ReceiptDetails.TotalReceiptAmountTagName = dtTotalReceipt.Rows[0][0].ToString();
                ReceiptDetails.TotalReceiptAmountTagValue = JsonConvert.DeserializeObject<string>("\"" + dtTotalReceipt.Rows[0][1].ToString() + "\" ");
                ReceiptDetails.TotalReceiptTagNameColor = JsonConvert.DeserializeObject<string>("\"" + dtTotalReceipt.Rows[0][2].ToString() + "\" ");
                ReceiptDetails.TotalReceiptTagNameStyle = JsonConvert.DeserializeObject<string>("\"" + dtTotalReceipt.Rows[0][3].ToString() + "\" ");

                //var Recpt = Common.DataTableToJSONWithStringBuilderReciptDeatil(dt);
                ReList = JsonConvert.DeserializeObject<List<ReceiptDetailsDataList>>(Common.DataTableToJSONWithStringBuilderReciptDeatil(dt));
                //ReList = Common.DataTableToJSONObjectReceiptDetail(dt);

                if (dt.Rows.Count > 0)
                {
                    ReceiptDetails.IsSuccess = true;
                    ReceiptDetails.Status = System.Net.HttpStatusCode.OK;
                    ReceiptDetails.Message = MessageClass.sRecordFound;
                    ReceiptDetails.Title = "Success";
                    ReceiptDetails.LstData = ReList;
                }
                else
                {
                    ReceiptDetails.IsSuccess = true;
                    ReceiptDetails.Status = System.Net.HttpStatusCode.NotFound;
                    ReceiptDetails.Message = MessageClass.sRecordFound;
                    ReceiptDetails.Title = "Success";
                    ReceiptDetails.LstData = ReList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetReceiptDetails( Token=" + portalKey + ",  RegistrationId=" + objuser.RegistrationId + ")", portalKey);
                ReceiptDetails.Status = System.Net.HttpStatusCode.ExpectationFailed;
                ReceiptDetails.IsSuccess = false;
                ReceiptDetails.Message = ex.Message;
                ReceiptDetails.Title = "Error";
                ReceiptDetails.Data = null;
                ReceiptDetails.LstData = null;
            }

            return ReceiptDetails;
        }

        public async Task<ResponseStatus_New<LetterDetailCustomer>> getLetterDetailCustomerPortal(CustomerCore2_2 objuser, HttpRequest request)
        {
            ResponseStatus_New<LetterDetailCustomer> LetterDetail = new ResponseStatus_New<LetterDetailCustomer>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<LetterDetailCustomer> LetterDetailList = new List<LetterDetailCustomer>();
                LetterDetailList = await Common.GetLetterDetailCustomerPortal(portalKey, objuser.Registration_Id);

                if (LetterDetailList != null &&LetterDetailList.Count() > 0)
                {

                    LetterDetail.IsSuccess = true;
                    LetterDetail.Status = System.Net.HttpStatusCode.OK;
                    LetterDetail.Message = MessageClass.sRecordFound;
                    LetterDetail.Title = "Success";
                    LetterDetail.LstData = LetterDetailList;
                }
                else
                {
                    LetterDetail.IsSuccess = true;
                    LetterDetail.Status = System.Net.HttpStatusCode.NotFound;
                    LetterDetail.Message = MessageClass.sRecordFound;
                    LetterDetail.Title = "Success";
                    LetterDetail.LstData = LetterDetailList;
                }
            }


            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetLetterDetailCustomer( Token=" + portalKey + ",  RegistrationId=" + objuser.Registration_Id + ")", portalKey);
                LetterDetail.Status = System.Net.HttpStatusCode.ExpectationFailed;
                LetterDetail.IsSuccess = false;
                LetterDetail.Message = ex.Message;
                LetterDetail.Title = "Error";
                LetterDetail.Data = null;
                LetterDetail.LstData = null;
            }
            return LetterDetail;
        }

        public async Task<ResponseStatus_New<ContactUS>> getContactUS(HttpRequest request)
        {
            ResponseStatus_New<ContactUS> ContactUs = new ResponseStatus_New<ContactUS>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<ContactUS> ContactUSList = new List<ContactUS>();
                DataSet dsData = await Common.GetContactUS(portalKey);
                if (!Object.Equals(dsData, null))
                {
                    if (dsData.Tables[0].Rows.Count > 0)
                    {
                        ContactUSList.Add(new ContactUS
                        {
                            Mobile_Number = dsData.Tables[0].Rows[0]["Mobile_Number"].ToString(),
                            HTMLString = dsData.Tables[0].Rows[0]["Website"].ToString()
                        });
                    }
                }
                if (ContactUSList.Count() > 0)
                {
                    ContactUs.IsSuccess = true;
                    ContactUs.Status = System.Net.HttpStatusCode.OK;
                    ContactUs.Message = MessageClass.sRecordFound;
                    ContactUs.Title = "Success";
                    ContactUs.LstData = ContactUSList;
                }
                else
                {
                    ContactUs.IsSuccess = true;
                    ContactUs.Status = System.Net.HttpStatusCode.NotFound;
                    ContactUs.Message = MessageClass.sRecordNotFound;
                    ContactUs.Title = "Success";
                    ContactUs.LstData = ContactUSList;
                }
            }

            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetContactUS( Token=" + portalKey + ")", portalKey);
                ContactUs.Status = System.Net.HttpStatusCode.ExpectationFailed;
                ContactUs.IsSuccess = false;
                ContactUs.Message = ex.Message;
                ContactUs.Title = "Error";
                ContactUs.Data = null;
                ContactUs.LstData = null;
            }
            return ContactUs;
        }

        public async Task<ResponseStatus_New<ViewProjectDocuments>> GetProjectDocuments(CustomerCore2 objuser, HttpRequest request)
        {
            ResponseStatus_New<ViewProjectDocuments> projectDocument = new ResponseStatus_New<ViewProjectDocuments>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                if (objuser.RegistrationId > 0)
                {
                    projectDocument.IsSuccess = true;
                    projectDocument.Status = System.Net.HttpStatusCode.OK;
                    projectDocument.Message = MessageClass.sRecordFound;
                    projectDocument.Title = "Success";
                    projectDocument.Data = await Common.GetProjectDocument(portalKey,objuser.RegistrationId);
                }
                else
                {
                    projectDocument.IsSuccess = true;
                    projectDocument.Status = System.Net.HttpStatusCode.NotFound;
                    projectDocument.Message = MessageClass.sRecordNotFound;
                    projectDocument.Title = "Success";
                    projectDocument.LstData = null;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetProjectDocuments( Token=" + portalKey + ",  CustomerId=" + objuser.RegistrationId + ")", portalKey);
                projectDocument.Status = System.Net.HttpStatusCode.ExpectationFailed;
                projectDocument.IsSuccess = false;
                projectDocument.Message = ex.Message;
                projectDocument.Title = "Error";
            }
            return projectDocument;
        }

        public async Task<ResponseStatus_New<ViewCustomerDocument>> GetCustomerDocuments(CustomerDoc objuser, HttpRequest request)
        {
            ResponseStatus_New<ViewCustomerDocument> customerDocument = new ResponseStatus_New<ViewCustomerDocument>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<ViewCustomerDocument> CustList = new List<ViewCustomerDocument>();
                CustList = await Common.GetCustomerDocument(portalKey,objuser);

                if (CustList != null &&CustList.Count() > 0)
                {
                    customerDocument.IsSuccess = true;
                    customerDocument.Status = System.Net.HttpStatusCode.OK;
                    customerDocument.Message = MessageClass.sRecordFound;
                    customerDocument.Title = "Success";
                    customerDocument.LstData = CustList;
                }
                else
                {
                    customerDocument.IsSuccess = true;
                    customerDocument.Status = System.Net.HttpStatusCode.NotFound;
                    customerDocument.Title = "Success";
                    customerDocument.Message = MessageClass.sRecordNotFound;
                    customerDocument.LstData = CustList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetCustomerDocuments( Token=" + portalKey + ",  CustomerId=" + objuser.CustomerId + ")", portalKey);
                customerDocument.Status = System.Net.HttpStatusCode.ExpectationFailed;
                customerDocument.IsSuccess = false;
                customerDocument.Message = ex.Message;
                customerDocument.Title = "Error";
            }
            return customerDocument;
        }

        public async Task<ResponseStatus_New<CustomerDuesPaid>> getCustomerDuesPaid(CustomerCore2_2 ObjCustomer, HttpRequest request)
        {
            ResponseStatus_New<CustomerDuesPaid> DemandStatus = new ResponseStatus_New<CustomerDuesPaid>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                List<CustomerDuesPaid> DemandStatusList = new List<CustomerDuesPaid>();
                DemandStatusList = await Common.GetCustomerDuesPaid(portalKey, ObjCustomer.Registration_Id);

                if (DemandStatusList != null && DemandStatusList.Count() > 0)
                {
                    DemandStatus.IsSuccess = true;
                    DemandStatus.Status = System.Net.HttpStatusCode.OK;
                    DemandStatus.Message = MessageClass.sRecordFound;
                    DemandStatus.Title = "Success";
                    DemandStatus.LstData = DemandStatusList;
                }
                else
                {
                    DemandStatus.IsSuccess = true;
                    DemandStatus.Status = System.Net.HttpStatusCode.NotFound;
                    DemandStatus.Message = MessageClass.sRecordNotFound;
                    DemandStatus.Title = "Success";
                    DemandStatus.LstData = DemandStatusList;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer GetCustomerDuesPaid( Token=" + portalKey + ",  RegistrationId=" + ObjCustomer.Registration_Id + ")", portalKey);
                DemandStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                DemandStatus.IsSuccess = false;
                DemandStatus.Message = ex.Message;
                DemandStatus.Title = "Error";
            }
            return DemandStatus;
        }       

        public async Task<ResponseStatus_New<NoData>> saveCustomerReferral_P2(CustomerReferralRequest ObjCustomer, HttpRequest request, HttpContext context)
        {
            ResponseStatus_New<NoData> objResponse = new ResponseStatus_New<NoData>();

            string portalKey = "NoToken";
            try
            {
                string message = JsonConvert.SerializeObject(ObjCustomer);
                Log.LogPayloadDateWise(message, "SaveCustomerReferral", context);

                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objResponse = await Common.SaveCustomerReferral_P2(ObjCustomer, portalKey, customerId);
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "SaveCustomerReferral( Token=" + portalKey + ",  CustomerId=" + ObjCustomer.customerId + ")", portalKey);
                objResponse.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objResponse.IsSuccess = false;
                objResponse.Message = ex.Message;
                objResponse.Title = "Error";
            }
            return objResponse;
        }

        public async Task<ResponseStatus_New<CustomerReferralDetails>> getCustomerReferralReport(CustomerCore1 ObjCustomer, HttpRequest request, HttpContext context)
        {
            ResponseStatus_New<CustomerReferralDetails> objResponse = new ResponseStatus_New<CustomerReferralDetails>();

            string portalKey = "NoToken";
            try
            {
                string message = JsonConvert.SerializeObject(ObjCustomer);
                Log.LogPayloadDateWise(message, "GetCustomerReferralReport", context);

                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                objResponse.LstData = await Common.getCustomerReferralReport(ObjCustomer, portalKey, customerId);
                objResponse.Status = System.Net.HttpStatusCode.OK;
                objResponse.Message = "Success";
                objResponse.Title = "Success";
                objResponse.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "GetCustomerReferralReport( Token=" + portalKey + ",  CustomerId=" + ObjCustomer.CustomerId + ")", portalKey);
                objResponse.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objResponse.Message = ex.Message;
                objResponse.IsSuccess = false;
                objResponse.Title = "Error";
                objResponse.LstData = null;
            }
            return objResponse;
        }

        public async Task<ResponseStatus_New<AccountDetail_New_Wrap>> getAccount_New(CustomerCore2 objuser, HttpRequest request)
        {          
            ResponseStatus_New<AccountDetail_New_Wrap> objStatus = new ResponseStatus_New<AccountDetail_New_Wrap>();
            AccountDetail_New_Wrap accountDetails = new AccountDetail_New_Wrap();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                DataSet ds = await Common.GetAccountDetails_New(portalKey, objuser.RegistrationId);

                if (ds != null && ds.Tables.Count > 0) 
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0) 
                    {
                        List<AccountDetails_New_Charge> chargeList = new List<AccountDetails_New_Charge>();
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            AccountDetails_New_Charge charge = new AccountDetails_New_Charge();
                            charge.particular = (dt.Rows[i]["Particular"] != null ? dt.Rows[i]["Particular"].ToString() : "");
                            charge.type = (dt.Rows[i]["Type"] != null ? dt.Rows[i]["Type"].ToString() : "");
                            charge.quantity = (dt.Rows[i]["Quantity"] != null ? dt.Rows[i]["Quantity"].ToString() : "");
                            charge.rate = (dt.Rows[i]["Rate"] != null ? dt.Rows[i]["Rate"].ToString() : "");
                            charge.amount = (dt.Rows[i]["Amount"] != null ? dt.Rows[i]["Amount"].ToString() : "");
                            charge.rank = (Int32.TryParse(dt.Rows[i]["Rank"].ToString(), out int id) ? id : 0);
                            charge.isFooter = (Boolean.TryParse(dt.Rows[i]["IsFooter"].ToString(), out Boolean isFoot) ? isFoot : false);
                            chargeList.Add(charge);
                        }

                        accountDetails.ChargeList = chargeList;
                    }
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        accountDetails.totalArea = (dt.Rows[0]["TotalArea"] != null ? dt.Rows[0]["TotalArea"].ToString() : "");
                        accountDetails.totalAmount = (dt.Rows[0]["TotalAmount"] != null ? dt.Rows[0]["TotalAmount"].ToString() : "");
                        accountDetails.totalAmountWords = (dt.Rows[0]["TotalAmountWords"] != null ? dt.Rows[0]["TotalAmountWords"].ToString() : "");
                        accountDetails.showAddonTable = (Boolean.TryParse(dt.Rows[0]["ShowAddonTable"].ToString(), out Boolean show) ? show : false);
                        accountDetails.totalAddonAmount = (dt.Rows[0]["TotalAddonAmount"] != null ? dt.Rows[0]["TotalAddonAmount"].ToString() : "");
                        accountDetails.totalAddonAmountWords = (dt.Rows[0]["TotalAddonAmountWords"] != null ? dt.Rows[0]["TotalAddonAmountWords"].ToString() : "");
                        accountDetails.addonTableName = (dt.Rows[0]["TableName"] != null ? dt.Rows[0]["TableName"].ToString() : "");
                    }
                }

                if (ds != null && ds.Tables.Count > 2)
                {
                    DataTable dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<AccountDetails_New_Charge> chargeList = new List<AccountDetails_New_Charge>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AccountDetails_New_Charge charge = new AccountDetails_New_Charge();
                            charge.particular = (dt.Rows[i]["Particular"] != null ? dt.Rows[i]["Particular"].ToString() : "");
                            charge.type = (dt.Rows[i]["Type"] != null ? dt.Rows[i]["Type"].ToString() : "");
                            charge.quantity = (dt.Rows[i]["Quantity"] != null ? dt.Rows[i]["Quantity"].ToString() : "");
                            charge.rate = (dt.Rows[i]["Rate"] != null ? dt.Rows[i]["Rate"].ToString() : "");
                            charge.amount = (dt.Rows[i]["Amount"] != null ? dt.Rows[i]["Amount"].ToString() : "");
                            charge.rank = (Int32.TryParse(dt.Rows[i]["Rank"].ToString(), out int id) ? id : 0);
                            charge.isFooter = (Boolean.TryParse(dt.Rows[i]["IsFooter"].ToString(), out Boolean isFoot) ? isFoot : false);
                            chargeList.Add(charge);
                        }

                        accountDetails.addonChargeList = chargeList;
                    }
                }

                objStatus.Data = accountDetails;
                objStatus.Status = System.Net.HttpStatusCode.OK;
                objStatus.IsSuccess = true;
                objStatus.Message = "Success";
                objStatus.Title = "Success";

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " Customer GetAccount( Token=" + portalKey + ",RegistrationId=" + objuser.RegistrationId + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
                objStatus.Data = null;
                objStatus.LstData = null;
            }

            return objStatus;
        }

        public async Task<ResponseStatus_New<PortalExtraInfo_Wrap>> getPortalExtraInfo(HttpRequest request)
        {
            ResponseStatus_New<PortalExtraInfo_Wrap> objStatus = new ResponseStatus_New<PortalExtraInfo_Wrap>();
            PortalExtraInfo_Wrap portalInfo = new PortalExtraInfo_Wrap();

            string portalKey = "NoToken";

            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                DataSet ds = await Common.GetPortalExtraInfo(portalKey, customerId);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        portalInfo.supportNo = (dt.Rows[0]["SupportNo"] != null ? dt.Rows[0]["SupportNo"].ToString() : "");
                    }
                }

                objStatus.Data = portalInfo;
                objStatus.Status = System.Net.HttpStatusCode.OK;
                objStatus.IsSuccess = true;
                objStatus.Message = "Success";
                objStatus.Title = "Success";

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " Get Portal Extra Info( Token=" + portalKey + ",RegistrationId=" + "0" + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
            }

            return objStatus;
        }

        public async Task<ResponseStatus_New<dynamic>> getRaisedQueryDetails(QueryDetailsRequest objRequest, HttpRequest request)
        {
            ResponseStatus_New<dynamic> objStatus = new ResponseStatus_New<dynamic>();

            string portalKey = "NoToken";

            try
            {
                if (objRequest != null && objRequest.detailType != null)
                {
                    (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                    DataSet ds = await Common.GetRaisedQueryDetails(objRequest, portalKey, customerId);

                    if (objRequest.detailType.ToUpper() == "C")
                    {
                        List<QueryConversationResponse>  data = new List<QueryConversationResponse>();

                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for(int i = 0; i < dt.Rows.Count; i++)
                                {
                                    QueryConversationResponse resp = new QueryConversationResponse();
                                    resp.conversationId = (Int32.TryParse(dt.Rows[i]["AutoId"].ToString(), out int id) ? id : 0);
                                    resp.remark = (dt.Rows[i]["Remark"] != null ? dt.Rows[i]["Remark"].ToString() : "");
                                    resp.status = (dt.Rows[i]["Status"] != null ? dt.Rows[i]["Status"].ToString() : "");
                                    resp.responseBy = (dt.Rows[i]["CreatedBy"] != null ? dt.Rows[i]["CreatedBy"].ToString() : "");
                                    resp.responseDate = (dt.Rows[i]["CreatedDate"] != null ? dt.Rows[i]["CreatedDate"].ToString() : "");                                  
                                    data.Add(resp);
                                } 
                            }
                        }

                        objStatus.Data = data;
                    }
                    else
                    {
                        List<QueryHistoryResponse>  data = new List<QueryHistoryResponse>();

                        if (ds != null && ds.Tables.Count > 0)
                        {
                            DataTable dt = ds.Tables[0];
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                for (int i = 0; i < dt.Rows.Count; i++)
                                {
                                    QueryHistoryResponse resp = new QueryHistoryResponse();
                                    resp.queryId = (Int32.TryParse(dt.Rows[i]["QH_id"].ToString(), out int id) ? id : 0);
                                    resp.queryType = (dt.Rows[i]["QueryType"] != null ? dt.Rows[i]["QueryType"].ToString() : "");
                                    resp.text = (dt.Rows[i]["Text"] != null ? dt.Rows[i]["Text"].ToString() : "");
                                    resp.createdDate = (dt.Rows[i]["CreatedDate"] != null ? dt.Rows[i]["CreatedDate"].ToString() : "");
                                    resp.status = (dt.Rows[i]["Status"] != null ? dt.Rows[i]["Status"].ToString() : "");
                                    resp.customerName = (dt.Rows[i]["Customer"] != null ? dt.Rows[i]["Customer"].ToString() : "");
                                    resp.regNo = (dt.Rows[i]["Registration_No"] != null ? dt.Rows[i]["Registration_No"].ToString() : "");
                                    resp.projectName = (dt.Rows[i]["Project_Name"] != null ? dt.Rows[i]["Project_Name"].ToString() : "");
                                    resp.towerName = (dt.Rows[i]["Project_Tower_Name"] != null ? dt.Rows[i]["Project_Tower_Name"].ToString() : "");
                                    resp.unitNo = (dt.Rows[i]["Address"] != null ? dt.Rows[i]["Address"].ToString() : "");
                                    resp.attachment = (dt.Rows[i]["Attachment"] != null ? dt.Rows[i]["Attachment"].ToString() : "");
                                    resp.registrationId = (Int32.TryParse(dt.Rows[i]["Registration_Id"].ToString(), out id) ? id : 0);
                                    resp.lastRemark = (dt.Rows[i]["LastRemarks"] != null ? dt.Rows[i]["LastRemarks"].ToString() : "");
                                    resp.lastRemarkBy = (dt.Rows[i]["LastRemarksBy"] != null ? dt.Rows[i]["LastRemarksBy"].ToString() : "");
                                    resp.lastRemarkDate = (dt.Rows[i]["LastRemarksDate"] != null ? dt.Rows[i]["LastRemarksDate"].ToString() : "");
                                    resp.convoCount = (Int32.TryParse(dt.Rows[i]["ConvoCount"].ToString(), out id) ? id : 0);

                                    data.Add(resp);
                                }
                            }
                        }

                        objStatus.Data = data;
                    }
                   
                    objStatus.Status = System.Net.HttpStatusCode.OK;
                    objStatus.IsSuccess = true;
                    objStatus.Message = "Success";
                    objStatus.Title = "Success";
                }
                else {
                    objStatus.Status = System.Net.HttpStatusCode.BadRequest;
                    objStatus.IsSuccess = false;
                    objStatus.Message = "Invalid Request";
                    objStatus.Title = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " GetRaisedQueryDetails( Token=" + portalKey + ",RegistrationId=" + "0" + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
            }

            return objStatus;
        }

        public async Task<dynamic> GetProjectDocumentsByType(CustomerCore5 objRequest, HttpRequest request)
        {
            ResponseStatus_New<List<CommonDocumentList_New>> objStatus = new ResponseStatus_New<List<CommonDocumentList_New>>();
            List<CommonDocumentList_New> data = new List<CommonDocumentList_New>();

            string portalKey = "NoToken";

            try
            {
                if (objRequest != null)
                {
                    (new JWTTokenMethods()).GetConnectionDetailsCustomer(request, out int customerId, out portalKey);

                    DataSet ds = await Common.GetProjectDocumentsByType(objRequest, portalKey, customerId);                  

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                CommonDocumentList_New resp = new CommonDocumentList_New();
                                resp.projectId = (Int32.TryParse(dt.Rows[i]["Project_Id"].ToString(), out int id) ? id : 0);
                                resp.documentId = (Int32.TryParse(dt.Rows[i]["Doc_Id"].ToString(), out id) ? id : 0);
                                resp.documentName = (dt.Rows[i]["Document_name"] != null ? dt.Rows[i]["Document_name"].ToString() : "");
                                resp.fileName = (dt.Rows[i]["FileName"] != null ? dt.Rows[i]["FileName"].ToString() : "");
                                resp.fileLink = (dt.Rows[i]["FileLink"] != null ? dt.Rows[i]["FileLink"].ToString() : "");
                                resp.fileType = (dt.Rows[i]["FileType"] != null ? dt.Rows[i]["FileType"].ToString() : "");
                                resp.fullFilePath = (dt.Rows[i]["FullFilePath"] != null ? dt.Rows[i]["FullFilePath"].ToString() : "");
                                resp.portalType = (dt.Rows[i]["PortalType"] != null ? dt.Rows[i]["PortalType"].ToString() : "");
                                resp.docType = (dt.Rows[i]["Doc_Type"] != null ? dt.Rows[i]["Doc_Type"].ToString() : "");
                                data.Add(resp);
                            }
                        }
                    }

                    objStatus.Data = data;

                    objStatus.Status = System.Net.HttpStatusCode.OK;
                    objStatus.IsSuccess = true;
                    objStatus.Message = "Success";
                    objStatus.Title = "Success";
                }
                else
                {
                    objStatus.Status = System.Net.HttpStatusCode.BadRequest;
                    objStatus.IsSuccess = false;
                    objStatus.Message = "Invalid Request";
                    objStatus.Title = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, " GetProjectDocumentsByType( Token=" + portalKey + ",RegistrationId=" + "0" + ")", portalKey);
                objStatus.Status = System.Net.HttpStatusCode.ExpectationFailed;
                objStatus.IsSuccess = false;
                objStatus.Message = ex.Message;
                objStatus.Title = "Error";
            }

            return objStatus;
        }
    }
}
