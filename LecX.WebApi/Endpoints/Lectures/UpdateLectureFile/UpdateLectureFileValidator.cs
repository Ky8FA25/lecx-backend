using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.UpdateLectureFile;

namespace LecX.WebApi.Endpoints.Lectures.UpdateLectureFile
{
    public class UpdateLectureFileValidator : Validator<UpdateLectureFileRequest>
    {
        public UpdateLectureFileValidator() 
        {
            RuleFor(x => x.FileId).NotEmpty().GreaterThan(0).WithMessage("FileId must be not empty and greater than 0.");
            RuleFor(x => x.FilePath).Must(url => string.IsNullOrWhiteSpace(url) || (
                    url.Length <= 2048 &&
                    Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                )).WithMessage("FilePath muse be a valid URL");
            RuleFor(x => x.FileType)
                .IsInEnum()
                .WithMessage("Invalid FileType value. Must be one of: Image-0, Video-1, Document-2, or Other-3.");
        }
    }
}
