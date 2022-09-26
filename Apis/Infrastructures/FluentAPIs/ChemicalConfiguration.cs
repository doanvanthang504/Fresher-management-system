using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ChemicalConfiguration : BaseEntityConfiguration<Chemical>
    {
        public override void Configure(EntityTypeBuilder<Chemical> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ChemicalType).HasMaxLength(100);
        }
    }
}
