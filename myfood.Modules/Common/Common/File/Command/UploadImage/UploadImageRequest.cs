using Shared.Contract.CQRS;

namespace Common.File.Command.UploadImage;

public class UploadImageRequest: ICommand
{
    
    public string Image { get; set; }
    
}