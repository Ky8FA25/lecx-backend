using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FastEndpoints;
using LecX.Application.Features.AssignmentScores.UpdateAssignmentScore;
namespace LecX.WebApi.Endpoints.AssignmentScores.UpdateAssignmentScore
{
    public class UpdateAssignmentScoreValidator : Validator<UpdateAssignmentScoreRequest>
    {
        public UpdateAssignmentScoreValidator()
        {
            RuleFor(x => x.AssignmentScoreId)
                .GreaterThan(0).WithMessage("AssignmentScoreId must be greater than 0.");
            RuleFor(x => x.Score)
                .InclusiveBetween(0, 10).WithMessage("Score must be between 0 and 10.");
        }
    }
}
