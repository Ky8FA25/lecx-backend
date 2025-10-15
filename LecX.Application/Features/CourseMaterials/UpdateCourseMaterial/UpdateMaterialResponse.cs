using LecX.Application.Common.Dtos;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;

namespace LecX.Application.Features.CourseMaterials.UpdateCourseMaterial
{
    public sealed record UpdateMaterialResponse(bool Success, string Message  )
        : ResponseRecord<CourseMaterialDto>(Success,Message);
}
