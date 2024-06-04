using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Drawing;

namespace FourQT.Reports
{
    public class ReportsBLL
    {
        public async Task<dynamic> GetAllReportList(string Key, int login_Id, string moduleTtype)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<Report> rep = new List<Report>();
            try
            {
                string spName = "API_Adm_IUDS_AppModule";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",Size= 500},
                    new SqlParameter() { ParameterName = "@Action", Value =  "S"},
                    new SqlParameter() { ParameterName = "@Module_Type", Value = (moduleTtype != null ? moduleTtype.ToString().Trim().ToUpper() : "") },
                    new SqlParameter() { ParameterName = "@Login_ID", Value = login_Id}                
                };
                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;
                
                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0) {
                    DataTable dt = ds.Tables[0];
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            Report l1 = new Report();
                            l1.id = (Int32.TryParse(rows["App_Id"].ToString(), out int id) ? id : 0);
                            l1.moduleName = (rows["Module_Name"] != null ? rows["Module_Name"].ToString() : "");
                            l1.moduleType = (rows["Module_Type"] != null ? rows["Module_Type"].ToString() : "");
                            l1.processName = (rows["Process_Name"] != null ? rows["Process_Name"].ToString() : "");
                            l1.processType = (rows["Process_Type"] != null ? rows["Process_Type"].ToString() : "");
                            l1.iconUrl = (rows["IconUrl"] != null ? rows["IconUrl"].ToString() : "");
                            //l1.link = (rows["Link"] != null ? rows["Link"].ToString() : "");
                            rep.Add(l1);
                        }
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Data = rep;
                genresponse.IsSuccess = true;
                genresponse.Title = "Success";
            }
            catch(Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Data = null;
                genresponse.IsSuccess = false;
                genresponse.Title = "Error";

            }
            return genresponse;
        }

        public async Task<dynamic> GetLeadStatus(string Key, int login_Id, LeadStatusReportRequest model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<LeadStatusReportResponse> rep = new List<LeadStatusReportResponse>();
            try
            {
                string spName = "API_Get_LeadStatus";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@EmpName", SqlDbType = SqlDbType.VarChar, Size=50, Value = (model.employeeName!=null?model.employeeName.ToString().Trim():"") }
                };


                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if(ds!=null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            LeadStatusReportResponse row = new LeadStatusReportResponse();
                            //row.employeeId = (Int32.TryParse(rows["EMP_ID"].ToString(), out int id) ? id : 0);
                            row.loginId = (Int32.TryParse(rows["Login_Id"].ToString(), out int id) ? id : 0);
                            row.loginType = (rows["Login_Type"] != null ? rows["Login_Type"].ToString() : "");
                            row.empName = (rows["EMP_NAME"] != null ? rows["EMP_NAME"].ToString() : "");
                            row.Active = (rows["Active"] != null ? rows["Active"].ToString() : "");
                            //row.mId = (Int32.TryParse(rows["MID"].ToString(), out id) ? id : 0);
                            row.manager = (rows["MANAGER"] != null ? rows["MANAGER"].ToString() : "");
                            row.hOd = (rows["HOD"] != null ? rows["HOD"].ToString() : "");
                            row.location = (rows["LOCATION"] != null ? rows["LOCATION"].ToString() : "");
                            row.empCode = (rows["Emp_code"] != null ? rows["Emp_code"].ToString() : "");
                            row.newLeads = (Int32.TryParse(rows["NEW"].ToString(), out id) ? id : 0);
                            row.todayLeads = (Int32.TryParse(rows["TODAY"].ToString(), out id) ? id : 0);
                            row.pendingLeads = (Int32.TryParse(rows["PENDING"].ToString(), out id) ? id : 0);
                            row.futureLeads = (Int32.TryParse(rows["FUTURE"].ToString(), out id) ? id : 0);
                            row.attemptedLeads = (Int32.TryParse(rows["ATTEMPTED"].ToString(), out id) ? id : 0);
                            row.totalActiveLeads = (Int32.TryParse(rows["TOTAL_ACTIVE"].ToString(), out id) ? id : 0);
                            row.rejectedDumpLeads = (Int32.TryParse(rows["REJECTED_DUMP"].ToString(), out id) ? id : 0);
                            row.successLeads = (Int32.TryParse(rows["SUCCESS"].ToString(), out id) ? id : 0);
                            row.totalLeads = (Int32.TryParse(rows["TOTAL"].ToString(), out id) ? id : 0);
                            row.filterUserLoginType = ((dt.Columns.Contains("Filtered_User_Login_Type") && rows["Filtered_User_Login_Type"] != null) ? rows["Filtered_User_Login_Type"].ToString() : "");

                            rep.Add(row);
                        }
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }

        public async Task<dynamic> GetLoginWiseEnquiryCount(string Key, int login_Id, LoginWiseEnqCountRequest model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<LoginWiseEnquiryCountResponse> rep = new List<LoginWiseEnquiryCountResponse>();
            try
            {
                string spName = "GetEnqueryCount_LoginWise";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@sCurrentPageSize",  SqlDbType = SqlDbType.Int, Value = model.currentPageSize},
                    new SqlParameter() { ParameterName = "@sPageSize",  SqlDbType = SqlDbType.Int, Value = model.pageSize},
                    new SqlParameter() { ParameterName = "@CommaSeparatedSource_Ids", SqlDbType = SqlDbType.VarChar, Size = 500, Value = (model.sourceIds!=null?model.sourceIds.ToString().Trim():"") },
                    new SqlParameter() { ParameterName = "@EmployeeID",  SqlDbType = SqlDbType.Int, Value = model.employeeId},
                    new SqlParameter() { ParameterName = "@projectId",  SqlDbType = SqlDbType.Int, Value = model.projectId},
                    new SqlParameter() { ParameterName = "@EmpType", SqlDbType = SqlDbType.Char, Size=1, Value = (model.employeeType!=null?model.employeeType.ToString():"") },
                    new SqlParameter() { ParameterName = "@EmployeeIDs", SqlDbType = SqlDbType.VarChar, Size = 200, Value = (model.employeeIds!=null?model.employeeIds.ToString():"") },
                    new SqlParameter() { ParameterName = "@IncludeCampaign",  SqlDbType = SqlDbType.Bit, Value = model.includeCampaign},
                    new SqlParameter() { ParameterName = "@Campaign_Id",  SqlDbType = SqlDbType.Int, Value = model.campaignId},
                    new SqlParameter() { ParameterName = "@ResponseId",  SqlDbType = SqlDbType.Int, Value = model.responseId},
                    new SqlParameter() { ParameterName = "@SubResponseId",  SqlDbType = SqlDbType.Int, Value = model.subResponseId},
                    new SqlParameter() { ParameterName = "@RevivalType", SqlDbType = SqlDbType.VarChar, Size=1, Value = (model.revivalType!=null?model.revivalType.ToString():"") },
                    new SqlParameter() { ParameterName = "@FromDate", SqlDbType = SqlDbType.VarChar, Size=10, Value = (model.startDate!=null?model.startDate.ToString().Trim(): "") },
                    new SqlParameter() { ParameterName = "@ToDate", SqlDbType = SqlDbType.VarChar, Size = 10, Value = (model.endDate != null ? model.endDate.ToString().Trim() : "") }
                };
                                
                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            LoginWiseEnquiryCountResponse row = new LoginWiseEnquiryCountResponse();
                            row.categoryName = (rows["Category_Name"] != null ? rows["Category_Name"].ToString() : "");
                            row.rowNumber = (Int32.TryParse(rows["RowNumber"].ToString(), out int id) ? id : 0);
                            row.totalRowsCount = (Int32.TryParse(rows["TotalRowsCount"].ToString(), out id) ? id : 0);
                            row.lngSort = (Int32.TryParse(rows["lngSort"].ToString(), out id) ? id : 0);
                            row.sourceEnqName = (rows["Source_Enq_Name"] != null ? rows["Source_Enq_Name"].ToString() : "");
                            row.emAddId = (Int32.TryParse(rows["EM_Add_Id"].ToString(), out id) ? id : 0);
                            row.totalLeads = (Int32.TryParse(rows["TEnq"].ToString(), out id) ? id : 0);
                            row.newLeads = (Int32.TryParse(rows["TOEnq"].ToString(), out id) ? id : 0);
                            row.pEnq = (Int32.TryParse(rows["PEnq"].ToString(), out id) ? id : 0);
                            row.rejectedDumpLeads = (Int32.TryParse(rows["FEnq"].ToString(), out id) ? id : 0);
                            row.successLeads = (Int32.TryParse(rows["CEnq"].ToString(), out id) ? id : 0);
                            row.attemptedLeads = (Int32.TryParse(rows["Attempted"].ToString(), out id) ? id : 0);
                            row.sThrough = (Int32.TryParse(rows["sThrough"].ToString(), out id) ? id : 0);
                            row.todayLeads = (Int32.TryParse(rows["Today"].ToString(), out id) ? id : 0);
                            row.pendingLeads = (Int32.TryParse(rows["Pending"].ToString(), out id) ? id : 0);
                            row.futureLeads = (Int32.TryParse(rows["Future"].ToString(), out id) ? id : 0);
                            row.qualified = (Int32.TryParse(rows["Qualified"].ToString(), out id) ? id : 0);
                            row.sVDone = (Int32.TryParse(rows["SVDone"].ToString(), out id) ? id : 0);
                            row.campaign = (rows["Campaign"] != null ? rows["Campaign"].ToString() : "");
                            row.loginType = (rows["Login_Type"] != null ? rows["Login_Type"].ToString() : "");

                            rep.Add(row);
                        }
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;
        }

        public async Task<dynamic> GetEnquiryTypeReport(string Key, int login_Id)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<GetEnquiryTypeReportResponse> rep = new List<GetEnquiryTypeReportResponse>();
            try
            {
                string spName = "API_Enquiry_Type_Report";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@Month", SqlDbType = SqlDbType.Int, Value = 0 },
                    new SqlParameter() { ParameterName = "@Year", SqlDbType = SqlDbType.Int, Value = 0 }
                };


                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetEnquiryTypeReportResponse row = new GetEnquiryTypeReportResponse();
                            row.enquiryType = (rows["Enquiry_Type"] != null ? rows["Enquiry_Type"].ToString() : "");                           
                            row.newLeads = (Int32.TryParse(rows["New"].ToString(), out int id) ? id : 0);
                            row.todayLeads = (Int32.TryParse(rows["Today"].ToString(), out id) ? id : 0);
                            row.pendingLeads = (Int32.TryParse(rows["Pending"].ToString(), out id) ? id : 0);
                            row.futureLeads = (Int32.TryParse(rows["Future"].ToString(), out id) ? id : 0);
                            row.attemptedLeads = (Int32.TryParse(rows["Attempted"].ToString(), out id) ? id : 0);
                            row.rejectedDumpLeads = (Int32.TryParse(rows["Dump"].ToString(), out id) ? id : 0);
                            row.successLeads = (Int32.TryParse(rows["Success"].ToString(), out id) ? id : 0);
                            row.totalLeads = (Int32.TryParse(rows["Total"].ToString(), out id) ? id : 0);
                            row.loginType = (rows["Login_Type"] != null ? rows["Login_Type"].ToString() : "");

                            rep.Add(row);
                        }
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }

        public async Task<dynamic> GetEmployeewiseEnquiryToday(string Key, int login_Id, GetEmployeewiseEnquiry_TodayRequest model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<GetEmployeewiseEnquiry_TodayResponse> rep = new List<GetEmployeewiseEnquiry_TodayResponse>();
            try
            {
                string spName = "API_GetEmployeewiseEnquiry_Today";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@Type", SqlDbType = SqlDbType.VarChar, Size = 1, Value = model.type }
                };


                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetEmployeewiseEnquiry_TodayResponse row = new GetEmployeewiseEnquiry_TodayResponse();
                            row.loginId = (Int32.TryParse(rows["Login_Id"].ToString(), out int id) ? id : 0);
                            row.empName = (rows["EmpName"] != null ? rows["EmpName"].ToString() : "");
                            row.loginType = (rows["LOG_TYPE"] != null ? rows["LOG_TYPE"].ToString() : "");
                            row.todayFollowUp = (Int32.TryParse(rows["TodayFollowup"].ToString(), out id) ? id : 0);
                            row.period9amTo12am = (Int32.TryParse(rows["9AM-12AM"].ToString(), out id) ? id : 0);
                            row.period12amTo3pm = (Int32.TryParse(rows["12AM-3PM"].ToString(), out id) ? id : 0);
                            row.period3pmTo6pm = (Int32.TryParse(rows["3PM-6PM"].ToString(), out id) ? id : 0);
                            row.period6pmTo9pm = (Int32.TryParse(rows["6PM-9PM"].ToString(), out id) ? id : 0);
                            row.todayTransferToMe = (Int32.TryParse(rows["Today_Transferred_To_Me"].ToString(), out id) ? id : 0);
                            row.todayTransferByMe = (Int32.TryParse(rows["Today_Transferred"].ToString(), out id) ? id : 0);
                            row.todayWorked = (Int32.TryParse(rows["TODAY_CALLEDUP"].ToString(), out id) ? id : 0);
                            row.newLeads = (Int32.TryParse(rows["New"].ToString(), out id) ? id : 0);
                            row.svCvDone = (Int32.TryParse(rows["sv_cv_done"].ToString(), out id) ? id : 0);
                            row.newUploaded = (Int32.TryParse(rows["NewUploaded"].ToString(), out id) ? id : 0);
                            row.newNotUploaded = (Int32.TryParse(rows["NEWNotUploaded"].ToString(), out id) ? id : 0);
                            row.totalActiveLeads = (Int32.TryParse(rows["TotalActive"].ToString(), out id) ? id : 0);
                            row.filterUserLoginType = (dt.Columns.Contains("Filtered_User_Login_Type") && rows["Filtered_User_Login_Type"] != null ? rows["Filtered_User_Login_Type"].ToString() : "");

                            rep.Add(row);
                        }
                    }
                }

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }

        public async Task<dynamic> GetMiscellaneousReports_Master(string Key, int login_Id)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<GetMiscellaneousReportsResponse_Master> rep = new List<GetMiscellaneousReportsResponse_Master>();

            try
            {
                string spName = "API_GetMiscellaneousReports_Master";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id}
                };

                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetMiscellaneousReportsResponse_Master row = new GetMiscellaneousReportsResponse_Master();
                            row.id = (Int32.TryParse(rows["Id"].ToString(), out int id) ? id : 0);
                            row.name = (rows["ReportName"] != null ? rows["ReportName"].ToString() : "");                           
                            row.reportType = (rows["ReportType"] != null ? rows["ReportType"].ToString() : "");
                            row.directCallToLeadLisitng = (Boolean.TryParse(rows["directCallToLeadLisitng"].ToString(), out Boolean call) ? call : false);
                            rep.Add(row);
                        }
                    }
                }              

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }

        public async Task<dynamic> GetMiscellaneousReports(string Key, int login_Id, GetMiscellaneousReportsRequest model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<GetMiscellaneousReportsResponse> rep = new List<GetMiscellaneousReportsResponse>();
            List<GetMiscellaneousReportsResponse> sideRep = new List<GetMiscellaneousReportsResponse>();
            GetMiscellaneousReportsResponseWrap repObj = new GetMiscellaneousReportsResponseWrap();

            try
            {
                string spName = "API_GetMiscellaneousReports";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@ReportType", SqlDbType = SqlDbType.VarChar, Size = 5, Value = model.reportType },
                    new SqlParameter() { ParameterName = "@Param1", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param1 },
                    new SqlParameter() { ParameterName = "@Param2", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param2 },
                    new SqlParameter() { ParameterName = "@Param3", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param3 },
                    new SqlParameter() { ParameterName = "@Param4", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param4 }
                };

                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetMiscellaneousReportsResponse row = new GetMiscellaneousReportsResponse();
                            row.id = (Int32.TryParse(rows["Id"].ToString(), out int id) ? id : 0);
                            row.name = (rows["Name"] != null ? rows["Name"].ToString() : "");
                            row.value = (Int32.TryParse(rows["Value"].ToString(), out id) ? id : 0);
                            row.loginType = (rows["LoginType"] != null ? rows["LoginType"].ToString() : "");
                            rep.Add(row);
                        }
                    }
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetMiscellaneousReportsResponse row = new GetMiscellaneousReportsResponse();
                            row.id = (Int32.TryParse(rows["Id"].ToString(), out int id) ? id : 0);
                            row.name = (rows["Name"] != null ? rows["Name"].ToString() : "");
                            row.value = (Int32.TryParse(rows["Value"].ToString(), out id) ? id : 0);
                            row.loginType = (rows["LoginType"] != null ? rows["LoginType"].ToString() : "");
                            sideRep.Add(row);
                        }
                    }
                }

                string totalLeads = "";
                if (ds != null && ds.Tables.Count > 2)
                {
                    DataTable dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        totalLeads = (dt.Rows[0]["TotalLeads"] != null ? dt.Rows[0]["TotalLeads"].ToString() : "");
                    }
                }

                repObj.mainReport = rep;
                repObj.sideReport = sideRep;
                repObj.totalLeads = totalLeads;
                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = repObj;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }

        public async Task<dynamic> GetMiscellaneousReports_Emp(string Key, int login_Id, GetMiscellaneousReportsRequest_Emp model)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genresponse = new APIObjectResponse();
            List<GetMiscellaneousReportsResponse_Emp> rep = new List<GetMiscellaneousReportsResponse_Emp>();

            try
            {
                string spName = "API_GetMiscellaneousReports_Emp";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", SqlDbType = SqlDbType.Int, Value = login_Id},
                    new SqlParameter() { ParameterName = "@ReportType", SqlDbType = SqlDbType.VarChar, Size = 5, Value = model.reportType },
                    new SqlParameter() { ParameterName = "@Param1", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param1 },
                    new SqlParameter() { ParameterName = "@Param2", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param2 },
                    new SqlParameter() { ParameterName = "@Param3", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param3 },
                    new SqlParameter() { ParameterName = "@Param4", SqlDbType = SqlDbType.VarChar, Size = 200, Value = model.param4 }
                };

                ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow rows in dt.Rows)
                        {
                            GetMiscellaneousReportsResponse_Emp row = new GetMiscellaneousReportsResponse_Emp();
                            row.id = (Int32.TryParse(rows["Id"].ToString(), out int id) ? id : 0);
                            row.name = (rows["Name"] != null ? rows["Name"].ToString() : "");
                            row.value = (Int32.TryParse(rows["Value"].ToString(), out id) ? id : 0);
                            row.loginType = (rows["LoginType"] != null ? rows["LoginType"].ToString() : "");
                            rep.Add(row);
                        }
                    }
                }              

                genresponse.Status = HttpStatusCode.OK;
                genresponse.Message = "Success";
                genresponse.Title = "Success";
                genresponse.IsSuccess = true;
                genresponse.Data = rep;

            }
            catch (Exception ex)
            {
                genresponse.Status = HttpStatusCode.BadRequest;
                genresponse.Message = ex.ToString();
                genresponse.Title = "Error";
                genresponse.IsSuccess = false;

            }
            return genresponse;

        }
    }
}
