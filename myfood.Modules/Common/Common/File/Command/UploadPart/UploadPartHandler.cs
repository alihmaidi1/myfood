using Microsoft.AspNetCore.Http;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.UploadPart;

public class UploadPartHandler: ICommandHandler<UploadPartRequest>
{

    
    private readonly IAwsStorageService  _awsStorageService;
    
    public UploadPartHandler(IAwsStorageService  awsStorageService)
    {
        
        _awsStorageService= awsStorageService;
    }
    public async Task<IResult> Handle(UploadPartRequest request, CancellationToken cancellationToken)
    {
        using var stream = request.file.OpenReadStream();

        var uploadResult =await _awsStorageService.UploadPartAsync(request.uploadId,
            request.fileName,
            request.partNumber,
            stream);
        return Result.SuccessResult(uploadResult);
    }
}