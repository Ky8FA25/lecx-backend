using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LecX.Infrastructure.Persistence.EntityConfiguration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            // Map vào bảng Identity mặc định
            b.ToTable("Users");

            // Primary Key
            b.HasKey(e => e.Id);

            b.Property(x => x.ProfileImagePath).HasColumnType("varchar(512)");
            b.Property(x => x.Address).HasColumnType("longtext");
            b.Property(x => x.Dob).HasColumnType("date");
            b.Property(x => x.WalletUser).HasColumnType("double");
        }
    }
}
