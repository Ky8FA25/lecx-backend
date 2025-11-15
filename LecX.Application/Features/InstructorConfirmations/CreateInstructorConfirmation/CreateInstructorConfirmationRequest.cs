using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation
{
    public sealed class CreateInstructorConfirmationRequest : IRequest<CreateInstructorConfirmationResponse>
    {
        public string FileName { get; set; }
        public string Certificatelink { get; set; }
        public string Description { get; set; }

        [JsonIgnore]
        public string UserId { get; set; }

    }
}
