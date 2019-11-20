using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace Api.Dto
{
    public class ResponseDto
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
        public string ErrorMessage { get; set; }
    }
}