using LecX.Application.Features.Courses.DeleteCourse;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;
using FastEndpoints;

namespace LecX.WebApi.Endpoints.Courses.DeleteCourse
{
    public sealed class DeleteCourseValidator : Validator<DeleteCourseRequest>
    {
        public DeleteCourseValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than 0.");
        }
    }
}
