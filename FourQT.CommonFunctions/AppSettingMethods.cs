using Newtonsoft.Json;

namespace FourQT.CommonFunctions
{
    public class AppSettingMethods
    {
        public static string GetSecretKey()
        {
            string secretKey = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        secretKey = array.ApiSettings.SecretKey;
                    }
                }
            }
            catch (Exception ex)
            {
                secretKey = "";
            }

            return secretKey;
        }
    }
}
