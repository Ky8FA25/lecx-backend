using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Submissions.DeleteSubmission
{
    public sealed record DeleteSubmissionRequest 
    (
         int SubmissionId 
    ): IRequest<DeleteSubmissionResponse>;

}
