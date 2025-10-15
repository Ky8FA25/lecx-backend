using LecX.Application.Common.Dtos;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;

namespace LecX.Application.Features.CourseMaterials.CreateCourseMaterials
{
    public sealed record CreateMaterialResponse(
        bool Success,
        string Message,
        CourseMaterialDto? Data = null
    ) : ResponseRecord<CourseMaterialDto>(Success, Message, Data);
}
