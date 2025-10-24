
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Assignments.AssignmentsDtos;
namespace LecX.Application.Features.Assignments.GetAssignmentsByCourse
{
    public sealed record GetAssignmentsByCourseResponse
    (
        bool Success,
        string Message,
        PaginatedList<AssignmentDto>? Data = null
    ) : GenericResponseRecord<PaginatedList<AssignmentDto>>(Success, Message, Data);
}
