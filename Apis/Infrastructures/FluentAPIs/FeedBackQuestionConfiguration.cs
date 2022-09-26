using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FeedBackQuestionConfiguration : BaseEntityConfiguration<FeedbackQuestion>
    {
        public override void Configure(EntityTypeBuilder<FeedbackQuestion> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Title).HasMaxLength(128);
            builder.Property(x => x.Content).HasMaxLength(256);
            builder.Property(x => x.Description).HasMaxLength(128).IsRequired(false);
            builder.HasOne(x => x.Feedback).WithMany(y => y.FeedbackQuestions).HasForeignKey(x => x.FeedbackId).IsRequired(false);
        }
    }
}
