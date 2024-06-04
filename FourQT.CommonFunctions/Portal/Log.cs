using Microsoft.AspNetCore.Http;

namespace FourQT.CommonFunctions.Portal
{
    public class Log
    {       
        public static void LogExceptionSubject(Exception ex, string Subject,string Token)
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                string fileName = "";
                string TokenPath = "";
                string logFilePath = "";
                if (Token != "")
                {
                    fileName = "log" + "_" + Token + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + "_" + nowDate.Ticks + ".txt";
                    //logFilePath = object.Equals(PortalAppSettingMethods.GetLogFilePathPortal(), null) == false ? PortalAppSettingMethods.GetLogFilePathPortal() : "";
                    logFilePath = Directory.GetCurrentDirectory() + PortalAppSettingMethods.GetLogFilePathPortal();
                    TokenPath = logFilePath + @"\" + Token;
                }
                else
                {
                    fileName = "log" + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + "_" + nowDate.Ticks + ".txt";
                    //logFilePath = object.Equals(PortalAppSettingMethods.GetLogFilePathPortal(), null) == false ? PortalAppSettingMethods.GetLogFilePathPortal() : "";
                    logFilePath = Directory.GetCurrentDirectory() + PortalAppSettingMethods.GetLogFilePathPortal();
                    TokenPath = logFilePath + @"\No Token";
                }
                if (logFilePath == "")
                {
                    logFilePath = Directory.GetCurrentDirectory() + "\\Temp";
                }
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                if (!Directory.Exists(TokenPath))
                {
                    Directory.CreateDirectory(TokenPath);
                }
                try
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName, true);
                    Tex.WriteLine("------------------------------------------ERROR EXCEPTION BEGIN ------------------------------------------");
                    Tex.WriteLine("############################################");
                    Tex.WriteLine("SUBJECT:- " + Subject);
                    Tex.WriteLine("############################################");
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Exception occurred from Client - " /* + HttpContext.Request.ServerVariables["REMOTE_HOST"] + "," + HttpContext.Current.Request.ServerVariables["SERVER_NAME"]*/);
                    Tex.Write(Tex.NewLine);
                    if (ex.InnerException != null)
                    {
                        Tex.WriteLine("InnerException - " + ex.InnerException.ToString());
                    }
                    Tex.WriteLine("Exception - " + ex.ToString());
                    Tex.WriteLine("StackTrace - " + ex.StackTrace);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------ERROR EXCEPTION END ------------------------------------------");
                    Tex.Close();
                }
                catch (IOException exception)
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName + "_2", true);
                    Tex.WriteLine("------------------------------------------ERROR EXCEPTION BEGIN ------------------------------------------");
                    Tex.WriteLine("############################################");
                    Tex.WriteLine("SUBJECT:- " + Subject);
                    Tex.WriteLine("############################################");
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Exception occurred from Client - " /* + HttpContext.Request.ServerVariables["REMOTE_HOST"] + "," + HttpContext.Current.Request.ServerVariables["SERVER_NAME"]*/);
                    Tex.Write(Tex.NewLine);
                    if (ex.InnerException != null)
                    {
                        Tex.WriteLine("InnerException - " + ex.InnerException.ToString());
                    }
                    Tex.WriteLine("Exception - " + ex.ToString());
                    Tex.WriteLine("StackTrace - " + ex.StackTrace);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------ERROR EXCEPTION END ------------------------------------------");
                    Tex.Close();
                }
            }
            catch (Exception exc) {
                return;
            }          
        }

        public static void LogMessage(string Message,string Token)
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                string fileName = "";
                string TokenPath = "";
                string logFilePath = "";
                if (Token != "")
                {
                    fileName = "log" + "_" + Token + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + "_" + nowDate.Ticks + ".txt";
                    //logFilePath = object.Equals(PortalAppSettingMethods.GetLogFilePathPortal(), null) == false ? PortalAppSettingMethods.GetLogFilePathPortal() : "";
                    logFilePath = Directory.GetCurrentDirectory() + PortalAppSettingMethods.GetLogFilePathPortal();
                    TokenPath = logFilePath + @"\" + Token;
                }
                else
                {
                    fileName = "log" + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + "_" + nowDate.Ticks + ".txt";
                    //logFilePath = object.Equals(PortalAppSettingMethods.GetLogFilePathPortal(), null) == false ? PortalAppSettingMethods.GetLogFilePathPortal() : "";
                    logFilePath = Directory.GetCurrentDirectory() + PortalAppSettingMethods.GetLogFilePathPortal();
                    TokenPath = logFilePath + @"\No Token";
                }
                if (logFilePath == "")
                {
                    logFilePath = Directory.GetCurrentDirectory() + "\\Temp";
                }
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                if (!Directory.Exists(TokenPath))
                {
                    Directory.CreateDirectory(TokenPath);
                }

                try
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName, true);
                    Tex.WriteLine("------------------------------------------MESSAGE BEGIN ------------------------------------------");
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Message from Client - "  /* + HttpContext.Current.Request.ServerVariables["REMOTE_HOST"] + "," + HttpContext.Current.Request.ServerVariables["SERVER_NAME"]*/);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("MESSAGE:- " + Message);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------MESSAGE END ------------------------------------------");
                    Tex.Close();
                }
                catch (IOException ioException)
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName + "_2", true);
                    Tex.WriteLine("------------------------------------------MESSAGE BEGIN ------------------------------------------");
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Message from Client - "  /* + HttpContext.Current.Request.ServerVariables["REMOTE_HOST"] + "," + HttpContext.Current.Request.ServerVariables["SERVER_NAME"]*/);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("MESSAGE:- " + Message);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------MESSAGE END ------------------------------------------");
                    Tex.Close();
                }
               
            }
            catch (Exception ex) {
                return;
            }
           
        }

        public static void LogPayloadDateWise(string Message, string Process,HttpContext context)
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                string fileName = "";
                string TokenPath = "";
                string logFilePath = "";
                if (Process != null && Process != "")
                {
                    fileName = "Log" + "_" + Process + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + ".txt";
                }
                else
                {
                    fileName = "Log" + "_" + nowDate.Day.ToString() + "_" + nowDate.Month.ToString() + "_" + nowDate.Year.ToString() + ".txt";
                }
                logFilePath = Directory.GetCurrentDirectory() + PortalAppSettingMethods.GetLogFilePathPortal();
                TokenPath = logFilePath + @"\" + "API_Payload_Log";

                if (logFilePath == "")
                {
                    logFilePath = Directory.GetCurrentDirectory() + "\\4QTMobileAPI_Log";
                }
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                if (!Directory.Exists(TokenPath))
                {
                    Directory.CreateDirectory(TokenPath);
                }

                try
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName, true);
                    Tex.WriteLine("------------------------------------------MESSAGE BEGIN ------------------------------------------");
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Message from Client - " + HttpContextServerVariableExtensions.GetServerVariable(context, "REMOTE_HOST") + "," + HttpContextServerVariableExtensions.GetServerVariable(context, "SERVER_NAME"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("MESSAGE:- " + Message);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------MESSAGE END ------------------------------------------");
                    Tex.Write(Tex.NewLine);
                    Tex.Close();
                }
                catch (IOException)
                {
                    TextWriter Tex = new StreamWriter(TokenPath + "/" + fileName + "_2", true);
                    Tex.WriteLine("------------------------------------------MESSAGE BEGIN ------------------------------------------");
                    Tex.WriteLine("Time: - " + DateTime.Now.ToString("dd MMM yyyy hh:mm:ss tt"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("Message from Client - " + HttpContextServerVariableExtensions.GetServerVariable(context, "REMOTE_HOST") + "," + HttpContextServerVariableExtensions.GetServerVariable(context, "SERVER_NAME"));
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("MESSAGE:- " + Message);
                    Tex.Write(Tex.NewLine);
                    Tex.WriteLine("------------------------------------------MESSAGE END ------------------------------------------");
                    Tex.Write(Tex.NewLine);
                    Tex.Close();
                }
            }
            catch (Exception ex) 
            {
                return;
            }                     
        }
    }
}
