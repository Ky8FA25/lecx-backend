using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.InstructorConfirmations.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.InstructorConfirmations.ApproveInstructorConfirmation
{
    public sealed class ApproveInstructorConfirmationHandler(
        IAppDbContext db,
        IMapper mapper,
        UserManager<User> userManager,
        RoleManager<IdentityRole> roleManager
    ) : IRequestHandler<ApproveInstructorConfirmationRequest, ApproveInstructorConfirmationResponse>
    {
        public async Task<ApproveInstructorConfirmationResponse> Handle(
            ApproveInstructorConfirmationRequest request,
            CancellationToken cancellationToken)
        {
            // Find the instructor confirmation
            var confirmation = await db.Set<InstructorConfirmation>()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.ConfirmationId == request.ConfirmationId, cancellationToken);

            if (confirmation == null)
            {
                return new("Instructor confirmation not found");
            }

            // Check if Instructor already exists for this user
            var instructorExists = await db.Set<Instructor>()
                .AnyAsync(i => i.InstructorId == confirmation.UserId, cancellationToken);

            if (instructorExists)
            {
                return new("Instructor already exists for this user");
            }

            // Create new Instructor record
            var instructor = new Instructor
            {
                InstructorId = confirmation.UserId,
                Bio = confirmation.Description ?? "Instructor profile"
            };

            await db.Set<Instructor>().AddAsync(instructor, cancellationToken);

            var instructorRoleExists = await roleManager.RoleExistsAsync("Instructor");
            if (!instructorRoleExists)
            {
                await roleManager.CreateAsync(new IdentityRole("Instructor"));
            }

            var user = confirmation.User;

            // Lấy role hiện tại
            var userRoles = await userManager.GetRolesAsync(user);

            // Nếu user đang có role khác, remove hết
            foreach (var role in userRoles)
            {
                await userManager.RemoveFromRoleAsync(user, role);
            }

            // Add role mới "Instructor"
            await userManager.AddToRoleAsync(user, "Instructor");

            try
            {
                var affected = await db.SaveChangesAsync(cancellationToken);

                return affected > 0
                    ? new("Instructor confirmation approved successfully", true, mapper.Map<InstructorConfirmationDto>(confirmation))
                    : new("Failed to approve instructor confirmation");
            }
            catch (DbUpdateException ex)
            {
                return new($"Error while approving instructor confirmation: {ex.Message}");
            }
        }
    }
}
