using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionById
{
    public sealed record GetQuestionByIdRequest(
        int QuestionId
        ) : IRequest<GetQuestionByIdResponse>;
}