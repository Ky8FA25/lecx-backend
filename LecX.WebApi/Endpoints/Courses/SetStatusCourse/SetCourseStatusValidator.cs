using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.SetStatusCourse;

namespace LecX.WebApi.Endpoints.Courses.SetStatusCourse
{
    public sealed class SetCourseStatusValidator : Validator<SetCourseStatusRequest>
    {
        public SetCourseStatusValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid course status.");
        }
    }
}
