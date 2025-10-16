using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.CourseMaterials.GetCourseMaterialById
{
    public sealed class GetMaterialByIdHandler(IAppDbContext db) : IRequestHandler<GetMaterialByIdRequest, GetMaterialByIdResponse>
    {

        public async Task<GetMaterialByIdResponse> Handle(GetMaterialByIdRequest request, CancellationToken ct)
        {
            try
            {
                var material = await db.Set<CourseMaterial>()
                    .FirstOrDefaultAsync(cm => cm.MaterialId == request.MaterialId, ct);
                if (material == null)
                {
                    return new GetMaterialByIdResponse(false, "Can not find MaterialId");
                }
                var dto = new CourseMaterialDto(
                    material.MaterialId,
                    material.CourseId,
                    material.FileType,
                    material.FIleName,
                    material.FileExtension,
                    material.MaterialsLink,
                    material.UploadDate
                );

                return new GetMaterialByIdResponse(true, "Materials retrieved successfully.", dto);
            }
            catch (Exception ex)
            {
                return new GetMaterialByIdResponse(false, $"Error retrieving materials: {ex.Message}");
            }
        }
    }
}
