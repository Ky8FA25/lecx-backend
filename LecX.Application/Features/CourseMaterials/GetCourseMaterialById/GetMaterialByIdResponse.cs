

using LecX.Application.Common.Dtos;
using LecX.Application.Features.CourseMaterials.CourseMaterialsDtos;

namespace LecX.Application.Features.CourseMaterials.GetCourseMaterialById
{
    public sealed record GetMaterialByIdResponse(bool Success, string Message, CourseMaterialDto? Data = null  )
        : GenericResponseRecord<CourseMaterialDto>(Success,Message,Data);
}
