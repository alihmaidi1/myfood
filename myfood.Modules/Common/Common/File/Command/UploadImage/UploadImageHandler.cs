using Microsoft.AspNetCore.Http;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;

namespace Common.File.Command.UploadImage;

public class UploadImageHandler: ICommandHandler<UploadImageRequest>
{

    private readonly IAwsStorageService _awsStorageService;
    public UploadImageHandler(IAwsStorageService awsStorageService)
    {
        _awsStorageService= awsStorageService;
        
    }
    public async Task<IResult> Handle(UploadImageRequest request, CancellationToken cancellationToken)
    {
        var presignedUrl = await _awsStorageService.GenerateImageUploadUrl(request.Image);
        return Result.SuccessResult(presignedUrl.Value);
    }
}