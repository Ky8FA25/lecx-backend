using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;
using LecX.Application.Features.Submissions.SubmissionDtos;
namespace LecX.Application.Features.Submissions.UpdateSubmission
{
    public sealed record UpdateSubmissionResponse
    (
    bool Success,
    string Message
    ): GenericResponseRecord<SubmissionDto>(Success, Message);
}
