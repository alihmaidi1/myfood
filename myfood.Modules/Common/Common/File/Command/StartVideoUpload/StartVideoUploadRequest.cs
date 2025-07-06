using Shared.Contract.CQRS;

namespace Common.File.Command.StartVideoUpload;

public class StartVideoUploadRequest: ICommand
{
    
    public string Video { get; set; }
    
}