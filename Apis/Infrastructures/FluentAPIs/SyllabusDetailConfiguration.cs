using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class SyllabusDetailConfiguration : BaseEntityConfiguration<SyllabusDetail>
    {
        public override void Configure(EntityTypeBuilder<SyllabusDetail> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.ChapterName)
                   .IsRequired()
                   .HasMaxLength(256);
            builder.Property(x => x.ClassId).IsRequired();
        }
    }
}
