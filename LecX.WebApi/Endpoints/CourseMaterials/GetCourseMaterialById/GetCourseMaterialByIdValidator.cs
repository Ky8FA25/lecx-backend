using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.CourseMaterials.GetCourseMaterialById;

namespace LecX.WebApi.Endpoints.CourseMaterials.GetCourseMaterialById
{
    public sealed class GetCourseMaterialByIdValidator : Validator<GetMaterialByIdRequest>
    {
        public GetCourseMaterialByIdValidator() 
        {
            RuleFor(x => x.MaterialId).GreaterThan(0);
        }
    }
}
