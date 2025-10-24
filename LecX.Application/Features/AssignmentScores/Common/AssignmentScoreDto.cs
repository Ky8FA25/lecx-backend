using LecX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.Common
{
    public class AssignmentScoreDto
    {
        public int AssignmentScoreId { get; set; }
        public string StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double Score { get; set; }
       
    }
}
