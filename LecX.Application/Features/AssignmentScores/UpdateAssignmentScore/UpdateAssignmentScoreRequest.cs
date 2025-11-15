using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.UpdateAssignmentScore
{
    public sealed class UpdateAssignmentScoreRequest:IRequest<UpdateAssignmentScoreResponse>
    {
        public int AssignmentScoreId { get; set; }

        public double Score { get; set; }
    }
}
