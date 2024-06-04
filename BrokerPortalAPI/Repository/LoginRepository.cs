using Microsoft.Extensions.Configuration;
using BrokerPortalAPI.Interfaces;
using FourQT.Entities;
using System.Threading.Tasks;

namespace BrokerPortalAPI.Repository
{
    public class LoginRepository:ILogin
    {
        private string secretKey;
        public LoginRepository(IConfiguration configuration)
        {
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }
        public async Task<APIObjectResponse> LoginAsync(LoginRequestDTO loginRequestDTO, string ekey)
        {
            return new APIObjectResponse();
        }
    }
}
