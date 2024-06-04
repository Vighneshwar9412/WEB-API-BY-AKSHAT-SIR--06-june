using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourQT.DAL;
using FourQT.Entities;
using Microsoft.AspNetCore.Http;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using FourQT.CommonFunctions;
using System.Net;
using FourQT.Entities.Portal;
using FourQT.Utilities;
using Newtonsoft.Json;
using FourQT.CommonFunctions.Portal;

namespace FourQT.Masters
{
    public class InventoryMastersBLL
    {
        public async Task<dynamic> getProject(HttpRequest req)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                DataSet ds = new DataSet();
                InventoryProjectList pr = new InventoryProjectList();
                List<EmployeeInventory> projectLst = new List<EmployeeInventory>();

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_GetMasters";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Type", Value = "L"},
                };

                dynamic[] resultAr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0) {
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Project_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Id"].ToString()));
                        proj.name = (rows["Project_Name"] == null ? "" : rows["Project_Name"].ToString());

                        projectLst.Add(proj);
                    }
                    pr.project = projectLst;
                }

                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
                genResponse.Data = pr;
            }
            catch (Exception ex) {
                genResponse.IsSuccess = false;
                genResponse.Message = ex.ToString();
                genResponse.Data = null;
                genResponse.Status = System.Net.HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> getMastersByProject(InventoryRequestShort model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "Employee_getMastersByProject", context);

                DataSet ds = new DataSet();
                InventoryMastersFromProject masters = new InventoryMastersFromProject();

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_GetMasters";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Type", Value = "P"},
                    new SqlParameter() { ParameterName = "@Project_Id", Value = model.projectId },
                };

                dynamic[] resultAr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<EmployeeInventorySelect> lst = new List<EmployeeInventorySelect>();
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        EmployeeInventorySelect proj = new EmployeeInventorySelect();
                        proj.id = (rows["Project_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Id"].ToString()));
                        proj.name = (rows["Project_Name"] == null ? "" : rows["Project_Name"].ToString());
                        proj.selected = (rows["Selected"] == null ? 0 : Convert.ToInt32(rows["Selected"].ToString()));

                        lst.Add(proj);
                    }
                    masters.project = lst;
                }

                if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[1].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Project_Tower_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Tower_Id"].ToString()));
                        proj.name = (rows["Project_Tower_Name"] == null ? "" : rows["Project_Tower_Name"].ToString());

                        lst.Add(proj);
                    }
                    masters.tower = lst;
                }

                if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[2].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Project_Tower_Floor_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Tower_Floor_Id"].ToString()));
                        proj.name = (rows["Project_Tower_Floor_Name"] == null ? "" : rows["Project_Tower_Floor_Name"].ToString());

                        lst.Add(proj);
                    }
                    masters.baseFloor = lst;
                }

                if (ds.Tables.Count > 3 && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[3].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Pg_ID"] == null ? 0 : Convert.ToInt32(rows["Pg_ID"].ToString()));
                        proj.name = (rows["UnitType_GroupName"] == null ? "" : rows["UnitType_GroupName"].ToString());

                        lst.Add(proj);
                    }
                    masters.unitTypeGroup = lst;
                }

                if (ds.Tables.Count > 4 && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[4].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Project_Unit_Type_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Unit_Type_Id"].ToString()));
                        proj.name = (rows["Project_Unit_Type_Name"] == null ? "" : rows["Project_Unit_Type_Name"].ToString());

                        lst.Add(proj);
                    }
                    masters.unitType = lst;
                }

                if (ds.Tables.Count > 5 && ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[5].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Location_Id"] == null ? 0 : Convert.ToInt32(rows["Location_Id"].ToString()));
                        proj.name = (rows["Unit_Location"] == null ? "" : rows["Unit_Location"].ToString());

                        lst.Add(proj);
                    }
                    masters.location = lst;
                }

                if (ds.Tables.Count > 6 && ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[6].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Puoid"] == null ? 0 : Convert.ToInt32(rows["Puoid"].ToString()));
                        proj.name = (rows["Unit_Owner"] == null ? "" : rows["Unit_Owner"].ToString());

                        lst.Add(proj);
                    }
                    masters.unitOwner = lst;
                }

                if (ds.Tables.Count > 7 && ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[7].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Broker_Id"] == null ? 0 : Convert.ToInt32(rows["Broker_Id"].ToString()));
                        proj.name = (rows["Borker_Company_name"] == null ? "" : rows["Borker_Company_name"].ToString());

                        lst.Add(proj);
                    }
                    masters.channelPartner = lst;
                }

                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
                genResponse.Data = masters;
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

        public async Task<dynamic> getInventory(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            InventoryPage inventoryPage = new InventoryPage();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "Employee_getInventory", context);

                DataSet ds = new DataSet();

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string type = "";
                if (model.type != null)
                {
                    string temp = model.type.ToString().Trim().ToUpper();
                    if (temp == "A" || temp == "H" || temp == "B")
                    {
                        type = temp;
                    }
                }

                string spName = "API_UnitStatus";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {      
                    new SqlParameter() { ParameterName = "@TotalRecords" , Value = 0 },
                    new SqlParameter() { ParameterName = "@Project_Id", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Tower_Id", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Floor_Id", Value = (Int32.TryParse(model.floorId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Pg_Id", Value = (Int32.TryParse(model.unitTypeGroupId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@UnitType_Id", Value = (Int32.TryParse(model.unitTypeId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Location_Id", Value = model.locationId },
                    new SqlParameter() { ParameterName = "@Puoid", Value = (Int32.TryParse(model.ownerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Type", Value = ((type!="") ? type : null) },                    
                    new SqlParameter() { ParameterName = "@Login_Id", Value = (Int32.TryParse(loginId.ToString(),out id) ? id : 0)  },
                    new SqlParameter() { ParameterName = "@AreaMin", Value = (Decimal.TryParse(model.areaMin.ToString(),out decimal d) ? d : null) },
                    new SqlParameter() { ParameterName = "@AreaMax", Value = (Decimal.TryParse(model.areaMax.ToString(),out d) ? d : null)},
                    new SqlParameter() { ParameterName = "@Unit_No", Value = model.unitNo },
                    new SqlParameter() { ParameterName = "@PageNo", Value = (Int32.TryParse(model.pageNo.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@PageSize", Value = (Int32.TryParse(model.pageSize.ToString(),out id) ? id : 0) }
                };

                lstParam[0].Direction = ParameterDirection.Output;

                dynamic[] resultAr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                int totalRecords = (Int32.TryParse(lstParam[0].Value.ToString(), out int total) ? total : 0);
                inventoryPage.totalRecords = totalRecords;

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {

                    List<InventoryUnitDetails> unitLst = new List<InventoryUnitDetails>();
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        InventoryUnitDetails unit = new InventoryUnitDetails();
                        unit.project = (rows["Project_Name"] == null ? "" : rows["Project_Name"].ToString());
                        unit.tower = (rows["Project_Tower_Name"] == null ? "" : rows["Project_Tower_Name"].ToString());
                        unit.floor = (rows["Project_Tower_Floor_Name"] == null ? "" : rows["Project_Tower_Floor_Name"].ToString());
                        unit.unitNo = (rows["UnitNo"] == null ? "" : rows["UnitNo"].ToString());
                        unit.unitGroup = (rows["UnitType_GroupName"] == null ? "" : rows["UnitType_GroupName"].ToString());
                        unit.unitType = (rows["Project_Unit_Type_Name"] == null ? "" : rows["Project_Unit_Type_Name"].ToString());
                        unit.superArea = (rows["SuperArea"] == null ? "" : rows["SuperArea"].ToString());
                        unit.carpetArea = (rows["Carpet_Area"] == null ? "" : rows["Carpet_Area"].ToString());
                        unit.buildupArea = (rows["Build_Up_Area"] == null ? "" : rows["Build_Up_Area"].ToString());
                        unit.location = (rows["Unit_Location"] == null ? "" : rows["Unit_Location"].ToString());
                        unit.unitPlan = (rows["UnitPlan"] == null ? "" : rows["UnitPlan"].ToString());
                        unit.floorPlan = (rows["FloorPlan"] == null ? "" : rows["FloorPlan"].ToString());
                        unit.status = (rows["Status"] == null ? "" : rows["Status"].ToString());
                        unit.unitId = (Int32.TryParse(rows["Address_Id"].ToString(),out int unitId) ? unitId : 0);
                        unit.cpName = (rows["ChannelPartner"] == null ? "" : rows["ChannelPartner"].ToString());
                        unit.holdDate = (rows["HoldDate"] == null ? "" : rows["HoldDate"].ToString());
                        unit.holdByEmployee = (rows["HoldBy"] == null ? "" : rows["HoldBy"].ToString());
                        unit.remarks = (rows["HoldRemark"] == null ? "" : rows["HoldRemark"].ToString());
                        unit.colorCode = (rows["ColorCode"] != null ? rows["ColorCode"].ToString() : "");
                        unit.customerName = (rows["CustomerName"] != null ? rows["CustomerName"].ToString() : "");
                        unit.customerMobile = (rows["CustomerMobile"] != null ? rows["CustomerMobile"].ToString() : "");
                        unit.superAreaLabel = (rows["SuperAreaLbl"] != null ? rows["SuperAreaLbl"].ToString() : "");
                        unit.carpetAreaLabel = (rows["Carpet_AreaLbl"] != null ? rows["Carpet_AreaLbl"].ToString() : "");
                        unit.buildupAreaLabel = (rows["Build_Up_AreaLbl"] != null ? rows["Build_Up_AreaLbl"].ToString() : "");
                        unit.superAreaDisplay = (rows["SuperAreaDisp"].ToString() != "0" ? true : false);
                        unit.carpetAreaDisplay = (rows["Carpet_AreaDisp"].ToString() != "0" ? true : false);
                        unit.buildupAreaDisplay = (rows["Build_Up_AreaDisp"].ToString() != "0" ? true : false);
                        unit.uId = (rows["UId"] != null ? rows["UId"].ToString() : "");

                        unitLst.Add(unit);
                    }

                    inventoryPage.unit = unitLst.OrderBy(o => o.unitId).ToList(); 
                }

                genResponse.Data = inventoryPage;
                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
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

        public async Task<dynamic> getTowerWiseFloor(FloorRequest model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                string message = JsonConvert.SerializeObject(model);
                Log.LogPayloadDateWise(message, "Employee_getTowerWiseFloor", context);

                DataSet ds = new DataSet();
                InventoryFloorList masters = new InventoryFloorList();

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_GetMasters";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = loginId },
                    new SqlParameter() { ParameterName = "@Type", Value = "T"},
                    new SqlParameter() { ParameterName = "@Project_Id", Value = (Int32.TryParse(model.projectId.ToString(),out int id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Project_Tower_Id", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                };

                dynamic[] resultAr= await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0) {
                    ds = resultAr[0];
                }

                if (ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<EmployeeInventory> lst = new List<EmployeeInventory>();
                    foreach (DataRow rows in ds.Tables[0].Rows)
                    {
                        EmployeeInventory proj = new EmployeeInventory();
                        proj.id = (rows["Project_Tower_Floor_Id"] == null ? 0 : Convert.ToInt32(rows["Project_Tower_Floor_Id"].ToString()));
                        proj.name = (rows["Project_Tower_Floor_Name"] == null ? "" : rows["Project_Tower_Floor_Name"].ToString());

                        lst.Add(proj);
                    }
                    masters.floor = lst;
                }

                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
                genResponse.Data = masters;
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
    }
}
