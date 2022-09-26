using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class PlanConfiguration : BaseEntityConfiguration<Plan>
    {
        public override void Configure(EntityTypeBuilder<Plan> builder)
        {
            base.Configure(builder);
            builder.ToTable("PlanConfiguration.Plan");
            builder.HasKey(p => p.Id);
            builder.Property(x => x.CourseName).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.CourseName).IsUnique();
            builder.Property(x => x.CourseCode).IsRequired().HasMaxLength(50);
            builder.HasIndex(x => x.CourseCode).IsUnique();
        }
    }
}
