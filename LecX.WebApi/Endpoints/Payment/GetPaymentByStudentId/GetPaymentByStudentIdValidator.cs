using FastEndpoints;
using FluentValidation;
using LecX.Application.Features.Payment.GetPaymentByStudentId;

namespace LecX.WebApi.Endpoints.Payment.GetPaymentByStudentId
{
    public class GetPaymentByStudentIdValidator : Validator<GetPaymentByStudentIdRequest>
    {
        public GetPaymentByStudentIdValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("StudentId is required.");
        }
    }
}
