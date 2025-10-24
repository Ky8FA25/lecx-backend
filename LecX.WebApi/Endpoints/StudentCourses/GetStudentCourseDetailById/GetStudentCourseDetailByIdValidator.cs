using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.StudentCourses.GetStudentCourseDetailById;

namespace LecX.WebApi.Endpoints.StudentCourses.GetStudentCourseDetailById
{
    public class GetStudentCourseDetailByIdValidator : Validator<GetStudentCourseDetailByIdRequest>
    {
        public GetStudentCourseDetailByIdValidator()
        {
            RuleFor(x => x.StudentCourseId)
                .NotEmpty().WithMessage("StudentCourseId is required.");
        }
    }
}
