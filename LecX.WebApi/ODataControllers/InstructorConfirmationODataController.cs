using AutoMapper;
using LecX.Application.Common.Dtos;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class InstructorConfirmationODataController : AbstractODataController<InstructorConfirmation, InstructorConfirmationODataDto>
    {
        public InstructorConfirmationODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<InstructorConfirmation> Query()
        {
            return _db.Set<InstructorConfirmation>()
                .Include(r => r.User);
        }
    }
}