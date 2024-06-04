using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net;
using System.Reflection.PortableExecutable;
using System.Xml;
using System.Xml.Linq;
using FourQT.Entities;
using FourQT.CommonFunctions;
using static System.Net.Mime.MediaTypeNames;
using FourQT.Utilities;

namespace MobAppCoreAPI.Repository
{
    public class ValidateKeyRepository : IValidateKey
    {
        public async Task<APIObjectResponse> ValidateKey(string dKey)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            try
            {
                ClientData response = new ClientData();
                //var textBytes = System.Text.Encoding.UTF8.GetBytes(mKey);
                
                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();
                if (check != null)
                {
                    response.company = check.Element("Company").Value;
                    response.logo = check.Element("Logo").Value;
                    response.ekey = Cryptography.Encrypt(dKey); 
                    //response.responses = check.Element("responses").Value;
                    //response.description = check.Element("description").Value;
                    //response.IsSuccess = true;

                    genResponse.Data = response;
                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = "Success";
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                }
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "v1/validatekey");
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.Unauthorized;
                genResponse.Message = ex.Message;
            }
            return genResponse;
        }

    }
}
