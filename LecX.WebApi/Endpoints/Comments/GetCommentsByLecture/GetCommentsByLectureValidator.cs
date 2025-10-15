using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Comments.GetCommentsByLecture;

namespace LecX.WebApi.Endpoints.Comments.GetCommentsByLecture
{
    public class GetCommentsByLectureValidator : Validator<GetCommentsByLectureRequest>
    {
        public GetCommentsByLectureValidator()
        {
            RuleFor(x => x.LectureId).GreaterThan(0);
            RuleFor(x => x.ParentCmtId)
                .GreaterThan(0)
                .When(x => x.ParentCmtId.HasValue);
        }
    }
}
