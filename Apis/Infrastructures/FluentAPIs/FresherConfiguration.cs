using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    internal class FresherConfiguration : BaseEntityConfiguration<Fresher>
    {
        public override void Configure(EntityTypeBuilder<Fresher> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.AccountName).IsRequired().HasMaxLength(20);
            builder.Property(x => x.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.LastName).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Phone).HasMaxLength(11);
            builder.Property(x => x.DOB).IsRequired();
            builder.Property(x => x.GPA).IsRequired();
            builder.Property(x => x.Graduation).IsRequired();
            builder.Property(x => x.University).IsRequired();
            builder.Property(x => x.OnBoard).IsRequired();
            builder.Property(x => x.Salary).IsRequired();
            builder.Property(x => x.Major).IsRequired();
        }
    }
}
