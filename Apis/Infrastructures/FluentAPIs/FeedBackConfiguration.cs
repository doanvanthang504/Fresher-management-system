using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FeedBackConfiguration : BaseEntityConfiguration<Feedback>
    {
        public override void Configure(EntityTypeBuilder<Feedback> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Content).HasMaxLength(256);
            builder.Property(x => x.Description).HasMaxLength(128).IsRequired(false);
        }
    }
}
