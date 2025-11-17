using AutoMapper;
using AutoMapper.QueryableExtensions;
using LecX.Application.Abstractions.Persistence;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace LecX.WebApi.ODataControllers
{
    [Authorize]
    public abstract class AbstractODataController<TEntity, TDto> : ODataController
        where TEntity : class
        where TDto : class
    {
        protected readonly IMapper _mapper;
        protected readonly IAppDbContext _db;

        protected AbstractODataController(IMapper mapper, IAppDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        /// <summary>
        /// Cho phép override nếu cần include
        /// </summary>
        protected virtual IQueryable<TEntity> Query()
            => _db.Set<TEntity>();

        //[ApiExplorerSettings(IgnoreApi = true)]
        [EnableQuery(PageSize = 50, MaxExpansionDepth = 2)]
        public virtual async Task<List<TDto>> Get()
            => await Query()
                .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
    }
}