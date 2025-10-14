using MediatR;

namespace LecX.Application.Features.CourseMaterials.DeleteCourseMaterial
{
    public sealed record DeleteMaterialRequest(int MaterialId) : IRequest<DeleteCourseResponse>;
}
