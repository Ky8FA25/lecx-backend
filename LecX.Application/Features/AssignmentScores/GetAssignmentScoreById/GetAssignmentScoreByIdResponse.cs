using LecX.Application.Common.Dtos;
using LecX.Application.Features.AssignmentScores.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.AssignmentScores.GetAssignmentScoreById
{
    public sealed record GetAssignmentScoreByIdResponse(bool Success, string Message, AssignmentScoreDto? Data) : GenericResponseRecord<AssignmentScoreDto>(Success, Message, Data); 
    
}
