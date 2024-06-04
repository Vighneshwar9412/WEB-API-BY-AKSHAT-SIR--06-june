using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FourQT.Entities;
using FourQT.Entities.General;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FourQT.Core.General
{
    public class MiscellaneousOperationsBLL
    {
        public async Task<dynamic> GenerateQRCode(QRCodeRequest model, HttpContext context)
        {
            APIObjectResponse genResponse = new APIObjectResponse();

            try {
                
            }
            catch (Exception ex) {
                genResponse.IsSuccess = false;
                genResponse.Status = HttpStatusCode.BadRequest;
                genResponse.Title = "Error";
                genResponse.Message = ex.Message;
            }

            return genResponse;
        }
    }
}
