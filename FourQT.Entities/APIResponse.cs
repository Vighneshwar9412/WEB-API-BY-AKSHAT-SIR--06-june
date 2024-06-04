using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;


namespace FourQT.Entities
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
        public string message { get; set; }
    }

    public class GeneralResponse
    {
        //public List<string> ErrorMessages { get; set; }
        public string company { get; set; }
        public string logo { get; set; }
        public string mKey { get; set; }
        //public string message { get; set; }
        //public bool IsSuccess { get; set; }
    }

    public class ClientData
    {
        public string company { get; set; }
        public string logo { get; set; }
        public string ekey { get; set; }
        public string responses { get; set; }
        public string description { get; set; }
    }

    public class APIGeneralResponse<T>
    {
        [DataMember]
        public HttpStatusCode status { get; set; }

        [DataMember]
        public bool issuccess { get; set; }

        [DataMember]
        public string? customerrorcode { get; set; }

        [DataMember]
        public string? message { get; set; }

        [DataMember]
        public List<T>? LstData { get; set; }

        [DataMember]
        public T? Data { get; set; }

    }

    public class APIObjectResponse
    {
        [DataMember]
        public HttpStatusCode Status { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string? Title { get; set; }

        [DataMember]
        public string? Message { get; set; }

        [DataMember]
        public Object? Data { get; set; }

    }
}
