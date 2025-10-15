using LecX.Application.Abstractions;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.CourseMaterials.CreateCourseMaterials
{
    public class CreateMaterialHandler(IAppDbContext db) : IRequestHandler<CreateMaterialRequest, CreateMaterialResponse>
    {
        public async Task<CreateMaterialResponse> Handle(CreateMaterialRequest req, CancellationToken ct)
        {
            try
            {
                var entity = new CourseMaterial
                {
                    CourseId = req.CourseId,
                    FileType = req.FileType,
                    FIleName = req.FileName,
                    FileExtension = req.FileExtension,
                    MaterialsLink = req.MaterialsLink,
                    UploadDate = DateTime.UtcNow
                };
                await db.Set<CourseMaterial>().AddAsync(entity, ct);
                await db.SaveChangesAsync(ct);
                var dto = new CourseMaterialDto(
                    entity.MaterialId,
                    entity.CourseId,
                    entity.FileType,
                    entity.FIleName,
                    entity.FileExtension,
                    entity.MaterialsLink,
                    entity.UploadDate
                    );
                return new CreateMaterialResponse(true, "Material created successfully.", dto);
            }
            catch (Exception ex)
            {
                return new CreateMaterialResponse(false, $"Error creating material: {ex.Message}", null);
            }
        }
    }
}

