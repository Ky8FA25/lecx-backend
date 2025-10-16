using FluentValidation;
using FastEndpoints;
using LecX.Application.Features.Assignments.GetAssignmentsByCourse;
namespace LecX.WebApi.Endpoints.Assignments.GetAssignmentsByCourse
{
    public class GetAssignmentsByCourseValidator:Validator<GetAssignmentsByCourseRequest>
    {
        public GetAssignmentsByCourseValidator()
        {
            RuleFor(x => x.CourseId).GreaterThan(0).WithMessage("CourseId must greater than 0");
        }
    }
}
