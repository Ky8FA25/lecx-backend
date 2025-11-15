using LecX.Application.Common.Dtos;
using LecX.Application.Features.InstructorConfirmations.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation
{
    public sealed record CreateInstructorConfirmationResponse
   (
         string Message,
        bool Success = false,
        InstructorConfirmationDto? Data = null
    ) : GenericResponseRecord<InstructorConfirmationDto>(Success, Message, Data);
    
}
