namespace FlexiApi.Utils;

public class ApiResponse<T>
{
    public static ApiResponse<T> Success(T data)
    {
        return new ApiResponse<T>(data);
    }
    
    public static ApiResponse<T> Error(Exception exception)
    {
        return new ApiResponse<T>(exception);
    }
    
    public static ApiResponse<T> Error(string errorMessage)
    {
        return new ApiResponse<T>(errorMessage);
    }
    
    
    
    public bool IsSuccess { get; set; }
    public T? Data { get; set; }
    public string ErrorMessage { get; set; }

    public ApiResponse()
    {
        IsSuccess = false;
        ErrorMessage = string.Empty;
    }
    
    public ApiResponse(T data)
    {
        IsSuccess = true;
        Data = data;
        ErrorMessage = string.Empty;
    }
    
    public ApiResponse(Exception exception)
    {
        IsSuccess = false;
        ErrorMessage = exception.Message;
    }
    
    public ApiResponse(string errorMessage)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }
}