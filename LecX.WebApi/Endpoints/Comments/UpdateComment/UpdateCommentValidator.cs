using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Comments.UpdateComment;

namespace LecX.WebApi.Endpoints.Comments.UpdateComment
{
    public class UpdateCommentValidator : Validator<UpdateCommentRequest>
    {
        public UpdateCommentValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .WithMessage("Comment not found");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.")
                .MaximumLength(1000).WithMessage("Content must not exceed 1000 characters.");
        }
    }
}
