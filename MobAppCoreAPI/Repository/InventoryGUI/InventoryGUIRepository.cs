using FourQT.DAL;
using FourQT.Entities.General;
using FourQT.Entities;
using System.Data;
using System.Xml.Linq;
using MobAppCoreAPI.Interfaces.InventoryGUI;
using FourQT.Entities.InventoryGUI;
using System.Data.SqlClient;
using System.Net;
using System;

namespace MobAppCoreAPI.Repository.InventoryGUI
{
    public class InventoryGUIRepository : IInventoryGUI
    {
        public async Task<dynamic> getInventoryGUI(InventoryGUIRequest model)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try
            {
                XDocument xdoc = XDocument.Load("keys.xml");
                dynamic? check = null;
                if (model.token != null)
                {
                    check= xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == model.token.ToString()).FirstOrDefault();
                }
                    
                if (check != null)
                {
                    string spName = "API_3D_ProjectView";
                    List<SqlParameter> lstParam = new List<SqlParameter> {
                        new SqlParameter() { ParameterName = "@Stage", Value = model.stage },
                        new SqlParameter() { ParameterName = "@ProjectId", Value = model.projectId },
                        new SqlParameter() { ParameterName = "@TowerId", Value = model.towerId },
                        new SqlParameter() { ParameterName = "@FloorId", Value = model.floorId },
                        new SqlParameter() { ParameterName = "@UnitId", Value = model.unitId }
                    };
                    DataSet ds = await DBHelper.GetDatasetGeneralASync((model.token!=null?model.token.ToString():""), CommandType.StoredProcedure, spName, lstParam);

                    if (model.stage.ToString() == "1")
                    {
                        InventoryGUIStage_1 stageWrap = new InventoryGUIStage_1();
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                InventoryGUIProject response = new InventoryGUIProject();
                                response.projectId = (Int32.TryParse(ds.Tables[0].Rows[0]["Project_Id"].ToString(), out int id) ? id : 0);
                                response.projectName = (ds.Tables[0].Rows[0]["Project_Name"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Name"].ToString() : "");
                                response.projectAddress = (ds.Tables[0].Rows[0]["Project_Address"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Address"].ToString() : "");
                                response.projectArea = (ds.Tables[0].Rows[0]["Project_Area"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Area"].ToString() : "");
                                response.areaUnit = (ds.Tables[0].Rows[0]["Unit_Areameasurement"].ToString() != null ? ds.Tables[0].Rows[0]["Unit_Areameasurement"].ToString() : "");
                                response.description = (ds.Tables[0].Rows[0]["Description"].ToString() != null ? ds.Tables[0].Rows[0]["Description"].ToString() : "");
                                response.towers = (Int32.TryParse(ds.Tables[0].Rows[0]["Towers"].ToString(), out id) ? id : 0);
                                response.floors = (Int32.TryParse(ds.Tables[0].Rows[0]["Floors"].ToString(), out id) ? id : 0);
                                response.units = (Int32.TryParse(ds.Tables[0].Rows[0]["Units"].ToString(), out id) ? id : 0);

                                stageWrap.projectDetails = response;
                            }
                        }

                        if (ds!= null && ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                List<InventoryGUITower> towerLst = new List<InventoryGUITower>();
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    InventoryGUITower tow = new InventoryGUITower();
                                    tow.towerId = (Int32.TryParse(ds.Tables[1].Rows[i]["Project_Tower_Id"].ToString(), out int id) ? id : 0);
                                    tow.towerName = (ds.Tables[1].Rows[i]["Project_Tower_Name"].ToString() != null ? ds.Tables[1].Rows[i]["Project_Tower_Name"].ToString() : "");
                                    tow.unitAreaRange = (ds.Tables[1].Rows[i]["Area_Range"].ToString() != null ? ds.Tables[1].Rows[i]["Area_Range"].ToString() : "");
                                    tow.unitTypeGroups = (ds.Tables[1].Rows[i]["Unit_Type_Groups"].ToString() != null ? ds.Tables[1].Rows[i]["Unit_Type_Groups"].ToString() : "");
                                    tow.availableUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Available"].ToString(), out id) ? id : 0);
                                    tow.mortgageUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Mortgage"].ToString(), out id) ? id : 0);
                                    tow.holdUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Hold"].ToString(), out id) ? id : 0);
                                    tow.soldUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Sold"].ToString(), out id) ? id : 0);
                                    tow.totalUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Total"].ToString(), out id) ? id : 0);
                                    tow.bookedUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Booked"].ToString(), out id) ? id : 0);

                                    towerLst.Add(tow);
                                }
                                stageWrap.towerList = towerLst;
                            }
                        }

                        genResponse.Data = stageWrap;
                    }
                    else if(model.stage.ToString() == "2")
                    {
                        InventoryGUIStage_2 stageWrap = new InventoryGUIStage_2();
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                InventoryGUITowerCore response = new InventoryGUITowerCore();
                                response.towerId = (Int32.TryParse(ds.Tables[0].Rows[0]["Project_Tower_Id"].ToString(), out int id) ? id : 0);
                                response.towerName = (ds.Tables[0].Rows[0]["Project_Tower_Name"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Tower_Name"].ToString() : "");

                                stageWrap.towerDetails = response;
                            }
                        }

                        if (ds != null && ds.Tables.Count > 1)
                        {                         
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                List<InventoryGUIFloor> floorLst = new List<InventoryGUIFloor>();
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    InventoryGUIFloor tow = new InventoryGUIFloor();
                                    tow.floorId = (Int32.TryParse(ds.Tables[1].Rows[i]["Project_Tower_Floor_Id"].ToString(), out int id) ? id : 0);
                                    tow.floorName = (ds.Tables[1].Rows[i]["Project_Tower_Floor_Name"].ToString() != null ? ds.Tables[1].Rows[i]["Project_Tower_Floor_Name"].ToString() : "");
                                    tow.unitAreaRange = (ds.Tables[1].Rows[i]["Area_Range"].ToString() != null ? ds.Tables[1].Rows[i]["Area_Range"].ToString() : "");
                                    tow.unitTypeGroups = (ds.Tables[1].Rows[i]["Unit_Type_Groups"].ToString() != null ? ds.Tables[1].Rows[i]["Unit_Type_Groups"].ToString() : "");
                                    tow.availableUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Available"].ToString(), out id) ? id : 0);
                                    tow.mortgageUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Mortgage"].ToString(), out id) ? id : 0);
                                    tow.holdUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Hold"].ToString(), out id) ? id : 0);
                                    tow.soldUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Sold"].ToString(), out id) ? id : 0);
                                    tow.totalUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Total"].ToString(), out id) ? id : 0);
                                    tow.bookedUnits = (Int32.TryParse(ds.Tables[1].Rows[i]["Booked"].ToString(), out id) ? id : 0);
                                    tow.status = (ds.Tables[1].Rows[i]["Status"].ToString() != null ? ds.Tables[1].Rows[i]["Status"].ToString() : "");

                                    floorLst.Add(tow);
                                }
                                stageWrap.floorList = floorLst;
                            }
                        }

                        genResponse.Data = stageWrap;
                    }
                    else if (model.stage.ToString() == "3")
                    {
                        InventoryGUIStage_3 stageWrap = new InventoryGUIStage_3();
                        if (ds != null && ds.Tables.Count > 0)
                        {
                            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                            {
                                InventoryGUIFloorAlt response = new InventoryGUIFloorAlt();
                                response.towerId = (Int32.TryParse(ds.Tables[0].Rows[0]["Project_Tower_Id"].ToString(), out int id) ? id : 0);
                                response.towerName = (ds.Tables[0].Rows[0]["Project_Tower_Name"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Tower_Name"].ToString() : "");
                                response.floorId = (Int32.TryParse(ds.Tables[0].Rows[0]["Project_Tower_Floor_Id"].ToString(), out id) ? id : 0);
                                response.floorName = (ds.Tables[0].Rows[0]["Project_Tower_Floor_Name"].ToString() != null ? ds.Tables[0].Rows[0]["Project_Tower_Floor_Name"].ToString() : "");

                                stageWrap.floorDetails = response;
                            }
                        }

                        if (ds != null && ds.Tables.Count > 1)
                        {
                            if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                            {
                                List<InventoryGUIUnit> unitLst = new List<InventoryGUIUnit>();
                                for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                {
                                    InventoryGUIUnit tow = new InventoryGUIUnit();
                                    tow.unitId = (Int32.TryParse(ds.Tables[1].Rows[i]["Address_Id"].ToString(), out int id) ? id : 0);
                                    tow.unitNo = (ds.Tables[1].Rows[i]["Address"].ToString() != null ? ds.Tables[1].Rows[i]["Address"].ToString() : "");
                                    tow.unitType = (ds.Tables[1].Rows[i]["Project_Unit_Type_Name"].ToString() != null ? ds.Tables[1].Rows[i]["Project_Unit_Type_Name"].ToString() : "");
                                    tow.unitTypeGroup = (ds.Tables[1].Rows[i]["UnitType_GroupName"].ToString() != null ? ds.Tables[1].Rows[i]["UnitType_GroupName"].ToString() : "");
                                    tow.unitArea = (ds.Tables[1].Rows[i]["UnitArea"].ToString() != null ? ds.Tables[1].Rows[i]["UnitArea"].ToString() : "");
                                    tow.unitStatus = (ds.Tables[1].Rows[i]["Status"].ToString() != null ? ds.Tables[1].Rows[i]["Status"].ToString() : "");

                                    unitLst.Add(tow);
                                }
                                stageWrap.unitList = unitLst;
                            }
                        }

                        genResponse.Data = stageWrap;
                    }

                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = "Success";
                    genResponse.Title= "Success";
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                    genResponse.Title = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.InternalServerError;
                genResponse.Message = "Error: " + ex.Message;
                genResponse.Title = "Error";
            }
            return genResponse;
        }
    }
}
