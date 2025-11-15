using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestHandler.GetTestsByCourse;

namespace LecX.WebApi.Endpoints.Tests.GetTestsByCourse
{
    public class GetTestsByCourseValidator : Validator<GetTestsByCourseRequest>
    {
        public GetTestsByCourseValidator()
        {
            RuleFor(x => x.CourseId)
                .NotEmpty().WithMessage("CourseId is required.");
        }
    }
}
