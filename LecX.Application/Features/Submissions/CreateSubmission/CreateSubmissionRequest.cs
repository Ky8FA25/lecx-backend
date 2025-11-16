using LecX.Application.Features.Submissions.SubmissionDtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LecX.Application.Features.Submissions.CreateSubmission
{
    public sealed class CreateSubmissionRequest : IRequest<CreateSubmissionResponse>
    {
       
        public int AssignmentId { get; set; }
        [JsonIgnore]
        public string StudentId { get; set; }
        public string SubmissionLink { get; set; }
        public string FileName { get; set; }
       
    }
}
