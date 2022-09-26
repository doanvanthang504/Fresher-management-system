using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class PlanInformationConfiguration : BaseEntityConfiguration<PlanInformation>
    {
        public override void Configure(EntityTypeBuilder<PlanInformation> builder)
        {
            base.Configure(builder);
            builder.ToTable("PlanInformation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ModuleName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.TopicName).IsRequired().HasMaxLength(100);
        }
    }
}
