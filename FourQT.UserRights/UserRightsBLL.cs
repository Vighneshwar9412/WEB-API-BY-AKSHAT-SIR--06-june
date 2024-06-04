using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;
using FourQT.Entities;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using FourQT.DAL;
using FourQT.CommonFunctions;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using FourQT.Entities.Portal;
using System.Linq;
using FourQT.CommonFunctions.Portal;
using Newtonsoft.Json;
using FourQT.Entities.Employee;
using System.Xml.Serialization;

namespace FourQT.UserRights
{
    
    public class UserRightsBLL
    {
        public async Task<dynamic> LoginAsync(LoginRequestDTO loginRequestDTO, string dkey, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            // private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);

            string message = JsonConvert.SerializeObject(loginRequestDTO);
            Log.LogPayloadDateWise(message, "LeadLogin", context);

            string secretKey = AppSettingMethods.GetSecretKey();
            DataSet ds = new DataSet();
            string keycode = Cryptography.Decrypt(dkey);
            string? userName = "";
            try
            {
                LoginResponseModel loginResponseModel = new LoginResponseModel();
                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault();
                if (check == null)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.IsSuccess = false;

                    return genResponse;
                }


                string spName = "API_Udp_CheckUserLogin";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@User_Name", Value = loginRequestDTO.userName },
                    new SqlParameter() { ParameterName = "@Password", Value = loginRequestDTO.password },
                    new SqlParameter() { ParameterName = "@Location", Value = loginRequestDTO.location},
                    new SqlParameter() { ParameterName = "@IPAddress", Value = loginRequestDTO.ipAddress},

                    new SqlParameter() { ParameterName = "@Output", Value = 0 }
                };
                lstParam[4].Direction = ParameterDirection.Output;

                ds = await DBHelper.GetDatasetAsyncNew(keycode, CommandType.StoredProcedure, spName, lstParam);

                if (ds.Tables.Count == 1)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized user";
                    genResponse.IsSuccess = false;
                    return genResponse;
                }

                DataTable userDetailsTable = ds.Tables[0];
                DataTable emailTable = ds.Tables[1];

                UserDetails model = new UserDetails();

                foreach (DataRow row in userDetailsTable.Rows)
                {

                    model.login_Id = (Int32.TryParse(row["Login_Id"].ToString(), out int id) ? id : 0);
                    userName = (row["User_Name"] != null ? row["User_Name"].ToString() : "");
                    model.EmailRight = (Boolean.TryParse(row["EmailRights"].ToString(), out Boolean b) ? b : false);
                    model.emp_Name = (row["EmpName"] != null ? row["EmpName"].ToString() : ""); 
                    model.Mobile = (row["MobileNo"] != null ? row["MobileNo"].ToString() : ""); 
                    model.ClickToCallRight = (Boolean.TryParse(row["ClickToCallRights"].ToString(), out b) ? b : false); 
                    model.DumpRight = (Boolean.TryParse(row["IS_DUMP"].ToString(), out b) ? b : false); 
                    model.TransferRight = (Boolean.TryParse(row["Is_Transfer"].ToString(), out b) ? b : false); 
                    model.SuccessRight = (Boolean.TryParse(row["CanSuccess"].ToString(), out b) ? b : false); 
                    model.LoginType = (row["LoginType"] != null ? row["LoginType"].ToString() : "");
                    model.channelId = (Int32.TryParse(row["Channel_Id"].ToString(), out int channelId) ? channelId : 0);
                    model.docNo = (Int32.TryParse(row["DocNo"].ToString(), out id) ? id : 0); 
                    model.callerId = (row["CallerId"] != null ? row["CallerId"].ToString() : "");  
                    model.agentNumber = (row["Agent_Number"] != null ? row["Agent_Number"].ToString() : ""); 
                    model.transferType = (row["TRANSFER_TYPE"] != null ? row["TRANSFER_TYPE"].ToString() : "");
                    model.transferAutoManual = (row["transfer_auto_manual"] != null ? row["transfer_auto_manual"].ToString() : "");
                    model.hRuserId= (Int32.TryParse(row["HRuserId"].ToString(), out id) ? id : 0);
                    model.attendenceModule = (Boolean.TryParse(row["AttendenceModule"].ToString(), out b) ? b : false);
                    model.leadInventory = (userDetailsTable.Columns.Contains("ShowLeadInventory") && Boolean.TryParse(row["ShowLeadInventory"].ToString(), out b) ? b : false);
                }
                foreach (DataRow row in emailTable.Rows)
                {
                    model.Email = row["Emp_MailId"].ToString();
                }
                loginResponseModel.userDetails=model;


