using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.CommonFunctions
{
    public  class JwtTokenAuthorize
    {
        public  void GetConnectionDetails(HttpRequest req, out int id, out string connection,out string username,out string SecToken)
        {
            string conn = string.Empty;
            connection = "";
            username = "";
            SecToken= string.Empty;
            id = 0;
            try
            {
                var authKey = string.Empty;

                if (req.Headers.TryGetValue("Authorization", out var headers))
                {
                    authKey = headers.FirstOrDefault();

                    if (authKey != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        authKey = authKey.Replace("Bearer ", "");
                        SecToken = authKey;
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;
                         
                        if (tokenS != null)
                        {
                            conn = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            username = tokenS.Claims.First(claim => claim.Type == "name").Value;
                            //byte[] textBytes = System.Convert.FromBase64String(conn);
                            Int32.TryParse((tokenS.Claims.First(claim => claim.Type == "LoginId").Value.ToString()), out id);
                            connection = Cryptography.Decrypt(conn);
                        }
                    }
                }
            }
            catch
            {
                connection = "";
                id = 0;
            }
        }

        public void GetConnectionDetailsPortal(HttpRequest req, out int customerId, out string eKey,out string token)
        {
            eKey = string.Empty; token=string.Empty;
            customerId = 0;

            try
            {
                var authKey = string.Empty;

                if (req.Headers.TryGetValue("Authorization", out var headers))
                {
                    authKey = headers.FirstOrDefault();

                    if (authKey != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        authKey = authKey.Replace("Bearer ", "");
                        token = authKey;
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            eKey = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            Int32.TryParse((tokenS.Claims.First(claim => claim.Type == "customerId").Value.ToString()), out customerId);
                        }
                    }
                }
            }
            catch
            {
                eKey = "";
                customerId = 0;
                token = "";
            }
        }

        public void GetConnectionDetailsEmployee(HttpRequest req, out int loginId, out string eKey, out string token)
        {
            eKey = string.Empty; token = string.Empty;
            loginId = 0;

            try
            {
                var authKey = string.Empty;

                if (req.Headers.TryGetValue("Authorization", out var headers))
                {
                    authKey = headers.FirstOrDefault();

                    if (authKey != null)
                    {
                        var handler = new JwtSecurityTokenHandler();
                        authKey = authKey.Replace("Bearer ", "");
                        token = authKey;
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            eKey = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            Int32.TryParse((tokenS.Claims.First(claim => claim.Type == "LoginId").Value.ToString()), out loginId);
                        }
                    }
                }
            }
            catch
            {
                eKey = "";
                loginId = 0;
                token = "";
            }
        }
       
    }
}
