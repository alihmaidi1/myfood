using Microsoft.AspNetCore.Http;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.StartVideoUpload;

public class StartVideoUploadHandler: ICommandHandler<StartVideoUploadCommand>
{

    private readonly IAwsStorageService  _awsStorageService;
    public StartVideoUploadHandler(IAwsStorageService  awsStorageService)
    {
        _awsStorageService= awsStorageService;
        
    }
    public async Task<IResult> Handle(StartVideoUploadCommand request, CancellationToken cancellationToken)
    {
        var uploadResult=await _awsStorageService.InitiateChunkedVideoUpload(request.Video);
        return Result.SuccessResult(uploadResult.Value);
        
    }
}