using System.Net;

namespace BrokerPortalAPI.Models.Response
{
    public class LoginResponseModel
    {
        //public HttpStatusCode StatusCode { get; set; }
        public string? Token { get; set; }
        public List<UserDetails> userDetails { get; set; } = new List<UserDetails>();
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        //public string message { get; set; }
    }
    public class UserDetails
    {
        public int loginid { get; set; }
        public string username { get; set; }
        public int roleid { get; set; }
        public int empid { get; set; }
        public string empname { get; set; }
        public bool isadmin { get; set; }
        public string role { get; set; }
        public string transfertype { get; set; }
        public string mobile { get; set; }
        public string androidkey { get; set; }
        public string ioskey { get; set; }
        public string mobkey { get; set; }
        public bool clicktocall { get; set; }
        public bool dump { get; set; }
        public bool transfer { get; set; }
        public bool success { get; set; }
        public string email { get; set; }
    }
}
