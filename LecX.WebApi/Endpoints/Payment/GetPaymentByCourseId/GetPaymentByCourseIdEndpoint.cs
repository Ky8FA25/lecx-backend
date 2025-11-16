using FastEndpoints;
using LecX.Application.Features.Payment.GetPaymentByCourseId;
using MediatR;

namespace LecX.WebApi.Endpoints.Payment.GetPaymentByCourseId
{
    public class GetPaymentByCourseIdEndpoint(ISender sender)
    : Endpoint<GetPaymentByCourseIdRequest, GetPaymentByCourseIdResponse>
    {
        public override void Configure()
        {
            Get("/api/payments/course/{CourseId}");
            Summary(s => s.Summary = "Get paginated payments by course ID");
            Description(d => d.WithTags("Payments"));
            Roles("Instructor", "Admin");
        }

        public override async Task HandleAsync(GetPaymentByCourseIdRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);
            await SendOkAsync(result, ct);
        }
    }
}

