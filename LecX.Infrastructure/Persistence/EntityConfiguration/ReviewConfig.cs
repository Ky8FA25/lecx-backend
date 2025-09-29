using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> b)
        {
            b.ToTable("Reviews");
            b.HasKey(x => x.ReviewId);

            b.Property(x => x.Rating).HasColumnType("double");
            b.Property(x => x.Comment).HasColumnType("longtext");

            b.HasOne(x => x.Course)
             .WithMany(c => c.Reviews)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();
        }
    }
}
