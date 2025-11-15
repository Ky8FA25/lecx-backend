using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Categories.CategoryDtos;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Categories.GetAllCategories
{
    public sealed class GetAllCategoriesHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetAllCategoriesRequest, GetAllCategoriesResponse>
    {
        public async Task<GetAllCategoriesResponse> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            var query = await db.Set<Category>()
                            .AsNoTracking()
                            .ToListAsync(cancellationToken);
            
            var categoryDtos = mapper.Map<List<CategoryDto>>(query);

            return new GetAllCategoriesResponse 
            {
                Categories = categoryDtos 
            };

        }
    }
}
