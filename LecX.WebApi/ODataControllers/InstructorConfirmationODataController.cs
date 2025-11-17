using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Dtos;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class InstructorConfirmationODataController : AbstractODataController<InstructorConfirmation, InstructorConfirmationODataDto>
    {
        public InstructorConfirmationODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<InstructorConfirmation> Query()
        {
            return _db.Set<InstructorConfirmation>()
                .Include(ic => ic.User)
                .Where(ic => !_db.Set<Instructor>()
                    .Any(i => i.User.Id == ic.UserId));
        }
    }
}