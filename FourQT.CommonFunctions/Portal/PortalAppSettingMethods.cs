using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace FourQT.CommonFunctions.Portal
{
    public class PortalAppSettingMethods
    {
        public static string GetConnectionSTringPortal()
        {
            string connStr = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        connStr = array.ConnectionStrings.FourQTMobAppApi;
                    }
                }
            }
            catch (Exception ex)
            {
                connStr = "";
            }

            return connStr;
        }

        public static string GetLogFilePathPortal()
        {
            string path = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        path = array.ApiSettings.LogFilePath;
                    }
                }
            }
            catch (Exception ex)
            {
                path = "";
            }

            return path;
        }

        public static string GetUploadPathPortal()
        {
            string path = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        path = array.ApiSettings.UploadPath;
                    }
                }
            }
            catch (Exception ex)
            {
                path = "";
            }

            return path;
        }

        public static string GetImagePath()
        {
            string path = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        path = array.ApiSettings.IMAGEEXT;
                    }
                }
            }
            catch (Exception ex)
            {
                path = "";
            }

            return path;
        }

        public static string GetRootPath()
        {
            string path = "";
            try
            {
                using (StreamReader r = new StreamReader("appsettings.json"))
                {
                    string json = r.ReadToEnd();
                    dynamic? array = JsonConvert.DeserializeObject(json);
                    if (array != null)
                    {
                        path = array.ApiSettings.ROOTPHYSICALPATH;
                    }
                }
            }
            catch (Exception ex)
            {
                path = "";
            }

            return path;
        }

        public static string GetCustomerPortalKey(string dKey)
        {
            string key = "";
            try
            {
                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();
                if (check != null)
                {
                    key = check.Element("CustomerPortalKey").Value;
                }
                else
                {
                    key = "";
                }
            }
            catch(Exception ex)
            {
                key = "";
            }

            return key;
            
        }
    }
}
