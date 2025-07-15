using Common.Services.File;
using Microsoft.AspNetCore.Http;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;


namespace Common.File.Command.CancelUpload;

public class CancelUploadHandler: ICommandHandler<CancelUploadCommand>
{

    private readonly IAwsStorageService  _awsStorageService;
    public CancelUploadHandler(IAwsStorageService  awsStorageService)
    {
        _awsStorageService= awsStorageService;
        
    }
    
    public async Task<IResult> Handle(CancelUploadCommand request, CancellationToken cancellationToken)
    {
        await _awsStorageService.AbortMultipartUploadAsync(request.UploadId, request.FileName);
        return Result.Success(true).ToActionResult();
    }
}