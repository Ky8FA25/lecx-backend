using AutoMapper;
using LecX.Application.Common.Dtos;
using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;

namespace LecX.WebApi.ODataControllers
{
    public class CategoryODataController : AbstractODataController<Category, CategoryODataDto>
    {
        public CategoryODataController(IMapper mapper, IAppDbContext db) : base(mapper, db) { }
    }
}