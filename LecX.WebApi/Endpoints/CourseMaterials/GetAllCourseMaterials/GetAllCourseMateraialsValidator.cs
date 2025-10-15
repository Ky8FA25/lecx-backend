using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.CourseMaterials.GetAllCourseMaterials;

namespace LecX.WebApi.Endpoints.CourseMaterials.GetAllCourseMaterials
{
    public sealed class GetAllCourseMateraialsValidator : Validator<GetAllMaterialRequest>
    {
        public GetAllCourseMateraialsValidator()
        {
            RuleFor(x => x.CourseId).GreaterThan(0).WithMessage("CourseId must be greater than 0.");
        }
    }
}
