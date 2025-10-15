using LecX.Application.Common.Dtos;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;
namespace LecX.Application.Features.CourseMaterials.DeleteCourseMaterial
{
    public sealed record DeleteMaterialResponse(
        bool Success,
        string Message
    ) : GenericResponseRecord<CourseMaterialDto>(Success, Message);
}
