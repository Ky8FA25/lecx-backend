using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.DeleteLectureFile;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLectureFile
{
    public class DeleteLectureFileValidator : Validator<DeleteLectureFileRequest>
    {
        public DeleteLectureFileValidator()
        {
            RuleFor(x => x.FileId).NotEmpty().WithMessage("FileId is required").GreaterThan(0).WithMessage("FileId must be greater than 0.");
        }
    }
}
