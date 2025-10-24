using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.StudentCourses.DeleteStudentCourse;

namespace LecX.WebApi.Endpoints.StudentCourses.DeleteStudentCourse
{
    public class DeleteStudentCourseValidator : Validator<DeleteStudentCourseRequest>
    {
        public DeleteStudentCourseValidator()
        {
            RuleFor(x => x.StudentCourseId)
                .NotEmpty().WithMessage("StudentCourseId is required.");
        }
    }
}
