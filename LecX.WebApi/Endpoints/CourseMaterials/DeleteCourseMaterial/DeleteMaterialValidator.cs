using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.CourseMaterials.DeleteCourseMaterial;

namespace LecX.WebApi.Endpoints.CourseMaterials.DeleteCourseMaterial
{
    public class DeleteMaterialValidator : Validator<DeleteMaterialRequest>
    {
        public DeleteMaterialValidator()
        {
            RuleFor(x => x.MaterialId).GreaterThan(0);
        }
    }
}
