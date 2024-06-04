using System.Net;
using System.Text;
using FourQT.Entities.Portal;
using Microsoft.AspNetCore.Mvc;
using MobAppCoreAPI.Interfaces.Portal;

namespace MobAppCoreAPI.Controllers.Portal
{
    [Route("/api/p1/[controller]/[action]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "p1")]
    public class LoginController : ControllerBase
    {
        private readonly IPortalLogin _IPortalLogin;

        public LoginController(IPortalLogin IPortalLogin)
        {
            _IPortalLogin = IPortalLogin;
        }

        [HttpPost]
        public ResponseStatus<Customeruserlist> CustomerLogin(CustomerLogin objuser)
        {
            return _IPortalLogin.customerLogin(objuser);
        }

        [HttpPost]
        public ResponseStatus<ChangePasswordList> CustomerChangePassword(ChangePassword objuser)
        {
            return _IPortalLogin.customerChangePassword(objuser);
        }

        [HttpPost]
        public ResponseStatus<NoData> GetOTP(CustomerLogin objuser)
        {
            return _IPortalLogin.getOTP(objuser);
        }

        //[HttpPost]
        //public async Task<dynamic> sendWatsApp()
        //{
        //    SendWhatsApp();
        //    return 0;
        //}

        //private static void SendWhatsApp()
        //{
        //    string result = "";

        //    try
        //    {
        //        ServicePointManager.Expect100Continue = true;
        //        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        //        //ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

        //        //ServicePointManager.ServerCertificateValidationCallback = delegate
        //        //{
        //        //    return true;
        //        //};


        //        HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create("https://api.interakt.ai/v1/public/message/");
        //        myReq.Method = "POST";
        //        myReq.ContentType = "application/json";

        //        string json = "{\"countryCode\": \"+91\", \"phoneNumber\": \"8377839192\", \"fullPhoneNumber\": \"\", \"callbackData\": \"\", \"type\": \"Template\",  \"template\": {\"name\": \"new_lead_es\",\"languageCode\": \"en\", \"bodyValues\": [ \"DHRISHA HOMES\" ],  \"buttonValues\" : {\"0\" : [\"Undefined\"] } } }";

        //        myReq.Headers.Add("Authorization", "Basic OFMxVTRjRHNNYmZWUVlWWDd2aUV3bjFVT0VWa2k2Y1FXTktFWnpxTW9rMDo=");

        //        byte[] data = Encoding.ASCII.GetBytes(json);
        //        myReq.ContentLength = data.Length;

        //        Stream requestStream = myReq.GetRequestStream();
        //        requestStream.Write(data, 0, data.Length);
        //        requestStream.Close();

        //        HttpWebResponse myResp = (HttpWebResponse)myReq.GetResponse();

        //        StreamReader respStreamReader = new StreamReader(myResp.GetResponseStream());
        //        string sStatus = respStreamReader.ReadToEnd();
        //        respStreamReader.Close();
        //        myResp.Close();

        //        result = "Success";
        //    }
        //    catch (Exception ex)
        //    {
        //        result = "Error: " + ex.Message;
        //    }

        //}

    }
}
