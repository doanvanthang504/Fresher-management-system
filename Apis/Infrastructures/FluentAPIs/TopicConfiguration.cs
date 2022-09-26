using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructures.FluentAPIs
{
    public class TopicConfiguration : BaseEntityConfiguration<Topic>
    {
        public override void Configure(EntityTypeBuilder<Topic> builder)
        {
            base.Configure(builder);
            builder.ToTable("PlanConfiguration.Topic");
            builder.HasKey(t => t.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasOne(x => x.Module).WithMany(x => x.Topics).HasForeignKey(x => x.ModuleId);
        }
    }
}
