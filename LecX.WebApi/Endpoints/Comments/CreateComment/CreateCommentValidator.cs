using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Comments.CreateComment;

namespace LecX.WebApi.Endpoints.Comments.CreateComment
{
    public class CreateCommentValidator : Validator<CreateCommentRequest>
    {
        public CreateCommentValidator()
        {
            RuleFor(x => x.LectureId).GreaterThan(0);
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");
            RuleFor(x => x.ParentCmtId)
                .GreaterThan(0)
                .When(x => x.ParentCmtId.HasValue);
        }
    }
}
