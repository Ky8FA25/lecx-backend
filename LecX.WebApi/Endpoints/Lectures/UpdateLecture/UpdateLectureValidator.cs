using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.UpdateLecture;

namespace LecX.WebApi.Endpoints.Lectures.UpdateLecture
{
    public class UpdateLectureValidator : Validator<UpdateLectureRequest>
    {
        public UpdateLectureValidator()
        {
            RuleFor(x => x.LectureId)
                .GreaterThan(0)
                .WithMessage("LectureId must be greater than 0.");

            RuleFor(x => x.Title)
                .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Title));

            RuleFor(x => x.Description)
                .MaximumLength(255)
                .WithMessage("Description must not exceed 255 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));
        }
    }
}
