namespace WorkingTimer.Shared.Response
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string[] Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
}
