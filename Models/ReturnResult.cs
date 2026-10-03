namespace TeamApi.Models;

public class ReturnResult
{
    public bool IsSuccess { get; set; }
    public object? Result { get; set; }
    public List<string> ErrorMessage { get; set; } = new();

    public static ReturnResult Success(object? result = null)
        => new() { IsSuccess = true, Result = result };

    public static ReturnResult Fail(params string[] errors)
        => new() { IsSuccess = false, ErrorMessage = errors.ToList() };
}
