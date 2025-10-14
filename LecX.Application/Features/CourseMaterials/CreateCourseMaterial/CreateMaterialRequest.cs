using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.CourseMaterials.CreateCourseMaterials
{
    public sealed record CreateMaterialRequest(
        int CourseId,
        FileType FileType,
        string FileName,
        string FileExtension,
        string MaterialsLink
        ) : IRequest<CreateMaterialResponse>;
}
