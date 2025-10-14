using LecX.Application.Abstractions;
using LecX.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.CourseMaterials.DeleteCourseMaterial
{
    public sealed class DeleteMaterialHandler(IAppDbContext db) : IRequestHandler<DeleteMaterialRequest, DeleteCourseResponse>
    {
        public async Task<DeleteCourseResponse> Handle(DeleteMaterialRequest request, CancellationToken ct)
        {
            try
            {
                var material = await db.Set<CourseMaterial>().FindAsync([request.MaterialId], ct);
                if (material == null)
                {
                    return new DeleteCourseResponse(false, "Material not found.");
                }
                db.Set<CourseMaterial>().Remove(material);
                await db.SaveChangesAsync(ct);
                return new DeleteCourseResponse(true, "Material deleted successfully.");
            }
            catch (Exception ex)
            {
                return new DeleteCourseResponse(false, $"Error deleting material: {ex.Message}");
            }
        }
    }
}
