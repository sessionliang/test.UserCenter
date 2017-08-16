using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Localink.UserCenter.Models.Api
{
    public class ApiResult
    {
        public dynamic Result { get; set; }
        public string TargetUrl { get; set; }
        public bool Success { get; set; }
        public Error Error { get; set; }
        public bool UnAuthorizedRequest { get; set; }

        public static ApiResult CreateErrorResult(Exception ex, bool unAuthorizedRequest)
        {
            return new ApiResult
            {
                Result = null,
                Success = false,
                Error = new Error
                {
                    Message = ex.Message,
                    DetailMessage = ex.InnerException?.Message
                },
                TargetUrl = string.Empty,
                UnAuthorizedRequest = unAuthorizedRequest
            };
        }
        public static ApiResult CreateErrorResult(string errorMsg, bool unAuthorizedRequest, string detailMessage = null)
        {
            return new ApiResult
            {
                Result = null,
                Success = false,
                Error = new Error
                {
                    Message = errorMsg,
                    DetailMessage = detailMessage
                },
                TargetUrl = string.Empty,
                UnAuthorizedRequest = unAuthorizedRequest
            };
        }
        public static ApiResult CreateSuccessResult(dynamic result, bool unAuthorizedRequest)
        {
            return new ApiResult
            {
                Result = result,
                Success = true,
                Error = null,
                TargetUrl = string.Empty,
                UnAuthorizedRequest = unAuthorizedRequest
            };
        }
    }

    public class Error
    {
        public string Message { get; set; }
        public string DetailMessage { get; set; }
    }
}