namespace Biblioteca.Application.DTOs.Responses;

public class ResultResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }

    public static ResultResponse<T> Ok(T data, string message = "")
    {
        return new ResultResponse<T> { Success = true, Message = message, Data = data };
    }

    public static ResultResponse<T> Fail(string message)
    {
        return new ResultResponse<T> { Success = false, Message = message };
    }
}
