using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.CourseMaterials.CreateCourseMaterials;

namespace LecX.WebApi.Endpoints.CourseMaterials.CreateCourseMaterial
{
    public sealed class CreateMaterialValidator : Validator<CreateMaterialRequest>  
    {
        public CreateMaterialValidator()
        {
            RuleFor(x => x.CourseId).NotEmpty().WithMessage("CourseId is required.");
            RuleFor(x => x.FileName).NotEmpty().MaximumLength(255).WithMessage("File name is required and must not exceed 255 characters.");
            RuleFor(x => x.MaterialsLink).NotEmpty().MaximumLength(2048).WithMessage("Material link is required.");
            RuleFor(x => x.FileType)
                .NotEmpty()
                .IsInEnum()
                .WithMessage("Invalid FileType value. Must be one of: Image-0, Video-1, Document-2, or Other-3.");
            RuleFor(x => x.MaterialsLink)
                .NotEmpty()
                .MaximumLength(2048)
                .WithMessage("Materials link is required and must not exceed 2048 characters.")
                .Must(BeAValidUrl)
                .WithMessage("MaterialsLink must be a valid URL.");
        }
        private bool BeAValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
