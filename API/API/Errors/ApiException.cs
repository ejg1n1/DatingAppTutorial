namespace API.Errors;

public class ApiException
{
    public ApiException(int statusCode, string message, string details)
    {
        Details = details;
        Message = message;
        StatusCode = statusCode;
    }

    public string Details { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; }
}