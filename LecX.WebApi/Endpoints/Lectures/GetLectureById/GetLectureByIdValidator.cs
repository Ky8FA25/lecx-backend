using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.GetLectureById;

namespace LecX.WebApi.Endpoints.Lectures.GetLectureById
{
    public class GetLecturesByIdValidator : Validator<GetLectureByIdRequest>
    {
        public GetLecturesByIdValidator() 
        {
            RuleFor(x => x.LectureId)
                .GreaterThan(0)
                .WithMessage("LectureId must be greater than 0.");
        }
    }
}
