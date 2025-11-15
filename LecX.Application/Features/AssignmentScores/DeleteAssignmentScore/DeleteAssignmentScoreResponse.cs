using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Features.AssignmentScores.Common;
using LecX.Application.Common.Dtos;
namespace LecX.Application.Features.AssignmentScores.DeleteAssignmentScore
{
    public sealed record DeleteAssignmentScoreResponse(bool Success,string Message) : GenericResponseRecord<AssignmentScoreDto>(Success, Message);

}
