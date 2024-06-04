using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;

namespace FourQT.Utilities
{
    public class Utility
    {
        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (String.Compare(pro.Name, column.ColumnName, true) == 0 && dr[column.ColumnName] != DBNull.Value)
                        pro.SetValue(obj, dr[column.ColumnName], null);
                    else
                        continue;
                }
            }
            return obj;
        }

        public static string GetMinDate()
        {
            return "1900-01-01";
        }

        public static string GetMaxDate()
        {
            return "2099-12-31";
        }

        public static string GetCurrentDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        public static string GetIPAddress()
        {
            string ipAddress = string.Empty;
            string hostName = string.Empty;
            try
            {
                hostName = Dns.GetHostName();
                if (!String.IsNullOrEmpty(hostName))
                {
                    IPHostEntry ipEntry = Dns.GetHostEntry(Dns.GetHostName());
                    IPAddress[] addr = ipEntry.AddressList;
                    if (!Object.Equals(addr, null))
                    {
                        ipAddress = addr[addr.Length - 1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return ipAddress;
        }

        public static string GetXMLFromCommaSeparated(string Input)
        {
            string xml = "<Rows>";

            if (!String.IsNullOrEmpty(Input))
            {
                string[] arr = Input.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                foreach (string item in arr)
                {
                    xml = xml + "<Row><Id>" + item + "</Id></Row>";
                }
            }

            xml = xml + "</Rows>";
            return xml;
        }

        public static string GetXMLFromIntList(List<int> List)
        {
            string xml = "<Rows>";

            if (!Object.Equals(List, null))
            {
                foreach (int listItem in List)
                {
                    xml = xml + "<Row><Id>" + listItem + "</Id></Row>";
                }
            }

            xml = xml + "</Rows>";
            return xml;
        }

        public static string GetXMLFromStringList(List<string> List)
        {
            string xml = "<Rows>";

            if (!Object.Equals(List, null))
            {
                foreach (string listItem in List)
                {
                    xml = xml + "<Row><Id>" + listItem + "</Id></Row>";
                }
            }

            xml = xml + "</Rows>";
            return xml;
        }

        public static string GetXMLFromDataTableRowWise(DataTable DtInput)
        {
            string xml = "";
            if (!Object.Equals(DtInput, null))
            {
                StringBuilder sBuild = new StringBuilder();
                sBuild.Append("<ROWS>");
                foreach (DataRow dr in DtInput.Rows)
                {
                    sBuild.Append("<ROW>");
                    foreach (DataColumn col in DtInput.Columns)
                    {
                        sBuild.Append("<" + col.ColumnName.ToUpper() + ">");

                        sBuild.Append(dr[col].ToString());

                        sBuild.Append("</" + col.ColumnName.ToUpper() + ">");
                    }
                    sBuild.Append("</ROW>");
                }
                sBuild.Append("</ROWS>");
                xml = sBuild.ToString();
            }
            return xml;
        }

        public static string GetXMLFromDataTable(DataTable DtInput)
        {
            string xml = "";
            if (!Object.Equals(DtInput, null))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    DtInput.WriteXml(ms, true);
                    ms.Position = 0;
                    //result = new XPathDocument(ms);

                    StreamReader sr = new StreamReader(ms);
                    xml = sr.ReadToEnd();
                }
            }
            return xml;
        }

        public static DateTime ConvertTo24HrFormat(DateTime Date, int Hour, int Minute, string AMPM)
        {
            DateTime ret24HrDate = Date;
            if (Hour == 12)
                Hour = 0;
            if (AMPM == "PM")
            {
                Hour = Hour + 12;
            }

            ret24HrDate = ret24HrDate.AddHours(Hour);
            ret24HrDate = ret24HrDate.AddMinutes(Minute);

            return ret24HrDate;
        }


        public static DataSet GetDsFromExcel(String FileName, String FilePath, string Ext)
        {
            DataSet output = new DataSet();
            string connStr = "";
            if (String.Compare(Ext, ".xls", true) == 0)
            {
                connStr = String.Format(ConfigurationManager.ConnectionStrings["conxls"].ConnectionString, FilePath);
            }
            else if (String.Compare(Ext, ".xlsx", true) == 0)
            {
                connStr = String.Format(ConfigurationManager.ConnectionStrings["conxlsx"].ConnectionString, FilePath);
            }
            else
            {
                throw new Exception("Invalid file/Not Excel file");
            }

            if (connStr != String.Empty)
            {
                using (OleDbConnection conn = new OleDbConnection(connStr))
                {
                    conn.Open();

                    //  DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    //int counter = -1;
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", conn);
                    cmd.CommandType = CommandType.Text;

                    DataTable outputTable = new DataTable("Data");
                    output.Tables.Add(outputTable);
                    new OleDbDataAdapter(cmd).Fill(outputTable);

                    //datatableName = "Followup";
                    //cmd = new OleDbCommand("SELECT * FROM [Sheet2$]", conn);
                    //cmd.CommandType = CommandType.Text;
                    //outputTable = new DataTable(datatableName);
                    //output.Tables.Add(outputTable);
                    //new OleDbDataAdapter(cmd).Fill(outputTable);
                }

            }

            return output;
        }

        public static String GetExcelColumnByIndex(int Index)
        {
            String excelCol;
            if (Index <= 26)
            {
                excelCol = Convert.ToChar(Index + 64).ToString();
            }
            else
            {
                if (Index % 26 == 0)
                    excelCol = Convert.ToChar(64 + Index / 26 - 1).ToString() + Convert.ToChar(64 + 26).ToString();
                else
                    excelCol = Convert.ToChar(64 + Index / 26).ToString() + Convert.ToChar(64 + Index % 26).ToString();
            }
            return excelCol;
        }

        public static string RemoveMultipleSpace(string Input)
        {
            return Regex.Replace(Input, @"\s+", " ", RegexOptions.Multiline);
        }


        public static void LogErrorText(string errormessage,string APIName)
        {
            //string path = "E:\\Yogesh\\MobAppCoreAPI_6\\FourQT.Utilities\\Utility.cs";
            //File.AppendAllTextAsync(path, "APIName-" + APIName + " ErrorMessage-" + errormessage+ " Datetime-"+DateTime.Now);

        }

        //public static string Encrypt(string toEncrypt, bool useHashing)
        //{
        //    byte[] keyArray;
        //    byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        //    System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        //    // Get the key from config file

        //    string key = "nitinmona@2007";// (string)settingsReader.GetValue("SecurityKey",typeof(String));
        //    //System.Windows.Forms.MessageBox.Show(key);
        //    //If hashing use get hashcode regards to your key
        //    if (useHashing)
        //    {
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //        //Always release the resources and flush data
        //        // of the Cryptographic service provide. Best Practice

        //        hashmd5.Clear();
        //    }
        //    else
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    //set the secret key for the tripleDES algorithm
        //    tdes.Key = keyArray;
        //    //mode of operation. there are other 4 modes.
        //    //We choose ECB(Electronic code Book)
        //    tdes.Mode = CipherMode.ECB;
        //    //padding mode(if any extra byte added)

        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateEncryptor();
        //    //transform the specified region of bytes array to resultArray
        //    byte[] resultArray =
        //      cTransform.TransformFinalBlock(toEncryptArray, 0,
        //      toEncryptArray.Length);
        //    //Release resources held by TripleDes Encryptor
        //    tdes.Clear();
        //    //Return the encrypted data into unreadable string format
        //    return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        //}


        //public static string Decrypt(string cipherString, bool useHashing)
        //{
        //    byte[] keyArray;
        //    //get the byte code of the string

        //    byte[] toEncryptArray = Convert.FromBase64String(cipherString);

        //    System.Configuration.AppSettingsReader settingsReader = new AppSettingsReader();
        //    //Get your key from config file to open the lock!
        //    string key = "nitinmona@2007";// (string)settingsReader.GetValue("SecurityKey",typeof(String));

        //    if (useHashing)
        //    {
        //        //if hashing was used get the hash code with regards to your key
        //        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
        //        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
        //        //release any resource held by the MD5CryptoServiceProvider

        //        hashmd5.Clear();
        //    }
        //    else
        //    {
        //        //if hashing was not implemented get the byte code of the key
        //        keyArray = UTF8Encoding.UTF8.GetBytes(key);
        //    }

        //    TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
        //    //set the secret key for the tripleDES algorithm
        //    tdes.Key = keyArray;
        //    //mode of operation. there are other 4 modes. 
        //    //We choose ECB(Electronic code Book)

        //    tdes.Mode = CipherMode.ECB;
        //    //padding mode(if any extra byte added)
        //    tdes.Padding = PaddingMode.PKCS7;

        //    ICryptoTransform cTransform = tdes.CreateDecryptor();
        //    byte[] resultArray = cTransform.TransformFinalBlock(
        //                         toEncryptArray, 0, toEncryptArray.Length);
        //    //Release resources held by TripleDes Encryptor                
        //    tdes.Clear();
        //    //return the Clear decrypted TEXT
        //    return UTF8Encoding.UTF8.GetString(resultArray);
        //}


        public static List<dynamic> ToDynamic(DataTable dt)
        {
            var dynamicDt = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                dynamic dyn = new ExpandoObject();
                dynamicDt.Add(dyn);
                foreach (DataColumn column in dt.Columns)
                {
                    var dic = (IDictionary<string, object>)dyn;
                    dic[column.ColumnName] = row[column];
                }
            }
            return dynamicDt;
        }


        public static string GetMonthAbbr(int MonthInt)
        {
            string[] arrMonthAbbr = { "Jan", "Feb", "Mar", "Apr", "May" , "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            return arrMonthAbbr[MonthInt-1];
        }

        public static string GetMonthName(int MonthInt)
        {
            string[] arrMonthAbbr = { "January", "February", "March", "April", "May", "Jun", "July", "August", "September", "October", "November", "December" };
            return arrMonthAbbr[MonthInt - 1];
        }

        public static string NumberToText(long number)
        {
            if (number == 0)
            {
                //gwProp.WordFromNumber = "Zero";
                return "Zero";
            }
            string and = ""; // deals with UK or US numbering
            if (number == -2147483648)
            {
                //gwProp.WordFromNumber = "Minus Two Billion One Hundred " + and +
                //    "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
                //    "Six Hundred " + and + "Forty Eight";
                return "Minus Two Billion One Hundred " + and +
                    "Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
                    "Six Hundred " + and + "Forty Eight";
            }

            long[] num = new long[6];
            long first = 0;
            long u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore ", "Arab ", "Kharab " };

            num[0] = number % 1000;     // units
            num[1] = number / 1000;     // thousands      
            num[2] = number / 100000;   // Lac

            num[3] = number / 10000000;     // Crore
            num[4] = number / 1000000000;   // Arab
            num[5] = number / 100000000000; // Kharab

            num[1] = num[1] - 100 * num[2];  // thousands            
            num[2] = num[2] - 100 * num[3];  // Lac
            num[3] = num[3] - 100 * num[4];  // Crore
            num[4] = num[4] - 100 * num[5];  // Arab

            for (int i = 5; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (long i = first; i >= 0; i--)
            {
                if (num[i] == 0)
                    continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        public static void GetDateParts(string combinedDate,out string date,out string time, out string format)
        {
            date = ""; time = ""; format="";
            if (combinedDate != null)
            {
                string[] dateArray = combinedDate.Trim().Split(' ');

                if (dateArray.Length > 2)
                {
                    date = dateArray[0];
                    time = dateArray[1];
                    format = dateArray[2];
                }
            }
        }

        public static string RemoveEscapeSeqChars(string? originalStr, string replaceWith = "")
        {
            try {
                if (originalStr != null && originalStr.Trim() != "") {
                    replaceWith = (replaceWith != null ? replaceWith : "");
                    originalStr = originalStr.Replace("\r\n", replaceWith);
                    originalStr = originalStr.Replace("\r", replaceWith);
                    originalStr = originalStr.Replace("\n", replaceWith);
                    originalStr = originalStr.Replace("\t", replaceWith);
                    originalStr = originalStr.Replace("<BR/>", replaceWith);
                    originalStr = originalStr.Replace("<br/>", replaceWith);
                }
            }
            catch {
                throw;
            }

            return originalStr;
        }

    }
}


