using AutoMapper;
using LecX.Application.Abstractions;
using MediatR;
using LecX.Application.Features.Submissions.SubmissionDtos;
using LecX.Domain.Entities;
using LecX.Application.Abstractions.Persistence;

namespace LecX.Application.Features.Submissions.CreateSubmission
{
    public sealed class CreateSubmissionHandler(IAppDbContext db , IMapper mapper): IRequestHandler<CreateSubmissionRequest, CreateSubmissionResponse>
    {
        public async Task<CreateSubmissionResponse> Handle(CreateSubmissionRequest req, CancellationToken ct)
        {
            var submission = mapper.Map<Submission>(req);
            await db.Set<Submission>().AddAsync(submission, ct);
            try
            {
                var affected = await db.SaveChangesAsync(ct);
                return affected > 0
                    ? new()
                    {
                        Submission = mapper.Map<SubmissionDto>(submission),
                        Success = true,
                        Message = "Success"
                    }
                    : new() { Message = "Failed" };
            }
            catch (Exception ex)
            {
                return new() { Message = ex.Message };
            }
        }
    }
}
