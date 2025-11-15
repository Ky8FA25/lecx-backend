using System.ComponentModel.DataAnnotations;
using FluentValidation;
using LecX.Application.Features.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent;
using FastEndpoints;

namespace LecX.WebApi.Endpoints.AssignmentScores.GetAssignmentScoreByAssignmentAndStudent
{
    public class GetAssignmentScoreByAssignmentAndStudentValidator : Validator<GetAssignmentScoreByAssignmentAndStudentRequest>
    {
        public GetAssignmentScoreByAssignmentAndStudentValidator()
        {

            RuleFor(x => x.AssignmentId)
                .GreaterThan(0).WithMessage("AssignmentId must be greater than 0.");
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.")
                .MaximumLength(50).WithMessage("StudentId must not exceed 50 characters.");

        }
    }
}

