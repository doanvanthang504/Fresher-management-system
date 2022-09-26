using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class ChapterSyllabusConfiguration : BaseEntityConfiguration<ChapterSyllabus>
    {
        public override void Configure(EntityTypeBuilder<ChapterSyllabus> builder)
        {
            base.Configure(builder);
            builder.ToTable("SyllabusConfiguration.ChapterSyllabus");

            builder.Property(x => x.Name).IsRequired().HasMaxLength(256);
            builder.HasOne(x => x.Topic).WithMany(x => x.ChapterSyllabuses).HasForeignKey(x => x.TopicId);
            builder.Property(x => x.Duration).HasDefaultValue(0d);
        }
    }
}
