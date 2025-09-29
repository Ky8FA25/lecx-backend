using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class LectureCompletionConfig : IEntityTypeConfiguration<LectureCompletion>
    {
        public void Configure(EntityTypeBuilder<LectureCompletion> b)
        {
            b.ToTable("LectureCompletions");
            b.HasKey(x => x.CompletionId);

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Lecture)
             .WithMany(l => l.LectureCompletions)
             .HasForeignKey(x => x.LectureId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
