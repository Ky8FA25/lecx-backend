using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.DeleteLecture;

namespace LecX.WebApi.Endpoints.Lectures.DeleteLecture
{
    public class DeleteLectureValidator : Validator<DeleteLectureRequest>
    {
        public DeleteLectureValidator() 
        {
            RuleFor(x => x.LectureId)
                .GreaterThan(0)
                .WithMessage("LectureId must be greater than 0.");
        }
    }
}
