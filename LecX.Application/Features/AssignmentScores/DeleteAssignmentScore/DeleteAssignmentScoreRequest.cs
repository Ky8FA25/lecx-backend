using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.DeleteAssignmentScore
{
    public sealed class DeleteAssignmentScoreRequest : IRequest<DeleteAssignmentScoreResponse>
    {
        public int AssignmentScoreId { get; set; }
    }
}
