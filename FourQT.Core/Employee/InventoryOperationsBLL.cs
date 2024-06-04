using System.Data.SqlClient;
using System.Data;
using FourQT.CommonFunctions.Portal;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities.Employee;
using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using FourQT.Entities.General;
using System.Net.Http.Json;
using FourQT.Core.General;
using System.Globalization;

namespace FourQT.Core.Employee
{
    public class InventoryOperationsBLL
    {
        public async Task<dynamic> holdInventory(HoldUnitRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "HoldUnit", context);

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_HoldUnit";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                    new SqlParameter() { ParameterName = "@Address_Id", Value = (Int32.TryParse(model.unitId.ToString(),out int id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@Broker_Id", Value = (Int32.TryParse(model.channelPartnerId.ToString(),out id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@HoldRemark", Value = model.remarks, SqlDbType=SqlDbType.VarChar, Size=500 },
                    new SqlParameter() { ParameterName = "@HoldBy", Value = loginId}
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                if (result != null)
                {
                    if (result[0] == "1")
                    {
                        genResponse.IsSuccess = true;
                        genResponse.Status = HttpStatusCode.OK;
                        genResponse.Title = "Success";

                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                        genResponse.Title = "Fail";
                    }

                    genResponse.Message = result[1].ToString();
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> unholdInventory(HoldUnitRequestU model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "UnHoldUnit", context);

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_UnHoldUnit";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Status", Value = 0 },
                    new SqlParameter() { ParameterName = "@OutMsg", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                    new SqlParameter() { ParameterName = "@Address_Id", Value = (Int32.TryParse(model.unitId.ToString(),out int id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@Broker_Id", Value = (Int32.TryParse(model.channelPartnerId.ToString(),out id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@UnHoldRemark", Value = model.remarks, SqlDbType=SqlDbType.VarChar, Size=500 },
                    new SqlParameter() { ParameterName = "@LoginId", Value = loginId}
                };

                lstParam[0].Direction = ParameterDirection.Output;
                lstParam[1].Direction = ParameterDirection.Output;

                string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                if (result != null)
                {
                    if (result[0] == "1")
                    {
                        genResponse.IsSuccess = true;
                        genResponse.Status = HttpStatusCode.OK;
                        genResponse.Title = "Success";

                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                        genResponse.Title = "Fail";
                    }

                    genResponse.Message = result[1].ToString();
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> sellInventory(SellInventoryRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            ServerResponse serverResponse = new ServerResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "SellUnit", context);

                serverResponse = await uploadSoldDocumentsToServer(model, req);
                if(serverResponse!=null && serverResponse.isSuccess)
                {
                    FileUploadResponse? upFiles = serverResponse.uploadedFiles;
                    if (upFiles != null && upFiles.files!=null && upFiles.files.Count >= 2)
                    {
                        string? custPhotoServerName = upFiles.files[0].filePathOnServer;
                        string? chequeServerName = upFiles.files[1].filePathOnServer;

                        if (custPhotoServerName != null && chequeServerName != null)
                        {
                            (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                            string spName = "API_SoldUnit";
                            List<SqlParameter> lstParam = new List<SqlParameter>
                            {
                                new SqlParameter() { ParameterName = "@Status", Value = 0 },
                                new SqlParameter() { ParameterName = "@OutMsg", Value = "", SqlDbType=SqlDbType.VarChar, Size=500 },
                                new SqlParameter() { ParameterName = "@Login_Id", Value = loginId},
                                new SqlParameter() { ParameterName = "@Address_Id", Value = (Int32.TryParse(model.unitId.ToString(),out int id) ? id : 0 ) },
                                new SqlParameter() { ParameterName = "@CustomerName", SqlDbType=SqlDbType.VarChar,Size=100, Value = (model.customerName!=null ? model.customerName.ToString() : "" ) },
                                new SqlParameter() { ParameterName = "@CustomerPhoto", SqlDbType=SqlDbType.VarChar,Size=300, Value = custPhotoServerName.ToString().Trim() },
                                new SqlParameter() { ParameterName = "@MobileNo", SqlDbType=SqlDbType.VarChar,Size=25, Value = (model.customerMobile!=null ? model.customerMobile.ToString() : "" ) },
                                new SqlParameter() { ParameterName = "@BookingAmount", Value = (Double.TryParse(model.amount.ToString(),out double amount) ? amount : 0 ) },
                                new SqlParameter() { ParameterName = "@ChequeCopy", SqlDbType=SqlDbType.VarChar,Size=300, Value = chequeServerName.ToString().Trim() }
                            };

                            lstParam[0].Direction = ParameterDirection.Output;
                            lstParam[1].Direction = ParameterDirection.Output;

                            string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                            if (result != null)
                            {
                                if (result[0] == "1")
                                {
                                    genResponse.IsSuccess = true;
                                    genResponse.Status = HttpStatusCode.OK;
                                    genResponse.Title = "Success";

                                }
                                else
                                {
                                    genResponse.IsSuccess = false;
                                    genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                                    genResponse.Title = "Fail";
                                }

                                genResponse.Message = result[1].ToString();
                            }
                        }
                        else
                        {
                            genResponse.IsSuccess = false;
                            genResponse.Message = "Error uploading files.";
                            genResponse.Data = null;
                            genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                            genResponse.Title = "Error";
                        }

                    }
                    else
                    {
                        genResponse.IsSuccess = false;
                        genResponse.Message = serverResponse.message;
                        genResponse.Data = null;
                        genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                        genResponse.Title = "Error";
                    }

                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Message = ((serverResponse != null && serverResponse.message != null) ? serverResponse.message : "Error uploading files.");
                    genResponse.Data = null;
                    genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                    genResponse.Title = "Error";
                }


            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Title = "Error";
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> uploadSoldDocumentsToServer(SellInventoryRequest model,HttpRequest req)
        {
            FileUploadRequest fileUploadRequest = new FileUploadRequest();
            FileUploadResponse uploadedDocList = new FileUploadResponse();
            ServerResponse response = new ServerResponse();
            int noOfFilesUploaded = 0;
            string errorMsg = "";
            Boolean uploadSuccess = false;

            try
            {
                if (model != null) {
                    string? customerPhoto = model.customerPhoto;
                    string? cheque = model.chequeCopy;
                    string? custPhotoFormat = model.customerPhotoFormat;
                    string? chequeFormat = model.chequeCopyFormat;

                    List<FileUpload> fileList = new List<FileUpload>();

                    if (customerPhoto != null && customerPhoto.ToString().Trim() != "") {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = customerPhoto;
                        file.action = "I";
                        file.fileFormat = (custPhotoFormat != null ? custPhotoFormat : "");
                        file.fileGroup = "E_S_CP";
                        file.fileName = "";
                        file.id = 1;

                        fileList.Add(file);
                    }

                    if (cheque != null && cheque.ToString().Trim() != "")
                    {
                        FileUpload file = new FileUpload();
                        file.fileBase64String = cheque;
                        file.action = "I";
                        file.fileFormat = (chequeFormat != null ? chequeFormat : "");
                        file.fileGroup = "E_S_CC";
                        file.fileName = "";
                        file.id = 2;

                        fileList.Add(file);
                    }

                    if (fileList != null && fileList.Count > 0) {
                        fileUploadRequest.files = fileList;

                        uploadedDocList = await UploadFilesToExternalServerBLL.SendFilesToExternalServer(fileUploadRequest, req);
                        if (uploadedDocList != null && uploadedDocList.files != null && uploadedDocList.files.Count > 0)
                        {
                            for (int i = 0; i < uploadedDocList.files.Count; i++)
                            {
                                UploadedFile upFile = uploadedDocList.files[i];

                                if (upFile.fileUploaded)
                                {
                                    noOfFilesUploaded++;
                                }
                                else {
                                    if (upFile.id == 1)
                                    {
                                        errorMsg = "Error uploading customer photo.";
                                    }
                                    else
                                    {
                                        errorMsg = "Error uploading cheque copy.";
                                    }
                                }
                                
                            }
                        }
                    }                    
                }

                if (noOfFilesUploaded >= 2) {
                    errorMsg = "Success";
                    uploadSuccess = true;
                }
                else if (noOfFilesUploaded <= 0)
                {
                    errorMsg = "Error uploading customer photo and cheque copy.";
                    uploadSuccess = false;
                }
            }
            catch (Exception ex) {
                errorMsg = ex.Message;
                uploadSuccess = false;
            }

            response.isSuccess = uploadSuccess;
            response.message = errorMsg;
            response.uploadedFiles = uploadedDocList;
            return response;
        }

        public async Task<dynamic> listSoldBookedInventoryDetails(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            InventoryPageSoldBooked inventoryPage = new InventoryPageSoldBooked();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "ListSoldBookedInventoryDetails", context);

                if (model.type != "S" && model.type != "B" && model.type != "A")
                {
                    model.type = "A";
                }

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_SoldBookedDetail";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@TotalRecords", Value = 0 },
                    new SqlParameter() { ParameterName = "@ProjectId", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@TowerId", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@FloorId", Value = (Int32.TryParse(model.floorId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Location_Id", Value = model.locationId },
                    new SqlParameter() { ParameterName = "@Type", Value = ((model.type!="") ? model.type : null) },
                    new SqlParameter() { ParameterName = "@Login_Id", Value = (Int32.TryParse(loginId.ToString(),out id) ? id : 0)  },
                    new SqlParameter() { ParameterName = "@AreaMin", Value = (Decimal.TryParse(model.areaMin.ToString(),out decimal d) ? d : null) },
                    new SqlParameter() { ParameterName = "@AreaMax", Value = (Decimal.TryParse(model.areaMax.ToString(),out d) ? d : null)},
                    new SqlParameter() { ParameterName = "@UnitNo", Value = model.unitNo },
                    new SqlParameter() { ParameterName = "@PageNo", Value = (Int32.TryParse(model.pageNo.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@PageSize", Value = (Int32.TryParse(model.pageSize.ToString(),out id) ? id : 0) },
                };

                if (model.bookingDateFrom != null && DateTime.TryParseExact(model.bookingDateFrom, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bookFrom) && bookFrom.Year > 1900)
                {
                    lstParam.Add(new SqlParameter() { ParameterName = "@BookingDateFrom", Value = bookFrom });
                }
                if (model.bookingDateTo != null && DateTime.TryParseExact(model.bookingDateTo, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime bookTo) && bookTo.Year > 1900)
                {
                    lstParam.Add(new SqlParameter() { ParameterName = "@BookingDateTo", Value = bookTo });
                }

                lstParam[0].Direction = ParameterDirection.Output;

                DataSet? result = null;
                dynamic[] data = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (data != null && data.Length > 0)
                {
                    result = data[0];
                }

                int totalRecords = (Int32.TryParse(lstParam[0].Value.ToString(), out int total) ? total : 0);
                inventoryPage.totalRecords = totalRecords;

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[0];
                        List<InventoryDetailsResponseSoldBooked> inventory = new List<InventoryDetailsResponseSoldBooked>();
                        foreach (DataRow row in dt.Rows)
                        {
                            InventoryDetailsResponseSoldBooked unit = new InventoryDetailsResponseSoldBooked();
                            unit.registrationId = (Int32.TryParse(row["Registration_Id"].ToString(), out int i) ? i : 0);
                            unit.registrationNo = (row["Registration_No"] != null ? row["Registration_No"].ToString() : "");
                            unit.unitId = (Int32.TryParse(row["Address_Id"].ToString(), out i) ? i : 0);
                            unit.unitNo = (row["UnitNo"] != null ? row["UnitNo"].ToString() : "");
                            unit.unitType = (row["Project_Unit_Type_Name"] != null ? row["Project_Unit_Type_Name"].ToString() : "");
                            unit.tower = (row["Tower"] != null ? row["Tower"].ToString() : "");
                            unit.floor = (row["Floor"] != null ? row["Floor"].ToString() : "");
                            unit.superArea = (row["SuperArea"] != null ? row["SuperArea"].ToString() : ""); 
                            unit.carpetArea = (row["CarpetArea"] != null ? row["CarpetArea"].ToString() : ""); 
                            unit.builtUpArea = (row["BuildupArea"] != null ? row["BuildupArea"].ToString() : "");
                            unit.customerName = (row["CustomerName"] != null ? row["CustomerName"].ToString() : "");
                            unit.customerPhoto = (row["CustomerPhoto"] != null ? row["CustomerPhoto"].ToString() : "");
                            unit.customerMobile = (row["MobileNo"] != null ? row["MobileNo"].ToString() : "");
                            unit.bookingAmount = (Decimal.TryParse(row["BookingAmount"].ToString(), out d) ? d : 0);
                            unit.chequeCopy = (row["ChequeCopy"] != null ? row["ChequeCopy"].ToString() : "");
                            unit.bookingDate = (row["BookingDate"] != null ? row["BookingDate"].ToString() : "");
                            unit.soldBy = (row["SoldBy"] != null ? row["SoldBy"].ToString() : "");
                            unit.type = (row["Type"] != null ? row["Type"].ToString() : "");
                            unit.colorCode= (row["ColorCode"] != null ? row["ColorCode"].ToString() : "");
                            unit.projectId= (Int32.TryParse(row["Project_Id"].ToString(), out i) ? i : 0);
                            unit.superAreaLabel = (row["SuperAreaLbl"] != null ? row["SuperAreaLbl"].ToString() : "");
                            unit.carpetAreaLabel = (row["Carpet_AreaLbl"] != null ? row["Carpet_AreaLbl"].ToString() : "");
                            unit.builtUpAreaLabel = (row["Build_Up_AreaLbl"] != null ? row["Build_Up_AreaLbl"].ToString() : "");
                            unit.superAreaDisplay = (row["SuperAreaDisp"].ToString() != "0" ? true : false);
                            unit.carpetAreaDisplay = (row["Carpet_AreaDisp"].ToString() != "0" ? true : false);
                            unit.builtUpAreaDisplay = (row["Build_Up_AreaDisp"].ToString() != "0" ? true : false);
                            inventory.Add(unit);
                        }

                        inventoryPage.unit = inventory.OrderBy(o => o.unitId).ToList();
                    }
                }

                genResponse.Data = inventoryPage;
                genResponse.IsSuccess = true;                
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> getUploadDocumentList(DocumentListRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "GetDocumentUploadList", context);

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_GetDocuments";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Project_Id", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@Registration_Id", Value = (Int32.TryParse(model.registrationId.ToString(),out id) ? id : 0 ) },
                    new SqlParameter() { ParameterName = "@Type", Value = "On Booking" }
                };

                DataSet? result = null;
                dynamic[] data = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (data != null && data.Length > 0)
                {
                    result = data[0];
                }

                if (result != null && result.Tables.Count > 0)
                {
                    if (result.Tables[0] != null && result.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = result.Tables[0];
                        List<DocumentListResponse> inventory = new List<DocumentListResponse>();
                        foreach (DataRow row in dt.Rows)
                        {
                            DocumentListResponse doc = new DocumentListResponse();
                            doc.docId = (Int32.TryParse(row["Doc_Id"].ToString(), out int i) ? i : 0);
                            doc.docName = (row["Document_name"] != null ? row["Document_name"].ToString() : "");
                            doc.type = (row["Type"] != null ? row["Type"].ToString() : "");
                            doc.description = (row["Discription"] != null ? row["Discription"].ToString() : "");
                            doc.barcode = (row["BarCode"] != null ? row["BarCode"].ToString() : "");
                            doc.physicalStorageNo = (row["PhysicalStorageNo"] != null ? row["PhysicalStorageNo"].ToString() : "");
                            doc.scanRefNo = (row["ScanReferenceNo"] != null ? row["ScanReferenceNo"].ToString() : "");
                            doc.image = (row["Image"] != null ? row["Image"].ToString() : "");
                            doc.mandatory = (row["Mandatory"] != null ? row["Mandatory"].ToString() : "");
                            doc.uploaded = (Boolean.TryParse(row["Uploaded"].ToString(), out bool b) ? b : false);
                            doc.canDelete = (Boolean.TryParse(row["CanDelete"].ToString(), out b) ? b : false);
                            inventory.Add(doc);
                        }
                        genResponse.Data = inventory;
                    }
                }

                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Message = "Success";
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }       

        public async Task<dynamic> uploadDocuments(UploadDocumentRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            try
            {
                if (model != null)
                {
                    List<UploadDocumentRequest> modelLst = new List<UploadDocumentRequest>();
                    modelLst.Add(model);

                    return await uploadDocumentsMulti(modelLst, req, context);
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Invalid";
                    genResponse.Message= "Invalid Request";
                }
            }
            catch(Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Title = "Error";
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> uploadDocumentsMulti(List<UploadDocumentRequest> model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            FileUploadRequest uploadList = new FileUploadRequest();
            FileUploadResponse uploadedDocList = new FileUploadResponse();
            int registrationId = 0,uploadedFileCount=0;
            int status = 0;string outMsg = "";

            genResponse.IsSuccess = false;
            genResponse.Title = "Failed";
            genResponse.Message = "Failed ";
            genResponse.Status = HttpStatusCode.BadRequest;

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "UploadDocuments", context);
              
                uploadList = UploadFilesToExternalServerBLL.ProcessDocumentUploadList(model);

                if(model!=null && model.Count > 0)
                {
                    if ((Int32.TryParse(model[0].registrationId.ToString(), out int reg) ? reg : 0) != 0)
                    {
                        Int32.TryParse(model[0].registrationId.ToString(), out registrationId);
                    }
                }

                uploadList = await UploadFilesToExternalServerBLL.GetDeletedFileNames(uploadList, req, registrationId);

                if (uploadList != null && model!=null)
                {
                    
                    string docXML = "<root>";
                    uploadedDocList = await UploadFilesToExternalServerBLL.SendFilesToExternalServer(uploadList,req);

                    if (uploadedDocList != null && uploadedDocList.files!=null && uploadedDocList.files.Count > 0) 
                    {
                        for(int i = 0; i < uploadedDocList.files.Count; i++)
                        {
                            UploadedFile upFile = uploadedDocList.files[i];

                            for(int j = 0; j < model.Count; j++)
                            {
                                UploadDocumentRequest upDoc = model[j];
                                if (upFile.id == upDoc.docId)
                                {
                                    if (upDoc.action != "D")
                                    {
                                        if (upFile.fileUploaded)
                                        {
                                            docXML = docXML + "<document>";
                                            docXML = docXML + "<Doc_Id>" + (Int32.TryParse(upDoc.docId.ToString(), out int id) ? id : 0) + "</Doc_Id>";
                                            docXML = docXML + "<Image>" + (upFile.fileNameOnServer != null ? upFile.fileNameOnServer : "") + "</Image>";
                                            docXML = docXML + "<BarCode>" + (upDoc.barcode != null ? upDoc.barcode : "") + "</BarCode>";
                                            docXML = docXML + "<PhysicalStorageNo>" + (upDoc.physicalStorageNo != null ? upDoc.physicalStorageNo : "") + "</PhysicalStorageNo>";
                                            docXML = docXML + "<ScanReferenceNo>" + (upDoc.scanRefNo != null ? upDoc.scanRefNo : "") + "</ScanReferenceNo>";
                                            docXML = docXML + "<Action>" + (upDoc.action != null ? upDoc.action : "I") + "</Action>";
                                            docXML = docXML + "<FileNameWithPath>" + (upFile.filePathOnServer != null ? upFile.filePathOnServer : "") + "</FileNameWithPath>";
                                            docXML = docXML + "</document>";
                                            uploadedFileCount++;
                                        }
                                        else
                                        {
                                            outMsg = "Error Uploading File: " + upFile.message;
                                        }
                                    }
                                    else
                                    {
                                        if (upFile.fileUploaded)
                                        {
                                            uploadedFileCount++;
                                        }
                                        else
                                        {
                                            outMsg = "Error Deleting File: " + upFile.message;
                                        }
                                    }
                                                                 
                                    break;
                                }
                            }
                        }
                    }
                    docXML = docXML + "</root>";

                    if (uploadedFileCount > 0)
                    {
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

                        string[] result = await DBHelper.ExecuteNonQueryEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);

                        if (result != null && result.Length >= 2)
                        {
                            status = (Int32.TryParse(result[0].ToString(), out int s) ? s : 0);
                            outMsg = (result[1] != null ? result[1].ToString() : "");
                        }
                    }
                    else
                    {
                        status = 0;
                    }
                }

                if (status == 1)
                {
                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Title = "Success";
                    genResponse.Message = "Success";
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Failed";
                    genResponse.Message = outMsg;
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> FileUploadTestAPI(FileUploadRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            FileUploadResponse response = new FileUploadResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "FileUploadTestAPI", context);

                response = await SendUploadRequestToServer(model,req);

                if (response != null)
                {
                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Title = "Success";
                    genResponse.Data = response;
                    genResponse.Message = "Success";
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.BadRequest;
                    genResponse.Title = "Failed";
                    genResponse.Message = "Failed";
                }
                         
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Data = null;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<FileUploadResponse> SendUploadRequestToServer(FileUploadRequest model,HttpRequest request)
        {
            FileUploadResponse serverResponse=new FileUploadResponse();
            FileUploadAPIRequest req = new FileUploadAPIRequest();
            string fileUploadDirectory = "MobAppFileUploads";
            string defaultFileGroup = "Default";
            string defaultFileFormat = "";
            DateTime nowDate = DateTime.Now;

            try
            {
                if (model != null)
                {

                    if(model.files!=null && model.files.Count > 0)
                    {
                        List<FileUploadAPI> apiLst = new List<FileUploadAPI>();
                        for(int i = 0; i < model.files.Count; i++)
                        {
                            string? fileGroup = model.files[i].fileGroup;
                            string? fileFormat= model.files[i].fileFormat;
                            if (fileGroup == null || fileGroup=="") { fileGroup = defaultFileGroup; }
                            if(fileFormat == null || fileFormat == "") { fileFormat = defaultFileFormat; }

                            string serverFileName = "MobAppUpload_" + fileGroup + "_" + nowDate.Day + "_" + nowDate.Month + "_" + nowDate.Year + "_" + nowDate.Ticks + "_" + i + "." + fileFormat;

                            FileUploadAPI api = new FileUploadAPI();
                            api.fileName = serverFileName;
                            api.fileFormat = fileFormat;
                            api.fileGroup = fileGroup;
                            api.fileBase64String = model.files[i].fileBase64String;
                            api.filePath = fileUploadDirectory + "\\" + fileGroup;
                            apiLst.Add(api);
                        }
                        req.files = apiLst;
                    }

                    HttpClient httpClient = new HttpClient();
                    httpClient.Timeout = TimeSpan.FromSeconds(200);

                    string Url = "";// await UploadFilesToExternalServerBLL.GetFileUploadAPIServer(request);

                    HttpResponseMessage respMsg = await httpClient.PostAsJsonAsync(Url, req);
                    string respContent = await respMsg.Content.ReadAsStringAsync();
                    dynamic? respObject = JsonConvert.DeserializeObject<dynamic>(respContent);

                    if (respObject!=null) {
                        if(respObject.isSuccess!=null && (Boolean.TryParse(respObject.isSuccess.ToString(),out Boolean s) ? s : false))
                        {
                            if (respObject.data != null)
                            {
                                string data = JsonConvert.SerializeObject(respObject.data);
                                if (data != null) { serverResponse = JsonConvert.DeserializeObject<FileUploadResponse>(data); }                              
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

        
    }
}
