using System.Net;
using System.Text.Json;
using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.UserRights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MobAppCoreAPI.Attributes.ChannelPartner
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class APICPKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "APICPKey";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {
                JwtTokenAuthorize jwt = new JwtTokenAuthorize();
                jwt.GetConnectionDetailsEmployee(context.HttpContext.Request, out int loginId, out string eKey, out string token);

                if (token == "")
                {
                    generalResponse.IsSuccess = false;
                    generalResponse.Message = "JWT Security Token Not found";
                    generalResponse.Status = HttpStatusCode.BadRequest;
                    generalResponse.Title = "Invalid JWT Token";
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
                    generalResponse.IsSuccess = false;
                    generalResponse.Message = "JWT Security Token is Not Valid";
                    generalResponse.Status = HttpStatusCode.BadRequest;
                    generalResponse.Title = "Invalid JWT Token";
                    context.Result = new ContentResult()
                    {
                        StatusCode = 401,
                        ContentType = "application/json",
                        Content = JsonSerializer.Serialize(generalResponse)
                    };

                    return;
                }

                var apikey = await ChannelPartnerUserRightsBLL.GetAPITokenCP(dKey, loginId);
                string[] temp = { token, apikey };

                if (!token.Equals(apikey))
                {
                    generalResponse.IsSuccess = false;
                    generalResponse.Message = "JWT Security Token is Not Valid";
                    generalResponse.Status = HttpStatusCode.Unauthorized;
                    generalResponse.Title = "Invalid JWT Token";
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
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.Message;
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
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


