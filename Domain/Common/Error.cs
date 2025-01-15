namespace Domain.Common;

public sealed record Error
{
    public int? StatusCode { get; init; }

    public string? Message { get; init; }

    public ErrorType ErrorType { get; init; }

    public Error()
    {
        StatusCode = 500;
        Message = "Internal server error...!";
        ErrorType = ErrorType.InternalServerError;
    }

    public Error(int? statusCode, string? message, ErrorType errorType)
    {
        StatusCode = statusCode;
        Message = message;
        ErrorType = errorType;
    }

    public static Error None()
        => new(null, null, ErrorType.None);
    
    public static Error NotFound(string message = "Data not found!") 
        => new(404, message, ErrorType.NotFound);

    public static Error BadRequest(string message = "Bad request!") 
        => new(400, message, ErrorType.BadRequest);

    public static Error AlreadyExists(string message = "Already exists!") 
        => new(409, message, ErrorType.AlreadyExist);
    
    public static Error Conflict(string message = "Conflict!") 
        => new (409, message, ErrorType.Conflict);

    public static Error InternalServerError(string message = "Internal Server Error!") 
        => new(500, message, ErrorType.InternalServerError);
}