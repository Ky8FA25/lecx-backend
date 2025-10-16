using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.GetAllCourses;

namespace LecX.WebApi.Endpoints.Courses.GetAllCourses
{
    public sealed class GetAllCoursesValidator : Validator<GetAllCoursesRequest>
    {
        public GetAllCoursesValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0).WithMessage("PageIndex must be greater than 0.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100).WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
