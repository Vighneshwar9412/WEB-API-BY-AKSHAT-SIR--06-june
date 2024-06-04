using FourQT.DAL;
using FourQT.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Nancy.Json;
using FourQT.CommonFunctions.Portal;
using FourQT.CommonFunctions;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;
using FourQT.Entities.Construction;
using FourQT.Entities.General;
using FourQT.Core.General;

namespace FourQT.Core.Construction
{
    public class HRBLL
    {
        public async Task<dynamic> getAttendenceDetails(HRAttendenceReportRequestCore model, HttpRequest req, HttpContext context, String Type)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int loginId = 0; string key = "";
            DataSet ds = new DataSet();
            HRAttendence attendence = new HRAttendence();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "GetAttendenceDetails", context);

                if (Type != null)
                {
                    Type = Type.ToString().ToUpper().Trim();
                    if (Type == "L")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "E")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "CP")
                    {
                        (new JWTTokenMethods()).GetConnectionDetailsCP(req, out loginId, out key);
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                        genResponse.Message = "Incorrect Module type.";

                        return genResponse;
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                    genResponse.Message = "Incorrect Module type.";

                    return genResponse;
                }

                string spName = "Api_GetEmployeeAndAttendanceDetail";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@UserId", Value = model.hRuserId }
                    
                };

                ds = await DBHelper.GetDatasetAsyncHR(key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        HRAttendenceEmpDetails empDetails = new HRAttendenceEmpDetails();

                        DataRow row = dt.Rows[0];
                        empDetails.employeeCode = (row["EmployeeCode"] != null ? row["EmployeeCode"].ToString() : "");
                        empDetails.employeeName = (row["EmployeeName"] != null ? row["EmployeeName"].ToString() : "");
                        empDetails.employeeDesignation = (row["Designation"] != null ? row["Designation"].ToString() : "");
                        empDetails.employeeDept = (row["Deparrtment"] != null ? row["Deparrtment"].ToString() : "");
                        empDetails.employeeAddress = (row["Location"] != null ? row["Location"].ToString() : "");
                        empDetails.currentDateTime = (row["CurrentDate"] != null ? row["CurrentDate"].ToString() : "");
                        empDetails.batchTiming = (row["BatchTiming"] != null ? row["BatchTiming"].ToString() : "");
                        empDetails.totalTime = (row["TotalTime"] != null ? row["TotalTime"].ToString() : "");
                        attendence.empDetails = empDetails;
                    }
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        HRAttendenceLoginDetails loginDetails = new HRAttendenceLoginDetails();

                        DataRow row = dt.Rows[0];
                        string? loginEnabled = (row["loginEnabled"] != null ? row["loginEnabled"].ToString() : "");
                        string? logoutEnabled = (row["logoutEnabled"] != null ? row["logoutEnabled"].ToString() : "");
                        string? showLoginData = (row["ShowLoginData"] != null ? row["ShowLoginData"].ToString() : "");
                        string? showLogoutData = (row["ShowLogoutData"] != null ? row["ShowLogoutData"].ToString() : "");
                        loginDetails.loginEnabled = (loginEnabled == "1" ? true : false);
                        loginDetails.logoutEnabled = (logoutEnabled == "1" ? true : false);
                        loginDetails.showLoginData = (showLoginData == "1" ? true : false);
                        loginDetails.showLogoutData = (showLogoutData == "1" ? true : false);
                        loginDetails.loginTime = (row["loginTime"] != null ? row["loginTime"].ToString() : "");
                        loginDetails.logoutTime = (row["logoutTime"] != null ? row["logoutTime"].ToString() : "");
                        loginDetails.loginLocation = (row["loginLocation"] != null ? row["loginLocation"].ToString() : "");
                        loginDetails.logoutLocation = (row["logoutLocation"] != null ? row["logoutLocation"].ToString() : "");
                        loginDetails.loginRemarks = (row["loginRemarks"] != null ? row["loginRemarks"].ToString() : "");
                        loginDetails.logoutRemarks = (row["logoutRemarks"] != null ? row["logoutRemarks"].ToString() : "");
                        loginDetails.loginPhoto = (row["loginPhoto"] != null ? row["loginPhoto"].ToString() : "");
                        loginDetails.logoutPhoto = (row["logoutPhoto"] != null ? row["logoutPhoto"].ToString() : "");
                        loginDetails.workingTimeRequired = (row["RequiredHourMinute"] != null ? row["RequiredHourMinute"].ToString() : "");
                        loginDetails.workingTimeCompleted = (row["TotalHourMinute"] != null ? row["TotalHourMinute"].ToString() : "");
                        loginDetails.logDate = (row["LogDate"] != null ? row["LogDate"].ToString() : "");
                        attendence.loginDetails = loginDetails;
                    }
                }

                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Data = attendence;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
                genResponse.Title = "Error";
            }

            return genResponse;
        }

        public async Task<dynamic> postAttendence(HRAttendenceRequest model,HttpRequest req, HttpContext context, String Type)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int loginId = 0; string key = "";
            DataSet ds = new DataSet();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "PostAttendenceHR", context);

                if (Type != null)
                {
                    Type = Type.ToString().ToUpper().Trim();
                    if (Type == "L")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "E")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "CP")
                    {
                        (new JWTTokenMethods()).GetConnectionDetailsCP(req, out loginId, out key);
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                        genResponse.Message = "Incorrect Module type.";

                        return genResponse;
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                    genResponse.Message = "Incorrect Module type.";

                    return genResponse;
                }

                ServerResponse serverResponse = new ServerResponse();
                serverResponse = await uploadAttendencePhotoToServer(model, req);

                if ((serverResponse != null && serverResponse.isSuccess)|| true)
                {
                    FileUploadResponse? upFiles = serverResponse.uploadedFiles;
                    if ((upFiles != null && upFiles.files != null && upFiles.files.Count >= 1)||true)
                    {
                        string? custPhotoServerName = "";
                        try { custPhotoServerName = upFiles.files[0].fileNameOnServer; }
                        catch { custPhotoServerName = ""; }

                        if (custPhotoServerName != null)
                        {
                            string spName = "Api_usp_hr_AttendanceInsert";
                            List<SqlParameter> lstParam = new List<SqlParameter>
                            {
                                new SqlParameter() { ParameterName = "@Status", Value = 0 },
                                new SqlParameter() { ParameterName = "@MessageStatus", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                                new SqlParameter() { ParameterName = "@UserId", Value = model.hRuserId },
                                new SqlParameter() { ParameterName = "@InputTime", Value = DateTime.Now },
                                new SqlParameter() { ParameterName = "@Remarks",SqlDbType= SqlDbType.VarChar,Size=100, Value = (model.remarks!=null ? model.remarks : "")},
                                new SqlParameter() { ParameterName = "@Mode",SqlDbType= SqlDbType.VarChar,Size=5, Value = (model.loginAction!=null ? model.loginAction : "")},
                                new SqlParameter() { ParameterName = "@AttendanceLocation",SqlDbType= SqlDbType.VarChar,Size=500, Value = (model.location!=null ? model.location : "")},
                                new SqlParameter() { ParameterName = "@AttendanceLocationPhoto", Value = custPhotoServerName.ToString().Trim()},
                            };

                            lstParam[0].Direction = ParameterDirection.Output;
                            lstParam[1].Direction = ParameterDirection.Output;

                            string[] result = await DBHelper.ExecuteNonQueryAsyncHR(key, CommandType.StoredProcedure, spName, lstParam);

                            if (result != null && result.Length > 0)
                            {
                                string resStatus = (result[0] != null ? result[0] : "0");
                                string resMsg = (result[1] != null ? result[1] : "");

                                if (resStatus == "0")
                                {
                                    HRAttendence attendence = new HRAttendence();

                                    genResponse.IsSuccess = true;
                                    genResponse.Status = HttpStatusCode.OK;
                                    genResponse.Title = "Success";
                                    genResponse.Message = resMsg;
                                }
                                else
                                {
                                    genResponse.IsSuccess = false;
                                    genResponse.Status = HttpStatusCode.BadRequest;
                                    genResponse.Title = "Error";
                                    genResponse.Message = "Error: " + resMsg;
                                }
                            }
                            else
                            {
                                genResponse.IsSuccess = false;
                                genResponse.Status = HttpStatusCode.BadRequest;
                                genResponse.Title = "Error";
                                genResponse.Message = "Error posting attendence";
                            }
                        }
                        else
                        {
                            genResponse.IsSuccess = false;
                            genResponse.Message = "Error uploading photo.";
                            genResponse.Data = null;
                            genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                            genResponse.Title = "Error";
                        }
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Message = serverResponse.message;
                        genResponse.Data = null;
                        genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Message = ((serverResponse != null && serverResponse.message != null) ? serverResponse.message : "Error uploading photo.");
                    genResponse.Data = null;
                    genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> getAttendenceReport(HRAttendenceReportRequest model, HttpRequest req, HttpContext context, String Type)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int loginId = 0; string key = "";
            DataSet ds = new DataSet();
            HRAttendenceReportWrap attendence = new HRAttendenceReportWrap();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "GetAttendenceReport", context);

                if (Type != null)
                {
                    Type = Type.ToString().ToUpper().Trim();
                    if (Type == "L")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "E")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "CP")
                    {
                        (new JWTTokenMethods()).GetConnectionDetailsCP(req, out loginId, out key);
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                        genResponse.Message = "Incorrect Module type.";

                        return genResponse;
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                    genResponse.Message = "Incorrect Module type.";

                    return genResponse;
                }

                string spName = "Api_udp_hr_GetEmployeeWiseAttendanceDetail";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@UserId", Value = model.hRuserId },
                    new SqlParameter() { ParameterName = "@Month", Value = model.month },
                    new SqlParameter() { ParameterName = "@Year", Value = model.year }                    
                };

                ds = await DBHelper.GetDatasetAsyncHR(key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<HRAttendenceReport> repList = new List<HRAttendenceReport>();

                        for (int i = 0; i < dt.Rows.Count; i++) {
                            HRAttendenceReport rep = new HRAttendenceReport();
                            DataRow row = dt.Rows[i];

                            rep.hRuserId = (Int32.TryParse(row["sUserId"].ToString(),out int id) ? id : 0);
                            rep.employeeName = (row["EmployeeName"] != null ? row["EmployeeName"].ToString() : "");
                            rep.loginDate = (DateTime.TryParse(row["sDate"].ToString(), out DateTime sDate) ? sDate.ToString("dd MMM yyyy") : "--");
                            rep.loginTime = (row["TimeIn"] != null ? row["TimeIn"].ToString() : "");
                            rep.logoutTime = (row["TimeOut"] != null ? row["TimeOut"].ToString() : "");
                            rep.loginLocation = (row["loginLocation"] != null ? row["loginLocation"].ToString() : "");
                            rep.logoutLocation = (row["logoutLocation"] != null ? row["logoutLocation"].ToString() : "");
                            rep.loginRemarks = (row["EmpInRemarks"] != null ? row["EmpInRemarks"].ToString() : "");
                            rep.logoutRemarks = (row["EmpOutRemarks"] != null ? row["EmpOutRemarks"].ToString() : "");
                            rep.loginPhoto = (row["loginPhoto"] != null ? row["loginPhoto"].ToString() : "");
                            rep.logoutPhoto = (row["logoutPhoto"] != null ? row["logoutPhoto"].ToString() : "");
                            rep.workingTimeRequired = (row["RequiredHourMinute"] != null ? row["RequiredHourMinute"].ToString() : "");
                            rep.workingTimeCompleted = (row["TotalHourMinute"] != null ? row["TotalHourMinute"].ToString() : "");

                            repList.Add(rep);
                        }

                        attendence.attendenceList = repList;                      
                    }                  
                }

                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
                genResponse.Message = "Success";
                genResponse.Data = attendence;
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> uploadAttendencePhotoToServer(HRAttendenceRequest model, HttpRequest req)
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
                    string? customerPhoto = model.photo;
                    string? custPhotoFormat = model.photoFormat;

                    List<FileUpload> fileList = new List<FileUpload>();

                    if (customerPhoto != null && customerPhoto.ToString().Trim() != "")
                    {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = customerPhoto;
                        file.action = "I";
                        file.fileFormat = (custPhotoFormat != null ? custPhotoFormat : "");
                        file.fileGroup = "HR_A_P";
                        file.fileName = "";
                        file.id = 1;

                        fileList.Add(file);
                    }

                    if (fileList != null && fileList.Count > 0)
                    {
                        fileUploadRequest.files = fileList;

                        uploadedDocList = await UploadFilesToExternalServerBLL.SendFilesToExternalServer(fileUploadRequest, req, "HR");
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
                    errorMsg = "Error uploading photo.";
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

        public async Task<dynamic> getAttendenceReportMonths(HttpRequest req, HttpContext context, String Type)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int loginId = 0; string key = "";
            DataSet ds = new DataSet();
            List<HRAttendenceReportMonth> monthList = new List<HRAttendenceReportMonth>();

            try
            {

                if (Type != null)
                {
                    Type = Type.ToString().ToUpper().Trim();
                    if (Type == "L")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "E")
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out key);
                    }
                    else if (Type == "CP")
                    {
                        (new JWTTokenMethods()).GetConnectionDetailsCP(req, out loginId, out key);
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                        genResponse.Message = "Incorrect Module type.";

                        return genResponse;
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                    genResponse.Message = "Incorrect Module type.";

                    return genResponse;
                }

                string spName = "API_GetAttendenceReportMonths";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@LoginId", Value = loginId }
                };

                ds = await DBHelper.GetDatasetAsyncHR(key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            HRAttendenceReportMonth rep = new HRAttendenceReportMonth();
                            DataRow row = dt.Rows[i];

                            rep.month = (Int32.TryParse(row["MonthId"].ToString(), out int id) ? id : 0);
                            rep.year = (Int32.TryParse(row["YearId"].ToString(), out id) ? id : 0);
                            rep.displayMonth = (row["DisplayVal"] != null ? row["DisplayVal"].ToString() : "");

                            monthList.Add(rep);
                        }
                    }
                }

                genResponse.IsSuccess = true;
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
                genResponse.Message = "Success";
                genResponse.Data = monthList;
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        
    }
}
