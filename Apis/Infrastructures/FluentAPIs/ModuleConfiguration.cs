using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ModuleConfiguration : BaseEntityConfiguration<Module>
    {
        public override void Configure(EntityTypeBuilder<Module> builder)
        {
            base.Configure(builder);
            builder.ToTable("PlanConfiguration.Module");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ModuleName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.WeightedNumberQuizz).IsRequired();
            builder.Property(x => x.WeightedNumberAssignment).IsRequired();
            builder.Property(x => x.WeightedNumberFinal).IsRequired();
            builder.Property(x => x.WeightedNumberQuizz).IsRequired();
            builder.HasOne(x => x.Plan).WithMany(x => x.Modules).HasForeignKey(x => x.PlanId);
        }
    }
}
