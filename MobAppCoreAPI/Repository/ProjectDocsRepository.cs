using FourQT.CommonFunctions;
using FourQT.Entities;
using FourQT.Reports;
using System.Net;
using FourQT.Utilities;
using FourQT.Core;
using MobAppCoreAPI.Interfaces;
using FourQT.Masters;

namespace MobAppCoreAPI.Repository
{
    public class ProjectDocsRepository:IProjectDocs
    {
        public async Task<dynamic> projectdocslisting(HttpRequest req, int project_id,int item_id)
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
                generalResponse.Data = (new MastersBLL()).projectdocslisting(mKey, loginId, project_id,item_id);

                return generalResponse;
            }
            catch (Exception ex)
            {
                Utility.LogErrorText(ex.ToString(), "api/v1/project-docs");
                generalResponse.IsSuccess = false;
                generalResponse.Message = ex.ToString();
                generalResponse.Data = null;
                generalResponse.Status = System.Net.HttpStatusCode.BadRequest;
                return generalResponse;
            }
        }
    }
}
