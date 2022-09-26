using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ClassFresherConfiguration : BaseEntityConfiguration<ClassFresher>
    {
        public override void Configure(EntityTypeBuilder<ClassFresher> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
            builder.Property(x => x.Location).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Budget).IsRequired();
            builder.Property(x => x.ClassCode).HasMaxLength(100);      
            builder.Property(x => x.ClassCode).IsUnicode(true);
            builder.HasMany(x => x.Freshers).WithOne(x => x.ClassFresher).HasForeignKey(x => x.ClassFresherId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
