using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Assignments.AssignmentsDtos;    
namespace LecX.Application.Features.Assignments.DeleteAssignment
{
    public sealed record DeleteAssignmentResponse (
        bool Success,
        string Message,
        AssignmentDto? Data = null
    ): GenericResponseRecord<AssignmentDto>(Success, Message, Data);

}
