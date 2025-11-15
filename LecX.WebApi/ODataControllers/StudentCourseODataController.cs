using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class StudentCourseODataController : AbstractODataController<StudentCourse, StudentCourseODataDto>
    {
        public StudentCourseODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<StudentCourse> Query()
        {
            return _db.Set<StudentCourse>()
                .Include(sc => sc.Student)
                .Include(sc => sc.Course);
        }
    }
}
