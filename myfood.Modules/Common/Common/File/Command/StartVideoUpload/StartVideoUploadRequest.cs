using Shared.Contract.CQRS;

namespace Common.File.Command.StartVideoUpload;

public class StartVideoUploadRequest
{
    public string Video { get; set; }

    
}

public class StartVideoUploadCommand: StartVideoUploadRequest,ICommand
{
    public Guid? RequestId { get; set; }
}