using AutoMapper;
using LecX.Application.Common.Dtos;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class ReportODataController : AbstractODataController<Report, ReportODataDto>
    {
        public ReportODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<Report> Query()
        {
            return _db.Set<Report>()
                .Include(r => r.User);
        }
    }
}