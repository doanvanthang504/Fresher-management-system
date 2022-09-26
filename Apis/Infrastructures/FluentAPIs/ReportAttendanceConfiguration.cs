using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ReportAttendanceConfiguration : BaseEntityConfiguration<ReportAttendance>
    {
        public override void Configure(EntityTypeBuilder<ReportAttendance> builder)
        {
            base.Configure(builder);
            builder.ToTable("Report.Attendance");

            builder.Property(x => x.FresherId).IsRequired();
        }
    }
}
