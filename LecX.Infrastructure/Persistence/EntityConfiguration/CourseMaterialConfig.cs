using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class CourseMaterialConfig : IEntityTypeConfiguration<CourseMaterial>
    {
        public void Configure(EntityTypeBuilder<CourseMaterial> b)
        {
            b.ToTable("CourseMaterials");
            b.HasKey(x => x.MaterialId);

            b.Property(x => x.FileExtension).HasColumnType("varchar(50)");
            b.Property(x => x.MaterialsLink).HasColumnType("varchar(1024)");

            b.HasOne(x => x.Course)
             .WithMany(c => c.CourseMaterials)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
