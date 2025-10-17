using LecX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Submissions.SubmissionDtos
{
    public sealed class SubmissionDto
    {
        public int SubmissionId { get; set; }
        public int AssignmentId { get; set; }
        public string StudentId { get; set; }
        public string SubmissionLink { get; set; }
        public DateTime SubmissionDate { get; set; } 
        public string FileName { get; set; }
        public SubmissionUserDto Student { get; set; }

    }

    public sealed class SubmissionUserDto
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? AvatarUrl { get; set; }
    }


}
