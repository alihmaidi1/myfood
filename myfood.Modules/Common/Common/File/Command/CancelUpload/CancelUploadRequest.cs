using Shared.Contract.CQRS;

namespace Common.File.Command.CancelUpload;

public class CancelUploadRequest: ICommand
{
    
    public string UploadId { get; set; }
    
    public string FileName { get; set; }
    
}