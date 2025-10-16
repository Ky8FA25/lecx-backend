using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Application.Common.Dtos;

namespace LecX.Application.Features.Submissions.DeleteSubmission
{
    public sealed record DeleteSubmissionResponse 
    (
        bool Success,
        string Message
    ): GenericResponseRecord<object>(Success, Message);

}
