using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FeedBackResultConfiguration : BaseEntityConfiguration<FeedbackResult>
    {
        public override void Configure(EntityTypeBuilder<FeedbackResult> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Fullname).HasMaxLength(60);
            builder.Property(x => x.Content).HasMaxLength(256);
            builder.Property(x => x.Note).HasMaxLength(128).IsRequired(false);

            builder.HasOne(x => x.Feedback)
                   .WithMany(y => y.FeedbackResults)
                   .HasForeignKey(x => x.FeedbackId);

            builder.HasOne(x => x.FeedbackQuestion)
                   .WithMany(y => y.FeedbackResults)
                   .HasForeignKey(x => x.QuestionId);
        }
    }
}
