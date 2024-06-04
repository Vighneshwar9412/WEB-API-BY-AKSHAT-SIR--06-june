using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Reports
{
    public class DashboardBLL
    {
        public async Task<dynamic> GetDashboardWrapper(string Key, int Login_Id)
        {
            DataSet ds = new DataSet();
            Dashboard dash = new Dashboard();

            List<LeadIcon> lstLeadIcons = await GetDashboardIcons(Key, Login_Id);

            TodayLeadsResponseModelNew objTodayLeadsResponseModel = await GetDashboardTodayLeads(Key, Login_Id, -1, 0);

            SiteVisitResponseModelNew objSiteVisitResponseModel = await GetDashboardTodaySiteVisitScheduled(Key, Login_Id, -1, 0);

            dash.leadIconList = lstLeadIcons;
            dash.todayTodoLeadsList = objTodayLeadsResponseModel.todayTodoLeadsList;
            dash.todaySiteVisitScheduledList = objSiteVisitResponseModel.todaySiteVisitScheduledList;

            return dash;
        }

        public async Task<List<LeadIcon>> GetDashboardIcons(string Key, int Login_Id)
        {
            DataSet ds = new DataSet();
            List<LeadIcon> lstIcon = new List<LeadIcon>();

            string spName = "API_GetUserStatusWiseLeads";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
            };

            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                LeadIcon l1 = new LeadIcon();

                l1.loginid = Login_Id;
                l1.iconpath = (rows[2] != null ? rows[2].ToString():"");
                l1.count = (rows[1] != null ? rows[1].ToString() : "");
                l1.status = (rows[0] != null ? rows[0].ToString() : "");
                l1.sortOrder = (ds.Tables[0].Columns.Contains("Ordering") && Int32.TryParse(rows["Ordering"].ToString(), out int ordering) ? ordering : 9999);

                lstIcon.Add(l1);
            }

            lstIcon = lstIcon.OrderBy(l => l.sortOrder).ThenBy(l => l.status).ToList();

            return lstIcon;
        }

        public async Task<TodayLeadsResponseModelNew> GetDashboardTodayLeads(string Key, int Login_Id, int page_index, int page_size)
        {
            DataSet ds = new DataSet();

            List<Dashboard_TodayLeadsNew> lstTodayLeads = new List<Dashboard_TodayLeadsNew>();

            string spName = "API_GetTodayToWorkLeads";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                new SqlParameter() { ParameterName = "@Page_Index", Value = page_index },
                new SqlParameter() { ParameterName = "@Page_Size", Value = page_size },
                new SqlParameter() { ParameterName = "@TotalRecords", Value = 0}
            };
            lstParam[3].Direction = ParameterDirection.Output;
            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            TodayLeadsResponseModelNew lstData = new TodayLeadsResponseModelNew();
            if (ds != null)
            {
                DataTable dt = ds.Tables[0];

                foreach (DataRow rows in dt.Rows)
                {
                    Dashboard_TodayLeadsNew leadsData = new Dashboard_TodayLeadsNew();
                    leadsData.DisplayNumber = rows["Display_Mobile_No"].ToString();
                    leadsData.MobileNo = rows["Mobile_No"].ToString();
                    leadsData.ProjectName = rows["Project_Name"].ToString();
                    leadsData.TodayTime = rows["TodayTime"].ToString();
                    leadsData.SubResponse = rows["SubResponse"].ToString();
                    leadsData.ColorCode = rows["ColorCode"].ToString();
                    leadsData.enquiryId = Convert.ToInt32(rows["sEnqueryid"]);

                    leadsData.status = rows["Status"].ToString();
                    leadsData.source = rows["Source"].ToString();
                    leadsData.campaign = rows["Campaign"].ToString();
                    leadsData.displayEnquiryDate = rows["Enquiry_Date"].ToString();
                    leadsData.response = rows["Response"].ToString();
                    leadsData.SubResponse = rows["SubResponse"].ToString();
                    leadsData.displayLastFollowedDate = rows["Last_Followed_Date"].ToString();
                    leadsData.displayNextFollowupDate = rows["Next_Followup_Date"].ToString();
                    leadsData.handler = rows["Handler"].ToString();
                    leadsData.owner = rows["Owner"].ToString();
                    leadsData.enquiryType = rows["Enquiry_Type"].ToString();
                    leadsData.remarks = rows["Remarks"].ToString();
                    leadsData.sVDone = Convert.ToBoolean(rows["SV_CV_Done"].ToString());
                    leadsData.FUT = Convert.ToInt32(rows["FUT"].ToString());
                    leadsData.enquiryTypeId = Convert.ToInt32(rows["EnqType_Id"].ToString());
                    leadsData.dumpReason = rows["DumpReason"].ToString();
                    leadsData.projectUnitType = rows["Project_Unit"].ToString();
                    leadsData.unitNo = rows["UnitNo"].ToString();
                    leadsData.displayArea = rows["display_Area"].ToString();
                    leadsData.displayCost = rows["display_Cost"].ToString();
                    leadsData.field1 = rows["Field1"].ToString();
                    leadsData.field2 = rows["Field2"].ToString();
                    leadsData.field3 = rows["Field3"].ToString();

                    leadsData.mobileNo1 = rows["Mobile_No"].ToString();
                    leadsData.name = rows["Name"].ToString();
                    leadsData.emailId1 = rows["EmailId"].ToString();
                    leadsData.project = rows["Project_Name"].ToString();

                    leadsData.FUTAll = Convert.ToInt32(rows["FUTAll"]);
                    leadsData.ResponseSequenceNo = (Int32.TryParse(rows["ResponseSequenceNo"].ToString(), out int seq) ? seq : 0);

                    leadsData.salutation = (rows["Salutation"] != null ? rows["Salutation"].ToString() : "");
                    leadsData.firstName = (rows["First_Name"] != null ? rows["First_Name"].ToString() : "");
                    leadsData.lastName = (rows["Last_Name"] != null ? rows["Last_Name"].ToString() : "");
                    leadsData.mobileNo2 = (rows["Mobile_No2"] != null ? rows["Mobile_No2"].ToString() : "");
                    leadsData.mobileNo3 = (rows["Mobile_No3"] != null ? rows["Mobile_No3"].ToString() : "");
                    leadsData.emailId2 = (rows["Email_Id2"] != null ? rows["Email_Id2"].ToString() : "");
                    leadsData.dob = (rows["DOB"] != null ? rows["DOB"].ToString() : "");
                    leadsData.doa = (rows["DOA"] != null ? rows["DOA"].ToString() : "");

                    lstTodayLeads.Add(leadsData);
                }

                lstData.totalRecords = Convert.ToInt32(lstParam[3].Value);
                lstData.todayTodoLeadsList = lstTodayLeads;

                return lstData;
            }
            else
            {
                return null;
            }
        }

        public async Task<SiteVisitResponseModelNew> GetDashboardTodaySiteVisitScheduled(string Key, int Login_Id, int page_index, int page_size)
        {
            try
            {
                DataSet ds = new DataSet();
                SiteVisitResponseModelNew leads = new SiteVisitResponseModelNew();
                string spName = "API_GetTodayToWorkSVSchedule";
                List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                new SqlParameter() { ParameterName = "@Page_Index", Value = page_index},
                new SqlParameter() { ParameterName = "@Page_Size", Value = page_size },
                new SqlParameter() { ParameterName = "@TotalRecords", Value = 0}
            };
                lstParam[3].Direction = ParameterDirection.Output;
                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                foreach (DataRow rows in ds.Tables[0].Rows)
                {
                    SiteVisitNew l1 = new SiteVisitNew();
                    l1.EnquiryId = rows["sEnqueryId"].ToString();
                    l1.DisplayName = rows["Name"].ToString();
                    l1.DisplayMobile = rows["Mobile_No"].ToString();
                    l1.ProjectName = rows["Project_name"].ToString();
                    l1.TodayTime = rows["TodayTime"].ToString();

                    l1.status = rows["Status"].ToString();
                    l1.source = rows["Source"].ToString();
                    l1.campaign = rows["Campaign"].ToString();
                    l1.displayEnquiryDate = rows["Enquiry_Date"].ToString();
                    l1.response = rows["Response"].ToString();
                    l1.subResponse = rows["SubResponse"].ToString();
                    l1.displayLastFollowedDate = rows["Last_Followed_Date"].ToString();
                    l1.displayNextFollowupDate = rows["Next_Followup_Date"].ToString();
                    l1.handler = rows["Handler"].ToString();
                    l1.owner = rows["Owner"].ToString();
                    l1.enquiryType = rows["Enquiry_Type"].ToString();
                    l1.remarks = rows["Remarks"].ToString();
                    l1.sVDone = Convert.ToBoolean(rows["SV_CV_Done"].ToString());
                    l1.FUT = Convert.ToInt32(rows["FUT"].ToString());
                    l1.enquiryTypeId = Convert.ToInt32(rows["EnqType_Id"].ToString());
                    l1.dumpReason = rows["DumpReason"].ToString();
                    l1.projectUnitType = rows["Project_Unit"].ToString();
                    l1.unitNo = rows["UnitNo"].ToString();
                    l1.displayArea = rows["display_Area"].ToString();
                    l1.displayCost = rows["display_Cost"].ToString();
                    l1.field1 = rows["Field1"].ToString();
                    l1.field2 = rows["Field2"].ToString();
                    l1.field3 = rows["Field3"].ToString();

                    l1.mobileNo1 = rows["Mobile_No"].ToString();
                    l1.name = rows["Name"].ToString();
                    l1.emailId1 = rows["EmailId"].ToString();
                    l1.project = rows["Project_Name"].ToString();

                    l1.FUTAll = Convert.ToInt32(rows["FUTAll"].ToString());
                    l1.ResponseSequenceNo = (Int32.TryParse(rows["ResponseSequenceNo"].ToString(), out int seq) ? seq : 0);

                    l1.salutation = (rows["Salutation"] != null ? rows["Salutation"].ToString() : "");
                    l1.firstName = (rows["First_Name"] != null ? rows["First_Name"].ToString() : "");
                    l1.lastName = (rows["Last_Name"] != null ? rows["Last_Name"].ToString() : "");
                    l1.mobileNo2 = (rows["Mobile_No2"] != null ? rows["Mobile_No2"].ToString() : "");
                    l1.mobileNo3 = (rows["Mobile_No3"] != null ? rows["Mobile_No3"].ToString() : "");
                    l1.emailId2 = (rows["Email_Id2"] != null ? rows["Email_Id2"].ToString() : "");
                    l1.dob = (rows["DOB"] != null ? rows["DOB"].ToString() : "");
                    l1.doa = (rows["DOA"] != null ? rows["DOA"].ToString() : "");

                    leads.todaySiteVisitScheduledList.Add(l1);
                }
                leads.totalRecords = Convert.ToInt32(lstParam[3].Value);

                return leads;
            }
            catch {
                throw;
            }
                      
        }

    }
}