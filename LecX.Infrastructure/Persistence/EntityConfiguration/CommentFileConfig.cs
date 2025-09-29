using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class CommentFileConfig : IEntityTypeConfiguration<CommentFile>
    {
        public void Configure(EntityTypeBuilder<CommentFile> b)
        {
            b.ToTable("CommentFiles");
            b.HasKey(x => x.FileId);

            b.Property(x => x.FileName)
             .HasMaxLength(255)
             .HasColumnType("varchar(255)");

            b.Property(x => x.FilePath).HasColumnType("varchar(1024)");

            b.HasOne(x => x.Comment)
             .WithMany(c => c.CommentFiles)
             .HasForeignKey(x => x.CommentId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
