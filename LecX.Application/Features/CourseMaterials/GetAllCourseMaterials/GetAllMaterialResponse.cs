using LecX.Application.Common.Dtos;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;

namespace LecX.Application.Features.CourseMaterials.GetAllCourseMaterials
{
    public sealed record GetAllMaterialResponse(
        bool Success,
        string Message,
        List<CourseMaterialDto>? Data = null
    ) : ResponseRecord<List<CourseMaterialDto>>(Success, Message, Data);
}
