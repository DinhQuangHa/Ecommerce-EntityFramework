using Api.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace Api.Helper
{
    public class RespronMessageHelper
    {
        public static HttpResponseMessage ResponseMessage(HttpRequestMessage request, ResponseDto dto)
        {
            return request.CreateResponse(dto.StatusCode, new ResponseDto
            {
                StatusCode = dto.StatusCode,
                ErrorMessage = dto.ErrorMessage,
                ResponseMessage = dto.ResponseMessage
            });
        }
    }
}