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

namespace FourQT.Reports
{
    public class FollowUpBLL
    {
        public async Task<dynamic> followuplisting(string Key, int Login_Id, int Enquiry_ID)
        {
            FollowupResponseModel follow = new FollowupResponseModel();

            try
            {
                string spName = "API_GetFUTList";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                    new SqlParameter() { ParameterName = "@Enquiry_Id", Value = Enquiry_ID}
                };

                DataSet ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    List<FollowUpList> followUps = new List<FollowUpList>();

                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        FollowUpList f1 = new FollowUpList();

                        f1.displayName = rows["Name"].ToString();
                        f1.displayEnquiryDate = rows["Enquiry_Date"].ToString();
                        f1.enquiryType = rows["Enquiry_Type"].ToString();
                        f1.response = rows["Response"].ToString();
                        f1.subResponse = rows["SubResponse"].ToString();
                        f1.displayFollowedDate = rows["Followed_Date"].ToString();
                        f1.followedBy = rows["Followed_By"].ToString();
                        f1.displayStatus = rows["Status"].ToString();
                        f1.displayNextFollowDate = rows["Next_Follow_Date"].ToString();
                        f1.meetingDate = rows["Meeting_Datetime"].ToString();
                        f1.callDirection = rows["Bound_Type"].ToString();
                        f1.projectName = rows["Project_Name"].ToString();
                        f1.unitType = rows["Unit_Type"].ToString();
                        f1.remarks = rows["Remarks"].ToString();
                        f1.recordingUrl = rows["Recording_Url"].ToString();
                        f1.displaySubResponse = (ds.Tables[0].Columns.Contains("DisplaySubResponse") ? rows["DisplaySubResponse"].ToString() : "");
                        f1.source = (ds.Tables[0].Columns.Contains("Source") ? rows["Source"].ToString() : "");
                        f1.meetingDuration = (ds.Tables[0].Columns.Contains("MeetingDuration") ? rows["MeetingDuration"].ToString() : "");

                        followUps.Add(f1);
                    }

                    follow.FollowupList = followUps;
                }
            }
            catch
            {
                throw;
            }

            return follow.FollowupList;
        }

        public async Task<dynamic> GetTodayFollowup_Notification(string Key, int Login_Id)
        {
            FollowupNotificationWrap follow = new FollowupNotificationWrap();

            try
            {               
                string spName = "API_GetTodayFollowupForNotification";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_ID", Value = Login_Id }
                };

                DataSet ds = await DBHelper.GetDatasetAsyncNew(Key, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0)
                {
                    List<FollowupNotification> followups = new List<FollowupNotification>();

                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        FollowupNotification f1 = new FollowupNotification();

                        f1.sno = (Int32.TryParse(rows["S.No"].ToString(), out int id) ? id : 0);
                        f1.mobile = (rows["Mobile"] != null ? rows["Mobile"].ToString() : "");
                        f1.time = (rows["Time"] != null ? rows["Time"].ToString() : "");
                        f1.title = (rows["Title"] != null ? rows["Title"].ToString() : "");
                        f1.subtitle = (rows["SubTitle"] != null ? rows["SubTitle"].ToString() : "");
                        f1.description = (rows["Description"] != null ? rows["Description"].ToString() : "");
                        followups.Add(f1);
                    }

                    followups = followups.OrderBy(f => f.sno).ToList();

                    follow.followupNotifications = followups;
                }
            }
            catch {
                throw;
            }

            return follow;
        }
    }
}
