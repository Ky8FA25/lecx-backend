using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.CreateLecture;

namespace LecX.WebApi.Endpoints.Lectures.CreateLecture
{
    public class CreateLectureValidator : Validator<CreateLectureRequest>
    {
        public CreateLectureValidator() 
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than 0.");

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required.")
                .MaximumLength(255)
                .WithMessage("Title must not exceed 255 characters.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(255)
                .WithMessage("Description must not exceed 255 characters.");
        }
    }
}
