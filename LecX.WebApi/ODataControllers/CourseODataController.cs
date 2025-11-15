using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class CourseODataController : AbstractODataController<Course, CourseODataDto>
    {
        public CourseODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<Course> Query()
        {
            return _db.Set<Course>()
                .Include(c => c.Category)
                .Include(c => c.Instructor)
                    .ThenInclude(i => i.User);
        }
    }
}
