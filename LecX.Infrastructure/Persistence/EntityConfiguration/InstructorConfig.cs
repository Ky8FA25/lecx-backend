using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class InstructorConfig : IEntityTypeConfiguration<Instructor>
    {
        public void Configure(EntityTypeBuilder<Instructor> b)
        {
            b.ToTable("Instructors");
            b.HasKey(x => x.InstructorId);
            b.Property(x => x.Bio).HasColumnType("longtext");

            b.HasOne(x => x.User)
             .WithOne()
             .HasForeignKey<Instructor>(x => x.InstructorId)
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
