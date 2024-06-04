using System.Xml.Linq;
using FourQT.CommonFunctions;
using FourQT.CommonFunctions.Portal;
using FourQT.Entities;
using FourQT.Entities.Portal;
using FourQT.UserRights;
using MobAppCoreAPI.Attributes.Portal_P2;
using MobAppCoreAPI.Interfaces.Portal_P2;
using Nancy;

namespace MobAppCoreAPI.Repository.Portal
{
    public class LoginRepositoryPortal_P2 : IPortalLogin_P2
    {     

        public async Task<ResponseStatusToken_New<Customeruserlist>> customerLogin(CustomerLoginShort objuser)
        {
            ResponseStatusToken_New<Customeruserlist> Process = new ResponseStatusToken_New<Customeruserlist>();
            string portalKey = PortalAppSettingMethods.GetCustomerPortalKey(Cryptography.Decrypt(objuser.Token));

            try
            {
                List<Customeruserlist> PList = new List<Customeruserlist>();
                PList = await Common.GetCustomerLogin(portalKey, objuser.username, objuser.password, objuser.DeviceId, objuser.FromDevice);

                if (PList.Count() > 0)
                {
                    Process.IsSuccess = true;
                    Process.Status = System.Net.HttpStatusCode.OK;
                    Process.Message = MessageClass.sLoginS;
                    Process.LstData = PList;
                    Process.Title = "Success";

                    int customerId = 0;
                    Int32.TryParse(Convert.ToString(PList[0].CustomerId),out customerId);
                    Process.token = new UserRightsBLL().GeneratePortalToken(portalKey,customerId);

                    await Common.SetJWTokenPortal(portalKey, customerId, Process.token);
                }
                else
                {
                    Process.IsSuccess = true;
                    Process.Status = System.Net.HttpStatusCode.OK;
                    Process.Message = MessageClass.sLoginF;
                    Process.Title = "Fail";
                    Process.LstData = PList;
                }

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User CheckLogin( Token=" + portalKey + ",  UserID=" + objuser.username + ")", portalKey);
                Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                Process.IsSuccess = false;
                Process.Message = ex.Message;
                Process.Title = "Error";
                Process.LstData = null;
            }
            return Process;
        }
       
        public async Task<ResponseStatus_New<ChangePasswordList>> customerChangePassword(ChangePasswordNew objuser, HttpRequest req)
        {
            ResponseStatus_New<ChangePasswordList> Process = new ResponseStatus_New<ChangePasswordList>();

            string portalKey = "NoToken";       
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(req, out int customerId, out portalKey);

                if (customerId != 0 && objuser.NewPassword != null && objuser.OldPassword != null && 
                    Convert.ToString(objuser.NewPassword)!= Convert.ToString(objuser.OldPassword) && Convert.ToString(objuser.NewPassword).Length >= 6)
                {
                    objuser.UserName = (objuser.UserName != null ? objuser.UserName : "");

                    List<ChangePasswordList> PList = new List<ChangePasswordList>();
                    PList = await Common.ChangePassword(portalKey, objuser.UserName, objuser.OldPassword, objuser.NewPassword, customerId);

                    if (PList.Count() > 0)
                    {
                        Process.IsSuccess = true;
                        Process.Status = System.Net.HttpStatusCode.OK;
                        Process.Message = MessageClass.sChangePasswordS;
                        Process.Title = "Success";
                        Process.LstData = PList;
                    }
                    else
                    {
                        Process.IsSuccess = false;
                        Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                        Process.Message = MessageClass.sChangePasswordF;
                        Process.Title = "Fail";
                        Process.LstData = PList;      
                    }
                }
                else
                {
                    Process.IsSuccess = false;
                    Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                    Process.Message = MessageClass.invalidNewPassword;
                    Process.Title = "Fail";
                    Process.LstData = null;
                }
               
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User CheckLogin( Token=" + portalKey + ",  username=" + objuser.UserName + ")", portalKey);
                Process.IsSuccess = false;
                Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                Process.Message = ex.Message;
                Process.Title = "Error";
            }
            return Process;
        }

        public async Task<ResponseStatus_New<NoData>> getOTP(CustomerLogin objuser, HttpRequest req)
        {
            ResponseStatus_New<NoData> Process = new ResponseStatus_New<NoData>();
            JWTTokenMethods jwt = new JWTTokenMethods();
            string dKey = jwt.GetTokenPortal(req);
            try
            {
                string mobile = await Common.GetRegisteredMobile(dKey, (objuser.username != null ? objuser.username : ""));
                if (!String.IsNullOrEmpty(mobile))
                {
                    // send sms
                    int randomNum = (new Random()).Next();
                    string message = "Your OTP to change/set password is " + randomNum;
                    if (await Common.SendSMS(dKey, mobile, message))
                    {
                        Process.IsSuccess = true;
                        Process.Status = System.Net.HttpStatusCode.OK;
                        Process.Message = "OTP sent to Registered Mobile.";
                        Process.Title = "Success";
                        Process.LstData = null;
                    }
                    else
                    {
                        Process.IsSuccess = false;
                        Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                        Process.Message = "Failure sending OTP!";
                        Process.Title = "Fail";
                        Process.LstData = null;
                    }
                }

            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User GetOTP(Token=" + dKey + ",  UserID=" + objuser.username + ")", dKey);
                Process.IsSuccess = false;
                Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                Process.Message = ex.Message;
                Process.Title = "Error";
                Process.LstData = null;
            }
            return Process;
        }

        public async Task<ResponseStatus_New<InsertqueryStatus>> CustomerLogOut(CustomerCore1 objuser, HttpRequest req)
        {
            ResponseStatus_New<InsertqueryStatus> Process = new ResponseStatus_New<InsertqueryStatus>();
            List<InsertqueryStatus> list = new List<InsertqueryStatus>();

            string portalKey = "NoToken";
            try
            {
                (new JWTTokenMethods()).GetConnectionDetailsCustomer(req, out int customerId, out portalKey);

                if (portalKey != "" && portalKey != "NoToken")
                {
                    await Common.SetJWTokenPortalLogout(portalKey, customerId);

                    list.Add(new InsertqueryStatus
                    {
                        Status = 1

                    });
                }

                Process.IsSuccess = true;
                Process.Status = System.Net.HttpStatusCode.OK;
                Process.Message = MessageClass.sLogOutS;
                Process.Title = "Success";
                Process.LstData = list;
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "Customer LogOut( Token=" + portalKey + ")", portalKey);
                Process.IsSuccess = false;
                Process.Status = System.Net.HttpStatusCode.ExpectationFailed;
                Process.Message = ex.Message;
                Process.Title = "Error";
                Process.LstData = null;
            }
            return Process;
        }
    }
}
