using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class AssignmentConfig : IEntityTypeConfiguration<Assignment>
    {
        public void Configure(EntityTypeBuilder<Assignment> b)
        {
            b.ToTable("Assignments");
            b.HasKey(x => x.AssignmentId);

            b.Property(x => x.AssignmentLink)
             .HasColumnType("varchar(1024)");

            b.HasOne(x => x.Course)
             .WithMany(c => c.Assignments)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
