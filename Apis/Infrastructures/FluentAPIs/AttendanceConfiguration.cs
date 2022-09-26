using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class AttendanceConfiguration : BaseEntityConfiguration<Attendance>
    {
        public override void Configure(EntityTypeBuilder<Attendance> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.FresherId).IsRequired();
            builder.Property(x => x.Status1).IsRequired();
            builder.Property(x => x.Status2).IsRequired();
            builder.Property(x => x.Note).HasMaxLength(200);
        }
    }
}
