using Microsoft.Data.SqlClient;
using System.Data;
using System.Net;
using System.Xml.Linq;
using MobAppCoreAPI.Interfaces;
using MobAppCoreAPI.Models.Response;
using FourQT.CommonFunctions;
using FourQT.Masters;
using FourQT.Entities;
using FourQT.Utilities;
using System;

namespace MobAppCoreAPI.Repository
{
    public class ListAllEnquiryMastersRepository : IListAllEnquiryMasters
    {      
        public async Task<dynamic> listAllEnquiryMasters(HttpRequest req, int typeId)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            generalResponse.IsSuccess = true;
            generalResponse.Status = HttpStatusCode.OK;
            generalResponse.Message = "Success";

            try
            {
                JWTTokenMethods jwt = new JWTTokenMethods();
                //JwtTokenAuthorize jwtauth = new JwtTokenAuthorize();
                jwt.GetConnectionDetails(req, out int loginId, out string mKey);
                //jwtauth.GetConnectionDetails(req, out int loginIdd, out string mKeyy,out string SecToken);
                generalResponse.Data = (new MastersBLL()).GetAllMasters(mKey, loginId, typeId);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/list-enquiry-masters");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString() ;
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }

        public async Task<dynamic> enquiryMastersByEnqId(HttpRequest req, MastersByEnquiryReq model)
        {
            APIObjectResponse generalResponse = new APIObjectResponse();

            try
            {               
                generalResponse.Data = await (new MastersBLL()).EnquiryMastersByEnqId(req, model);
                generalResponse.IsSuccess = true;
                generalResponse.Message = "Success";
                generalResponse.Status = HttpStatusCode.OK;
                generalResponse.Title = "Success";
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/EnquiryMastersByEnqId");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Status = HttpStatusCode.BadRequest;
                generalResponse.Title = "Error";
            }

            return generalResponse;
        }
    }
}
