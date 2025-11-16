using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Features.Assignments.AssignmentsDtos;
using LecX.Domain.Entities;

namespace LecX.Application.Features.AssignmentScores.Common
{
    public class AssignmentScoreFullDataDto
    {
        public int AssignmentScoreId { get; set; }
        public string StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double Score { get; set; }

        public AssignmentDto Assignment { get; set; }

    }
}
