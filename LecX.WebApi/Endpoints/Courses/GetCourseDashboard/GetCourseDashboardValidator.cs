using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.GetCourseDashboard;

namespace LecX.WebApi.Endpoints.Courses.GetCourseDashboard
{
    public sealed class GetCourseDashboardValidator : Validator<GetCourseDashboardRequest>
    {
        public GetCourseDashboardValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than 0");
        }
    }
}

