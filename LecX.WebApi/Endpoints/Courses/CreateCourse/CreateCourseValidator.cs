using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.CreateCourse;
using LecX.Domain.Enums;

namespace LecX.WebApi.Endpoints.Courses.CreateCourse
{
    public sealed class CreateCourseValidator : Validator<CreateCourseRequest>
    {
        public CreateCourseValidator()
        {
            // 🔹 Validate Title
            RuleFor(x => x.CreateCourseDto.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            // 🔹 Validate CourseCode
            RuleFor(x => x.CreateCourseDto.CourseCode)
                .NotEmpty().WithMessage("Course code is required.")
                .MaximumLength(20).WithMessage("Course code cannot exceed 20 characters.");

            // 🔹 Validate Price
            RuleFor(x => x.CreateCourseDto.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be greater than or equal to 0.");

            // 🔹 Validate InstructorId
            RuleFor(x => x.CreateCourseDto.InstructorId)
                .NotEmpty().WithMessage("InstructorId is required.");

            // 🔹 Validate Level
            RuleFor(x => x.CreateCourseDto.Level)
                .IsInEnum().WithMessage("Invalid course level.");           

            // 🔹 Optional: validate CategoryId
            RuleFor(x => x.CreateCourseDto.CategoryId)
                .GreaterThan(0).WithMessage("CategoryId must be greater than 0.");
        }
    }
}
