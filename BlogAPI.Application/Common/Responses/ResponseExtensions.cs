using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Common.Responses
{
    public static class ResponseExtensions
    {
        public static BaseResponse<T> ToResponse<T>(this T data, string message)
        {
            return new BaseResponse<T>(data, message);
        }

        public static BaseResponse<T> ToErrorResponse<T>(this string message)
        {
            return new BaseResponse<T>(message);
        }
    }
}
