using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Payment.GetPaymentByCourseId;

namespace LecX.WebApi.Endpoints.Payment.GetPaymentByCourseId
{
    public class GetPaymentByCourseIdValidator : Validator<GetPaymentByCourseIdRequest>
    {
        public GetPaymentByCourseIdValidator()
        {
            RuleFor(x => x.CourseId)
                .GreaterThan(0)
                .WithMessage("CourseId must be greater than 0.");
        }
    }
}

