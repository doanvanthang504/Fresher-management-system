using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Username)
                   .HasMaxLength(20)
                   .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email)
                   .HasMaxLength(50)
                    .IsRequired();

            builder.Property(x => x.HashedPassword)
                   .HasMaxLength(256)
                   .IsRequired();

            builder.Property(x => x.Role)
                   .HasDefaultValue(RoleEnum.Fresher)
                   .HasConversion<string>()
                   .HasColumnType("varchar(24)");
        }
    }
}
