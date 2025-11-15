using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Comments.Common;
using LecX.Application.Features.InstructorConfirmations.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace LecX.Application.Features.InstructorConfirmations.CreateInstructorConfirmation
{
    public sealed class CreateInstructorConfirmationHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<CreateInstructorConfirmationRequest, CreateInstructorConfirmationResponse>
    {
        public async Task<CreateInstructorConfirmationResponse> Handle(CreateInstructorConfirmationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var instructorConfirmation = mapper.Map<InstructorConfirmation>(request);

                await db.Set<InstructorConfirmation>().AddAsync(instructorConfirmation, cancellationToken);
                var affected = await db.SaveChangesAsync(cancellationToken);

                return affected > 0
                    ? new("Success", true, mapper.Map<InstructorConfirmationDto>(instructorConfirmation))
                    : new("Failed");
            }
            catch (DbUpdateException ex)
            {
                return new($"Error while creating Instructor Confirmation: {ex.Message}");
            }
        }
    }
}
