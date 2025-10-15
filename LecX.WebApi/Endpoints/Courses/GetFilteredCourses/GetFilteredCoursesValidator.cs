using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.GetFilteredCourses;

namespace LecX.WebApi.Endpoints.Courses.GetFilteredCourses
{
    public sealed class GetFilteredCoursesValidator : Validator<GetFilteredCoursesRequest>
    {
        public GetFilteredCoursesValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageIndex must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
