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
using MobAppCoreAPI.Models.Response;
using System.Data.SqlClient;
using FourQT.Entities.Portal;
using FourQT.CommonFunctions.Portal;

namespace MobAppCoreAPI.Attributes.Portal_P2
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class APIPortalKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKeyPortal";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            ResponseStatus<NoData> generalResponse = new ResponseStatus<NoData>();

            try
            {
                JwtTokenAuthorize jwt = new JwtTokenAuthorize();

                jwt.GetConnectionDetailsPortal(context.HttpContext.Request, out int customerId, out string eKey, out string token);

                if (token == "")
                {
                    generalResponse.Status = false;
                    generalResponse.Message = "JWT Security Token Not found";
                    generalResponse.ErrorCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(generalResponse)
                    };

                    return;
                }

                string dKey = Cryptography.Decrypt(eKey);
                if (dKey == "")
                {
                    generalResponse.Status = false;
                    generalResponse.Message = "JWT Security Token is Not Valid";
                    generalResponse.ErrorCode = (int)HttpStatusCode.BadRequest;
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(generalResponse)
                    };

                    return;
                }


                var apikey = await Common.GetJWTokenPortal(dKey, customerId);

                if (!token.Equals(apikey))
                {
                    generalResponse.Status = false;
                    generalResponse.Message = "JWT Security Token is Not Valid";
                    generalResponse.ErrorCode = (int)HttpStatusCode.Unauthorized;

                    context.Result = new ContentResult()
                    {
                        StatusCode = 400,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(generalResponse)
                    };

                    return;
                }
            }
            catch (Exception ex)
            {
                generalResponse.Status = false;
                generalResponse.Message = ex.Message;
                generalResponse.ErrorCode = (int)HttpStatusCode.BadRequest;

                context.Result = new ContentResult()
                {
                    StatusCode = 400,
                    ContentType = "application/json",
                    Content = JsonSerializer.Serialize(generalResponse)
                };

                return;
            }

            await next();
        }
    }

}


