
namespace Shared.OperationResult;

public class Result
{
    public Result(bool isSuccess,HttpStatusCode statusCode, Error? error=null)
    {
        if (isSuccess && error != null)
        {
            throw new InvalidOperationException();
        }

        if (!isSuccess && error == null)
        {
            throw new InvalidOperationException();
        }

        IsSuccess = isSuccess;
        Error = error;
        StatusCode = statusCode;
    }

    public bool IsSuccess { get; }
    public HttpStatusCode StatusCode { get; }

    public bool IsFailure => !IsSuccess;

    public Error? Error { get; }


    public static TResult<TValue> Failure<TValue>(Error error,HttpStatusCode statusCode) => new(default, false,statusCode, error);


    
    public static TResult<TValue?> Success<TValue>(TValue? value) => new(value, true, HttpStatusCode.OK);

    public static IResult SuccessResult<TValue>(TValue? value) => Results.Ok(new TResult<TValue>(value, true, HttpStatusCode.OK));

    
    
    public static TResult<TValue?> ValidationFailure<TValue>(Error error)=> new(default, false, HttpStatusCode.UnprocessableContent,error);

    public static IResult ValidationFailureResult<TValue>(Error error)=> Results.UnprocessableEntity(new TResult<TValue>(default, false, HttpStatusCode.UnprocessableContent,error));
    
    public static TResult<TValue?> InternalFailure<TValue>(Error error)=> new(default, false,HttpStatusCode.InternalServerError, error);
    
    public static IResult InternalFailureResult<TValue>(Error error)=> Results.InternalServerError(new TResult<TValue>(default, false,HttpStatusCode.InternalServerError, error));
    
    public static TResult<TValue?> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : InternalFailure<TValue>(Error.NullValue);


}