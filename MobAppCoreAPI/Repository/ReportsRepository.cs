using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using FourQT.CommonFunctions;
using FourQT.Masters;
using FourQT.Entities;
using FourQT.Reports;
using FourQT.Utilities;
using System;

namespace MobAppCoreAPI.Repository
{
    public class ReportsRepository :IReports
    {
        public async Task<APIObjectResponse> GetAllReportList(HttpRequest req,string moduleType)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                generalResponse = await (new ReportsBLL()).GetAllReportList(mKey, loginId, moduleType);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/reports");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
                return generalResponse;
            }
        }

        public async Task<APIObjectResponse> GetLeadStatus(HttpRequest req, LeadStatusReportRequest model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetLeadStatus(mKey, loginId, model);               
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetLeadStatus");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetLoginWiseEnquiryCount(HttpRequest req, LoginWiseEnqCountRequest model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetLoginWiseEnquiryCount(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetLoginWiseEnquiryCount");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetEnquiryTypeReport(HttpRequest req, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetEnquiryTypeReport(mKey, loginId);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetEnquiryTypeReport");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetEmployeewiseEnquiryToday(HttpRequest req, GetEmployeewiseEnquiry_TodayRequest model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetEmployeewiseEnquiryToday(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetEmployeewiseEnquiryToday");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetMiscellaneousReports_Master(HttpRequest req, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetMiscellaneousReports_Master(mKey, loginId);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetMiscellaneousReports_Master");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetMiscellaneousReports(HttpRequest req, GetMiscellaneousReportsRequest model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await(new ReportsBLL()).GetMiscellaneousReports(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetMiscellaneousReports");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }

        public async Task<APIObjectResponse> GetMiscellaneousReports_Emp(HttpRequest req, GetMiscellaneousReportsRequest_Emp model, HttpContext context)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);

                generalResponse = await (new ReportsBLL()).GetMiscellaneousReports_Emp(mKey, loginId, model);
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/GetMiscellaneousReports_Emp");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }
    }
}
