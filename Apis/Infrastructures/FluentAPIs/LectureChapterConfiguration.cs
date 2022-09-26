using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class LectureChapterConfiguration : BaseEntityConfiguration<LectureChapter>
    {
        public override void Configure(EntityTypeBuilder<LectureChapter> builder)
        {
            base.Configure(builder);
            builder.ToTable("SyllabusConfiguration.LectureChapter");

            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.DeliveryType).IsRequired();
            builder.Property(x => x.Duration).IsRequired();
            builder.HasOne(x => x.ChapterSyllabus)
                   .WithMany(x => x.LectureChapters)
                   .HasForeignKey(x => x.ChapterSyllabusId);
        }
    }
}
