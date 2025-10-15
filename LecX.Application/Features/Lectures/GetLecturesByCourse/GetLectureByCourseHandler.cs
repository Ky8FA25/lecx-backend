using LecX.Application.Abstractions;
using MediatR;

namespace LecX.Application.Features.Lectures.GetLecturesByCourse
{
    public sealed class GetLectureByCourseHandler(IAppDbContext db) : IRequestHandler<GetLectureByCourseRequest, GetLectureByCourseResponse>
    {
        public async Task<GetLectureByCourseResponse> Handle(GetLectureByCourseRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return null;
            } catch(Exception ex)
            {
                return ;
            }
        }
    }
}
