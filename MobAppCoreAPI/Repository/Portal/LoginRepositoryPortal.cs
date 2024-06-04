using FourQT.CommonFunctions.Portal;
using FourQT.Entities.Portal;
using MobAppCoreAPI.Interfaces.Portal;

namespace MobAppCoreAPI.Repository.Portal
{
    public class LoginRepositoryPortal : IPortalLogin
    {
        public ResponseStatus<Customeruserlist> customerLogin(CustomerLogin objuser)
        {
            ResponseStatus<Customeruserlist> Process = new ResponseStatus<Customeruserlist>();
            //try
            //{
            //    List<Customeruserlist> PList = new List<Customeruserlist>();
            //    PList = Common.GetCustomerLogin(objuser.Token, objuser.username, objuser.password, objuser.DeviceId, objuser.FromDevice);

            //    if (PList.Count() > 0)
            //    {
            //        Process.Status = true;
            //        Process.ErrorCode = 200;
            //        Process.Message = MessageClass.sLoginS;
            //        Process.LstData = PList;
            //    }
            //    else
            //    {
            //        Process.Status = true;
            //        Process.ErrorCode = 200;
            //        Process.Message = MessageClass.sLoginF;
            //        Process.LstData = PList;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Log.LogExceptionSubject(ex, "User CheckLogin( Token=" + objuser.Token + ",  UserID=" + objuser.username + ")", objuser.Token);
            //    Process.ErrorCode = 417;
            //    Process.Message = MessageClass.sLoginF;
            //    Process.LstData = null;
            //}
            return Process;
        }

        public ResponseStatus<ChangePasswordList> customerChangePassword(ChangePassword objuser)
        {
            ResponseStatus<ChangePasswordList> Process = new ResponseStatus<ChangePasswordList>();
            //try
            //{
            //    List<ChangePasswordList> PList = new List<ChangePasswordList>();
            //    PList = Common.ChangePassword(objuser.Token, objuser.UserName, objuser.OldPassword, objuser.NewPassword);

            //    if (PList.Count() > 0)
            //    {
            //        Process.Status = true;
            //        Process.ErrorCode = 200;
            //        Process.Message = MessageClass.sChangePasswordS;
            //        Process.LstData = PList;
            //    }
            //    else
            //    {
            //        Process.Status = true;
            //        Process.ErrorCode = 417;
            //        Process.Message = MessageClass.sChangePasswordF;
            //        Process.LstData = PList;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Log.LogExceptionSubject(ex, "User CheckLogin( Token=" + objuser.Token + ",  username=" + objuser.UserName + ")", objuser.Token);
            //    Process.ErrorCode = 417;
            //    Process.Message = ex.Message;
            //    Process.LstData = null;
            //}
            return Process;
        }

        public ResponseStatus<NoData> getOTP(CustomerLogin objuser)
        {
            ResponseStatus<NoData> Process = new ResponseStatus<NoData>();
            //try
            //{
            //    string mobile = Common.GetRegisteredMobile(objuser.Token, objuser.username);
            //    if (!String.IsNullOrEmpty(mobile))
            //    {
            //        // send sms
            //        int randomNum = (new Random()).Next();
            //        string message = "Your OTP to change/set password is " + randomNum;
            //        if (Common.SendSMS(objuser.Token, mobile, message))
            //        {
            //            Process.Status = true;
            //            Process.ErrorCode = 200;
            //            Process.Message = "OTP sent to Registered Mobile.";
            //            Process.LstData = null;
            //        }
            //        else
            //        {
            //            Process.ErrorCode = 417;
            //            Process.Message = "Failure sending OTP!";
            //            Process.LstData = null;
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    Log.LogExceptionSubject(ex, "User GetOTP(Token=" + objuser.Token + ",  UserID=" + objuser.username + ")", objuser.Token);
            //    Process.ErrorCode = 417;
            //    Process.Message = "Failure sending OTP!";
            //    Process.LstData = null;
            //}
            return Process;
        }
    }
}
