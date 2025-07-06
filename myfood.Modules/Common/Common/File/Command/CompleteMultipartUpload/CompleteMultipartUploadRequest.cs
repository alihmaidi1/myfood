using Shared.Contract.CQRS;
using Shared.Services.File;

namespace Common.File.Command.CompleteMultipartUpload;

public class CompleteMultipartUploadRequest: ICommand
{
    public string uploadId { get; set; }
    public string fileName { get; set; }
    public List<PartETag> partETags { get; set; }

}