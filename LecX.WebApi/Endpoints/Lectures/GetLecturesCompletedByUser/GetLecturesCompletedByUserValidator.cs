using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Lectures.GetLecturesCompletedByUser;

namespace LecX.WebApi.Endpoints.Lectures.GetLecturesCompletedByUser
{
    public class GetLecturesCompletedByUserValidator : Validator<GetLectureCompletedByUserRequest>
    {
        public GetLecturesCompletedByUserValidator() 
        {
            RuleFor(x => x.CourseId).NotEmpty().GreaterThan(0).WithMessage("CourseId must be greater than 0");
            RuleFor(x => x.PageIndex).GreaterThan(0).WithMessage("PageIndex must be greater than 0");
            RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }
}
