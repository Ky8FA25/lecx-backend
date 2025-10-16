using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Assignments.GetAssignmentById
{
    public sealed record GetAssignmentByIdRequest
    (
        int AssignmentId
    ):IRequest<GetAssignmentByIdResponse>;
}
