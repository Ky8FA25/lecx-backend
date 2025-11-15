using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.StudentCourses.UpdateStudentCourse;

namespace LecX.WebApi.Endpoints.StudentCourses.UpdateStudentCourse
{
    public class UpdateStudentCourseValidator : Validator<UpdateStudentCourseRequest>
    {
        public UpdateStudentCourseValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty()
                .WithMessage("StudentId is required.");

            RuleFor(x => x.CourseId)
                .NotEmpty()
                .WithMessage("CourseId is required.")
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than zero.");

            RuleFor(x => x.Progress)
                .InclusiveBetween(0, 100)
                .WithMessage("Progress must be between 0 and 100.")
                .When(x => x.Progress.HasValue);

            RuleFor(x => x.CertificateStatus)
                .IsInEnum()
                .WithMessage("CertificateStatus is invalid.")
                .When(x => x.CertificateStatus.HasValue);

            RuleFor(x => x.CompletionDate)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("CompletionDate cannot be in the future.")
                .When(x => x.CompletionDate.HasValue);
        }
    }
}
