using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class RefundConfig : IEntityTypeConfiguration<Refund>
    {
        public void Configure(EntityTypeBuilder<Refund> b)
        {
            b.ToTable("Refunds");
            b.HasKey(x => x.RefundId);

            b.Property(x => x.AccountNumber).HasColumnType("varchar(64)");
            b.Property(x => x.Status).HasColumnType("varchar(50)");

            b.HasOne(x => x.Student)
             .WithMany()
             .HasForeignKey(x => x.StudentId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
