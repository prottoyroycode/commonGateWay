using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bkash_Service_API.Models.Response
{
    public class ServiceResponse<T>
    {
        
        public bool Status { get; set; } = true;
        public ResStatusCode StatusCode { get; set; } = ResStatusCode.Success;
        public string Message { get; set; } = null;
        public int TotalRecords { get; set; } = 0;
        public T Data { get; set; }

    }
    public enum ResStatusCode
    {
        Success = 200,
        Created = 201,
        NoDataAvailable = 204,
        BadRequest = 400,
        Unauthorized = 401,
        NotFound = 404,
        InternalServerError = 500,
        ExistBySocial = 1000
    }
}
