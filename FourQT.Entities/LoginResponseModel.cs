using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities
{
    public class LoginResponseModel
    {
        //public HttpStatusCode StatusCode { get; set; }
        public string? Token { get; set; }
        public UserDetails userDetails { get; set; } = new UserDetails();
        //public bool IsSuccess { get; set; }
        //public List<string> ErrorMessages { get; set; }
        //public string message { get; set; }


    }

    public class BrokerResponseModel
    {
        public string? Token { get; set; }
        public List<BrokerDetails> lstbrokerDetails { get; set; } = new List<BrokerDetails>();
    }

    public class LoginResponseModelCP
    {
        public string? Token { get; set; }
        public UserDetailsCP userDetails { get; set; } = new UserDetailsCP();
    }
}
