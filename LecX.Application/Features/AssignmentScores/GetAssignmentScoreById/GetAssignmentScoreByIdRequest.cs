using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreById
{
    public sealed record GetAssignmentScoreByIdRequest
    (
        int AssignmentScoreId
    ): IRequest<GetAssignmentScoreByIdResponse>;
}
