namespace ProjectManagement.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultErrorMessage(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultErrorMessage(int statusCode)
        {
            switch (statusCode)
            {
                case 400:
                    return "BadRequest , had been done";
                case 401:
                    return "UnAuthorized , You must login first";
                case 403:
                    return "Access Denied , You don't have Access";
                case 404:
                    return "Not Found , Resource you want not founded";
                case 500:
                    return "Server Error , Go back to developer to fix this error";

                default:
                    return null;
            }
        }
    }
}

