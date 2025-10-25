using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestHandler.GetTestById;

namespace LecX.WebApi.Endpoints.Tests.GetTestById
{
    public class GetTestByIdValidator : Validator<GetTestByIdRequest>
    {
        public GetTestByIdValidator()
        {
            RuleFor(x => x.TestId)
                .NotEmpty().WithMessage("TestId is required.");
        }
    }
}
