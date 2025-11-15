using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestHandler.DeleteTest;

namespace LecX.WebApi.Endpoints.Tests.DeleteTest
{
    public sealed class DeleteTestValidator : Validator<DeleteTestRequest>
    {
        public DeleteTestValidator()
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
        }
    }
}
