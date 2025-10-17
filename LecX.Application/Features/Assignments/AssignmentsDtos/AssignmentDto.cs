using LecX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Assignments.AssignmentsDtos
{
    public sealed record AssignmentDto
    (
         int AssignmentId,
         int CourseId,
         string Title,
         DateTime StartDate, 
         DateTime DueDate, 
         string AssignmentLink 
    );
}
