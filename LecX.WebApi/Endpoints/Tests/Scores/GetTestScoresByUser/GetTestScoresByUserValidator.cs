using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Tests.TestScoreHandler.GetTestScoresByUser;

namespace LecX.WebApi.Endpoints.Tests.Scores.GetTestScoresByUser
{
    public sealed class GetTestScoresByUserValidator : Validator<GetTestScoresByUserRequest>
    {
        public GetTestScoresByUserValidator()
        {
            // 🔹 Allow only one of CourseId or TestId, not both
            RuleFor(x => x)
                .Must(x => !(x.CourseId.HasValue && x.TestId.HasValue))
                .WithMessage("You can only provide either CourseId or TestId, not both.");

            // 🔹 Require at least one
            RuleFor(x => x)
                .Must(x => x.CourseId.HasValue || x.TestId.HasValue)
                .WithMessage("You must provide either CourseId or TestId.");
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1)
                .WithMessage("PageIndex must be at least 1.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 100)
                .WithMessage("PageSize must be between 1 and 100.");
        }
    }
}
