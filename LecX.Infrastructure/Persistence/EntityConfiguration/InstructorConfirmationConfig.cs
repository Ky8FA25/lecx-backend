using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class InstructorConfirmationConfig : IEntityTypeConfiguration<InstructorConfirmation>
    {
        public void Configure(EntityTypeBuilder<InstructorConfirmation> b)
        {
            b.ToTable("InstructorConfirmations");
            b.HasKey(x => x.ConfirmationId);

            b.Property(x => x.Certificatelink).HasColumnType("varchar(1024)");
            b.Property(x => x.Description).HasColumnType("longtext");

            b.HasOne(x => x.User)
             .WithMany()
             .HasForeignKey(x => x.UserId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
