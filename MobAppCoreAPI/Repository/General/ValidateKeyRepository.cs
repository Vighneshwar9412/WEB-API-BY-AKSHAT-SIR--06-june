using FourQT.CommonFunctions;
using System.Xml.Linq;
using FourQT.Entities.Portal;
using MobAppCoreAPI.Interfaces.General;
using System.Net;
using System.Data;
using FourQT.CommonFunctions.Portal;

namespace MobAppCoreAPI.Repository.General
{
    public class ValidateKeyRepository : IValidateKey
    {
        public ResponseStatus<ValidateKey> validateKey(string mKey)
        {
            ResponseStatus<ValidateKey> genResponse = new ResponseStatus<ValidateKey>();
            try
            {
                ValidateKey response = new ValidateKey();

                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == mKey).FirstOrDefault();
                if (check != null)
                {
                    response.eKey = Cryptography.Encrypt(mKey);

                    genResponse.Data = response;
                    genResponse.ErrorCode = (int)HttpStatusCode.OK;
                    genResponse.Status = true;
                    genResponse.Message = MessageClass.validKey;
                }
                else
                {
                    genResponse.Status = false;
                    genResponse.ErrorCode = (int)HttpStatusCode.Unauthorized;
                    genResponse.Message = MessageClass.invalidKey;
                }
            }
            catch (Exception ex)
            {
                Log.LogExceptionSubject(ex, "User ValidateKey( Token=" + mKey + ",  UserID=" + ")", mKey);
                genResponse.Status = false;
                genResponse.ErrorCode = (int)HttpStatusCode.Unauthorized;
                genResponse.Message = ex.Message;
            }
            return genResponse;
        }
    }    
}
