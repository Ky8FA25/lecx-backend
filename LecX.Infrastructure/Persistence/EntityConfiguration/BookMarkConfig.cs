using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class BookMarkConfig : IEntityTypeConfiguration<BookMark>
    {
        public void Configure(EntityTypeBuilder<BookMark> b)
        {
            b.ToTable("BookMarks");
            b.HasKey(x => x.BookmarkId);

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Course)
             .WithMany(c => c.BookMarks)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StudentId, x.CourseId }).IsUnique(); // mỗi SV bookmark 1 khoá học duy nhất
        }
    }
}
