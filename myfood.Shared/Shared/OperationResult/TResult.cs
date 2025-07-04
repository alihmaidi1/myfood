namespace Shared.OperationResult;

public class TResult<TValue>: Result
{

    public TResult(TValue? value, bool isSuccess,HttpStatusCode statusCode, Error? error=null)
        : base(isSuccess,statusCode, error)
    {
        Value = value;
    }

    public TValue? Value { get; }
    public static implicit operator TResult<TValue>(TValue? value)=> Create(value);





    
}