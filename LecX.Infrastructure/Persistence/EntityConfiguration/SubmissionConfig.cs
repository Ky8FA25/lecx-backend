using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class SubmissionConfig : IEntityTypeConfiguration<Submission>
    {
        public void Configure(EntityTypeBuilder<Submission> b)
        {
            b.ToTable("Submissions");
            b.HasKey(x => x.SubmissionId);

            b.Property(x => x.SubmissionLink).HasColumnType("varchar(1024)");

            b.HasOne(x => x.Assignment)
             .WithMany(a => a.Submissions)
             .HasForeignKey(x => x.AssignmentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
