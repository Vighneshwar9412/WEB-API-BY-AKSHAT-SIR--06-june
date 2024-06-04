using FourQT.Entities.Portal.Referrals;
using FourQT.Entities.Portal;
using System.Data;
using MobAppCoreAPI.Interfaces.Portal;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository.Portal
{
    public class CustomerRepository : IPortalCustomer
    {
        //public ResponseStatus<HomePages> getPortal_HomePages(CustomerLogin objuser)
        //{
        //    HomePages objEnquiryMasters;
        //    ResponseStatus<HomePages> objStatus = new ResponseStatus<HomePages>();
        //    try
        //    {
        //        objEnquiryMasters = new HomePages();
        //        DataSet dsHomePage = Common.GetPortal_HomePages(objuser.Token, objuser.CustomerId);
        //        DataTable dt = Common.GetdtImageURL(objuser.Token);
        //        string ClintImageURL = dt.Rows[0][18].ToString();
        //        string PortalImageURL = dt.Rows[0][20].ToString();
        //        if (objuser.CustomerId >= 0)
        //        {
        //            objEnquiryMasters.DisplayList = getHomePagesList1(dsHomePage.Tables[0], objuser.Token, PortalImageURL);
        //            objEnquiryMasters.UnitList = getHomePagesList2(dsHomePage.Tables[1], objuser.Token);
        //            objEnquiryMasters.IconList = getHomePagesList3(dsHomePage.Tables[2], objuser.Token, PortalImageURL);

        //            List<CustomerMessage> lstMessage = new List<CustomerMessage>();
        //            foreach (DataRow dr in dsHomePage.Tables[3].Rows)
        //            {
        //                lstMessage.Add(new CustomerMessage()
        //                {
        //                    M_Id = Convert.ToInt32(dr["M_Id"].ToString()),
        //                    Message = dr["Message"].ToString()
        //                });
        //            }

        //            objEnquiryMasters.MessageList = lstMessage;

        //            List<CustomerNameWithHeader> lstCName = new List<CustomerNameWithHeader>();
        //            foreach (DataRow dr in dsHomePage.Tables[4].Rows)
        //            {
        //                lstCName.Add(new CustomerNameWithHeader()
        //                {
        //                    CustomerName = Convert.ToString(dr["CustomerName"])
        //                });
        //            }
        //            objEnquiryMasters.CustomerNameList = lstCName;
        //            objStatus.Data = objEnquiryMasters;
        //        }
        //        if (dsHomePage.Tables[0].Rows.Count > 0 || dsHomePage.Tables[1].Rows.Count > 0 || dsHomePage.Tables[2].Rows.Count > 0)
        //        {
        //            objStatus.Status = true;
        //            objStatus.ErrorCode = 200;
        //            objStatus.Message = string.Empty;
        //            objStatus.LstData = null;
        //            objStatus.Message = MessageClass.sRecordFound;
        //        }
        //        else
        //        {
        //            objStatus.Status = true;
        //            objStatus.ErrorCode = 404;
        //            objStatus.Message = string.Empty;
        //            objStatus.LstData = null;
        //            objStatus.Message = MessageClass.sRecordNotFound;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, " Customer GetPortal_HomePages( Token=" + objuser.Token + ",CustomerId=" + objuser.CustomerId + ")", objuser.Token);
        //        objStatus.Status = false;
        //        objStatus.ErrorCode = 417;
        //        objStatus.Message = ex.Message;
        //        objStatus.Data = null;
        //        objStatus.LstData = null;
        //        return objStatus;
        //    }
        //    return objStatus;
        //}
     
        //public ResponseStatus<CustomerLoginList> getCustomerUnitNumber(CustomerLogin objuser)
        //{
        //    ResponseStatus<CustomerLoginList> CustomerUnit = new ResponseStatus<CustomerLoginList>();
        //    try
        //    {
        //        List<CustomerLoginList> CList = new List<CustomerLoginList>();
        //        CList = Common.GetCustomerUnitNumber(objuser.Token, objuser.CustomerId);

        //        if (CList.Count() > 0)
        //        {
        //            CustomerUnit.Status = true;
        //            CustomerUnit.ErrorCode = 200;
        //            CustomerUnit.Message = MessageClass.sRecordFound;
        //            CustomerUnit.LstData = CList;
        //        }
        //        else
        //        {
        //            CustomerUnit.Status = true;
        //            CustomerUnit.ErrorCode = 404;
        //            CustomerUnit.Message = MessageClass.sRecordNotFound;
        //            CustomerUnit.LstData = CList;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "User GetCustomerUnitNumber( Token=" + objuser.Token + ",  CustomerId=" + objuser.CustomerId + ")", objuser.Token);
        //        CustomerUnit.ErrorCode = 417;
        //        CustomerUnit.Message = ex.Message;
        //        CustomerUnit.LstData = null;
        //    }
        //    return CustomerUnit;
        //}
         
        //public ResponseStatus<CustomerReferral> getCustomerReferralList(CustomerReferral Obj)
        //{
        //    ResponseStatus<CustomerReferral> objResponse = new ResponseStatus<CustomerReferral>();
        //    try
        //    {
        //        objResponse = Common.GetCustomerReferralList(Obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "GetCustomerReferralList(Token=" + Obj.Token + ",  CustomerId=" + Obj.CustomerId + ")", Obj.Token);
        //        objResponse.ErrorCode = 417;
        //        objResponse.Message = ex.Message;
        //        objResponse.Status = false;
        //        objResponse.LstData = null;
        //    }
        //    return objResponse;
        //}

        //public ResponseStatus<Getquery> getudp_getquery(CustomerDemand objuser)
        //{
        //    ResponseStatus<Getquery> getquery = new ResponseStatus<Getquery>();
        //    try
        //    {
        //        List<Getquery> getqueryList = new List<Getquery>();
        //        getqueryList = Common.Getudp_getquery(objuser.Token);

        //        if (getqueryList.Count() > 0)
        //        {
        //            getquery.Status = true;
        //            getquery.ErrorCode = 200;
        //            getquery.Message = MessageClass.sRecordFound;
        //            getquery.LstData = getqueryList;
        //        }
        //        else
        //        {
        //            getquery.Status = true;
        //            getquery.ErrorCode = 404;
        //            getquery.Message = MessageClass.sRecordNotFound;
        //            getquery.LstData = getqueryList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "Customer Getudp_getquery( Token=" + objuser.Token + ")", objuser.Token);
        //        getquery.ErrorCode = 417;
        //        getquery.Message = ex.Message;
        //        getquery.LstData = null;
        //    }
        //    return getquery;
        //}

        //public ResponseStatus<InsertqueryStatus> getudp_InsertqueryHistory(InsertqueryHistory ObjInsertqueryHistory)
        //{
        //    ResponseStatus<InsertqueryStatus> InsertqueryHistory = new ResponseStatus<InsertqueryStatus>();
        //    try
        //    {
        //        string fileName = "File_" + DateTime.Now.Ticks + "_" + ObjInsertqueryHistory.FileName;
        //        string domain = "";// HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);

        //        //string physicalPath = HttpContext.Current.Server.MapPath(domain) + "/API/Queries/" + ObjInsertqueryHistory.Token + "/";
        //        string physicalPath = PortalAppSettingMethods.GetRootPath() + @"\Queries\" + ObjInsertqueryHistory.Token + @"\";
        //        if (!Directory.Exists(physicalPath))
        //        {
        //            Directory.CreateDirectory(physicalPath);
        //        }


        //        Common.UploadImage(ObjInsertqueryHistory.FileContent, physicalPath + "/" + fileName);

        //        ObjInsertqueryHistory.RootDomain = domain + "/Queries/" + ObjInsertqueryHistory.Token + "/";
        //        ObjInsertqueryHistory.FileName = fileName;

        //        List<InsertqueryStatus> InsertqueryHistoryList = new List<InsertqueryStatus>();
        //        //InsertqueryHistoryList = Common.InsertqueryHistory(objuser.Token, objuser.queryid, objuser.text, objuser.createdby, objuser.mailstatus, objuser.regid);
        //        InsertqueryHistoryList = Common.InsertqueryHistory(ObjInsertqueryHistory);

        //        if (InsertqueryHistoryList.Count() > 0)
        //        {
        //            InsertqueryHistory.Status = true;
        //            InsertqueryHistory.ErrorCode = 200;
        //            InsertqueryHistory.Message = MessageClass.sInsertqueryHistoryS;
        //            InsertqueryHistory.LstData = InsertqueryHistoryList;
        //        }
        //        else
        //        {
        //            InsertqueryHistory.Status = true;
        //            InsertqueryHistory.ErrorCode = 200;
        //            InsertqueryHistory.Message = MessageClass.sInsertqueryHistoryN;
        //            InsertqueryHistory.LstData = InsertqueryHistoryList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "Customer InsertqueryHistory( Token=" + ObjInsertqueryHistory.Token + ",Queryid=" + ObjInsertqueryHistory.queryid + ",Text=" + ObjInsertqueryHistory.text + ",Createdby=" + ObjInsertqueryHistory.createdby + ",Mailstatus=" + ObjInsertqueryHistory.mailstatus + ",Regid=" + ObjInsertqueryHistory.regid + ")", ObjInsertqueryHistory.Token);
        //        InsertqueryHistory.ErrorCode = 417;
        //        InsertqueryHistory.Message = ex.Message;
        //        InsertqueryHistory.LstData = null;
        //    }
        //    return InsertqueryHistory;
        //}

        //public ResponseStatus<FAQList> getFAQ(CustomerLoginList objuser)
        //{
        //    ResponseStatus<FAQList> CustomerUnit = new ResponseStatus<FAQList>();
        //    try
        //    {
        //        List<FAQList> CList = new List<FAQList>();
        //        DataTable dtFaq = Common.GetFAQ(objuser.Token);
        //        var Faq = Common.JSONWithStringBuilderFAQ(dtFaq);
        //        CList = JsonConvert.DeserializeObject<List<FAQList>>(Faq);
        //        if (CList.Count() > 0)
        //        {
        //            CustomerUnit.Status = true;
        //            CustomerUnit.ErrorCode = 200;
        //            CustomerUnit.Message = MessageClass.sRecordFound;
        //            CustomerUnit.LstData = CList;
        //        }
        //        else
        //        {
        //            CustomerUnit.Status = true;
        //            CustomerUnit.ErrorCode = 404;
        //            CustomerUnit.Message = MessageClass.sRecordNotFound;
        //            CustomerUnit.LstData = CList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "Customer GetFAQ( Token=" + objuser.Token + ")", objuser.Token);
        //        CustomerUnit.ErrorCode = 417;
        //        CustomerUnit.Message = ex.Message;
        //        CustomerUnit.LstData = null;
        //    }
        //    return CustomerUnit;
        //}

        //public ResponseStatus<NoData> saveCustomerReferral(CustomerReferral Obj)
        //{
        //    ResponseStatus<NoData> objResponse = new ResponseStatus<NoData>();
        //    try
        //    {
        //        objResponse = Common.SaveCustomerReferral(Obj);
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "SaveCustomerReferral( Token=" + Obj.Token + ",  CustomerId=" + Obj.CustomerId + ")", Obj.Token);
        //        objResponse.ErrorCode = 417;
        //        objResponse.Message = ex.Message;
        //        objResponse.Status = false;
        //        objResponse.LstData = null;
        //    }
        //    return objResponse;
        //}

        //public ResponseStatus<ConstructionUpdate> getConstruction(CustomerLoginList objuser)
        //{
        //    ConstructionUpdate objConstruction;
        //    ResponseStatus<ConstructionUpdate> objStatus = new ResponseStatus<ConstructionUpdate>();
        //    try
        //    {
        //        objConstruction = new ConstructionUpdate();
        //        DataSet dsProject = Common.GetConstructionProject(objuser.Token, objuser.RegistrationId);
        //        DataSet dsTower = Common.GetConstructionTower(objuser.Token, objuser.RegistrationId);

        //        if (dsProject.Tables[0].Rows.Count > 0 || dsTower.Tables[0].Rows.Count > 0)
        //        {
        //            if (objuser.RegistrationId >= 0)
        //            {
        //                objConstruction.ProjctList = Common.GetProjectList(dsProject.Tables[0], objuser.Token);
        //                objConstruction.TowerList = Common.GetTowerList(dsTower.Tables[0], objuser.Token);

        //                objStatus.Data = objConstruction;
        //            }

        //            objStatus.Status = true;
        //            objStatus.ErrorCode = 200;
        //            objStatus.Message = MessageClass.sRecordFound;
        //            objStatus.LstData = null;
        //        }
        //        else
        //        {
        //            objStatus.Status = true;
        //            objStatus.ErrorCode = 404;
        //            objStatus.Message = MessageClass.sRecordNotFound;
        //            objStatus.LstData = null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, " Customer GetConstruction( Token=" + objuser.Token + ",RegistrationId=" + objuser.RegistrationId + ")", objuser.Token);
        //        objStatus.Status = false;
        //        objStatus.ErrorCode = 417;
        //        objStatus.Message = ex.Message;
        //        objStatus.Data = null;
        //        objStatus.LstData = null;
        //        return objStatus;
        //    }
        //    return objStatus;
        //}

        //public ResponseStatus<Notification> getNotification(CustomerLoginList objuser)
        //{
        //    ResponseStatus<Notification> Notification = new ResponseStatus<Notification>();
        //    try
        //    {
        //        List<Notification> NotificationList = new List<Notification>();
        //        DataSet ds = Common.GetNotificationjson(objuser.Token);
        //        DataTable dtnotifi = ds.Tables[0];
        //        var notification = Common.JSONWithStringBuilderNotification(dtnotifi);

        //        NotificationList = JsonConvert.DeserializeObject<List<Notification>>(notification);
        //        if (NotificationList!=null && NotificationList.Count() > 0)
        //        {
        //            Notification.Status = true;
        //            Notification.ErrorCode = 200;
        //            Notification.Message = MessageClass.sNotificationS;
        //            Notification.LstData = NotificationList;
        //        }
        //        else
        //        {
        //            Notification.Status = true;
        //            Notification.ErrorCode = 404;
        //            Notification.Message = MessageClass.sNotificationN;
        //            Notification.LstData = NotificationList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "Customer GetNotification( Token=" + objuser.Token + ")", objuser.Token);
        //        Notification.ErrorCode = 417;
        //        Notification.Message = ex.Message;
        //        Notification.LstData = null;
        //    }
        //    return Notification;
        //}

        //public ResponseStatus<CustomerNameMobile> getCustomerNameMobile(CustomerDemand objuser)
        //{
        //    ResponseStatus<CustomerNameMobile> CustomerMobile = new ResponseStatus<CustomerNameMobile>();
        //    try
        //    {
        //        List<CustomerNameMobile> CustomerMobileList = new List<CustomerNameMobile>();
        //        CustomerMobileList = Common.GetCustomerNameMobile(objuser.Token, objuser.PortalId);

        //        if (CustomerMobileList.Count() > 0)
        //        {

        //            CustomerMobile.Status = true;
        //            CustomerMobile.ErrorCode = 200;
        //            CustomerMobile.Message = MessageClass.sRecordFound;
        //            CustomerMobile.LstData = CustomerMobileList;
        //        }
        //        else
        //        {
        //            CustomerMobile.Status = true;
        //            CustomerMobile.ErrorCode = 404;
        //            CustomerMobile.Message = MessageClass.sRecordNotFound;
        //            CustomerMobile.LstData = CustomerMobileList;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "Customer GetCustomerNameMobile( Token=" + objuser.Token + ",  PortalId=" + objuser.PortalId + ")", objuser.Token);
        //        CustomerMobile.ErrorCode = 417;
        //        CustomerMobile.Message = ex.Message;
        //        CustomerMobile.LstData = null;
        //    }
        //    return CustomerMobile;
        //}



        //public List<HomePageDisplay> getHomePagesList1(DataTable dtHomePagesList1, string Token, string ImageURL)
        //{
        //    List<HomePageDisplay> list;
        //    try
        //    {
        //        list = new List<HomePageDisplay>(
        //                   (from dRow in dtHomePagesList1.AsEnumerable()
        //                    select (GetdtHomePagesList1Row(dRow, Token, ImageURL)))
        //                   );
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList1 + ",Token=" + Token + ")", Token);
        //        return null;
        //    }
        //    return list;
        //}

        //public HomePageDisplay GetdtHomePagesList1Row(DataRow dr, string Token, string ImageURL)
        //{
        //    HomePageDisplay objDisplay;
        //    try
        //    {
        //        objDisplay = new HomePageDisplay();
        //        var path = ImageURL + "/APPImages/HomePages/";
        //        objDisplay.Display = path + dr["Display"].ToString();
        //        objDisplay.Types = dr["Type"].ToString();
        //        objDisplay.Postion = int.Parse(dr["Postion"].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "GetdtHomePagesList1Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
        //        throw ex;
        //    }
        //    return objDisplay;
        //}

        //public List<HomePageUnit> getHomePagesList2(DataTable dtHomePagesList2, string Token)
        //{
        //    List<HomePageUnit> list;
        //    try
        //    {
        //        list = new List<HomePageUnit>(
        //                   (from dRow in dtHomePagesList2.AsEnumerable()
        //                    select (GetdtHomePagesList2Row(dRow, Token)))
        //                   );
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList2 + ",Token=" + Token + ")", Token);
        //        return null;
        //    }
        //    return list;
        //}

        //public static HomePageUnit GetdtHomePagesList2Row(DataRow dr, string Token)
        //{
        //    HomePageUnit objUnit;
        //    try
        //    {
        //        objUnit = new HomePageUnit();
        //        objUnit.Registration_Id = Convert.ToInt32(dr["Registration_Id"].ToString());
        //        objUnit.Project = dr["Project"].ToString();
        //        objUnit.UnitNo = dr["Unit No"].ToString();
        //        objUnit.Dues = JsonConvert.DeserializeObject<string>("\"" + dr["Dues"].ToString() + "\" ");

        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "GetdtHomePagesList2Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
        //        throw ex;
        //    }
        //    return objUnit;
        //}

        //public List<HomePagesIcon> getHomePagesList3(DataTable dtHomePagesList3, string Token, string ImageURL)
        //{
        //    List<HomePagesIcon> list;
        //    try
        //    {
        //        list = new List<HomePagesIcon>(
        //                   (from dRow in dtHomePagesList3.AsEnumerable()
        //                    select (GetdtHomePagesList3Row(dRow, Token, ImageURL)))
        //                   );
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "List<MainFollowupType> GetMainFollowupType(DataTable =" + dtHomePagesList3 + ",Token=" + Token + ")", Token);
        //        return null;
        //    }
        //    return list;
        //}

        //public HomePagesIcon GetdtHomePagesList3Row(DataRow dr, string Token, string ImageURL)
        //{
        //    HomePagesIcon objIcon;
        //    var path = ImageURL + "/APPImages/Header/";
        //    //var path = "http://apexthekremlin.realeasy.in/APPImages/Header/";
        //    try
        //    {
        //        objIcon = new HomePagesIcon();
        //        objIcon.Icon = path + dr["Icon"].ToString();
        //        objIcon.Name = dr["Name"].ToString();
        //        objIcon.Position = int.Parse(dr["Position"].ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.LogExceptionSubject(ex, "GetdtHomePagesList3Row(DataRow =" + dr + ",Token=" + Token + ")", Token);
        //        throw ex;
        //    }
        //    return objIcon;
        //}

    }
}
