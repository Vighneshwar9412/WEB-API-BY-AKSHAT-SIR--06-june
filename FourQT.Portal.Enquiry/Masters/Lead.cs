using System.Data;
using FourQT.CommonFunctions.Portal;
using FourQT.DAL.Portal;
using FourQT.Utilities.Portal;
using FourQT.Entities.Portal.Masters;
using FourQT.Entities.Portal.Core;
using FourQT.Entities.Portal;

namespace FourQT.Portal.Enquiry.Masters
{
    public class Lead
    {
        #region Enquiry

        public static DataTable GetPendingFollowupEnquiryAdmin(string Token, int LoginID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetPendingFollowupEnquiryAdmin(ConnString, LoginID, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Master.Lead.GetPendingFollowupEnquiryAdmin(Token=" + Token + ", LoginID=" + LoginID + ")", Token);
                return null;
            }
        }

        public static DataTable GetEmployeeEnquiryList(string Token, int EmpID, string Type)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEmployeeEnquiryList(ConnString, Token, EmpID, Type);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEmployeeEnquiryList( Token=" + Token + ",EmpID=" + EmpID + ", Type=" + Type + ")", Token);
                return null;
            }
        }

        public static DataTable GetPreSaleEmployeeList(string Token, int LoginID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetPreSaleEmployeeList(ConnString, Token, LoginID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetPreSaleEmployeeList( Token=" + Token + ", LoginID=" + LoginID + ")", Token);
                return null;
            }
        }

        public static int TransferEnquiry(string Token, string EnquiryIDs, string TransferToEmpID, int LoginID, string Remark
                , string Latitude, string Longitude, DateTime LocationTime)
        {
            int retVal = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return retVal;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.TransferEnquiry(ConnString, Token, EnquiryIDs, TransferToEmpID, LoginID, Remark, Latitude, Longitude, LocationTime);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FoourQT.Emquiry.Masters.Lead.TransferEnquiry( Token=" + Token + ",EnquiryIDs=" + EnquiryIDs + ",  TransferToEmpID=" + TransferToEmpID + ",LoginID=" + LoginID + ",Remark=" + Remark + ",Latitude=" + Latitude + ",Longitude=" + Longitude + ",LocationTime=" + LocationTime + ")", Token);
                return retVal;
            }
        }

        public static int SaveNewLead(string Token, SaveNewLead Enq)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveNewEnquiry(ConnString, Token, Enq);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveNewEnquiry(Token=" + Token + ",  LoginID=" + Enq.LoginID + ",ThroughSource=" + Enq.ThroughSource + ",FirstName=" + Enq.FirstName + ",ISDCode=" + Enq.ISDCode + ", MobileNo=" + Enq.MobileNo + ", EmailID=" + Enq.EmailID + ",EnqType=" + Enq.EnqType + ", EnquiryFrom=" + Enq.EnquiryFrom + ", BoundType=" + Enq.BoundType + ", MinBudget=" + Enq.MinBudget + ",MaxBudget=" + Enq.MaxBudget + ",City_Id=" + Enq.City_Id + ",Zone_Ids=" + Enq.Zone_Ids + ", Location_Ids=" + Enq.Location_Ids + ",Locality_Ids=" + Enq.Location_Ids + ",PropertyType_Ids=" + Enq.PropertyType_Ids + ",UnitIDs=" + Enq.Project_UnitType_Ids + ",CreatedBy=" + Enq.LoginID + ",Channel=" + Enq.Channel + ",Latitude=" + Enq.Latitude + ",MinSaleableArea=" + Enq.MinSaleableArea + ",MaxSalableArea=" + Enq.MaxSalableArea + ")", Token);
                return status;
            }
        }

        public static EnquirySaveResponse SaveNewLeadRetEnq(string Token, FourQT.Entities.Portal.Core.Enquiry member)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveNewEnquiryRetEnq(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveNewEnquiryRetEnq(Token=" + Token + ", FourQT.Entities.Core.Enquiry=" + member + ")", Token);
                return null;
            }
        }

        public static DataSet GetProjectUnit(string Token, int LoginID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetProjectUnit(ConnString, Token, LoginID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetProjectUnit(Token=" + Token + ",LoginID=" + LoginID + ")", Token);
                return null;
            }
        }

        public static DataTable GetEnquiryPersonalDetail(string Token, int EnquiryID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryPersonalDetail(ConnString, Token, EnquiryID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryPersonalDetail(Token=" + Token + ",  EnquiryID=" + EnquiryID + ")", Token);
                return null;
            }
        }

        public static DataTable GetCommunication(string Token)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetCommunication(ConnString, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetCommunication(Token=" + Token + ")", Token);
                return null;
            }
        }

        public static DataTable GetDumpMaster(string Token)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetDumpMaster(ConnString, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetDumpMaster(Token=" + Token + ")", Token);
                throw;
            }
        }

        public static DataTable GetEnquirySource(string Token)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquirySource(ConnString, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquirt.Lead.GetEnquirySource(Token=" + Token + ")", Token);
                throw;
            }
        }

        public static DataSet GetEnquiryMasters(string Token)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryMasters(ConnString, Token);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquirt.Masters.Lead.GetEnquiryMasters(Token=" + Token + ")", Token);
                return null;
            }
        }

        #region AllEnquiryMasters
        public static DataSet GetAllEnquiryMasters(string Token, int UserId, int ID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetAllEnquiryMasters(ConnString, Token, UserId, ID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetAllEnquiryMasters(Token=" + Token + ")", Token);
                return null;
            }
        }



        #endregion

        public static DataTable GetEnquiryProject(string Token, int EnquiryID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryProject(ConnString, Token, EnquiryID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryProject(Token=" + Token + ",EnquiryID=" + EnquiryID + ")", Token);
                return null;
            }
        }

        public static DataTable GetEnquiryProjectUnitType(string Token, string ProjectID, int LoginID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryProjectUnitType(ConnString, Token, ProjectID, LoginID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryProjectUnitType(Token=" + Token + ",ProjectID=" + ProjectID + ",LoginID=" + LoginID + ")", Token);
                return null;
            }
        }

        public static DataTable GetEnquiryFollowupDetail(string Token, int EnquiryID)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryFollowupDetail(ConnString, Token, EnquiryID);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryFollowupDetail(Token=" + Token + ",EnquiryID=" + EnquiryID + ")", Token);
                return null;
            }
        }

        public static int SaveDumpEnquiry(string Token, EnquiryFollowupDetailMasters member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveDumpEnquiry(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveDumpEnquiry(Token=" + Token + ",  LoginID=" + member.LoginID + ",EnquiryID=" + member.EnquiryID + ", Remarks=" + member.Remarks + ",CommID=" + member.CommunicationId + ",DumpReasonId=" + member.DumpID + ",Longitude=" + member.Lat + ",Longitude=" + member.Long + ")", Token);
                return status;
            }
        }

        public static int AddFollowUp(string Token, EnquiryFollowupDetailMasters member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.AddFollowUp(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.AddFollowUp(Token=" + Token + ", LoginID=" + member.LoginID + ",EnquiryID=" + member.EnquiryID + ",FollowedDate=" + member.FollowedDate + ", NextFollowedDate=" + member.NextFollowedDate + ",Remarks=" + member.Remarks + ",CommentUnitType=" + member.CommentUnitType + ",Status=" + member.Status + ",ProjectId=" + member.ProjectID + ",FollowType=" + member.FollowType + ",CommID=" + member.CommunicationId + ",Time=" + member.Time + ",TimeFormat=" + member.Timeformat + ",ResponseType=" + member.ResponseType + ",DumpID=" + member.DumpID + ",RegNo=" + member.RegistrationNo + ",ProjectName=" + member.ProjectName + ", MeetingTime=" + member.MeetingTime + ", MeetingDatetime=" + member.MeetingDatetime + ",MeetingAddress=" + member.MeetingAddress + ",LastResponseID=" + member.LastResponseID + ", CallDirection=" + member.CallDirection + ", Type=" + member.Type + ",Tower=" + member.Tower + ",Floor=" + member.Floor + ",UnitNo=" + member.UnitNo + ",SuccessProjectId=" + member.SuccessProjectID + ",SuccessUnittypeId=" + member.SuccessUnitTypeID + ",UnitIds=" + member.UnitIDs + ",MeetingDuration=" + member.MeetingDurationInMinute + ")", Token);
                return status;
            }
        }

        public static int AddShortFollowUp(string Token, AddFollowUp member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.AddShortFollowUp(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.AddShortFollowUp(Token=" + Token + ", LoginID=" + member.LoginID + ",EnquiryID=" + member.EnquiryID + ",FollowedDate=" + member.FollowedDate + ", NextFollowedDate=" + member.NextFollowupDate + ",Remarks=" + member.Remarks + ",Status=" + member.Status + ",FollowType=" + member.FollowType + ",CommID=" + member.CommID + ",Time=" + member.Time + ",TimeFormat=" + member.Timeformat + ",DumpID=" + member.DumpID + ", MeetingTime=" + member.MeetingTime + ", MeetingDatetime=" + member.MeetingDatetime + ",MeetingAddress=" + member.MeetingAddress + ",LastResponseID=" + member.LastResponseID + ",MeetingDuration=" + member.MeetingDuration + ")", Token);
                return status;
            }
        }

        public static int SaveSuccessEnquiry(string Token, EnquiryFollowupDetailMasters member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveSuccessEnquiry(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveSuccessEnquiry(Token=" + Token + ", EnquiryFollowupDetailMasters=" + member + ")", Token);
                return status;
            }
        }

        public static int SaveSMSFollowUpDetails(string Token, SMS member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveSMSFollowUpDetails(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveSMSFollowUpDetails(Token=" + Token + ", Enquiry_Id=" + member.Enquiry_Id + ",Number=" + member.Number + ",SMSSubject_Id=" + member.SMSSubject_Id + ",MessageBody=" + member.MessageBody + ",Login_Id=" + member.Login_Id + ",Latitude=" + member.Lat + ",Longitude=" + member.Long + ")", Token);
                return status;
            }
        }

        public static int SaveEmailFollowUpDetails(string Token, EmailMessage member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveEmailFollowUpDetails(ConnString, Token, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveEmailFollowUpDetails(Token=" + Token + ", Enquiry_Id=" + member.Enquiry_Id + ",Subject_Id=" + member.Subject_Id + ",Subject=" + member.Subject + ",MessageBody=" + member.MessageBody + ",Login_Id=" + member.Login_Id + ",CC=" + member.CC + ",To=" + member.To + ",BCC=" + member.BCC + ",Latitute=" + member.Lat + ",Longitute=" + member.Long + ",CommaSeparatedAttachmentFileNames=" + member.CommaSeparatedFileNames + ")", Token);
                return status;
            }
        }

        public static int UpdatePartyDetails(string Token, int Login_Id, FourQT.Entities.Portal.Core.Enquiry member)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.UpdatePartyDetails(ConnString, Token, Login_Id, member);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.UpdatePartyDetails(Token=" + Token + ",Login_Id=" + Login_Id + ", LoginID=" + member.LoginID + ",EnquiryID=" + member.EnquiryID + ",FirstName=" + member.FirstName + ",Email=" + member.EmailID + ",Latitude=" + member.Latitude + ",Longitude=" + member.Latitude + ")", Token);
                return status;
            }
        }
        #endregion

        #region SaveHR Convance

        public static int SaveHRLogin(LoginInfoHR.AddHrAttendance jsnObject)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(jsnObject.Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveHRLogin(ConnString, jsnObject.Token, jsnObject);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveHRLogin(Token=" + jsnObject.Token + ",LoginID=" + jsnObject.LoginID + ",AttendanceDate=" + jsnObject.AttendanceDate + ",XCoordinate=" + jsnObject.XCoOrdinate + ",YCoordinate=" + jsnObject.YCoOrdinate + ",ipaddress=" + jsnObject.IPAdress + ",Type=" + jsnObject.Type + ",Remarks=" + jsnObject.Remarks + ",Latitude=" + jsnObject.Latitude + ",Longitude= " + jsnObject.Longitude + ",LocationTime=" + jsnObject.LocationTime + ",imageBase64=" + jsnObject.imageBase64 + ")", jsnObject.Token);
                return status;
            }
        }

        #endregion

        #region Save Call Record
        public static int SaveCallRecord(LoginInfoHR.AgentCallDetailMaster jsnObject)
        {
            int status = -1;
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(jsnObject.Token);
                if (Equals(ConnString, null) == true)
                {
                    return status;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.SaveCallRecord(ConnString, jsnObject.Token, jsnObject);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.SaveCallRecord(Token=" + jsnObject.Token + ",PhoneNo=" + jsnObject.PhoneNo + ",CallTime=" + jsnObject.CallTime + ",PhoneMacAdd=" + jsnObject.PhoneMacAdd + ",Xco_oridinate" + jsnObject.Xco_oridinate + ",YCo_oridinate=" + jsnObject.YCo_oridinate + ",duration=" + jsnObject.callRecord + ",LoginID=" + jsnObject.LoginID + ")", jsnObject.Token);
                return status;
            }
        }
        #endregion

        #region Get Lead

        public static DataSet GetEnquiryListByType(string Token, int Login_Id, string Type, string Login_Type, int PageIndex, int PageSize, out int totalRecords)
        {
            string ConnString = "";
            totalRecords = 0;
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryList(ConnString, Token, Login_Id, Type, Login_Type, PageIndex, PageSize, out totalRecords);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryListByType(Token=" + Token + ",  Login_Id=" + Login_Id + ",  Type=" + Type + ",  Login_Type=" + PageIndex + ",  PageIndex=" + PageIndex + ", PageSize=" + PageSize + ", out int totalRecords=" + totalRecords + ")", Token);
                return null;
            }
        }

        public static DataSet GetEnquiryListByTypeText(string Token, int Login_Id, string Type, string Login_Type, string miscFilter, int PageIndex, int PageSize, out int totalRecords)
        {
            string ConnString = "";
            totalRecords = 0;
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.GetEnquiryList(ConnString, Token, Login_Id, Type, Login_Type, PageIndex, PageSize, out totalRecords);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.GetEnquiryListByType(Token=" + Token + ",  Login_Id=" + Login_Id + ",  Type=" + Type + ",  Login_Type=" + PageIndex + ",  PageIndex=" + PageIndex + ", PageSize=" + PageSize + ", out int totalRecords=" + totalRecords + ")", Token);
                return null;
            }
        }

        #endregion

        #region Check Duplicate

        public static bool CheckDuplicate(string Token, string M1, string M2, string M3, string LL, out string Message)
        {
            string ConnString = "";
            Message = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return false;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.CheckDuplicate(ConnString, Token, M1, M2, M3, LL, out Message);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.CheckDuplicate(Token=" + Token + ", M1=" + M1 + ", M2=" + M2 + ",M3=" + M3 + ", LL=" + LL + ", out string Message=" + Message + ")", Token);
                return false;
            }
        }

        #endregion

        #region

        public static DataSet getProject(string Token, int Login_Id, string Type, string LL)
        {
            string ConnString = "";
            try
            {
                ConnString = Common.CheckToken(Token);
                if (Equals(ConnString, null) == true)
                {
                    return null;
                }
                else
                {
                    return FourQT.DAL.Portal.DAL.getProject(Token, Login_Id, Type, LL, ConnString);
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "FourQT.Enquiry.Masters.Lead.getProject(Token=" + Token + ",  Login_Id=" + Login_Id + ",  Type=" + Type + ",  LL=" + LL + ")", Token);
                return null;
            }
        }

        #endregion
    }
}
