using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Tests.QuestionHandler.GetQuestionById
{
    public sealed class GetQuestionByIdHandler(IAppDbContext context, IMapper mapper) : IRequestHandler<GetQuestionByIdRequest, GetQuestionByIdResponse>
    {
        public async Task<GetQuestionByIdResponse> Handle(GetQuestionByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var question = await context.Set<Question>()
                .AsNoTracking()
                .FirstOrDefaultAsync(q => q.QuestionId == request.QuestionId, cancellationToken);
                if (question is null)
                {
                    return new GetQuestionByIdResponse
                    {
                        Success = false,
                        Message = "Question not found."
                    };
                }
                var questionDto = mapper.Map<QuestionDTO>(question);
                return new GetQuestionByIdResponse
                {
                    Data = questionDto,
                    Success = true,
                    Message = "Question retrieved successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetQuestionByIdResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
