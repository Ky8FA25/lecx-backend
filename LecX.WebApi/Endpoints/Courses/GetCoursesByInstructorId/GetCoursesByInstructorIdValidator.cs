using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.GetCoursesByInstructorId;

namespace LecX.WebApi.Endpoints.Courses.GetCoursesByInstructorId
{
    public sealed class GetCoursesByInstructorIdValidator : Validator<GetCoursesByInstructorIdRequest>
    {
        public GetCoursesByInstructorIdValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThan(0)
                .WithMessage("PageIndex must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("PageSize must be between 1 and 100");
        }
    }
}