                spName = "GetSetDeviceKey";
                List<SqlParameter> lstParam2 = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = model.login_Id },
                    new SqlParameter() { ParameterName = "@User_Id", Value = model.login_Id },
                    new SqlParameter() { ParameterName = "@DeviceType", Value = loginRequestDTO.deviceType },
                    new SqlParameter() { ParameterName = "@DeviceKey", Value = loginRequestDTO.fcmToken },
                    new SqlParameter() { ParameterName = "@Action", Value = 'U' },
                    new SqlParameter() { ParameterName = "@Status", Value = 0 }
                };
                lstParam2[5].Direction = ParameterDirection.Output;

                int status = await DBHelper.ExecuteNonQueryAsyncNew(keycode, CommandType.StoredProcedure, spName, lstParam2);

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, (userName != null ? userName : "")),
                    //new Claim(ClaimTypes.Role, model.role),
                    new Claim(ClaimTypes.Authentication,dkey),
                    new Claim(ClaimTypes.Email, (model.Email!=null?model.Email:"")),
                    new Claim("LoginId", model.login_Id.ToString()),
                    new Claim("mkey", dkey),
                    new Claim("serverTicks",DateTime.UtcNow.Ticks.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault().Element("Validity").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                


                var token = tokenHandler.CreateToken(tokenDescriptor);
                loginResponseModel.Token = tokenHandler.WriteToken(token);
                
                //Save the Token In The database 
                spName = "CreateTokenDescriptor";
                List<SqlParameter> lstParam3 = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@User_Id", Value = model.login_Id },
                    new SqlParameter() { ParameterName = "@Token", Value = loginResponseModel.Token },
                };

                int res = await DBHelper.ExecuteNonQueryAsyncNew(keycode, CommandType.StoredProcedure, spName, lstParam3);


                //loginResponseModel.StatusCode = HttpStatusCode.OK;
                //loginResponseModel.IsSuccess = true;

                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
                genResponse.IsSuccess = true;
                genResponse.Data = loginResponseModel;

            }
            catch (Exception er)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadGateway;
                genResponse.Message = er.ToString();

            }

            return genResponse;

        }

        public Object logout(string Key, int Login_Id)
        {
            DataSet ds = new DataSet();
            APIObjectResponse genResponse = new APIObjectResponse();
            try
            {
               
                string spName = "API_logoutuser";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                 new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                 };

                ds = DBHelper.GetDataset(Key, CommandType.StoredProcedure, spName, lstParam);
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
                genResponse.IsSuccess = true;
            }
            catch (Exception er)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadGateway;
                genResponse.Message = er.ToString();

            }

            return genResponse;
        }

        public async Task<APIObjectResponse> checkduplicity(HttpRequest req,string mobileNo)
        {
            DataSet ds = new DataSet();
            JWTTokenMethods jwt = new JWTTokenMethods();
            //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
            jwt.GetConnectionDetails(req, out int loginId, out string mKey);
            APIObjectResponse genResponse = new APIObjectResponse();
            // private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);


            try
            {
            

                string spName = "sp_CheckMobileNumber";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@MobileNo", Value = mobileNo},
                };
                
                ds = DBHelper.GetDataset(mKey, CommandType.StoredProcedure, spName, lstParam);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Message = "Mobile Number Already Exists";
                    genResponse.IsSuccess = false;
                    return genResponse;
                }
                else
                {
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = "Success";
                    genResponse.IsSuccess = true;
                }

            }
            catch (Exception er)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadGateway;
                genResponse.Message = er.ToString();

            }

            return genResponse;

        }

        public async Task<APIObjectResponse> LoginBrokerAsync(LoginRequestDTO loginRequestDTO, string dkey)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            // private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);


            string secretKey = AppSettingMethods.GetSecretKey();
            DataSet ds = new DataSet();
            int loginId = 0;
            string keycode = Cryptography.Decrypt(dkey);
            string userName = "";
            try
            {
                BrokerResponseModel loginResponseModel = new BrokerResponseModel();
                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault();
                if (check == null)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.IsSuccess = false;

                    return genResponse;
                }

                string spName = "ONLINE_BROKER_LOGIN_CHECK";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@UserName", Value = loginRequestDTO.userName },
                    new SqlParameter() { ParameterName = "@Password", Value = loginRequestDTO.password },
                    new SqlParameter() { ParameterName = "@status", Value = 0},
                    new SqlParameter() { ParameterName = "@OutMessage", Value = ""},                    
                };
                lstParam[2].Direction = ParameterDirection.Output;
                lstParam[3].Direction = ParameterDirection.Output;


                ds = DBHelper.GetDataset(keycode, CommandType.StoredProcedure, spName, lstParam);

                if (ds.Tables[0].Rows.Count > 0)
                {
                        DataRow row = ds.Tables[0].Rows[0];
                        BrokerDetails bk = new BrokerDetails();
                        bk.Id = Convert.ToInt32(row["Broker_id"]);
                        bk.LoginId = Convert.ToInt32(row["LoginId"]);
                        loginId = bk.LoginId;
                        bk.CompanyName = row["Borker_Company_name"].ToString();
                        bk.ParentBrokerId = Convert.ToInt32(row["ParentBroker_id"]);
                        bk.EmployeeId = row["EmployeeId"].ToString()==""?0:Convert.ToInt32(Convert.ToString(row["EmployeeId"]));
                        loginResponseModel.lstbrokerDetails.Add(bk);
                    
                }

                if (lstParam[2].Value.ToString() == "0")
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = lstParam[3].Value.ToString();
                    genResponse.IsSuccess = false;
                    return genResponse;
                }
                else
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(secretKey);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                    new Claim(ClaimTypes.Name, userName),
                    //new Claim(ClaimTypes.Role, model.role),
                    new Claim(ClaimTypes.Authentication,dkey),
                    //new Claim(ClaimTypes.Email, model.Email),
                    new Claim("LoginId", loginId.ToString()),
                    new Claim("mkey", dkey)
                        }),
                        Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault().Element("Validity").Value)),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };




                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    loginResponseModel.Token = tokenHandler.WriteToken(token);

                    //Save the Token In The database 
                    spName = "API_BCreateTokenDescriptor";
                    List<SqlParameter> lstParam3 = new List<SqlParameter>
                    {
                    new SqlParameter() { ParameterName = "@User_Id", Value = loginId},
                   new SqlParameter() { ParameterName = "@Token", Value = loginResponseModel.Token },
                };

                    int res = DBHelper.ExecuteNonQuery(keycode, CommandType.StoredProcedure, spName, lstParam3);


                    //loginResponseModel.StatusCode = HttpStatusCode.OK;
                    //loginResponseModel.IsSuccess = true;

                   

                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = lstParam[3].Value.ToString();
                    genResponse.Data = loginResponseModel;
                    genResponse.IsSuccess = true;
                    return genResponse;
                }

            }
            catch (Exception er)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadGateway;
                genResponse.Message = er.ToString();

            }

            return genResponse;

        }

        public string GeneratePortalToken(string key,int customerId)
        {
            string token = "";

            try
            {
                XDocument xdoc = XDocument.Load("keys.xml");
                string secretKey = AppSettingMethods.GetSecretKey();

                var sKey = Encoding.ASCII.GetBytes(secretKey);
                var tokenHandler = new JwtSecurityTokenHandler();
                var encKey = Cryptography.Encrypt(key);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("mkey", encKey),
                    new Claim("customerId", Convert.ToString(customerId)),
                    new Claim("serverTicks",DateTime.UtcNow.Ticks.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(sKey), SecurityAlgorithms.HmacSha256Signature)
                };

                var tok = tokenHandler.CreateToken(tokenDescriptor);
                token = tokenHandler.WriteToken(tok);
            }
            catch(Exception ex)
            {
                token = "";
            }
            

            return token;
        }

        public async Task<APIObjectResponse> changePassword(ChangePasswordLeadRequest model, HttpRequest req)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            int status = 0;string? outMsg = "";

            try
            {
                if (model != null && model.newPassword != null && model.oldPassword != null && model.confirmNewPassword != null) {
                    string oldPass = model.oldPassword.Trim();
                    string newPass = model.newPassword.Trim();
                    string confNewPass = model.confirmNewPassword.Trim();

                    if (newPass == confNewPass)
                    {
                        (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                        string spName = "API_ChangePassword_Lead";
                        List<SqlParameter> lstParam = new List<SqlParameter>
                        {
                            new SqlParameter() { ParameterName = "@Status", Value = 0 },
                            new SqlParameter() { ParameterName = "@OutMsg", SqlDbType = SqlDbType.VarChar, Size = 200, Value = "" },
                            new SqlParameter() { ParameterName = "@Login_Id", Value = (Int32.TryParse(loginId.ToString(),out int id) ? id : 0) },
                            new SqlParameter() { ParameterName = "@OldPassword", SqlDbType = SqlDbType.VarChar, Size = 100, Value = oldPass },
                            new SqlParameter() { ParameterName = "@NewPassword", SqlDbType = SqlDbType.VarChar, Size = 100, Value = newPass },
                        };

                        lstParam[0].Direction = ParameterDirection.Output;
                        lstParam[1].Direction = ParameterDirection.Output;

                        await DBHelper.ExecuteNoQueryAsyncNew(conn, CommandType.StoredProcedure, spName, lstParam);

                        Int32.TryParse(lstParam[0].Value.ToString(), out status);
                        outMsg = lstParam[1].Value.ToString();

                        outMsg = outMsg != null ? outMsg : "";

                        if (status == 1)
                        {
                            genResponse.IsSuccess = true;
                            genResponse.Message = outMsg;
                            genResponse.Status = HttpStatusCode.OK;
                            genResponse.Title = "Success";
                        }
                        else
                        {
                            genResponse.IsSuccess = false;
                            genResponse.Message = (outMsg != "" ? outMsg : "Failed to change password.");
                            genResponse.Status = HttpStatusCode.BadRequest;
                            genResponse.Title = "Failed";
                        }
                    }
                    else {
                        genResponse.IsSuccess = false;
                        genResponse.Title = "Invalid Request";
                        genResponse.Status = HttpStatusCode.BadRequest;
                        genResponse.Message = "New Password does not match Confirm New Password.";
                    }
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Title = "Invalid Request";
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Message = "Invalid Request";
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Title = "Error";
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }
    }

}
