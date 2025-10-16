using MediatR;


namespace LecX.Application.Features.Assignments.UpdateAssignment
{
    public sealed record UpdateAssignmentRequest
    (
        int AssignmentId,
        int CourseId,
        string Title,
        DateTime StartDate,
        DateTime DueDate,
        string AssignmentLink
    ):IRequest<UpdateAssignmentResponse>;

}
