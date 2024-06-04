using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using BrokerPortalAPI.Models.Response;
using System.Data.SqlClient;

namespace BrokerPortalAPI.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class APIKeyAttribute:Attribute,IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKey";
       
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            JwtTokenAuthorize jwt = new JwtTokenAuthorize();
            APIObjectResponse generalResponse = new APIObjectResponse();
            jwt.GetConnectionDetails(context.HttpContext.Request, out int loginId, out string mKey,out string username, out string tokens);

            if (tokens == "")
            {
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Message = "JWT Security Token Not found";
                generalResponse.IsSuccess = false;
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize<APIObjectResponse>(generalResponse)
                };

                return;
            }
            else if (mKey == "")
            {
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Message = "JWT Security Token is Not Valid";
                generalResponse.IsSuccess = false;
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize<APIObjectResponse>(generalResponse)
                };

                return;
            }
          
            SqlParameter[] lstParam4 = { new SqlParameter() { ParameterName = "@User_Id", Value = loginId } };

            //var apiKey = appSettings.GetValue<string>(APIKEYNAME);
            var apikey = DBHelper.ExecuteScalarkey(mKey, System.Data.CommandType.StoredProcedure, "FindTokenDescriptor", lstParam4);
            
            if (!tokens.Equals(apikey))
            {
                generalResponse.Status = HttpStatusCode.Unauthorized;
                generalResponse.Message = "JWT Security Token is Not Valid";
                generalResponse.IsSuccess = false;

                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    ContentType= "application/json",
                    Content = JsonSerializer.Serialize<APIObjectResponse>(generalResponse)                    
                };

                
                return;
            }
            await next();
        }
    }

}
    

