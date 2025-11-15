using LecX.Application.Features.AssignmentScores.GetAssignmentScoreById;
using System.ComponentModel.DataAnnotations;
using FluentValidation;
using FastEndpoints;
namespace LecX.WebApi.Endpoints.AssignmentScores.GetAssignmentScoreById
{
    public class GetAssignmentScoreByIdValidator : Validator<GetAssignmentScoreByIdRequest>
    {
        public GetAssignmentScoreByIdValidator()
        {
            RuleFor(x => x.AssignmentScoreId)
                .GreaterThan(0).WithMessage("AssignmentScoreId must be greater than 0.");
        }
    }
}
