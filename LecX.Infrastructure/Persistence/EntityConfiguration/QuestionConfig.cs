using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> b)
        {
            b.ToTable("Questions");
            b.HasKey(x => x.QuestionId);

            b.Property(x => x.QuestionContent).HasColumnType("longtext");
            b.Property(x => x.CorrectAnswer).HasColumnType("varchar(10)");
            b.Property(x => x.ImagePath).HasColumnType("varchar(512)");

            b.HasOne(x => x.Test)
             .WithMany(t => t.Questions)
             .HasForeignKey(x => x.TestId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
