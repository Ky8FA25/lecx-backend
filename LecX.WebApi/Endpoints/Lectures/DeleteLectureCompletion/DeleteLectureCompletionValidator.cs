using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.DeleteLectureCompletion;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLectureCompletion
{
    public class DeleteLectureCompletionValidator : Validator<DeleteLectureCompletionRequest>
    {
        public DeleteLectureCompletionValidator()
        {
            RuleFor(x => x.LectureId)
                .NotEmpty().WithMessage("LectureId is required.")
                .GreaterThan(0).WithMessage("LectureId must be greater than 0.");
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");
        }
    }
}
