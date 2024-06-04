using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Xml.Linq;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using FourQT.Entities.General;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;

namespace FourQT.UserRights
{
    public class EmployeeUserRightsBLL
    {
        public async Task<dynamic> EmployeeLoginAsync(EmployeeLoginRequest loginRequestModel)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string keycode = "", userName = "";
                string secretKey = AppSettingMethods.GetSecretKey();
                DataSet ds = new DataSet();

                if (loginRequestModel.token != null)
                {
                    keycode = Cryptography.Decrypt(Convert.ToString(loginRequestModel.token));
                }

                EmployeeCommonLoginResponse loginResponseModel = new EmployeeCommonLoginResponse();
                loginResponseModel.commonLoginType = "E";

                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault();
                if (check == null)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.IsSuccess = false;

                    return genResponse;
                }

                string spName = "API_CheckLogin";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@User_Name", Value = loginRequestModel.userName },
                    new SqlParameter() { ParameterName = "@Password", Value = loginRequestModel.password },
                    new SqlParameter() { ParameterName = "@Output", Value = 0 }
                };
                lstParam[2].Direction = ParameterDirection.Output;

                dynamic[] resArray = await DBHelper.GetDataSetEmployeeAsync(keycode, CommandType.StoredProcedure, spName, lstParam);
                if (resArray != null && resArray.Length > 0) {
                    ds = resArray[0];
                }

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataTable empDetails = ds.Tables[0];
                    DataRow dr = empDetails.Rows[0];

                    loginResponseModel.name = (dr["Emp_Name"] != null ? dr["Emp_Name"].ToString() : "");
                    loginResponseModel.id= (Int32.TryParse(dr["Login_Id"].ToString(), out int id) ? id : 0);
                    loginResponseModel.photo = (dr["EmployeePhoto"] != null ? dr["EmployeePhoto"].ToString() : "");
                    loginResponseModel.mobileNo = (dr["Mobile"] != null ? dr["Mobile"].ToString() : "");
                    loginResponseModel.email = (dr["EmailId"] != null ? dr["EmailId"].ToString() : "");
                    loginResponseModel.companyName = (dr["CompanyName"] != null ? dr["CompanyName"].ToString() : "");
                    loginResponseModel.companyLogo = (dr["CompanyLogo"] != null ? dr["CompanyLogo"].ToString() : "");
                    loginResponseModel.sendOtpForLeadRegister = (dr["sendOtpForLeadRegister"] != null ? dr["sendOtpForLeadRegister"].ToString() : "N");
                    loginResponseModel.uploadCustomerPhoto = (dr["uploadCustomerPhoto"] != null ? dr["uploadCustomerPhoto"].ToString() : "N");
                    loginResponseModel.status = 0;
                    loginResponseModel.type = "";
                    loginResponseModel.sendWhatsAppForLeadRegister = (dr["sendWhatsAppForLeadRegister"] != null ? dr["sendWhatsAppForLeadRegister"].ToString() : "N");
                    loginResponseModel.qrCode = (dr["QRCode"] != null ? dr["QRCode"].ToString() : "");
                    loginResponseModel.hRuserId = (Int32.TryParse(dr["HRuserId"].ToString(), out id) ? id : 0);
                    loginResponseModel.attendenceModule = false;
                    loginResponseModel.enableInventoryOperationsCP = false;
                    loginResponseModel.enableInventory = true;

                    var user = dr["User_Name"].ToString();
                    if (user != null) { userName = user.ToString(); }                     

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(secretKey);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Authentication,(loginRequestModel.token != null ? loginRequestModel.token : "")),
                            new Claim("LoginId", loginResponseModel.id.ToString()),
                            new Claim("mkey", (loginRequestModel.token != null ? loginRequestModel.token : "")),
                            new Claim("serverTicks",DateTime.UtcNow.Ticks.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault().Element("Validity").Value)),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    loginResponseModel.token = tokenHandler.WriteToken(token);

                    spName = "API_GetSetJWTToken";
                    List<SqlParameter> lstParam2 = new List<SqlParameter>
                    {
                        new SqlParameter() { ParameterName = "@Login_Id", Value = loginResponseModel.id },
                        new SqlParameter() { ParameterName = "@Action", Value = "S" },
                        new SqlParameter() { ParameterName = "@DeviceType", Value = loginRequestModel.deviceType },
                        new SqlParameter() { ParameterName = "@DeviceFCMKey", Value = loginRequestModel.fcmToken },
                        new SqlParameter() { ParameterName = "@APIToken", Value = loginResponseModel.token },
                        new SqlParameter() { ParameterName = "@Status", Value = "0" }
                    };

                    lstParam2[5].Direction = ParameterDirection.Output;

                    await DBHelper.ExecuteNonQueryEmployeeAsync(keycode, CommandType.StoredProcedure, spName, lstParam2);

                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = "Success";
                    genResponse.IsSuccess = true;
                    genResponse.Data = loginResponseModel;
                    genResponse.Title = "Success";
                }
                else {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized user";
                    genResponse.IsSuccess = false;
                    genResponse.Title = "Unauthorized user";
                    return genResponse;
                }
            }
            catch (Exception er)
            {
                throw;
            }
            return genResponse;
        }

        public async Task<dynamic> changePasswordEmployee(ChangePasswordEmployeeRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ChangePasswordEmployee", context);

                string oldPass = "", newPass = "", confirm = "";
                genResponse.IsSuccess = false;
                genResponse.Title = "Failed";
                genResponse.Status = HttpStatusCode.BadRequest;

                if (model.oldPassword!=null && model.newPassword!=null && model.confirmNewPassword != null)
                {
                    oldPass = model.oldPassword.Trim();
                    newPass = model.newPassword.Trim();
                    confirm = model.confirmNewPassword.Trim();

                    if (newPass.Length < 6)
                    {
                        genResponse.Message = "Invalid new password.";
                        return genResponse;
                    }

                    if (newPass == oldPass)
                    {
                        genResponse.Message = "New password cannot be same as old password.";
                        return genResponse;
                    }

                    if (newPass != confirm)
                    {
                        genResponse.Message = "Confirm new password does not match new password.";
                        return genResponse;
                    }
                }
                else
                {
                    genResponse.Message = "Password fields invalid.";
                    return genResponse;
                }

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);
               

                string spName = "API_ChangePasswordEmployee";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status" , Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg" , Value = "",SqlDbType=SqlDbType.VarChar,Size=200 },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId  },
                    new SqlParameter() { ParameterName = "@OldPassword", Value = model.oldPassword ,SqlDbType=SqlDbType.VarChar,Size=100 },
                    new SqlParameter() { ParameterName = "@NewPassword", Value = model.newPassword ,SqlDbType=SqlDbType.VarChar,Size=100 },
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                if (result!=null && result.Length >= 2)
                {
                    genResponse.Message = result[1];

                    if (result[0] == "1")
                    {
                        genResponse.Title = "Success";
                        genResponse.Status = HttpStatusCode.OK;
                        genResponse.IsSuccess = true;
                    }
                }

            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }
    }
}
