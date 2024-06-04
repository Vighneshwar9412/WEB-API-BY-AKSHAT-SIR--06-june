using FourQT.Entities;
using FourQT.Entities.Employee;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using MobAppCoreAPI.Data;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Request;
using MobAppCoreAPI.Models.Response;
using System.Data;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Xml.Linq;

namespace MobAppCoreAPI.Repository
{
    public class LoginRepository : ILogin
    {
        private string secretKey;
        public LoginRepository(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }

        public async Task<dynamic> LoginAsync(LoginRequestDTO loginRequestDTO, string ekey)
        {
            return new APIObjectResponse();
        }
    }
}
