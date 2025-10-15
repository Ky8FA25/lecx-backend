using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.UpdateCourse;

namespace LecX.WebApi.Endpoints.Courses.UpdateCourse
{
    public sealed class UpdateCourseValidator : Validator<UpdateCourseRequest>
    {
        public UpdateCourseValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be greater than 0.");

            RuleFor(x => x.UpdateCourseDto.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(x => x.UpdateCourseDto.CourseCode)
                .NotEmpty().WithMessage("CourseCode is required.")
                .MaximumLength(20).WithMessage("CourseCode cannot exceed 20 characters.");

            RuleFor(x => x.UpdateCourseDto.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be >= 0.");

            RuleFor(x => x.UpdateCourseDto.Level)
                .IsInEnum().WithMessage("Invalid course level.");
            
        }
    }
}
