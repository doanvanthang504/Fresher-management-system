using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.FluentAPIs
{
    public class FeedbackAnswerConfiguration : IEntityTypeConfiguration<FeedbackAnswer>
    {
        public void Configure(EntityTypeBuilder<FeedbackAnswer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasQueryFilter(x => !x.IsDeleted);

            builder.Property(x => x.Content).HasMaxLength(500);
            builder.HasOne(x => x.Question).WithMany(y => y.FeedbackAnswers).HasForeignKey(x => x.QuestionId);

        }
    }
}
