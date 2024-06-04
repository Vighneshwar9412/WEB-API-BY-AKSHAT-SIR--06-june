using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FourQT.CommonFunctions.Portal;
using FourQT.CommonFunctions;
using FourQT.DAL;
using FourQT.Entities;
using FourQT.Entities.Employee;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Xml.Serialization;
using System.Dynamic;
using System.Xml.Linq;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace FourQT.Masters
{
    public class MastersBLL
    {
        public Object GetAllMasters(string Key, int Login_Id, int typeId)
        {
            DataSet ds = new DataSet();

            string spName = "Pre_Masters_Wrapper_GetAllMasters_ForApp";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@UserId", Value = Login_Id },
                new SqlParameter() { ParameterName = "@Type", Value = typeId }
            };

            ds = DBHelper.GetDataset(Key, CommandType.StoredProcedure, spName, lstParam);

            if (typeId == 0) {

                EnquiryMastersType0 returnModel = new EnquiryMastersType0();
                DataTable cityTable = ds.Tables[1];
                DataTable zoneTable = ds.Tables[2];
                DataTable locationTable = ds.Tables[3];
                DataTable localityTable = ds.Tables[4];
                DataTable sourceTable = ds.Tables[6];
                DataTable budgetTable = ds.Tables[9];
                DataTable enqFromTable = ds.Tables[10];                    
                DataTable enqTypeTable = ds.Tables[11];
                DataTable timeFrameTable = ds.Tables[13];
                DataTable mainFollowupTable = ds.Tables[14];
                DataTable subFollowupTable = ds.Tables[15];
                DataTable dumpTable = ds.Tables[17];
                DataTable communicationTable = ds.Tables[18];
                DataTable countryTable = ds.Tables[21];
                DataTable unitTypeTable = ds.Tables[22];
                DataTable projectTable = ds.Tables[52];    
                DataTable paymentPlanTable = ds.Tables[57];
                DataTable salutationTable = ds.Tables[58];
                DataTable areaTable = ds.Tables[59];

                //add city
                foreach (DataRow rows in cityTable.Rows)
                {
                    City city = new City();
                    city.id = Convert.ToInt32(rows[1]);
                    city.name = rows[4].ToString();
                    city.defaultcity = Convert.ToBoolean(rows[6]);      
                    city.haszone = Convert.ToBoolean(rows[7]);
                    city.haslocation = Convert.ToBoolean(rows[8]);
                    returnModel.city.Add(city);
                }

                //add zone
                foreach (DataRow rows in zoneTable.Rows)
                {
                    Zone zone = new Zone();
                    zone.id = Convert.ToInt32(rows[1]);
                    zone.name = rows[2].ToString();
                    zone.cityid = Convert.ToInt32(rows[3]);
                    returnModel.zone.Add(zone);
                }

                //add location
                foreach (DataRow rows in locationTable.Rows)
                {
                    Location location = new Location();
                    location.id = Convert.ToInt32(rows[0]);
                    location.name = rows[1].ToString();
                    location.cityid = Convert.ToInt32(rows[2]);
                    returnModel.location.Add(location);
                }

                //add locality
                foreach (DataRow rows in localityTable.Rows)
                {
                    Locality locality = new Locality();
                    locality.id = Convert.ToInt32(rows[0]);
                    locality.name = rows[1].ToString();
                    locality.locationid = Convert.ToInt32(rows[4]);
                    returnModel.locality.Add(locality);
                }

                //source
                foreach (DataRow rows in sourceTable.Rows)
                {
                    EnquirySource model = new EnquirySource();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    model.cid = Convert.ToInt32(rows[3]);
                    model.category = rows[4].ToString();
                    model.displaystatus = Convert.ToInt32(rows[6]);
                    returnModel.enqirySource.Add(model);
                }

                //budget
                foreach (DataRow rows in budgetTable.Rows)
                {
                    Budget model = new Budget();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[4].ToString();
                    model.budgetvalue = Convert.ToInt32(rows[5]);
                    returnModel.budget.Add(model);
                }

                //enquiry from
                foreach (DataRow rows in enqFromTable.Rows)
                {
                    EnquiryFrom model = new EnquiryFrom();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    returnModel.enquiryFrom.Add(model);
                }

                //enquiry type
                foreach (DataRow rows in enqTypeTable.Rows)
                {
                    EnquiryType model = new EnquiryType();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    returnModel.enquiryType.Add(model);
                }

                //timeframe
                foreach (DataRow rows in timeFrameTable.Rows)
                {
                    TimeFrame model = new TimeFrame();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.timeFrame.Add(model);
                }

                //main followup
                foreach (DataRow rows in mainFollowupTable.Rows)
                {
                    MainFollowupType model = new MainFollowupType();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.mainFollowUpType.Add(model);
                }

                //sub followup
                foreach (DataRow rows in subFollowupTable.Rows)
                {
                    SubFollowupType model = new SubFollowupType();
                    model.followuptypeid = rows[1].ToString();
                    model.name = rows[3].ToString();
                    model.mainfollowuptypeid = Convert.ToInt32(rows[5]);
                    model.mainfollowuptypename = rows[4].ToString();
                    model.subResponseIdStageId = rows["subresponseId_StageId"].ToString();
                    model.behaviour = (Int32.TryParse(rows["Behaviour"].ToString(), out int b) ? b : 0);
                    returnModel.subFollowUpType.Add(model);
                }

                //dump
                foreach (DataRow rows in dumpTable.Rows)
                {
                    DumpReason model = new DumpReason();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.dumpReason.Add(model);
                }

                //communication
                foreach (DataRow rows in communicationTable.Rows)
                {
                    Communication model = new Communication();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.communication.Add(model);
                }

                //add country
                foreach (DataRow rows in countryTable.Rows)
                {
                    Country country = new Country();
                    country.id = Convert.ToInt32(rows[2]);
                    country.name = rows[0].ToString();
                    country.isocode = rows[3].ToString();
                    country.isdcode = rows[4].ToString();
                    country.mobdigits = Convert.ToByte(rows[5]);
                    returnModel.country.Add(country);
                }

                //Unit type
                foreach (DataRow rows in unitTypeTable.Rows)
                {
                    UnitType model = new UnitType();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.unitType.Add(model);
                }

                //project
                foreach (DataRow rows in projectTable.Rows)
                {
                    Project project = new Project();
                    project.id = Convert.ToInt32(rows[0]);
                    project.name = rows[1].ToString();
                    project.locationId = Convert.ToInt32(rows[2]);
                    returnModel.project.Add(project);
                }

                //payment plan
                foreach (DataRow rows in paymentPlanTable.Rows)
                {
                    PaymentPlan plan = new PaymentPlan();
                    plan.id = Convert.ToInt32(rows[1]);
                    plan.projectId = Convert.ToInt32(rows[0]);
                    plan.name = rows[2].ToString();
                    returnModel.paymentPlan.Add(plan);
                }

                //salutation
                foreach (DataRow rows in salutationTable.Rows)
                {
                    Salutation sal = new Salutation();
                    sal.name = rows[0].ToString();
                    returnModel.salutation.Add(sal);
                }

                //area
                foreach (DataRow rows in areaTable.Rows)
                {
                    Area area = new Area();
                    area.name = rows["Area_Name"].ToString();
                    area.id = (Int32.TryParse(rows["Area_Id"].ToString(), out int id) ? id : 0);
                    returnModel.area.Add(area);
                }

                return returnModel;
            }

            if (typeId == 1)
            {
                EnquiryMastersType1 returnModel = new EnquiryMastersType1();
                DataTable countryTable = ds.Tables[0];
                DataTable stateTable = ds.Tables[1];
                DataTable cityTable = ds.Tables[2];
                DataTable zoneTable = ds.Tables[3];
                DataTable locationTable = ds.Tables[4];
                DataTable localityTable = ds.Tables[5];
                DataTable purposeTable = ds.Tables[6];
                DataTable landmarkTable = ds.Tables[7];
                DataTable directionTable = ds.Tables[8];
                DataTable planTable = ds.Tables[9];

                //add country
                foreach (DataRow rows in countryTable.Rows)
                {
                    Country country = new Country();
                    country.id = Convert.ToInt32(rows[2]);
                    country.name = rows[0].ToString();
                    country.isocode = rows[3].ToString();
                    country.isdcode = rows[4].ToString();
                    country.mobdigits = Convert.ToByte(rows[5]);
                    returnModel.country.Add(country);
                }

                //add state
                foreach (DataRow rows in stateTable.Rows)
                {
                    State state = new State();
                    state.id = Convert.ToInt32(rows[1]);
                    state.name = rows[2].ToString();
                    state.code = rows[3].ToString();
                    state.isdefault = Convert.ToBoolean(rows[4]);
                    returnModel.state.Add(state);
                }

                //add city
                foreach (DataRow rows in cityTable.Rows)
                {
                    City city = new City();
                    city.id = Convert.ToInt32(rows[1]);
                    city.name = rows[4].ToString();
                    city.defaultcity = Convert.ToBoolean(rows[6]);
                    city.haszone= Convert.ToBoolean(rows[7]);
                    city.haslocation= Convert.ToBoolean(rows[8]);
                    returnModel.city.Add(city);
                }

                //add zone
                foreach (DataRow rows in zoneTable.Rows)
                {
                    Zone zone = new Zone();
                    zone.id = Convert.ToInt32(rows[1]);
                    zone.name = rows[2].ToString();
                    zone.cityid= Convert.ToInt32(rows[3]);
                    returnModel.zone.Add(zone);
                }

                //add location
                foreach (DataRow rows in locationTable.Rows)
                {
                    Location location = new Location();
                    location.id = Convert.ToInt32(rows[0]);
                    location.name = rows[1].ToString();
                    location.cityid= Convert.ToInt32(rows[2]);
                    returnModel.location.Add(location);
                }

                //add locality
                foreach (DataRow rows in localityTable.Rows)
                {
                    Locality locality = new Locality();
                    locality.id = Convert.ToInt32(rows[0]);
                    locality.name = rows[1].ToString();
                    locality.locationid = Convert.ToInt32(rows[4]);
                    returnModel.locality.Add(locality);
                }

                foreach (DataRow rows in purposeTable.Rows)
                {
                    Purpose purpose = new Purpose();
                    purpose.id = Convert.ToInt32(rows[0]);
                    purpose.POI= rows[1].ToString();
                    
                    returnModel.purpose.Add(purpose);
                }

                foreach (DataRow rows in landmarkTable.Rows)
                {
                    Landmark landmark = new Landmark();
                    landmark.id = Convert.ToInt32(rows[0]);
                    landmark.name= rows[1].ToString();
                    landmark.create_date= rows[2].ToString()==""?null: Convert.ToDateTime(rows[2].ToString());
                    landmark.created_By = rows[3].ToString() == "" ? 0 : Convert.ToInt32(rows[3].ToString());
                    landmark.update_date = rows[4].ToString() == "" ? null : Convert.ToDateTime(rows[4].ToString());
                    landmark.updated_By = rows[5].ToString() == "" ? 0 : Convert.ToInt32(rows[5].ToString());


                    returnModel.landmark.Add(landmark);
                }

                foreach (DataRow rows in directionTable.Rows)
                {
                    Direction direction = new Direction();
                    direction.id = Convert.ToInt32(rows[0].ToString());
                    direction.name = rows[1].ToString();
                    direction.order= rows[2].ToString() == "" ? 0 : Convert.ToInt32(rows[2].ToString());


                    returnModel.direction.Add(direction);
                }

                foreach (DataRow rows in planTable.Rows)
                {
                    Plan plan= new Plan();
                    plan.project_id= rows[0].ToString() ==""?0: Convert.ToInt32(rows[0].ToString());
                    plan.id= Convert.ToInt32(rows[1].ToString());
                    plan.name = rows[2].ToString();


                    returnModel.plan.Add(plan);
                }
                return returnModel;
            }
            else if (typeId == 2)
            {
                EnquiryMastersType2 returnModel = new EnquiryMastersType2();

                DataTable sourceCategoryTable = ds.Tables[0];
                DataTable enquirySourceTable = ds.Tables[1];
                foreach (DataRow rows in sourceCategoryTable.Rows)
                {
                    SourceCategory model = new SourceCategory();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.sourceCategories.Add(model);
                }
                foreach (DataRow rows in enquirySourceTable.Rows)
                {
                    EnquirySource model = new EnquirySource();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    model.cid = Convert.ToInt32(rows[3]);
                    model.category= rows[4].ToString();
                    model.displaystatus = Convert.ToInt32(rows[6]);
                    returnModel.enquirySources.Add(model);
                }
                return returnModel;
            }

            //todo: type 3 and others.. also add bearer token + mkey in header
            else if (typeId == 3)
            {
                EnquiryMastersType3 returnModel = new EnquiryMastersType3();
                DataTable propertyTypeTable = ds.Tables[0];
                foreach (DataRow rows in propertyTypeTable.Rows)
                {
                    PropertyType model = new PropertyType();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.propertytype.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 4)
            {
                EnquiryMastersType4 returnModel = new EnquiryMastersType4();
                DataTable requirementTypeTable = ds.Tables[0];
                foreach (DataRow rows in requirementTypeTable.Rows)
                {
                    RequirementType model = new RequirementType();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    returnModel.requirementtype.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 5)
            {
                EnquiryMastersType5 returnModel = new EnquiryMastersType5();
                DataTable budgetTable = ds.Tables[0];
                foreach (DataRow rows in budgetTable.Rows)
                {
                    Budget model = new Budget();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[4].ToString();
                    model.budgetvalue = Convert.ToInt32(rows[5]);
                    returnModel.budget.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 6)
            {
                EnquiryMastersType6 returnModel = new EnquiryMastersType6();
                DataTable enquiryFromTable = ds.Tables[0];
                foreach (DataRow rows in enquiryFromTable.Rows)
                {
                    EnquiryFrom model = new EnquiryFrom();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    returnModel.enquiryFrom.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 7)
            {
                EnquiryMastersType7 returnModel = new EnquiryMastersType7();
                DataTable enquiryTypeTable = ds.Tables[0];
                foreach (DataRow rows in enquiryTypeTable.Rows)
                {
                    EnquiryType model = new EnquiryType();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    returnModel.enquiryType.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 8)
            {
                EnquiryMastersType8 returnModel = new EnquiryMastersType8();
                DataTable floorPreferenceTable = ds.Tables[0];
                foreach (DataRow rows in floorPreferenceTable.Rows)
                {
                    FloorPreference model = new FloorPreference();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.floorPreference.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 9)
            {
                EnquiryMastersType9 returnModel = new EnquiryMastersType9();
                DataTable timeFrameTable = ds.Tables[0];
                foreach (DataRow rows in timeFrameTable.Rows)
                {
                    TimeFrame model = new TimeFrame();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.timeframe.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 10)
            {
                EnquiryMastersType10 returnModel = new EnquiryMastersType10();
                DataTable mainFollowupTypeTable = ds.Tables[0];
                DataTable subFollowupTypeTable = ds.Tables[1];
                DataTable enquiryStageTable = ds.Tables[3];
                foreach (DataRow rows in mainFollowupTypeTable.Rows)
                {
                    MainFollowupType model = new MainFollowupType();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.mainFollowupType.Add(model);
                }
                foreach (DataRow rows in subFollowupTypeTable.Rows)
                {
                    SubFollowupType model = new SubFollowupType();
                    model.followuptypeid = rows[1].ToString();
                    model.name = rows[3].ToString();
                    model.mainfollowuptypeid = Convert.ToInt32(rows[5]);
                    model.mainfollowuptypename = rows[4].ToString();
                    model.subResponseIdStageId = rows["subresponseId_StageId"].ToString();
                    model.behaviour = (Int32.TryParse(rows["Behaviour"].ToString(), out int b) ? b : 0);
                    returnModel.subFollowupType.Add(model);
                }
                foreach (DataRow rows in enquiryStageTable.Rows)
                {
                    EnquiryStage model = new EnquiryStage();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.enquiryStage.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 11)
            {
                EnquiryMastersType11 returnModel = new EnquiryMastersType11();
                DataTable dumpReasonTable = ds.Tables[0];
                foreach (DataRow rows in dumpReasonTable.Rows)
                {
                    DumpReason model = new DumpReason();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.dumpReason.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 12)
            {
                EnquiryMastersType12 returnModel = new EnquiryMastersType12();
                DataTable statusLegendsTable = ds.Tables[0];
                foreach (DataRow rows in statusLegendsTable.Rows)
                {
                    StatusLegends model = new StatusLegends();
                    model.status = rows[0].ToString();
                    model.colorcode = rows[1].ToString();
                    returnModel.statusLegends.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 13)
            {
                EnquiryMastersType13 returnModel = new EnquiryMastersType13();
                DataTable communicationTable = ds.Tables[0];
                foreach (DataRow rows in communicationTable.Rows)
                {
                    Communication model = new Communication();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.communication.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 14)
            {
                EnquiryMastersType14 returnModel = new EnquiryMastersType14();
                DataTable unitTypeTable = ds.Tables[0];
                foreach (DataRow rows in unitTypeTable.Rows)
                {
                    UnitType model = new UnitType();
                    model.id = Convert.ToInt32(rows[1]);
                    model.name = rows[2].ToString();
                    returnModel.unitType.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 20 || typeId == 21|| typeId==22)
            {
                EnquiryMastersType20 returnModel = new EnquiryMastersType20();
                DataTable templateTypeTable = ds.Tables[0];
                foreach (DataRow rows in templateTypeTable.Rows)
                {
                    Template model = new Template();
                    model.id= Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    model.body = rows[2].ToString();
                    returnModel.templateType.Add(model);
                }
                return returnModel;
            }
            else if (typeId == 23)
            {
                EnquiryMastersType23 returnModel = new EnquiryMastersType23();
                DataTable emailTypeTable = ds.Tables[0];
                DataTable SMSTypeTable = ds.Tables[1];
                DataTable WhatsappTypeTable = ds.Tables[2]; 
                foreach (DataRow rows in emailTypeTable.Rows)
                {
                    Template model = new Template();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    model.body = rows[2].ToString();
                    returnModel.emailType.Add(model);
                }
                foreach (DataRow rows in SMSTypeTable.Rows)
                {
                    Template model = new Template();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    model.body = rows[2].ToString();
                    returnModel.SMSType.Add(model);
                }

                foreach (DataRow rows in WhatsappTypeTable.Rows)
                {
                    Template model = new Template();
                    model.id = Convert.ToInt32(rows[0]);
                    model.name = rows[1].ToString();
                    model.body = rows[2].ToString();
                    returnModel.WhatsappType.Add(model);
                }

                return returnModel;

            }
            else
            {
                return null;
            }
        }

        public async Task<dynamic> EnquiryMastersByEnqId(HttpRequest req, MastersByEnquiryReq model)
        {
            EnquiryMastersByEnqIdResp masters = new EnquiryMastersByEnqIdResp();

            try
            {
                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string mKey);

                string spName = "API_GetMasters_By_EnquiryId";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Enquiry_Id", Value = model.enquiryId }
                };

                DataSet ds = new DataSet();
                ds = await DBHelper.GetDatasetAsyncNew(mKey, CommandType.StoredProcedure, spName, lstParam);

                if (ds != null && ds.Tables.Count > 0) {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0) {
                        List<MainFollowupType> mainFollowups = new List<MainFollowupType>();

                        for (int i = 0; i < dt.Rows.Count; i++) {
                            MainFollowupType followup = new MainFollowupType();
                            followup.id = (Int32.TryParse(dt.Rows[i]["Main_FollowupType_Id"].ToString(), out int id) ? id : 0);
                            followup.name = (dt.Rows[i]["Main_FollowupType_Name"] != null ? dt.Rows[i]["Main_FollowupType_Name"].ToString() : "");
                            followup.sortOrder = (dt.Columns.Contains("SrNo") && Int32.TryParse(dt.Rows[i]["SrNo"].ToString(), out id) ? id : 9999);
                            mainFollowups.Add(followup);
                        }

                        masters.mainFollowUpType = mainFollowups.OrderBy(o => o.sortOrder).ThenBy(o => o.id).ToList();
                    }
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<Template> templates = new List<Template>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Template template = new Template();
                            template.id = (Int32.TryParse(dt.Rows[i]["Id"].ToString(), out int id) ? id : 0);
                            template.name = (dt.Rows[i]["Name"] != null ? dt.Rows[i]["Name"].ToString() : "");
                            template.body = (dt.Rows[i]["Body"] != null ? dt.Rows[i]["Body"].ToString() : "");
                            templates.Add(template);
                        }

                        masters.SMSType = templates;
                    }
                }

                if (ds != null && ds.Tables.Count > 2)
                {
                    DataTable dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<Template> templates = new List<Template>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Template template = new Template();
                            template.id = (Int32.TryParse(dt.Rows[i]["Id"].ToString(), out int id) ? id : 0);
                            template.name = (dt.Rows[i]["Name"] != null ? dt.Rows[i]["Name"].ToString() : "");
                            template.body = (dt.Rows[i]["Body"] != null ? dt.Rows[i]["Body"].ToString() : "");
                            templates.Add(template);
                        }

                        masters.emailType = templates;
                    }
                }

                if (ds != null && ds.Tables.Count > 3)
                {
                    DataTable dt = ds.Tables[3];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<Template> templates = new List<Template>();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Template template = new Template();
                            template.id = (Int32.TryParse(dt.Rows[i]["Id"].ToString(), out int id) ? id : 0);
                            template.name = (dt.Rows[i]["Name"] != null ? dt.Rows[i]["Name"].ToString() : "");
                            template.body = (dt.Rows[i]["Body"] != null ? dt.Rows[i]["Body"].ToString() : "");
                            templates.Add(template);
                        }

                        masters.WhatsappType = templates;
                    }
                }
            }
            catch {
                throw;
            }

            return masters;
        }

        public Object projectdocslisting(string Key, int Login_Id, int Project_id, int item_id)
        {
            DataSet ds = new DataSet();
            ProjectDocsResponseModel project = new ProjectDocsResponseModel();
            string spName = "API_GetConfiguredProjectByItems";
            List<SqlParameter> lstParam = new List<SqlParameter>
            {
                new SqlParameter() { ParameterName = "@Login_Id", Value = Login_Id },
                new SqlParameter() { ParameterName = "@Project_Id", Value = Project_id},
                new SqlParameter() { ParameterName = "@Item_Id", Value = item_id}
            };

            ds = DBHelper.GetDataset(Key, CommandType.StoredProcedure, spName, lstParam);

            foreach (DataRow rows in ds.Tables[0].Rows)
            {
                ProjectDocs m1 = new ProjectDocs();
                m1.projectId = rows[1].ToString() == "" ? 0 : Convert.ToInt32(rows[1].ToString());
                m1.itemId = rows[2].ToString() == "" ? 0 : Convert.ToInt32(rows[2].ToString());
                m1.projectName = rows[3].ToString();
                m1.documentType = rows[4].ToString();
                m1.fileName = rows[5].ToString();
                m1.itemName = rows[6].ToString();
                m1.itemPath = rows[7].ToString();

                project.projectdocsSources.Add(m1);

            }
            return project.projectdocsSources;
        }

        public async Task<dynamic> getInventoryMasters(InventoryRequestMasters model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            LeadInventoryMasters masters = new LeadInventoryMasters();

            try
            {
                DataSet ds = new DataSet();

                (new JWTTokenMethods()).GetConnectionDetails(req, out int loginId, out string conn);

                string spName = "API_GetMasters_Lead";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
                    new SqlParameter() { ParameterName = "@Login_Id", Value = (Int32.TryParse(loginId.ToString(),out int id) ? id : 0)  },
                    new SqlParameter() { ParameterName = "@Project_Id", Value = (Int32.TryParse(model.projectId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Project_Tower_Id", Value = (Int32.TryParse(model.towerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Project_Tower_Floor_Id", Value = (Int32.TryParse(model.floorId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Pg_Id", Value = (Int32.TryParse(model.unitTypeGroupId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Project_Unit_Type_Id", Value = (Int32.TryParse(model.unitTypeId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Location_Id", Value = model.locationId },
                    new SqlParameter() { ParameterName = "@Puoid", Value = (Int32.TryParse(model.ownerId.ToString(),out id) ? id : 0) },
                    new SqlParameter() { ParameterName = "@Broker_Id", Value = (Int32.TryParse(model.brokerId.ToString(),out id) ? id : 0) }
                };

                dynamic[] resultAr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                if (ds != null && ds.Tables.Count > 0)
                {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> projects = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++) {
                            EmployeeInventorySelect project = new EmployeeInventorySelect();
                            project.id = (Int32.TryParse(dt.Rows[i]["Project_Id"].ToString(), out id) ? id : 0);
                            project.name = (dt.Rows[i]["Project_Name"] != null ? dt.Rows[i]["Project_Name"].ToString() : "");
                            project.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            projects.Add(project);
                        }

                        masters.project = projects;
                    }
                }

                if (ds != null && ds.Tables.Count > 1)
                {
                    DataTable dt = ds.Tables[1];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> towers = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect tower = new EmployeeInventorySelect();
                            tower.id = (Int32.TryParse(dt.Rows[i]["Project_Tower_Id"].ToString(), out id) ? id : 0);
                            tower.name = (dt.Rows[i]["Project_Tower_Name"] != null ? dt.Rows[i]["Project_Tower_Name"].ToString() : "");
                            tower.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            towers.Add(tower);
                        }

                        masters.tower = towers;
                    }
                }

                if (ds != null && ds.Tables.Count > 2)
                {
                    DataTable dt = ds.Tables[2];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> floors = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect floor = new EmployeeInventorySelect();
                            floor.id = (Int32.TryParse(dt.Rows[i]["Project_Tower_Floor_Id"].ToString(), out id) ? id : 0);
                            floor.name = (dt.Rows[i]["Project_Tower_Floor_Name"] != null ? dt.Rows[i]["Project_Tower_Floor_Name"].ToString() : "");
                            floor.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            floors.Add(floor);
                        }

                        masters.floor = floors;
                    }
                }

                if (ds != null && ds.Tables.Count > 3)
                {
                    DataTable dt = ds.Tables[3];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> unitGroups = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect unitGroup = new EmployeeInventorySelect();
                            unitGroup.id = (Int32.TryParse(dt.Rows[i]["Pg_ID"].ToString(), out id) ? id : 0);
                            unitGroup.name = (dt.Rows[i]["UnitType_GroupName"] != null ? dt.Rows[i]["UnitType_GroupName"].ToString() : "");
                            unitGroup.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            unitGroups.Add(unitGroup);
                        }

                        masters.unitTypeGroup = unitGroups;
                    }
                }

                if (ds != null && ds.Tables.Count > 4)
                {
                    DataTable dt = ds.Tables[4];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> unitTypes = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect unitType = new EmployeeInventorySelect();
                            unitType.id = (Int32.TryParse(dt.Rows[i]["Project_Unit_Type_Id"].ToString(), out id) ? id : 0);
                            unitType.name = (dt.Rows[i]["Project_Unit_Type_Name"] != null ? dt.Rows[i]["Project_Unit_Type_Name"].ToString() : "");
                            unitType.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            unitTypes.Add(unitType);
                        }

                        masters.unitType = unitTypes;
                    }
                }

                if (ds != null && ds.Tables.Count > 5)
                {
                    DataTable dt = ds.Tables[5];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> locations = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect location = new EmployeeInventorySelect();
                            location.id = (Int32.TryParse(dt.Rows[i]["Location_Id"].ToString(), out id) ? id : 0);
                            location.name = (dt.Rows[i]["Unit_Location"] != null ? dt.Rows[i]["Unit_Location"].ToString() : "");
                            location.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            locations.Add(location);
                        }

                        masters.location = locations;
                    }
                }

                if (ds != null && ds.Tables.Count > 6)
                {
                    DataTable dt = ds.Tables[6];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> owners = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect owner = new EmployeeInventorySelect();
                            owner.id = (Int32.TryParse(dt.Rows[i]["Puoid"].ToString(), out id) ? id : 0);
                            owner.name = (dt.Rows[i]["Unit_Owner"] != null ? dt.Rows[i]["Unit_Owner"].ToString() : "");
                            owner.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            owners.Add(owner);
                        }

                        masters.unitOwner = owners;
                    }
                }

                if (ds != null && ds.Tables.Count > 7)
                {
                    DataTable dt = ds.Tables[7];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        List<EmployeeInventorySelect> brokers = new List<EmployeeInventorySelect>();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            EmployeeInventorySelect broker = new EmployeeInventorySelect();
                            broker.id = (Int32.TryParse(dt.Rows[i]["Broker_id"].ToString(), out id) ? id : 0);
                            broker.name = (dt.Rows[i]["Borker_Company_name"] != null ? dt.Rows[i]["Borker_Company_name"].ToString() : "");
                            broker.selected = (Int32.TryParse(dt.Rows[i]["Selected"].ToString(), out id) ? id : 0);
                            brokers.Add(broker);
                        }

                        masters.channelPartner = brokers;
                    }
                }

                genResponse.Data = masters;
                genResponse.IsSuccess = true;
                genResponse.Message = "Success";
                genResponse.Status = HttpStatusCode.OK;
                genResponse.Title = "Success";
            }
            catch (Exception ex)
            {
                genResponse.IsSuccess = false;
                genResponse.Title = "Error";
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }

        public async Task<dynamic> getInventoryUnitStatus(InventoryRequestLong model, HttpRequest req, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            LeadInventoryWrap inventoryPage = new LeadInventoryWrap();

            try
            {
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

                string spName = "API_UnitStatus_LeadInventory";
                List<SqlParameter> lstParam = new List<SqlParameter>
                {
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
                    new SqlParameter() { ParameterName = "@Unit_No", Value = model.unitNo }
                };


                dynamic[] resultAr = await DBHelper.GetDataSetEmployeeAsync(conn, CommandType.StoredProcedure, spName, lstParam);
                if (resultAr != null && resultAr.Length > 0)
                {
                    ds = resultAr[0];
                }

                if (ds != null && ds.Tables.Count > 0) {
                    DataTable dt = ds.Tables[0];
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string? xmlData = (dt.Rows[0][0] != null ? dt.Rows[0][0].ToString() : "");

                        if (xmlData != null && xmlData.Trim() != "")
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(LeadInventoryWrap));
                            using (TextReader reader = new StringReader(xmlData))
                            {
                                inventoryPage = (LeadInventoryWrap)serializer.Deserialize(reader);
                            }
                        }
                    }
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
                genResponse.Title = "Error";
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }
    }
}
