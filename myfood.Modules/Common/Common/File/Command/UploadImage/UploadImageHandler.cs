using Common.Services.File;
using Microsoft.AspNetCore.Http;
using Shared.Domain.CQRS;
using Shared.Domain.OperationResult;

namespace Common.File.Command.UploadImage;

public class UploadImageHandler: ICommandHandler<UploadImageCommand>
{

    private readonly IAwsStorageService _awsStorageService;
    public UploadImageHandler(IAwsStorageService awsStorageService)
    {
        _awsStorageService= awsStorageService;
        
    }
    public async Task<IResult> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var presignedUrl = await _awsStorageService.GenerateImageUploadUrl(request.Image);
        return Result.Success(presignedUrl.Value).ToActionResult();
    }
}