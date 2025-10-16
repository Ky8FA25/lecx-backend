using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Assignments.CreateAssignment;
using LecX.Domain.Enums;

namespace LecX.WebApi.Endpoints.Assignments.CreateAssignment
{
    
    public class CreateAssignmentValidator :Validator<CreateAssignmentRequest>
    {
        public CreateAssignmentValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255).WithMessage("Title is required and must not exceed 255 characters.");
            RuleFor(x => x.StartDate).LessThan(x => x.DueDate).NotEmpty()
                .WithMessage("StartDate is required and must be earlier than DueDate.");
            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow).NotEmpty()
                .WithMessage("DueDate is required and must be a future date.");

            RuleFor(x => x.AssignmentLink).NotEmpty().MaximumLength(2048).WithMessage("Material link is required.");
            RuleFor(x => x.AssignmentLink)
                .NotEmpty()
                .MaximumLength(2048)
                .WithMessage("Assignments link is required and must not exceed 2048 characters.")
                .Must(BeAValidUrl)
                .WithMessage("Assignments link must be a valid URL.");
        }
        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
