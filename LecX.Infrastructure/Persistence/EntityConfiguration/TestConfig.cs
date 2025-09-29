using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class TestConfig : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> b)
        {
            b.ToTable("Tests");
            b.HasKey(x => x.TestId);

            b.Property(x => x.TestTime).HasConversion<long>(); // TimeSpan -> ticks
            b.Property(x => x.PassingScore).HasColumnType("double");
            b.Property(x => x.AlowRedo).HasColumnType("varchar(50)");
            b.Property(x => x.NumberOfMaxAttempt);

            b.HasOne(x => x.Course)
             .WithMany(c => c.Tests)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
