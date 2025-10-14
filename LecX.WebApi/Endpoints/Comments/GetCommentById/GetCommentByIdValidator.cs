using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Comments.GetCommentById;

namespace LecX.WebApi.Endpoints.Comments.GetCommentById
{
    public sealed class GetCommentByIdValidator : Validator<GetCommentByIdRequest>
    {
        public GetCommentByIdValidator()
        {
            RuleFor(x => x.CommentId)
                .GreaterThan(0)
                .WithMessage("Comment not found");
        }
    }
}
