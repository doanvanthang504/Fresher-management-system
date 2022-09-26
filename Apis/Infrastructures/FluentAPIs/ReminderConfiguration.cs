using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ReminderConfiguration : BaseEntityConfiguration<Reminder>
    {
        public override void Configure(EntityTypeBuilder<Reminder> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Subject).IsRequired().HasMaxLength(100);
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.SentReminderTime).HasDefaultValue(0);
            builder.HasIndex(r => new { r.ReminderTime1, r.ReminderTime2 });
        }
    }
}
