using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Tests.Common;
using LecX.Domain.Entities;
using MediatR;

namespace LecX.Application.Features.Tests.QuestionHandler.UpdateQuestion
{
    public sealed class UpdateQuestionHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<UpdateQuestionRequest, UpdateQuestionResponse>
    {
        public async Task<UpdateQuestionResponse> Handle(
            UpdateQuestionRequest request,
            CancellationToken cancellationToken)
        {
            try
            {
                var question = await db.Set<Question>()
                .FindAsync([request.QuestionId], cancellationToken);
                if (question is null)
                {
                    return new UpdateQuestionResponse
                    {
                        Success = false,
                        Message = "Question not found."
                    };
                }
                if (request.QuestionContent is not null)
                {
                    question.QuestionContent = request.QuestionContent;
                }
                if (request.AnswerA is not null)
                {
                    question.AnswerA = request.AnswerA;
                }
                if (request.AnswerB is not null)
                {
                    question.AnswerB = request.AnswerB;
                }
                if (request.AnswerC is not null)
                {
                    question.AnswerC = request.AnswerC;
                }
                if (request.AnswerD is not null)
                {
                    question.AnswerD = request.AnswerD;
                }
                if (request.CorrectAnswer is not null)
                {
                    question.CorrectAnswer = request.CorrectAnswer;
                }
                if (request.ImagePath is not null)
                {
                    question.ImagePath = request.ImagePath;
                }
                db.Set<Question>().Update(question);
                await db.SaveChangesAsync(cancellationToken);
                var questionDto = mapper.Map<QuestionDTO>(question);
                return new UpdateQuestionResponse
                {
                    Success = true,
                    Data = questionDto,
                    Message = "Question updated successfully."
                };
            } catch (Exception ex)
            {
                return new UpdateQuestionResponse
                {
                    Success = false,
                    Message = $"An error occurred while updating the question: {ex.Message}"
                };
            }
        }
    }
}
