using FourQT.CommonFunctions;
using FourQT.Entities;
using System.Xml.Linq;
using FourQT.Entities.General;
using FourQT.Entities.Portal;
using MobAppCoreAPI.Interfaces.General;
using System.Net;
using FourQT.Utilities;
using FourQT.DAL;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Data;
using System.Data.SqlClient;
using FourQT.CommonFunctions.Portal;
using System.Text.Json;
using Swashbuckle.AspNetCore.SwaggerGen;
using NPoco.Expressions;
using System.Configuration;

namespace MobAppCoreAPI.Repository.General
{
    public class GeneralHomePageRepository : IGeneralHomepage
    {
        public async Task<APIObjectResponse> getGeneralHomepageContent(string dKey)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            GeneralHomePage response = new GeneralHomePage();
            try
            {

                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();
                if (check != null)
                {

                    string spName = "API_GetGeneralHomePageContent";
                    List<SqlParameter> lstParam = new List<SqlParameter> { };
                    DataSet ds = await DBHelper.GetDatasetGeneralASync(dKey, CommandType.StoredProcedure, spName, lstParam);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            GenHomePageRow1 r = new GenHomePageRow1();
                            DataRow dr = ds.Tables[0].Rows[0];
                            r.headerLogo = (dr["HeaderLogo"] != null ? dr["HeaderLogo"].ToString() : "");
                            response.row1 = r;
                        }
                        if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                        {
                            GenHomePageRow2 r = new GenHomePageRow2();
                            r.mediaList = new List<Media>();
                            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            {
                                DataRow dr = ds.Tables[1].Rows[i];
                                Media mediaLst = new Media();
                                mediaLst.media = (dr["Media"] != null ? dr["Media"].ToString() : "");
                                mediaLst.type = (dr["Type"] != null ? dr["Type"].ToString() : "");
                                r.mediaList.Add(mediaLst);
                            }

                            if (ds.Tables.Count > 7 && ds.Tables[7] != null && ds.Tables[7].Rows.Count > 0)
                            {
                                DataRow dr0 = ds.Tables[7].Rows[0];
                                r.textLine1 = (dr0["Line1"] != null ? dr0["Line1"].ToString() : "");
                                r.textLine2 = (dr0["Line2"] != null ? dr0["Line2"].ToString() : "");
                                r.textLine3 = (dr0["Line3"] != null ? dr0["Line3"].ToString() : "");
                                r.textLine4 = (dr0["Line4"] != null ? dr0["Line4"].ToString() : "");
                                r.textLine5 = (dr0["Line5"] != null ? dr0["Line5"].ToString() : "");
                                r.textLine6 = (dr0["Line6"] != null ? dr0["Line6"].ToString() : "");
                                r.textLine7 = (dr0["Line7"] != null ? dr0["Line7"].ToString() : "");
                                r.visitLink = (dr0["VisitLink"] != null ? dr0["VisitLink"].ToString() : "");
                            }

                            response.row2 = r;
                        }
                        if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                        {
                            GenHomePageRow3 r = new GenHomePageRow3();
                            r.mediaList = new List<LinkMedia>();

                            for (int i = 0; i < ds.Tables[2].Rows.Count; i++)
                            {
                                DataRow dr = ds.Tables[2].Rows[i];
                                LinkMedia mediaLst = new LinkMedia();                                
                                mediaLst.media = (dr["Media"] != null ? dr["Media"].ToString() : "");
                                mediaLst.type = (dr["Type"] != null ? dr["Type"].ToString() : "");
                                mediaLst.link = (dr["Link"] != null ? dr["Link"].ToString() : "");
                                r.mediaList.Add(mediaLst);
                            }
                            DataRow dr0 = ds.Tables[2].Rows[0];
                            r.headerText = (dr0["Text"] != null ? dr0["Text"].ToString() : "");
                            r.headerSize = (dr0["Size"] != null ? dr0["Size"].ToString() : "");
                            r.headerFont = (dr0["Font"] != null ? dr0["Font"].ToString() : "");
                            r.headerColor = (dr0["Color"] != null ? dr0["Color"].ToString() : "");
                            r.projectName = (dr0["ProjectName"] != null ? dr0["ProjectName"].ToString() : "");
                            r.projectLocation = (dr0["ProjectLocation"] != null ? dr0["ProjectLocation"].ToString() : "");
                            r.projectCost = (dr0["ProjectCost"] != null ? dr0["ProjectCost"].ToString() : "");
                            r.rating = (dr0["Rating"] != null ? dr0["Rating"].ToString() : "");
                            r.reraNo = (dr0["ReraNo"] != null ? dr0["ReraNo"].ToString() : "");
                            r.configuration = (dr0["Configuration"] != null ? dr0["Configuration"].ToString() : "");
                            r.sizes = (dr0["Sizes"] != null ? dr0["Sizes"].ToString() : "");
                            r.landArea = (dr0["LandArea"] != null ? dr0["LandArea"].ToString() : "");
                            r.location = (dr0["Location"] != null ? dr0["Location"].ToString() : "");
                            r.projectDetails = (dr0["ProjectDetails"] != null ? dr0["ProjectDetails"].ToString() : "");
                            r.downloadBrochureLink = (dr0["DownloadBrochureLink"] != null ? dr0["DownloadBrochureLink"].ToString() : "");
                            response.row3 = r;
                        }
                        if (ds.Tables.Count > 3 && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                        {
                            DataRow dr = ds.Tables[3].Rows[0];
                            GenHomePageRow4 r = new GenHomePageRow4();
                            r.headerText = (dr["Text"] != null ? dr["Text"].ToString() : "");
                            r.headerSize = (dr["Size"] != null ? dr["Size"].ToString() : "");
                            r.headerFont = (dr["Font"] != null ? dr["Font"].ToString() : "");
                            r.headerColor = (dr["Color"] != null ? dr["Color"].ToString() : "");
                            r.buttonText = (dr["ButtonText"] != null ? dr["ButtonText"].ToString() : "");
                            r.buttonFontSize = (dr["ButtonTextFontSize"] != null ? dr["ButtonTextFontSize"].ToString() : "");
                            r.buttonFont = (dr["ButtonTextFont"] != null ? dr["ButtonTextFont"].ToString() : "");
                            r.buttonFontColor = (dr["ButtonTextColor"] != null ? dr["ButtonTextColor"].ToString() : "");
                            r.buttonBackgroundColor = (dr["ButtonBackColor"] != null ? dr["ButtonBackColor"].ToString() : "");
                            r.link = (dr["Link"] != null ? dr["Link"].ToString() : "");
                            response.row4 = r;
                        }
                        if (ds.Tables.Count > 4 && ds.Tables[4] != null && ds.Tables[4].Rows.Count > 0)
                        {
                            GenHomePageRow5 r = new GenHomePageRow5();
                            DataRow dr = ds.Tables[4].Rows[0];
                            r.video = new Media();
                            r.video.media = (dr["Media"] != null ? dr["Media"].ToString() : "");
                            r.video.type = (dr["Type"] != null ? dr["Type"].ToString() : "");
                            response.row5 = r;
                        }
                        if (ds.Tables.Count > 5 && ds.Tables[5] != null && ds.Tables[5].Rows.Count > 0)
                        {
                            GenHomePageRow6 r = new GenHomePageRow6();
                            r.mediaList = new List<Media>();

                            for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
                            {
                                Media mediaLst = new Media();
                                DataRow dr = ds.Tables[5].Rows[i];                              
                                mediaLst.media = (dr["Media"] != null ? dr["Media"].ToString() : "");
                                mediaLst.type = (dr["Type"] != null ? dr["Type"].ToString() : "");
                                r.mediaList.Add(mediaLst);
                            }

                            DataRow dr0 = ds.Tables[5].Rows[0];
                            r.headerText = (dr0["Text"] != null ? dr0["Text"].ToString() : "");
                            r.headerSize = (dr0["Size"] != null ? dr0["Size"].ToString() : "");
                            r.headerFont = (dr0["Font"] != null ? dr0["Font"].ToString() : "");
                            r.headerColor = (dr0["Color"] != null ? dr0["Color"].ToString() : "");
                            response.row6 = r;
                        }
                        if (ds.Tables.Count > 6 && ds.Tables[6] != null && ds.Tables[6].Rows.Count > 0)
                        {
                            GenHomePageRow7 r = new GenHomePageRow7();
                            r.mediaList = new List<LinkMedia>();
                            for (int i = 0; i < ds.Tables[6].Rows.Count; i++)
                            {
                                LinkMedia mediaLst = new LinkMedia();
                                DataRow dr = ds.Tables[6].Rows[i];                                
                                mediaLst.media = (dr["Media"] != null ? dr["Media"].ToString() : "");
                                mediaLst.type = (dr["Type"] != null ? dr["Type"].ToString() : "");
                                mediaLst.link = (dr["Link"] != null ? dr["Link"].ToString() : "");
                                r.mediaList.Add(mediaLst);
                            }
                            response.row7 = r;
                        }
                        if (ds.Tables.Count > 8 && ds.Tables[8] != null && ds.Tables[8].Rows.Count > 0)
                        {
                            HomepageModuleControl r = new HomepageModuleControl();
                            DataRow dr = ds.Tables[8].Rows[0];

                            r.showCustomerPortal = (Boolean.TryParse(dr["ShowCustomer"].ToString(), out Boolean showModule) ? showModule : true);
                            r.showEmployee = (Boolean.TryParse(dr["ShowEmployee"].ToString(), out showModule) ? showModule : true);
                            r.showChannelPartner = (Boolean.TryParse(dr["ShowCP"].ToString(), out showModule) ? showModule : true);
                            response.showModules = r;
                        }
                    }

                    genResponse.Data = response;
                    genResponse.IsSuccess = true;
                    genResponse.Status = HttpStatusCode.OK;
                    genResponse.Message = "Success";
                }
                else
                {
                    genResponse.IsSuccess = false;
                    genResponse.Status = HttpStatusCode.Unauthorized;
                    genResponse.Message = "Unauthorized: Access is denied due to invalid credentials";
                }
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "g1/getGeneralHomepageContent");
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.InternalServerError;
                genResponse.Message = "Error: " + ex.Message;
            }
            return genResponse;
        }

        public async Task<APIObjectResponse> getMultipleProjectKeys(string dKey)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            List<MultipleAppKeys> response = new List<MultipleAppKeys>();

            try
            {

                XDocument xdoc = XDocument.Load("keys.xml");
                var check = xdoc.Elements("connections").Elements("connection").Where(x => (string)x.Attribute("dkey") == dKey).FirstOrDefault();
                if (check != null)
                {

                    string spName = "API_GetMultipleProjectKeys";
                    List<SqlParameter> lstParam = new List<SqlParameter> { };
                    DataSet ds = await DBHelper.GetDatasetGeneralASync(dKey, CommandType.StoredProcedure, spName, lstParam);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                                DataRow dr = ds.Tables[0].Rows[i];
                                MultipleAppKeys r = new MultipleAppKeys();

                                r.projectKey = (dr["ProjectKey"] != null ? dr["ProjectKey"].ToString() : "");
                                r.projectName1 = (dr["ProjectName1"] != null ? dr["ProjectName1"].ToString() : "");
                                r.projectName2 = (dr["ProjectName2"] != null ? dr["ProjectName2"].ToString() : "");
                                r.projectLogo = (dr["ProjectLogo"] != null ? dr["ProjectLogo"].ToString() : "");
                                r.rank = (Int32.TryParse(dr["Rank"].ToString(), out int rank) ? rank : 0);

                                response.Add(r);
                            }
                        }                       
                    }

                    genResponse.Data = response;
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
                Utility.LogErrorText(ex.ToString(), "g1/getGeneralHomepageContent");
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.InternalServerError;
                genResponse.Message = "Error: " + ex.Message;
                genResponse.Title = "Error";
            }
            return genResponse;
        }
    }
}
