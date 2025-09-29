using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class AssignmentScoreConfig : IEntityTypeConfiguration<AssignmentScore>
    {
        public void Configure(EntityTypeBuilder<AssignmentScore> b)
        {
            b.ToTable("AssignmentScores");
            b.HasKey(x => x.AssignmentScoreId);

            b.Property(x => x.Score).HasColumnType("double");

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Assignment)
             .WithMany(a => a.AssignmentScores)
             .HasForeignKey(x => x.AssignmentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StudentId, x.AssignmentId }).IsUnique();
        }
    }
}
