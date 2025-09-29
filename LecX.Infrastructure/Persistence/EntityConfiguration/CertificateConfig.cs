using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class CertificateConfig : IEntityTypeConfiguration<Certificate>
    {
        public void Configure(EntityTypeBuilder<Certificate> b)
        {
            b.ToTable("Certificates");
            b.HasKey(x => x.CertificateId);

            b.Property(x => x.CertificateLink).HasColumnType("varchar(1024)");

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Course)
             .WithMany(c => c.Certificates)
             .HasForeignKey(x => x.CourseId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
