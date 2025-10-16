using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Submissions.SubmissionDtos;
namespace LecX.Application.Features.Submissions.GetSubmissionsByAssignment
{
    public sealed record GetSubmissionsByAssignmentResponse (PaginatedResponse<SubmissionDto> Data);
    //(
    //    bool Success,
    //    string Message,
    //    PaginatedList<SubmissionDto> Submissions
    //): GenericResponseRecord<PaginatedList<SubmissionDto>>(Success, Message, Submissions);

}
