using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace LecX.Application.Features.Assignments.CreateAssignment
{
    
    public sealed record CreateAssignmentRequest
    (
        int CourseId,
         string Title,
         DateTime StartDate, 
         DateTime DueDate,
         string AssignmentLink
    ): IRequest<CreateAssignmentResponse>;
}
