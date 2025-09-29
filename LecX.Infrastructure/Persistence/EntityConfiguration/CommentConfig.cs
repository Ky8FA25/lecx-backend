using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> b)
        {
            b.ToTable("Comments");
            b.HasKey(x => x.CommentId);

            b.Property(x => x.Content).HasColumnType("longtext");

            b.HasOne(x => x.Lecture)
             .WithMany(l => l.Comments)
             .HasForeignKey(x => x.LectureId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.ParentComment)
             .WithMany()
             .HasForeignKey(x => x.ParentCmtId)
             .OnDelete(DeleteBehavior.NoAction); // tránh cascade cycle
        }
    }
}
