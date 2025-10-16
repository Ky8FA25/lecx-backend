using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
﻿using LecX.Application.Abstractions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Assignments.AssignmentsDtos;
using LecX.Domain.Entities;
using MediatR;
namespace LecX.Application.Features.Assignments.CreateAssignment
{
    public class CreateAssignmentHandler (IAppDbContext db ) : IRequestHandler<CreateAssignmentRequest, CreateAssignmentResponse>
    {
        public async Task<CreateAssignmentResponse> Handle(CreateAssignmentRequest req, CancellationToken ct)
        {
            try
            {
                var entity = new Assignment
                {
                    CourseId = req.CourseId,
                    Title = req.Title,
                    StartDate = req.StartDate,
                    DueDate = req.DueDate,
                    AssignmentLink = req.AssignmentLink
                };
                await db.Set<Assignment>().AddAsync(entity, ct);
                await db.SaveChangesAsync(ct);
                var dto = new AssignmentDto(
                    entity.AssignmentId,
                    entity.CourseId,
                    entity.Title,
                    entity.StartDate,
                    entity.DueDate,
                    entity.AssignmentLink
                    );
                return new CreateAssignmentResponse(true, "Assignment created successfully.", dto);
            }
            catch (Exception ex)
            {
                return new CreateAssignmentResponse(false, $"Error creating assignment: {ex.Message}", null);
            }
        }
    }
    
    }

