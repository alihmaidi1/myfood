
namespace Shared.OperationResult;

public static class ResultOperation
{
    public static JsonResult ToJsonResult(this Result operationTResult)
    {
        var options = new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = false 
        };
        return new JsonResult(operationTResult,options)
        {

            StatusCode = (int)operationTResult.StatusCode,
                        

        };

    }


    public static async Task<JsonResult> ToJsonResultAsync(this Task<Result> operationResult)     {

        return (await operationResult).ToJsonResult();
    }
}