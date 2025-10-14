using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Comments.DeleteComment;

namespace LecX.WebApi.Endpoints.Comments.GetCommentById
{
    public sealed class DeleteCommentValidator : Validator<DeleteCommentRequest>
    {
        public DeleteCommentValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .WithMessage("Comment not found");
        }
    }
}
