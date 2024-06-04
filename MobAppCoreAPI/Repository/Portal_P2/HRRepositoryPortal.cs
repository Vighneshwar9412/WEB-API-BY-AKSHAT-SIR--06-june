using System.Data;
using FourQT.CommonFunctions;
using FourQT.CommonFunctions.Portal;
using FourQT.Entities.HR;
using MobAppCoreAPI.Interfaces.Portal_P2;

namespace MobAppCoreAPI.Repository.Portal
{
    public class HRRepositoryPortal : IPortalHR
    {
        public List<AttendanceResponseModel> HRAttendanceLogin(AttendanceRequestModel objAttendanceRequest,HttpRequest request)
        {
            List<AttendanceResponseModel> HRAttendanceList = new List<AttendanceResponseModel>();
            //DataSet ds = new DataSet();
            //string XLMDetails = string.Empty;
            //string portalKey = (new JWTTokenMethods()).GetTokenPortal(request);
            //try
            //{
            //    int hrCheck = await Common.CheckHRAttendanceLogin(portalKey);

            //    if (hrCheck == 0)
            //    {
            //        if (objAttendanceRequest.AttendanceList.Count > 0)
            //        {
            //            XLMDetails = "<ROWS>";
            //            for (int i = 0; i < objAttendanceRequest.AttendanceList.Count; i++)
            //            {
            //                XLMDetails += "<ROW>";
            //                XLMDetails += "<EMPCODE>" + objAttendanceRequest.AttendanceList[i].EmpCode + "</EMPCODE>";
            //                XLMDetails += "<ATTENDANCEDATE>" + objAttendanceRequest.AttendanceList[i].AttendanceDate + "</ATTENDANCEDATE>";
            //                XLMDetails += "<ATTENDANCETIME>" + objAttendanceRequest.AttendanceList[i].AttendanceTime + "</ATTENDANCETIME>";
            //                XLMDetails += "<TYPE>" + objAttendanceRequest.AttendanceList[i].Type + "</TYPE>";
            //                XLMDetails += "<REMARK>" + objAttendanceRequest.AttendanceList[i].Remark + "</REMARK>";
            //                XLMDetails += "</ROW>";
            //            }
            //            XLMDetails += "</ROWS>";
            //            ds = Common.InsertAttendanceAndGetDetails(portalKey, XLMDetails);
            //            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            //            {
            //                foreach (DataRow row in ds.Tables[0].Rows)
            //                {
            //                    HRAttendanceList.Add(new AttendanceResponseModel
            //                    {
            //                        EmpCode = row["EmpCode"].ToString(),
            //                        ErrorCode = Convert.ToInt32(row["ErrorCode"].ToString()),
            //                        Message = row["Message"].ToString(),
            //                        Status = Convert.ToBoolean(row["Status"].ToString())
            //                    });
            //                }
            //            }
            //        }
            //        else
            //        {
            //            HRAttendanceList.Add(new AttendanceResponseModel
            //            {
            //                EmpCode = "",
            //                ErrorCode = 200,
            //                Message = "Pass User attendance list",
            //                Status = true
            //            });
            //        }
            //    }
            //    else if (Common.CheckHRAttendanceLogin(portalKey) == 1)
            //    {
            //        HRAttendanceList.Add(new AttendanceResponseModel
            //        {
            //            EmpCode = "",
            //            ErrorCode = 200,
            //            Message = "Token does not exists !!",
            //            Status = true
            //        });
            //    }
            //    else
            //    {
            //        HRAttendanceList.Add(new AttendanceResponseModel
            //        {
            //            EmpCode = "",
            //            ErrorCode = 417,
            //            Message = "Some thing went wrong, please contact to Administrator!!",
            //            Status = false
            //        });
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.LogExceptionSubject(ex, "User HRAttendanceLogin( Token=" + portalKey + ")", portalKey);
            //    HRAttendanceList.Add(new AttendanceResponseModel
            //    {
            //        EmpCode = "",
            //        ErrorCode = 417,
            //        Message = "Some thing went wrong, please contact to Administrator!!",
            //        Status = false
            //    });
            //}
            return HRAttendanceList;
        }
    }
}
