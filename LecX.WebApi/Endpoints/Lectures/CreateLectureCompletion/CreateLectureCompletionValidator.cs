using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.CreateLectureCompletion;

namespace LecX.WebApi.Endpoints.Lectures.CreateLectureCompletion
{
    public class CreateLectureCompletionValidator : Validator<CreateLectureCompletionRequest>
    {
        public CreateLectureCompletionValidator()
        {
            RuleFor(x => x.LectureId)
                .NotEmpty().WithMessage("LectureId is required.")
                .GreaterThan(0).WithMessage("LectureId must be greater than 0.");
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");
        }

    }
}
