using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FourQT.Entities.Portal
{
    [DataContract]
    [Serializable]
    public class ResponseStatus<T>
    {
        [DataMember]
        public bool Status { get; set; }
        [DataMember]
        public int ErrorCode { get; set; }
        [DataMember]
        public string? Message { get; set; }
        [DataMember]
        public List<T>? LstData { get; set; }
        [DataMember]
        public T? Data { get; set; }

    }

    public class ResponseStatusToken<T> : ResponseStatus<T>
    {
        [DataMember]
        public string? token { get; set; }
    }

    public class ResponseStatus_New<T>
    {
        [DataMember]
        public HttpStatusCode? Status { get; set; }

        [DataMember]
        public bool IsSuccess { get; set; }

        [DataMember]
        public string? Title { get; set; }

        [DataMember]
        public string? Message { get; set; }

        [DataMember]
        public List<T>? LstData { get; set; }

        [DataMember]
        public T? Data { get; set; }

    }

    public class ResponseStatusToken_New<T> : ResponseStatus_New<T>
    {
        [DataMember]
        public string? token { get; set; }
    }
}
