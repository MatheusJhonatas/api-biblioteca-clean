using System;

namespace AuthApi.ViewModels;

public class ResultResponse<T>
{
    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new();
    public ResultResponse(T data, List<string> errors)
    {
        Data = data;
        Errors = errors;
    }
    public ResultResponse(T data)
    {
        Data = data;
    }
    public ResultResponse(List<string> errors)
    {
        Errors = errors;
    }
    public ResultResponse(string error)
    {
        Errors.Add(error);
    }
}