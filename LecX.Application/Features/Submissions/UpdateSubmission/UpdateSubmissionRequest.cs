using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Submissions.UpdateSubmission
{
    public  sealed record UpdateSubmissionRequest 
    (
        int SubmissionId,
        string FileName,
        string SubmissionLink

    ) : IRequest<UpdateSubmissionResponse>;



}
