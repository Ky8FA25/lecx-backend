using LecX.Application.Common.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Submissions.GetSubmissionsByAssignment
{
    public sealed record GetSubmissionsByAssignmentRequest
    (
        int AssignmentId,
        int PageIndex,
        int PageSize

    ) : IRequest<GetSubmissionsByAssignmentResponse>;
}
