using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities.Employee;
using FourQT.Entities.General;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Nancy.Bootstrapper;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace FourQT.Core.General
{
    public class UploadFilesToExternalServerBLL
    {
        public static async Task<FileUploadResponse> SendFilesToExternalServer(FileUploadRequest model,HttpRequest request, string Type="E")
        {
            FileUploadResponse serverResponse = new FileUploadResponse();
            FileUploadAPIRequest req = new FileUploadAPIRequest();
            string fileUploadDirectory = "MobAppFileUploads";
            string defaultFileGroup = "Default";
            string defaultFileFormat = "";
            DateTime nowDate = DateTime.Now;
            string apiUrl = "";

            try
            {
                if (model != null)
                {
                    if (model.files != null && model.files.Count > 0)
                    {
                        List<FileUploadAPI> apiLst = new List<FileUploadAPI>();
                        string docTypeXML = "<Root>";

                        for (int i = 0; i < model.files.Count; i++)
                        {
                            string? fileGroup = model.files[i].fileGroup;
                            string? fileFormat = model.files[i].fileFormat;
                            string? action = model.files[i].action;
                            string? fileName = model.files[i].fileName;
                            if (fileGroup == null || fileGroup.Trim() == "") { fileGroup = defaultFileGroup; }
                            if (fileFormat == null || fileFormat.Trim() == "") { fileFormat = defaultFileFormat; }
                            if (action == null || action.Trim() == "") { action = "I"; }
                            if (fileName == null || fileName.Trim() == "") { fileName = ""; }

                            string serverFileName = "MobAppUpload_" + fileGroup + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + "_" + nowDate.Ticks + "_" + i + "." + fileFormat;

                            FileUploadAPI api = new FileUploadAPI();
                            if (action != "D")
                            {
                                api.fileName = serverFileName;
                                api.fileFormat = fileFormat.Trim();
                                api.fileGroup = fileGroup.Trim();
                                api.fileBase64String = model.files[i].fileBase64String;
                                api.filePath = fileUploadDirectory + "\\" + fileGroup;
                            }
                            else
                            {
                                api.fileName = fileName.Trim();
                                api.fileFormat = fileFormat.Trim();
                                api.fileGroup = fileGroup.Trim();
                                api.fileBase64String = model.files[i].fileBase64String;
                                api.filePath = fileUploadDirectory + "\\" + fileGroup;
                            }

                            api.id = (Int32.TryParse(model.files[i].id.ToString(), out int id) ? id : 0);
                            api.action = action.Trim();

                            docTypeXML = docTypeXML + "<Row>"
                                        + "<Id>" + api.id + "</Id>"
                                        + "<DocType>" + api.fileGroup + "</DocType>"
                                        + "</Row>";

                            apiLst.Add(api);
                        }
                        docTypeXML = docTypeXML + "</Root>";

                        dynamic[]? pathConfig = await GetFileUploadAPIServer(request, docTypeXML, apiLst, Type);
                        if (pathConfig != null)
                        {
                            if (pathConfig.Length > 0)
                            {
                                apiUrl = pathConfig[0];
                            }
                            if (pathConfig.Length > 1)
                            {
                                List<FileUploadAPI>? apiLstModified = pathConfig[1];
                                if (apiLstModified != null)
                                {
                                    apiLst = apiLstModified;
                                }
                            }
                        }

                        req.files = apiLst;
                    }

                    if(apiUrl!=null && apiUrl.ToString().Trim() != "")
                    {
                        HttpClient httpClient = new HttpClient();
                        httpClient.Timeout = TimeSpan.FromSeconds(200);

                        HttpResponseMessage respMsg = await httpClient.PostAsJsonAsync(apiUrl.ToString().Trim(), req);
                        string respContent = await respMsg.Content.ReadAsStringAsync();
                        dynamic? respObject = JsonConvert.DeserializeObject<dynamic>(respContent);

                        if (respObject != null)
                        {
                            if (respObject.isSuccess != null && (Boolean.TryParse(respObject.isSuccess.ToString(), out Boolean s) ? s : false))
                            {
                                if (respObject.data != null)
                                {
                                    string data = JsonConvert.SerializeObject(respObject.data);
                                    if (data != null) { 
                                        serverResponse = JsonConvert.DeserializeObject<FileUploadResponse>(data);
                                    }
                                }
                            }
                        }
                    }
                }

                //var response = await httpClient.PostAsync(Url, new StringContent("Json string", Encoding.UTF8, "application/json"));
            }
            catch
            {
                throw;
            }

            return serverResponse;
        }

        public static FileUploadRequest ProcessDocumentUploadList(List<UploadDocumentRequest> docList)
        {
            FileUploadRequest uploadList = new FileUploadRequest();

            try
            {
                if(docList!=null && docList.Count > 0)
                {
                    List<FileUpload> lst = new List<FileUpload>();
                    for(int i = 0; i < docList.Count; i++)
                    {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = (docList[i].image != null ? docList[i].image : "");
                        file.fileGroup = "E_U_DOC";
                        file.fileFormat= (docList[i].docFormat != null ? docList[i].docFormat : "");
                        file.id = (Int32.TryParse(docList[i].docId.ToString(), out int id) ? id : 0);
                        file.action = (docList[i].action != null ? docList[i].action : "I");
                        lst.Add(file);
                    }
                    uploadList.files = lst;
                }
            }
            catch
            {
                throw;
            }

            return uploadList;
        }

        public static async Task<FileUploadRequest> GetDeletedFileNames(FileUploadRequest model, HttpRequest req,int registrationId)
        {
            FileUploadRequest lst = new FileUploadRequest();
            try
            {
                if (model != null && model.files != null && model.files.Count > 0)
                {
                    string docXML = "<root>";
                    for (int i = 0; i < model.files.Count; i++)
                    {
                        FileUpload request = model.files[i];
                        string action = (request.action != null ? request.action : "");

                        if (action == "D")
                        {
                            docXML = docXML + "<document>";
                            docXML = docXML + "<Doc_Id>" + (Int32.TryParse(request.id.ToString(), out int id) ? id : 0) + "</Doc_Id>";
                            docXML = docXML + "<Image></Image>";
                            docXML = docXML + "<BarCode></BarCode>";
                            docXML = docXML + "<PhysicalStorageNo></PhysicalStorageNo>";
                            docXML = docXML + "<ScanReferenceNo></ScanReferenceNo>";
                            docXML = docXML + "<Action>" + (request.action != null ? request.action : "I") + "</Action>";
                            docXML = docXML + "<FileNameWithPath></FileNameWithPath>";
                            docXML = docXML + "</document>";                            
                        }
                    }
                    docXML = docXML + "</root>";

                    (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                    string spName = "API_SaveDocuments_V1";
                    List<SqlParameter> lstParam = new List<SqlParameter>
                     {
                         new SqlParameter() { ParameterName = "@Status", Value = 0 },
                         new SqlParameter() { ParameterName = "@OutMsg", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                         new SqlParameter() { ParameterName = "@Registration_Id", Value = registrationId },
                         new SqlParameter() { ParameterName = "@DocXML", SqlDbType=SqlDbType.Xml, Value = docXML },
                     };

                    lstParam[0].Direction = ParameterDirection.Output;
                    lstParam[1].Direction = ParameterDirection.Output;

                    dynamic[] result = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                    if (result != null && result.Length > 0)
                    {
                        DataSet ds = result[0];
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                                {
                                    int doc_Id = (Int32.TryParse(ds.Tables[0].Rows[i]["Doc_Id"].ToString(), out int doc) ? doc : 0);
                                    string? document = ds.Tables[0].Rows[i]["DeleteList"].ToString();

                                    for (int j = 0; j < model.files.Count; j++)
                                    {
                                        int docModel = (Int32.TryParse(model.files[j].id.ToString(), out int id) ? id : 0);
                                        if (doc_Id != 0 && docModel != 0 && doc_Id == docModel)
                                        {
                                            model.files[j].fileName = (document != null ? document : "");
                                        }
                                    }
                                }
                            }
                        }

                    }
                }

                if (model != null) { lst = model; }
            }
            catch
            {
                throw;
            }

            return lst;
        }

        public static async Task<dynamic[]> GetFileUploadAPIServer(HttpRequest req,string docTypeXML="", List<FileUploadAPI>? fileList=null,string Type="E")
        {
            string? api = "";
            try
            {
                int loginId = 0;string conn = "";

                if (Type == "C")
                {
                    (new JWTTokenMethods()).GetConnectionDetailsCustomer(req, out loginId, out conn);
                    conn = (new JWTTokenMethods()).GetMainKeyFromCustomerKey(conn);
                }
                else {
                    (new JWTTokenMethods()).GetConnectionDetails(req, out loginId, out conn);
                }

                string spName = "API_GetFileUploadServer";
                List<SqlParameter> lstParam = new List<SqlParameter>
                     {
                         new SqlParameter() { ParameterName = "@Status", Value = 0 },
                         new SqlParameter() { ParameterName = "@OutMsg", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                         new SqlParameter() { ParameterName = "@DocTypeXML", Value = docTypeXML, SqlDbType=SqlDbType.Xml}
                     };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                DataSet? result = null;

                if (Type == "HR")
                {
                    result = await DBHelper.GetDatasetAsyncHR(conn, CommandType.StoredProcedure, spName, lstParam);
                }
                else {
                    dynamic[] resultArr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                    if (resultArr != null && resultArr.Length > 0) { 
                        result = resultArr[0];
                    }
                }
                
                if(result!=null && result.Tables.Count > 0)
                {
                    DataTable dt = result.Tables[0];
                    if(dt!=null && dt.Rows.Count > 0)
                    {
                        api = (dt.Rows[0]["APIServer"] != null ? dt.Rows[0]["APIServer"].ToString() : "");
                    }
                }
                if (result != null && result.Tables.Count > 1)
                {
                    DataTable dt = result.Tables[1];
                    if (dt != null && dt.Rows.Count > 0 && fileList!=null && fileList.Count>0)
                    {
                        for (int i = 0;i< dt.Rows.Count; i++)
                        {
                            int xmlId = 0;
                            Int32.TryParse(dt.Rows[i]["Id"].ToString(), out xmlId);

                            for (int j = 0; j < fileList.Count; j++) {
                                int fileId = 0;
                                Int32.TryParse(fileList[j].id.ToString(), out fileId);

                                if (xmlId == fileId)
                                {
                                    fileList[j].filePath = (dt.Rows[i]["FullPath"] != null ? dt.Rows[i]["FullPath"].ToString() : "");
                                    fileList[j].fileGroup = (dt.Rows[i]["DocType"] != null ? dt.Rows[i]["DocType"].ToString() : "");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw;
            }

            if (api == null) { api = ""; }
            return new dynamic[] { api, fileList };
        }
    }
}
