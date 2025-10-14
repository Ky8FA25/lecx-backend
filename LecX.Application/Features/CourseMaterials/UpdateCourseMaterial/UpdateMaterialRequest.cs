using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.CourseMaterials.UpdateCourseMaterial
{
    public sealed record UpdateMaterialRequest(
        int MaterialId,
        int? CourseId,
        FileType? FileType,
        string? FileName,
        string? FileExtension,
        string? MaterialsLink
        ) : IRequest<UpdateMaterialResponse>;
}
