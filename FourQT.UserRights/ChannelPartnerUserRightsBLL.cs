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
using FourQT.Entities.General;
using NPoco.RowMappers;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace FourQT.UserRights
{
    public class ChannelPartnerUserRightsBLL
    {
        public async Task<dynamic> CPLoginAsync(LoginRequestDTO loginRequestDTO, string dKey)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            string secretKey = AppSettingMethods.GetSecretKey();
            DataSet ds = new DataSet();
            string keycode = Cryptography.Decrypt(dKey);
            try
            {
                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault();
                if (check == null)
                {
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.IsSuccess = false;

                    return genResponse;
                }

                string spName = "API_ONLINE_BROKER_LOGIN_CHECK";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "",SqlDbType=SqlDbType.VarChar,Size=200 },
                    new SqlParameter() { ParameterName = "@Username", Value = loginRequestDTO.userName,SqlDbType=SqlDbType.VarChar,Size=200 },
                    new SqlParameter() { ParameterName = "@Password", Value = loginRequestDTO.password ,SqlDbType=SqlDbType.VarChar,Size=200},
                    new SqlParameter() { ParameterName = "@Location", Value = loginRequestDTO.location,SqlDbType=SqlDbType.VarChar,Size=200},
                    new SqlParameter() { ParameterName = "@IPAddress", Value = loginRequestDTO.ipAddress,SqlDbType=SqlDbType.VarChar,Size=200}, 
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                dynamic[] result = new dynamic[4];
                result = await DBHelper.GetDataSetCP(keycode, CommandType.StoredProcedure, spName, lstParam);

                string status = "0", outMsg = "";
                int loginId = 0;
                if (result != null)
                {
                    ds = result[0];
                    status = result[1];
                    outMsg = result[2];
                }

                ChannelPartnerCommonLoginResponse model = new ChannelPartnerCommonLoginResponse();
                if (ds!=null && ds.Tables.Count>0)
                {
                    DataTable dt = ds.Tables[0];
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        loginId = (Int32.TryParse(row["LoginId"].ToString(), out int id) ? id : 0);
                        model.id = (Int32.TryParse(row["Broker_Id"].ToString(), out id) ? id : 0);
                        model.name = (row["Borker_Company_name"] != null ? row["Borker_Company_name"].ToString() : "");
                        model.photo = (row["EmployeePhoto"] != null ? row["EmployeePhoto"].ToString() : "");
                        model.mobileNo = (row["Mobile_No"] != null ? row["Mobile_No"].ToString() : "");
                        model.email = (row["Email"] != null ? row["Email"].ToString() : "");
                        model.companyName = (row["CompanyName"] != null ? row["CompanyName"].ToString() : "");
                        model.companyLogo = (row["CompanyLogo"] != null ? row["CompanyLogo"].ToString() : "");
                        model.sendOtpForLeadRegister = (row["sendOtpForLeadRegister"] != null ? row["sendOtpForLeadRegister"].ToString() : "N");
                        model.uploadCustomerPhoto = (row["uploadCustomerPhoto"] != null ? row["uploadCustomerPhoto"].ToString() : "N");
                        model.status = 0;
                        model.type = "";
                        model.sendWhatsAppForLeadRegister = (row["sendWhatsAppForLeadRegister"] != null ? row["sendWhatsAppForLeadRegister"].ToString() : "N");
                        model.qrCode= (row["QRCode"] != null ? row["QRCode"].ToString() : "");
                        model.hRuserId= (Int32.TryParse(row["HRuserId"].ToString(), out id) ? id : 0);
                        model.attendenceModule = false;
                        model.enableInventoryOperationsCP = (Boolean.TryParse(row["EnableInventoryOperationsCP"].ToString(), out bool inv) ? inv : false);
                        model.enableInventory = (Boolean.TryParse(row["EnableInventory"].ToString(), out inv) ? inv : false);
                    }
                    else
                    {
                        genResponse.Title = "Failed";
                        genResponse.Status = HttpStatusCode.Unauthorized;
                        genResponse.Message = outMsg;
                        genResponse.IsSuccess = false;
                        return genResponse;
                    }
                }
                else
                {
                    genResponse.Title = "Failed";
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized user";
                    genResponse.IsSuccess = false;
                    return genResponse;
                }

                model.commonLoginType = "CP";              

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, (model.name!=null ? model.name:"")),
                    new Claim(ClaimTypes.Authentication,dKey),
                    new Claim("LoginId", loginId.ToString()),
                    new Claim("mkey", dKey),
                    new Claim("brokerId", model.id.ToString()),
                    new Claim("serverTicks",DateTime.UtcNow.Ticks.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(Convert.ToDouble(xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == keycode).FirstOrDefault().Element("Validity").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                model.token = tokenHandler.WriteToken(token);

                //Save the Token In The database 
                spName = "API_GetSetJWTToken_CP";
                List<SqlParameter> lstParam2 = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Action", Value = 'S' },
                    new SqlParameter() { ParameterName = "@APIToken", Value = model.token },
                };

                string[] resultAr = await DBHelper.ExecuteNonQueryCP(keycode, CommandType.StoredProcedure, spName, lstParam2);

                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
                genResponse.IsSuccess = true;
                genResponse.Data = model;
                genResponse.Title = "Success";

            }
            catch (Exception er)
            {
                throw;
            }

            return genResponse;
        }

        public async static Task<string> GetAPITokenCP(string dKey,int loginId)
        {
            string token = "";
            try
            {
                string keycode = dKey;

                string spName = "API_GetSetJWTToken_CP";
                List<SqlParameter> lstParam2 = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Action", Value = 'G' },
                };

                dynamic[] resultAr = await DBHelper.GetDataSetCP(keycode, CommandType.StoredProcedure, spName, lstParam2);

                if(resultAr!=null && resultAr.Length > 0)
                {
                    DataSet ds = resultAr[0];
                    if(ds!=null && ds.Tables.Count > 0)
                    {
                        DataTable dt = ds.Tables[0];
                        if(dt!=null && dt.Rows.Count > 0)
                        {
                            string? temp = (dt.Rows[0]["Token"] !=null ? dt.Rows[0]["Token"].ToString() : "");
                            if (temp != null) { token = temp; }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                token="";
            }

            return token;
        }
    }
}
