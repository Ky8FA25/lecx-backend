using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.GetCourseById;

namespace LecX.WebApi.Endpoints.Courses.GetCourseById
{
    public class GetCourseByIdValidator : Validator<GetCourseByIdRequest>
    {
        public GetCourseByIdValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0).WithMessage("CourseId must be greater than 0.");
        }
    }
}
