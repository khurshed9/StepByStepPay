namespace Application.Extensions.ResultPatterns;

public class Result<T> : BaseResult
{
    public T? Value { get; init; }
    
    public Result(bool isSuccess, Error error,T? value) : base(isSuccess, error)
    {
        Value = value;
    }
    
    public static Result<T> Success(T value) => new(true, Error.None(), value);

    public static Result<T> Failure(Error error) => new(false, error, default);
}