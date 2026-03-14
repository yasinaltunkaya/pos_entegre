using System;

namespace PayGoAgent.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public string ErrorCode { get; set; }
        public DateTime ServerTime { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                ErrorCode = null,
                ServerTime = DateTime.Now
            };
        }

        public static ApiResponse<T> Fail(string message, string errorCode, T data = default(T))
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = data,
                ErrorCode = errorCode,
                ServerTime = DateTime.Now
            };
        }
    }
}
