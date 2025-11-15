using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Comments.Common;
using LecX.Application.Features.StudentCourses.Common;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.StudentCourses.GetStudentCourseDetailById
{
    public sealed class GetStudentCourseDetailByIdHandler(IAppDbContext db, IMapper mapper) : IRequestHandler<GetStudentCourseDetailByIdRequest, GetStudentCourseDetailByIdResponse>
    {
        public async Task<GetStudentCourseDetailByIdResponse> Handle(GetStudentCourseDetailByIdRequest request, CancellationToken ct)
        {
            try
            {
                var studentCourse = await db.Set<StudentCourse>()
                    .Where(sc => sc.StudentCourseId == request.StudentCourseId)
                    .Include(sc => sc.Student).Include(sc => sc.Course)
                    .ProjectTo<StudentCourseDTO>(mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(ct);
                if (studentCourse == null)
                {
                    return new GetStudentCourseDetailByIdResponse
                    {
                        Success = false,
                        Message = "StudentCourseId not found"
                    };
                }
                return new GetStudentCourseDetailByIdResponse
                {
                    Success = true,
                    Data = studentCourse,
                    Message = "Retrived Student Course successfully."
                };
            }
            catch (Exception ex)
            {
                return new GetStudentCourseDetailByIdResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
