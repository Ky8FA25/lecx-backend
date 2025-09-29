using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class TestScoreConfig : IEntityTypeConfiguration<TestScore>
    {
        public void Configure(EntityTypeBuilder<TestScore> b)
        {
            b.ToTable("TestScores");
            b.HasKey(x => x.TestScoreId);

            b.Property(x => x.ScoreValue).HasColumnType("double");

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Test)
             .WithMany(t => t.TestScores)
             .HasForeignKey(x => x.TestId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasIndex(x => new { x.StudentId, x.TestId, x.NumberOfAttempt }).IsUnique();
        }
    }
}
