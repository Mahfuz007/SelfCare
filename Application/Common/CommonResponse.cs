using FluentValidation.Results;
using System.Net;

namespace Application.Common
{
    public class CommonResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
        public dynamic? ResponseData { get; set; }
        public long TotalCount { get; set; }

        public CommonResponse(dynamic responseData) { 
            ResponseData = responseData;
            StatusCode = HttpStatusCode.OK;
        }

        public CommonResponse(dynamic responseData, long totalCount)
        {
            ResponseData = responseData;
            StatusCode = HttpStatusCode.OK;
            TotalCount = totalCount;
        }

        public CommonResponse(ValidationResult validationResult) {
            Message = validationResult.ToString();
            ResponseData = validationResult;
            StatusCode = HttpStatusCode.BadRequest;
        }

        public CommonResponse(HttpStatusCode statusCode, dynamic data) {
            ResponseData = data;
            StatusCode = statusCode;
        }

        public CommonResponse(HttpStatusCode statusCode, string message) {
            Message = message;
            StatusCode = statusCode;
        }
    }
}
