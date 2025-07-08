using FluentValidation;

namespace Common.File.Command.CancelUpload;

public class CancelUploadValidation: AbstractValidator<CancelUploadRequest>
{

    public CancelUploadValidation()
    {
        RuleFor(x => x.UploadId)
            .NotEmpty();

        
        RuleFor(x => x.FileName)
            .NotEmpty();
        
        
    }
    
}