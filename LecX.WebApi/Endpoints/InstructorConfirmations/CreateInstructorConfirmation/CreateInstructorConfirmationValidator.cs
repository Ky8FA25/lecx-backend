using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation;
namespace LecX.WebApi.Endpoints.InstructorConfirmations.CreateInstructorConfirmation
{
    public class CreateInstructorConfirmationValidator : Validator<CreateInstructorConfirmationRequest>
    {
        public CreateInstructorConfirmationValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File name is required.")
                .MaximumLength(255).WithMessage("File name must not exceed 255 characters.");
            RuleFor(x => x.Certificatelink)
                .NotEmpty().WithMessage("Certificate link is required.")
                .MaximumLength(2048).WithMessage("Certificate link must not exceed 2048 characters.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(2000).WithMessage("Description must not exceed 2000 characters.");
        }
    }
}
