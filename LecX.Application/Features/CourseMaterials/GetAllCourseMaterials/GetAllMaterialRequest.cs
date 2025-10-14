using MediatR;

namespace LecX.Application.Features.CourseMaterials.GetAllCourseMaterials
{
    public sealed record GetAllMaterialRequest(int CourseId) : IRequest<GetAllMaterialResponse> ;
}
