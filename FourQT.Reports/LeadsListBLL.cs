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
    public class LeadsListBLL
    {
        public async Task<dynamic> GetAllLeads(string Key,int LoginId, LeadFilters lll)
        {
            DataSet ds = new DataSet();
            LeadsListResponseModel leads = new LeadsListResponseModel();
            string spName = "API_GetLeadList";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Total_Records", Value = 0},
                new SqlParameter() { ParameterName = "@Login_Id", Value = LoginId},
                new SqlParameter() { ParameterName = "@Login_Type", Value = lll.LoginType },
                new SqlParameter() { ParameterName = "@Page_Index", Value = lll.PageIndex},
                new SqlParameter() { ParameterName = "@Page_Size", Value = lll.PageSize},
                new SqlParameter() { ParameterName = "@Source_Id", Value = lll.Source_Id},
                new SqlParameter() { ParameterName = "@SStatus", Value = lll.SStatus},
                new SqlParameter() { ParameterName = "@EDF", Value = lll.EnquiryDateFrom},
                new SqlParameter() { ParameterName = "@EDT", Value = lll.EnquiryDateTo},
                new SqlParameter() { ParameterName = "@LFDF", Value = lll.LastFollowedDateFrom},
                new SqlParameter() { ParameterName = "@LFDT", Value = lll.LastFollowedDateTo},
                new SqlParameter() { ParameterName = "@NFDF", Value = lll.NexFollowupDateFrom},
                new SqlParameter() { ParameterName = "@NFDT", Value = lll.NexFollowupDateTo},
                new SqlParameter() { ParameterName = "@EnqType", Value = lll.EnquiryType},
                new SqlParameter() { ParameterName = "@SVStatus", Value = lll.SiteVisitStatus},
                new SqlParameter() { ParameterName = "@Filtered_User_Id", Value = lll.filteredUserId},
                new SqlParameter() { ParameterName = "@Searchtext",Size=200, Value = (lll.searchText!=null?lll.searchText.ToString().Trim():"")},
                new SqlParameter() { ParameterName = "@Project_Id", Value = lll.projectId},
                new SqlParameter() { ParameterName = "@last_Sub_Response_Id", Value = lll.lastSubResponseId },
                new SqlParameter() { ParameterName = "@Filtered_Login_Type", Value = lll.filterUserLoginType },
                new SqlParameter() { ParameterName = "@Last_Response_Id", Value = lll.lastResponseId },
                new SqlParameter() { ParameterName = "@Dump_Id", Value = lll.dumpId }
            };

            lstParam[0].Direction = ParameterDirection.Output;
            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                LeadsList l1 = new LeadsList();
                l1.enquiryId = Convert.ToInt32(rows["Enquiry_Id"].ToString());
                l1.name = rows["Name"].ToString();
                l1.mobileNo1 = rows["Mobile_No1"].ToString();
                l1.emailId1 = rows["Email_Id1"].ToString();
                l1.status = rows["Status"].ToString();
                l1.source = rows["Source"].ToString();
                l1.campaign = rows["Campaign"].ToString();
                l1.displayEnquiryDate = rows["Enquiry_Date"].ToString();
                l1.response = rows["Response"].ToString();
                l1.subResponse = rows["SubResponse"].ToString();
                l1.displayLastFollowedDate = Convert.ToString(rows["Last_Followed_Date"]);
                l1.displayNextFollowupDate = Convert.ToString(rows["Next_Followup_Date"]);
                l1.project = rows["Project"].ToString();
                l1.handler = rows["Handler"].ToString();
                l1.owner = rows["Owner"].ToString();
                l1.enquiryType = rows["Enquiry_Type"].ToString();
                l1.remarks = rows["Remarks"].ToString();
                l1.sVDone = (bool)rows["SV_CV_Done"];
                l1.dumpReason = rows["DumpReason"].ToString();
                l1.projectUnitType = rows["Project_UnitType"].ToString();
                l1.unitNo = rows["UnitNo"].ToString();
                l1.displayArea= rows["display_Area"].ToString();
                l1.displayCost = rows["display_Cost"].ToString();
                l1.field1 = rows["Field1"].ToString();
                l1.field2 = rows["Field2"].ToString();
                l1.field3 = rows["Field3"].ToString();
                l1.FUT = Convert.ToInt32(rows["FUT"].ToString());
                l1.enquiryTypeId = Convert.ToInt32(rows["EnqType_Id"].ToString());
                l1.bookingDate = rows["Booking_Date"].ToString();
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
                l1.DisplayNumber = (rows["Display_Mobile_No"] != null ? rows["Display_Mobile_No"].ToString() : "");

                l1.bookedCustName = (ds.Tables[0].Columns.Contains("BookingCustName") && rows["BookingCustName"] != null ? rows["BookingCustName"].ToString() : "");

                leads.LeadList.Add(l1);

            }
            leads.TotalRecords = lstParam[0].Value ==null ? 0 : Convert.ToInt32(lstParam[0].Value);
            return leads;
        }

        public async Task<dynamic> GetLeadDetails(string Key, int LoginId, int enquiryId)
        {
            DataSet ds = new DataSet();
            LeadDetailsResponse l1 = new LeadDetailsResponse();
            string spName = "API_GetLeadDetails";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@LOGIN_ID", Value = LoginId},
                new SqlParameter() { ParameterName = "@Enquiry_Id", Value = enquiryId }
            };

            ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

            if (ds != null && ds.Tables.Count > 0) {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
                    DataRow rows = ds.Tables[0].Rows[0];

                    l1.enqCityId = (Int32.TryParse(rows["Enquiry_City_Id"].ToString(), out int id) ? id : 0);
                    l1.enqcityName = (rows["CityName"] != null ? rows["CityName"].ToString() : "");
                    l1.enqFromId = (Int32.TryParse(rows["EnqFrom_Id"].ToString(), out id) ? id : 0);
                    l1.enqFromName = (rows["EnquiryFrom"] != null ? rows["EnquiryFrom"].ToString() : "");
                    l1.minBudget = (Int32.TryParse(rows["Min_Budget"].ToString(), out id) ? id : 0);
                    l1.maxBudget = (Int32.TryParse(rows["Max_Budget"].ToString(), out id) ? id : 0);
                    l1.minSaleableArea = (Decimal.TryParse(rows["Min_Saleable_Area"].ToString(), out decimal area) ? area : 0);
                    l1.maxSaleableArea = (Decimal.TryParse(rows["Max_Saleable_Area"].ToString(), out area) ? area : 0);
                    l1.projectIds = (rows["Projects_Ids"] != null ? rows["Projects_Ids"].ToString() : "");
                    l1.projectNames = (rows["Project_Names"] != null ? rows["Project_Names"].ToString() : "");
                    l1.unitTypeIds = (rows["Global_Unit_Type_Ids"] != null ? rows["Global_Unit_Type_Ids"].ToString() : "");
                    l1.unitTypeNames = (rows["Global_Unit_Type_Names"] != null ? rows["Global_Unit_Type_Names"].ToString() : "");
                    l1.minBudgetId = (Int32.TryParse(rows["Min_Budget_Id"].ToString(), out id) ? id : 0);
                    l1.maxBudgetId = (Int32.TryParse(rows["Max_Budget_Id"].ToString(), out id) ? id : 0);
                }
            }
           
            return l1;
        }
    }
}
