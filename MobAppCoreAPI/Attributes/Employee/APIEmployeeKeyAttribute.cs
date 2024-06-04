using System.Net;
using System.Text.Json;
using FourQT.CommonFunctions;
using FourQT.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FourQT.DAL.Employee;

namespace MobAppCoreAPI.Attributes.Employee
{
    [AttributeUsage(validOn: AttributeTargets.All)]
    public class APIEmployeeKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "ApiKeyEmployee";

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

                var apikey = Common.GetJWTTokenEmployee(dKey, loginId);

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


