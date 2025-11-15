using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.StudentCourses.CreateStudentCourse;

namespace LecX.WebApi.Endpoints.StudentCourses.CreateStudentCourse
{
    public class CreateStudentCourseValidator : Validator<CreateStudentCourseRequest>
    {
        public CreateStudentCourseValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.")
                .GreaterThan(0).WithMessage("CourseId must be greater than zero.");
        }
    }
}
