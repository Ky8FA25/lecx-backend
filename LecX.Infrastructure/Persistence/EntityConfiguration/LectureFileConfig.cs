using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class LectureFileConfig : IEntityTypeConfiguration<LectureFile>
    {
        public void Configure(EntityTypeBuilder<LectureFile> b)
        {
            b.ToTable("LectureFiles");
            b.HasKey(x => x.FileId);

            b.Property(x => x.FileExtension).HasColumnType("varchar(50)");
            b.Property(x => x.FilePath).HasColumnType("varchar(1024)");

            b.HasOne(x => x.Lecture)
             .WithMany(l => l.LectureFiles)
             .HasForeignKey(x => x.LectureId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
