using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.GetLecturesByCourse;

namespace LecX.WebApi.Endpoints.Lectures.GetLecturesByCourse
{
    public class GetLecturesByCourseValidator : Validator<GetLectureByCourseRequest>
    {
        public GetLecturesByCourseValidator() 
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("LectureId must be greater than 0.");
            RuleFor(x => x.PageIndex)
                .GreaterThan(0)
                .WithMessage("PageIndex must be greater than 0.");
            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .WithMessage("PageSize must be greater than 0.");
        }
    }
}
