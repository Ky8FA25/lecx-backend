using LecX.Domain.Entities;
using LecX.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Persistence.Configurations
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> b)
        {
            b.ToTable("Courses");
            b.HasKey(x => x.CourseId);

            // ===== Columns =====
            b.Property(x => x.Title)
             .IsRequired()
             .HasMaxLength(255)
             .HasColumnType("varchar(255)");

            b.Property(x => x.CourseCode)
             .IsRequired()
             .HasMaxLength(20)
             .HasColumnType("varchar(20)");

            b.Property(x => x.Description)
             .HasMaxLength(255)
             .HasColumnType("varchar(255)");

            b.Property(x => x.CoverImagePath)
             .HasMaxLength(512)
             .HasColumnType("varchar(512)");

            b.Property(x => x.InstructorId)
             .IsRequired()
             .HasColumnType("varchar(255)");

            b.Property(x => x.NumberOfStudents)
             .HasDefaultValue(0);

            b.Property(x => x.CategoryId)
             .IsRequired();

            b.Property(x => x.IsBaned) // bool -> tinyint(1)
             .HasColumnType("tinyint(1)")
             .HasDefaultValue(false);

            b.Property(x => x.Rating)
             .HasColumnType("double");

            b.Property(x => x.NumberOfRate)
             .HasDefaultValue(0);

            // ===== Relationships =====
            b.HasOne(x => x.Category)
             .WithMany(c => c.Courses)
             .HasForeignKey(x => x.CategoryId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(x => x.Instructor)
             .WithMany() 
             .HasForeignKey(x => x.InstructorId)
             .OnDelete(DeleteBehavior.Restrict);

            // ===== Indexes / Constraints =====
            b.HasIndex(x => x.CourseCode).IsUnique();             
            b.HasIndex(x => new { x.InstructorId, x.Title });     
            b.HasIndex(x => new { x.CategoryId, x.Status, x.Level });
        }
    }
}
