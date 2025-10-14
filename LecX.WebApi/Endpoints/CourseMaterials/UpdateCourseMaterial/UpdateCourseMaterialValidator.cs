using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.CourseMaterials.UpdateCourseMaterial;


namespace LecX.WebApi.Endpoints.CourseMaterials.UpdateCourseMaterial
{
    public sealed class UpdateCourseMaterialValidator : Validator<UpdateMaterialRequest>
    {
        public UpdateCourseMaterialValidator() 
        {
            RuleFor(x => x.MaterialId).NotEmpty().WithMessage("MaterialId is required.");
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
            RuleFor(x => x.FileName).MaximumLength(255).WithMessage("File name is required and must not exceed 255 characters.");
            RuleFor(x => x.FileType)
                .IsInEnum()
                .When(x => x.FileType.HasValue)
                .WithMessage("Invalid FileType value. Must be one of: Image-0, Video-1, Document-2, or Other-3.");
            RuleFor(x => x.MaterialsLink)
                .Must(url => string.IsNullOrWhiteSpace(url) || (
                    url.Length <= 2048 &&
                    Uri.TryCreate(url, UriKind.Absolute, out var uriResult) &&
                    (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)
                ))
                .WithMessage("If provided, MaterialsLink must be a valid URL and not exceed 2048 characters.");
        }
    }
}
