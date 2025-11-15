using LecX.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.InstructorConfirmations.Common
{
    public class InstructorConfirmationDto
    {

        public int ConfirmationId { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string Certificatelink { get; set; }
        public virtual UserConfirmationDto User { get; set; }
        public DateTime SendDate { get; set; } 
        public string Description { get; set; }
    }

    public class UserConfirmationDto
    {
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? ProfileImagePath { get; set; }
    }


}
