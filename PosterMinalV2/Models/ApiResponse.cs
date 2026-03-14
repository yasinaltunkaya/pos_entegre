namespace PosterMinalV2.Models
{
    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public long ElapsedMilliseconds { get; set; }
    }
}
