using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Categories.CategoryDtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }
        public string FullName { get; set; }
        public string? Description { get; set; }
    }
}
