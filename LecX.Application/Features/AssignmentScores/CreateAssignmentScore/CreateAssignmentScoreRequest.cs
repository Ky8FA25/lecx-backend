using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.CreateAssignmentScore
{
    public sealed class CreateAssignmentScoreRequest : IRequest<CreateAssignmentScoreResponse>
    {

        public string StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double Score { get; set; }
    }
}
