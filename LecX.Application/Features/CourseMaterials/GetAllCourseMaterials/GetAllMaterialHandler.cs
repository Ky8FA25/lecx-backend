using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.CourseMaterials.GetAllCourseMaterials
{
    public sealed class GetAllMaterialHandler(IAppDbContext db) : IRequestHandler<GetAllMaterialRequest, GetAllMaterialResponse>
    {
        public async Task<GetAllMaterialResponse> Handle(GetAllMaterialRequest request, CancellationToken ct)
        {
            try
            {
                var materials = await db.Set<CourseMaterial>()
                    .Where(cm => cm.CourseId == request.CourseId)
                    .ToListAsync(ct);
                var dtoList = materials.Select(m => new CourseMaterialDto(
                    m.MaterialId,
                    m.CourseId,
                    m.FileType,
                    m.FIleName,
                    m.FileExtension,
                    m.MaterialsLink,
                    m.UploadDate
                )).ToList();

                return new GetAllMaterialResponse(true, "Materials retrieved successfully.", dtoList);
            }
            catch (Exception ex)
            {
                return new GetAllMaterialResponse(false, $"Error retrieving materials: {ex.Message}");
            }
        }
    }
}
