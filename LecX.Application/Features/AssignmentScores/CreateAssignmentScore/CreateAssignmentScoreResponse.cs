using LecX.Application.Common.Dtos;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Application.Features.Lectures.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.CreateAssignmentScore
{
    public sealed record  CreateAssignmentScoreResponse(bool Success, string Message, AssignmentScoreDto? Data) : GenericResponseRecord<AssignmentScoreDto>(Success, Message, Data); 
    
}
