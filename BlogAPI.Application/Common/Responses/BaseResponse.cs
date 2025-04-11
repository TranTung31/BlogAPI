using System.Text.Json.Serialization;

namespace BlogAPI.Application.Common.Responses
{
    public class BaseResponse<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }

        public T? Result { get; set; }

        public BaseResponse()
        {
            Status = true;
        }

        public BaseResponse(T data, string message)
        {
            Status = true;
            Message = message;
            Result = data;
        }

        public BaseResponse(string message)
        {
            Status = false;
            Message = message;
            Result = default;
        }
    }
}
