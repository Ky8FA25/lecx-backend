using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.CourseMaterials.UpdateCourseMaterial
{
    public sealed class UpdateMaterialHandler(IAppDbContext db) : IRequestHandler<UpdateMaterialRequest, UpdateMaterialResponse>
    {
        public async Task<UpdateMaterialResponse> Handle(UpdateMaterialRequest request, CancellationToken ct)
        {
            try
            {
                var material = await db.Set<CourseMaterial>().FindAsync(request.MaterialId);
                if (material == null)
                {
                    return new UpdateMaterialResponse(false, "Material not found.");
                }
                if (request.CourseId.HasValue)
                    material.CourseId = request.CourseId.Value;
                if (request.FileType.HasValue)
                    material.FileType = request.FileType.Value;
                if (!string.IsNullOrEmpty(request.FileName))
                    material.FIleName = request.FileName;
                if (!string.IsNullOrEmpty(request.FileExtension))
                    material.FileExtension = request.FileExtension;
                if (!string.IsNullOrEmpty(request.MaterialsLink))
                    material.MaterialsLink = request.MaterialsLink;
                material.UploadDate = DateTime.Now;
                await db.SaveChangesAsync(ct);
                return new UpdateMaterialResponse(true, "Material updated successfully.");
            }
            catch (Exception ex)
            {
                return new UpdateMaterialResponse(false, $"Error updating material: {ex.Message}");
            }
        }
    }
}
