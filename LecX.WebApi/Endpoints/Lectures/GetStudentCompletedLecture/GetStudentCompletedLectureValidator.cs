using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.GetStudentCompletedLecture;

namespace LecX.WebApi.Endpoints.Lectures.GetStudentCompletedLecture
{
    public class GetStudentCompletedLectureValidator : Validator<GetStudentCompletedLectureRequest>
    {
        public GetStudentCompletedLectureValidator()
        {
            RuleFor(x => x.LectureId)
                .NotEmpty().WithMessage("LectureId is required.");
        }
    }
}
