using FourQT.Entities;
using FourQT.Entities.General;
using MobAppCoreAPI.Interfaces.General;
using FourQT.CommonFunctions.Portal;
using FourQT.CommonFunctions;
using FourQT.Entities.Portal;
using System.Net;
using FourQT.UserRights;
using FourQT.Entities.Employee;
using Newtonsoft.Json;
using FourQT.Core;

namespace MobAppCoreAPI.Repository.General
{
    public class CommonLoginRepository : ICommonLogin
    {
        public async Task<dynamic> forgotPassword(ForgotPasswordRequest model, HttpContext context)
        {
            return await ForgotPasswordDLL.forgotPasword(model, context);
        }

        public async Task<APIObjectResponse> getCommonLogin(CommonLoginRequest objuser,HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            string message = JsonConvert.SerializeObject(objuser);
            Log.LogPayloadDateWise(message, "CommonLogin", context);

            if (objuser.type == "C")
            {
                string portalKey = PortalAppSettingMethods.GetCustomerPortalKey(Cryptography.Decrypt(objuser.Token));
                try
                {
                    List<Customeruserlist> PList = new List<Customeruserlist>();
                    CustomerPortalCommonLoginResponse customer = new CustomerPortalCommonLoginResponse();
                    PList = await Common.GetCustomerLogin(portalKey, objuser.username, objuser.password, objuser.DeviceId, objuser.FromDevice);

                    customer.commonLoginType = "C";

                    if (PList.Count() > 0)
                    {
                        customer.id = PList[0].CustomerId;
                        customer.type = PList[0].Type;
                        customer.status = PList[0].Status;
                        customer.photo = PList[0].Profile_Photo;
                        customer.companyLogo = PList[0].Company_Logo;
                        customer.sendOtpForLeadRegister = "N";
                        customer.status = 0;
                        customer.type = "";
                        customer.uploadCustomerPhoto = "N";
                        customer.sendWhatsAppForLeadRegister = "N";
                        customer.qrCode = "";
                        customer.hRuserId = 0;
                        customer.attendenceModule = false;
                        customer.enableInventoryOperationsCP = false;
                        customer.enableInventory = false;

                        genResponse.IsSuccess = true;
                        genResponse.Status = HttpStatusCode.OK;
                        genResponse.Message= MessageClass.sLoginS;
                        genResponse.Title = "Success";

                        int customerId = 0;
                        Int32.TryParse(Convert.ToString(PList[0].CustomerId), out customerId);

                        customer.token= new UserRightsBLL().GeneratePortalToken(portalKey, customerId);
                        genResponse.Data = customer;

                        await Common.SetJWTokenPortal(portalKey, customerId, customer.token);
                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = HttpStatusCode.Unauthorized;
                        genResponse.Message = MessageClass.sLoginF;
                        genResponse.Title = "Invalid Login";
                    }

                }
                catch (Exception ex)
                {
                    Log.LogExceptionSubject(ex, "User CheckLogin( Token=" + portalKey + ",  UserID=" + objuser.username + ")", portalKey);
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.ExpectationFailed;
                    genResponse.Message = ex.Message;
                    genResponse.Title = "Error";
                }
            }
            else if (objuser.type == "E")
            {
                EmployeeCommonLoginResponse employee = new EmployeeCommonLoginResponse();

                try
                {
                    EmployeeLoginRequest emp = new EmployeeLoginRequest();
                    emp.userName = objuser.username;
                    emp.password = objuser.password;
                    emp.fcmToken = objuser.DeviceId;
                    emp.location = objuser.Location;
                    emp.ipAddress = objuser.ipAddress;
                    emp.token = objuser.Token;

                    if (objuser.FromDevice == 1) { emp.deviceType = "A"; }
                    else if (objuser.FromDevice == 2) { emp.deviceType = "I"; }
                    else { emp.deviceType = ""; }

                    return await (new EmployeeUserRightsBLL().EmployeeLoginAsync(emp));
                }
                catch (Exception ex) {                   
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.ExpectationFailed;
                    genResponse.Message = ex.Message;
                    genResponse.Title = "Error";
                } 
               
            }
            else if (objuser.type == "CP")
            {
                try
                {
                    LoginRequestDTO login = new LoginRequestDTO();
                    login.userName = (objuser.username != null ? objuser.username.ToString() : "");
                    login.password = (objuser.password != null ? objuser.password.ToString() : "");
                    login.fcmToken = (objuser.DeviceId != null ? objuser.DeviceId.ToString() : "");
                    login.deviceType = objuser.FromDevice.ToString();
                    login.location = objuser.Location;
                    login.ipAddress = objuser.ipAddress;
                    string eKey = (objuser.Token != null ? objuser.Token.ToString() : "");

                    return await (new ChannelPartnerUserRightsBLL()).CPLoginAsync(login, eKey);
                }
                catch(Exception ex)
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.ExpectationFailed;
                    genResponse.Message = ex.Message;
                    genResponse.Title = "Error";
                }
            }
            else
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.ExpectationFailed;
                genResponse.Message = "Invalid login type.";
                genResponse.Title = "Error";                
            }

            return genResponse;
        }

    }
}
