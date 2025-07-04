
namespace Shared.OperationResult;

public static class ResultOperation
{
    public static JsonResult ToJsonResult(this Result operationTResult)
    {
        
        return new JsonResult(operationTResult)
        {

            StatusCode = (int)operationTResult.StatusCode,
                        

        };

    }


    public static async Task<JsonResult> ToJsonResultAsync(this Task<Result> operationResult)     {

        return (await operationResult).ToJsonResult();
    }
}