using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Courses.CreateCourse;

namespace LecX.WebApi.Endpoints.Courses.CreateCourse
{
    public sealed class CreateCourseValidator : Validator<CreateCourseRequest>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
