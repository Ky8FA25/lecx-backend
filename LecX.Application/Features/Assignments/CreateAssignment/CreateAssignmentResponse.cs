using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Features.Assignments.AssignmentsDtos;
using LecX.Application.Common.Dtos;
namespace LecX.Application.Features.Assignments.CreateAssignment
{
    public sealed record CreateAssignmentResponse(
    
        bool Success,
        string Message,
        AssignmentDto? Data = null
    ) : GenericResponseRecord<AssignmentDto>(Success, Message, Data);
    
}
