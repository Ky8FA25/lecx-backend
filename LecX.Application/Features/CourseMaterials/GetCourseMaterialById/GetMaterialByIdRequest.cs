using MediatR;

namespace LecX.Application.Features.CourseMaterials.GetCourseMaterialById
{
    public sealed record GetMaterialByIdRequest(int MaterialId) : IRequest<GetMaterialByIdResponse>;
}
