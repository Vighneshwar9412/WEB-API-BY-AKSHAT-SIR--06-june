using System.Data;
using System.Net;
using System.Text;
using System.Xml.Linq;
using FourQT.CommonFunctions;
using FourQT.CommonFunctions.Portal;
using FourQT.DAL.Portal;
using FourQT.Entities;
using FourQT.Entities.Firebase;
using FourQT.Entities.Portal;
using FourQT.UserRights;
using MobAppCoreAPI.Interfaces.Portal_P2;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;

namespace MobAppCoreAPI.Repository.Portal
{
    public class FirebaseRepositoryPortal : IPortalFirebase
    {
        public ResponseStatus<Notification_Device> Notification(Firebase objFirebase,HttpRequest request)
        {
            ResponseStatus<Notification_Device> Process = new ResponseStatus<Notification_Device>();
            //Notification_Device Obj = new Notification_Device();
            //string portalKey = (new JWTTokenMethods()).GetTokenPortal(request);
            //try
            //{
            //    DataTable dt = DAL.CheckToken(portalKey, "S");
            //    if (dt.Rows.Count > 0)
            //    {
            //        string SERVER_API_KEY = objFirebase.ServerKey;
            //        var SENDER_ID = objFirebase.SenderId;
            //        var messageBody = objFirebase.Message;
            //        var messageTitle = objFirebase.Title;
            //        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            //        tRequest.Method = "post";
            //        //serverKey - Key from Firebase cloud messaging server  
            //        tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_API_KEY));
            //        //Sender Id - From firebase project setting  
            //        tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));
            //        tRequest.ContentType = "application/json";

            //        List<string> lstDevices = new List<string>();

            //        for (int i = 0; i < objFirebase.DeviceKeys.Count(); i++)
            //        {
            //            lstDevices.Add(objFirebase.DeviceKeys[i].Value);
            //        }
            //        var payload = new
            //        {
            //            registration_ids = lstDevices,
            //            priority = "high",
            //            content_available = true,
            //            collapse_key = "score_update",
            //            time_to_live = 108,
            //            data = new
            //            {
            //                message = messageBody,
            //                title = messageTitle,
            //                time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
            //                actionType = objFirebase.ActionId,
            //                registrationId = objFirebase.RegistrationId
            //            },
            //        };
            //        var serializer = new JavaScriptSerializer();
            //        Byte[] byteArray = Encoding.UTF8.GetBytes(serializer.Serialize(payload));
            //        tRequest.ContentLength = byteArray.Length;
            //        using (Stream dataStream = tRequest.GetRequestStream())
            //        {
            //            dataStream.Write(byteArray, 0, byteArray.Length);
            //            using (WebResponse tResponse = tRequest.GetResponse())
            //            {
            //                using (Stream dataStreamResponse = tResponse.GetResponseStream())
            //                {
            //                    if (dataStreamResponse != null)
            //                    {
            //                        using (StreamReader tReader = new StreamReader(dataStreamResponse))
            //                        {
            //                            String sResponseFromServer = tReader.ReadToEnd();
            //                            Obj = JsonConvert.DeserializeObject<Notification_Device>(sResponseFromServer);
            //                            Obj.MessageTitle = JsonConvert.DeserializeObject<string>("\"" + messageTitle + "\" ");
            //                            Obj.MessageBody = JsonConvert.DeserializeObject<string>("\"" + messageBody + "\" ");
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        Process.Status = true;
            //        Process.ErrorCode = 200;
            //        Process.Message = MessageClass.sNotificationS;
            //        Process.Data = Obj;
            //    }
            //    else
            //    {
            //        Process.Status = false;
            //        Process.ErrorCode = 417;
            //        Process.Message = MessageClass.sNotificationN;
            //        Process.Data = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Process.ErrorCode = 417;
            //    Process.Message = ex.Message;
            //    Process.LstData = null;
            //}
            return Process;
        }
    }
}
