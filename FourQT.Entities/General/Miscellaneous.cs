using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.General
{
    public class Miscellaneous
    {
    }

    public class QRCodeRequest
    {
        public string? inputString { get; set; }
    }

    public class QRCodeResponse
    {
        public string? qrCode { get; set; }
    }

    public class Headers
    {
        public int id { get; set; }
        public string? key { get; set; }
        public string? value { get; set; }
    }
}
