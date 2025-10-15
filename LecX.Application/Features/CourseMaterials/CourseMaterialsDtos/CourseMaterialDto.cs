using LecX.Domain.Enums;

namespace LecX.Application.Features.CourseMaterials.CourseMaterialsDtos
{
    public sealed record CourseMaterialDto(
        int MaterialId,
        int CourseId,
        FileType FileType,
        string FIleName,
        string FileExtension,
        string MaterialsLink,
        DateTime UploadDate
        );
}
