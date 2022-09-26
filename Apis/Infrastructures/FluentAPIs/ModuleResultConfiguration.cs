using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ModuleResultConfiguration : BaseEntityConfiguration<ModuleResult>
    {
        public override void Configure(EntityTypeBuilder<ModuleResult> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FinalAuditScore).HasDefaultValue(0d);
            builder.Property(x => x.AssignmentAvgScore).HasDefaultValue(0d);
            builder.Property(x => x.FinalMark).HasDefaultValue(0d);
        }
    }
}
