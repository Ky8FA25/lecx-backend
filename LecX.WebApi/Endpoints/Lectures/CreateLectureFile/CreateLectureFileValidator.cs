using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.CreateLectureFile;

namespace LecX.WebApi.Endpoints.Lectures.CreateLectureFile
{
    public class CreateLectureFileValidator : Validator<CreateLectureFileRequest>
    {
        public CreateLectureFileValidator()
        {
            RuleFor(x => x.LectureId).NotEmpty().GreaterThan(0).WithMessage("LectureId must be not empty and greater than 0.");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName is required.");
            RuleFor(x => x.FilePath).NotEmpty().WithMessage("FilePath is required.").Must(url => string.IsNullOrWhiteSpace(url) || (
                    url.Length <= 2048 &&
                    Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                )).WithMessage("FilePath muse be a valid URL");
            RuleFor(x => x.FileExtension).NotEmpty().WithMessage("FileExtension is required.");
            RuleFor(x => x.FileType)
                .IsInEnum()
                .WithMessage("Invalid FileType value. Must be one of: Image-0, Video-1, Document-2, or Other-3.");
        }
    }
}
