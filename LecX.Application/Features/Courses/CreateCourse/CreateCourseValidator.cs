using FluentValidation;

namespace LecX.Application.Features.Courses.CreateCourse
{
    public sealed class CreateCourseValidator : AbstractValidator<CreateCourseRequest>
    {
        public CreateCourseValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
        }
    }
}
