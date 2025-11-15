using AutoMapper;
using LecX.Application.Common.Dtos;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    public class PaymentODataController : AbstractODataController<Payment, PaymentODataDto>
    {
        public PaymentODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }

        protected override IQueryable<Payment> Query()
        {
            return _db.Set<Payment>()
                .Include(p => p.Student)
                .Include(p => p.Course);
        }
    }
}