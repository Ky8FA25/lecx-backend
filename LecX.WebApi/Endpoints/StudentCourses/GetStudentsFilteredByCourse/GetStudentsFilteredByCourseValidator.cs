
using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.StudentCourses.GetStudentsFilteredByCourse;

namespace LecX.WebApi.Endpoints.StudentCourses.GetStudentsFilteredByCourse
{
    public class GetStudentsFilteredByCourseValidator : Validator<GetStudentsFilteredByCourseRequest>
    {
        public GetStudentsFilteredByCourseValidator()
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
