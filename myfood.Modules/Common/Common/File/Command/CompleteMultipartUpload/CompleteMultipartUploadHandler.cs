using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Shared.Contract.CQRS;
using Shared.OperationResult;
using Shared.Services.File;
using PartETag = Amazon.S3.Model.PartETag;

namespace Common.File.Command.CompleteMultipartUpload;

public class CompleteMultipartUploadHandler: ICommandHandler<CompleteMultipartUploadRequest>
{
    
    private readonly IAwsStorageService  _awsStorageService;

    public CompleteMultipartUploadHandler(IAwsStorageService awsStorageService)
    {
        
        _awsStorageService= awsStorageService;
    }
    public async Task<IResult> Handle(CompleteMultipartUploadRequest request, CancellationToken cancellationToken)
    {
        await _awsStorageService.CompleteMultipartUploadAsync(request.uploadId, request.fileName, request.partETags.Select(x=>new PartETag()
        {
            ETag = x.ETag,
            PartNumber = x.PartNumber,
            
        }).ToList());
        return Result.SuccessResult(true);
    }
}