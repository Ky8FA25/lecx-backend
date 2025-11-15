using System.ComponentModel.DataAnnotations;
using FastEndpoints;
using LecX.Application.Features.AssignmentScores.DeleteAssignmentScore;
using FluentValidation;
namespace LecX.WebApi.Endpoints.AssignmentScores.DeleteAssignmentScore
{
    public class DeleteAssignmentScoreValidator : Validator<DeleteAssignmentScoreRequest>
    {
        public DeleteAssignmentScoreValidator()
        {
            RuleFor(x => x.AssignmentScoreId)
                .GreaterThan(0).WithMessage("AssignmentScoreId must be greater than 0.");
        }
    }
}
