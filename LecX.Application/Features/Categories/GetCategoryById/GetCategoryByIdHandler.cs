using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Execption;
using LecX.Application.Features.Categories.GetAllCategories;
using LecX.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LecX.Application.Features.Categories.GetCategoryById
{
    public class GetCategoryByIdHandler(IAppDbContext db, IMapper mapper)
       : IRequestHandler<GetCategoryByIdRequest, GetCategoryByIdResponse>
    {
        public async Task<GetCategoryByIdResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            var category = await db.Set<Category>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CategoryId == request.CategoryId, cancellationToken);

            if (category is null)
            {
                throw new NotFoundException("Category not found!");
            }

            return mapper.Map<GetCategoryByIdResponse>(category);
        }
    }
}
