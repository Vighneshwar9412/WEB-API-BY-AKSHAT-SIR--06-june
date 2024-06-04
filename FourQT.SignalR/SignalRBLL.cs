using System.Data;
using System.Data.SqlClient;
using FourQT.Entities.SignalR;
using FourQT.DAL;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using FourQT.Entities;
using System.Net;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using Microsoft.AspNetCore.SignalR;
using FourQT.SignalR.SignalRHub;
using System.Collections.ObjectModel;

namespace FourQT.SignalR
{
    public class SignalRBLL
    {
        private IHubContext<SignalRMessageHub, ISignalRMessageHubClient> messageHub;
        public SignalRBLL(IHubContext<SignalRMessageHub, ISignalRMessageHubClient> _messageHub)
        {
            messageHub = _messageHub;
        }

        public async Task<dynamic> sendNotification(SignalRRequest request)
        {
            APIObjectResponse genResponse = new APIObjectResponse();
            DataSet ds = new DataSet();

            int clientId = 0;
            string clientType = "", token = "";

            try
            {

                //    if (request != null && request.token != null && request.token.Trim() != "") {
                //        token = request.token.Trim();
                //    }
                //    else
                //    {
                //        genResponse.IsSuccess = false;
                //        genResponse.Message = "Invalid token.";
                //        genResponse.Title = "Invalid Request";
                //        genResponse.Status = HttpStatusCode.BadRequest;
                //        return genResponse;
                //    }

                //    if (request.client != null && (request.client.ToUpper() == "I" || request.client.ToUpper() == "G"))
                //    {
                //        clientId = request.clientId;
                //        clientType = request.client.ToUpper();
                //    }
                //    else {
                //        clientType = "A";
                //    }

                //    if (clientType != "A" && clientId <= 0) {
                //        genResponse.IsSuccess = false;
                //        genResponse.Message = "Invalid client Id.";
                //        genResponse.Title = "Invalid Request";
                //        genResponse.Status = HttpStatusCode.BadRequest;
                //        return genResponse;
                //    }

                //    string spName = "API_SendSignalRNotiffication";
                //    List<SqlParameter> lstParam = new List<SqlParameter>
                //    {
                //        new SqlParameter() { ParameterName = "@Status", SqlDbType = SqlDbType.Int, Value = 0 },
                //        new SqlParameter() { ParameterName = "@OutMsg", SqlDbType = SqlDbType.VarChar, Size = 200, Value = "" },
                //        new SqlParameter() { ParameterName = "@ClientId", SqlDbType = SqlDbType.Int, Value = clientId },
                //        new SqlParameter() { ParameterName = "@ClientType", SqlDbType = SqlDbType.VarChar, Size = 1, Value = clientType }                    
                //    };

                //    lstParam[0].Direction = ParameterDirection.Output;
                //    lstParam[1].Direction = ParameterDirection.Output;

                //    dynamic[] resArray = await DBHelper.GetDataSetEmployeeAsync(token, CommandType.StoredProcedure, spName, lstParam);
                //    if (resArray != null && resArray.Length > 0)
                //    {
                //        ds = resArray[0];
                //    }

                //    List<string?>? clientConnectionIds = new List<string?>();

                //    if (ds != null && ds.Tables.Count > 0) 
                //    {
                //        DataTable dt = ds.Tables[0];
                //        if(dt != null && dt.Rows.Count > 0)
                //        {
                //            for(int i = 0; i < dt.Rows.Count; i++)
                //            {
                //                clientConnectionIds.Add((dt.Rows[0]["connectionId"] != null ? dt.Rows[0]["connectionId"].ToString() : ""));
                //            }
                //        }
                //    }

                //    await NotifyClientConnections(clientType, clientConnectionIds);

            }
            catch {
                throw;
            }

            return genResponse;
        }

        public async Task<dynamic> NotifyClientConnections(string clientType, List<string?>? clientConnectionIds) {
            try {

                if (clientConnectionIds != null && clientConnectionIds.Count > 0) 
                {
                    IReadOnlyList<string> clientList = new List<string>();

                    for (int i = 0; i < clientConnectionIds.Count; i++) {
                        string? clientConn = clientConnectionIds[i];

                        if (clientConn != null && clientConn.ToString().Trim() != "") {
                            clientList.Append<string>(clientConn.ToString().Trim());
                        }
                    }

                    //if (clientList != null && clientList.Count > 0)
                    //{
                    //    messageHub.Clients.Clients(clientList).SendSignalRNotification("Test message");
                    //}
                

                }

                

            }
            catch {
                throw;
            }

            return 1;
        }
    }
}
