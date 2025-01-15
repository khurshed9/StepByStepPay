namespace Application.Extensions.ResultPatterns;

public class BaseResult
{
    public bool IsSuccess { get; init; }

    public Error Error { get; init; }

    public BaseResult(bool isSeccess, Error error)
    {
        IsSuccess = isSeccess;
        Error = error;
    }

    public static BaseResult Success() => new(true, Error.None());

    public static BaseResult Failure(Error error) => new(false, error);
}