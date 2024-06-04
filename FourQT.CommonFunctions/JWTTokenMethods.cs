using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Xml.Linq;
using Microsoft.AspNetCore.Http;

namespace FourQT.CommonFunctions
{
    public class JWTTokenMethods
    {
        public void GetConnectionDetails(HttpRequest req,out int id,out string connection)
        {
            string conn = string.Empty;
            connection = "";
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
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            conn = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
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

        public string GetTokenPortal(HttpRequest req)
        {
            string key = string.Empty;
            string dKey = "";

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
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            key = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            dKey = Cryptography.Decrypt(key);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                dKey = "";
            }

            return dKey;
        }

        public void GetConnectionDetailsCustomer(HttpRequest req, out int customerId, out string dKey)
        {
            string key = string.Empty;
            dKey = "";customerId = 0;

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
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            key = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            Int32.TryParse((tokenS.Claims.First(claim => claim.Type == "customerId").Value.ToString()), out customerId);
                            dKey = Cryptography.Decrypt(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                dKey = "";
                customerId = 0;
            }
        }

        public void GetConnectionDetailsCP(HttpRequest req, out int brokerId, out string connection)
        {
            string conn = string.Empty;
            connection = "";
            brokerId = 0;
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
                        var jsonToken = handler.ReadToken(authKey);
                        var tokenS = handler.ReadToken(authKey) as JwtSecurityToken;

                        if (tokenS != null)
                        {
                            conn = tokenS.Claims.First(claim => claim.Type == "mkey").Value;
                            //byte[] textBytes = System.Convert.FromBase64String(conn);
                            Int32.TryParse((tokenS.Claims.First(claim => claim.Type == "brokerId").Value.ToString()), out brokerId);
                            connection = Cryptography.Decrypt(conn);
                        }
                    }
                }
            }
            catch
            {
                connection = "";
                brokerId = 0;
            }
        }

        public string GetMainKeyFromCustomerKey(string CustomerKey)
        {
            string mainKey = "";
            var dKey = "";

            try {
                XDocument xdoc = XDocument.Load("keys.xml");

                foreach (var connNode in xdoc.Descendants("connection").Where(x => x.Descendants("CustomerPortalKey").First().Value == CustomerKey))
                {
                    dKey = connNode.Attribute("dkey").Value;
                    break;
                }

                mainKey = (dKey != null ? dKey.ToString() : "");
            }
            catch {
                
            }

            return mainKey;
        }
    }
}