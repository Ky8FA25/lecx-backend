using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class StudentCourseConfig : IEntityTypeConfiguration<StudentCourse>
    {
        public void Configure(EntityTypeBuilder<StudentCourse> b)
        {
            b.ToTable("StudentCourses");
            b.HasKey(x => x.StudentCourseId);

            b.Property(x => x.Progress).HasColumnType("decimal(5,2)");

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Course)
             .WithMany(c => c.StudentCourses)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique();
        }
    }
}
