using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructures.FluentAPIs
{
    public class FresherReportConfiguration : BaseEntityConfiguration<FresherReport>
    {
        public override void Configure(EntityTypeBuilder<FresherReport> builder)
        {
            base.Configure(builder);
            builder.Property(x => x.Account)
                   .IsRequired()
                   .HasMaxLength(20);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.CourseCode)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(x => x.CourseName)
                   .IsRequired()
                   .HasMaxLength(150);
            builder.Property(x => x.Status)
                   .HasDefaultValue(StatusFresherEnum.Active);
            builder.Property(x => x.CompletionLevel)
                   .HasDefaultValue(ReportCompletionLevelEnum.B);
            builder.Property(x => x.CourseStatus)
                   .HasDefaultValue(ReportStatusEnum.Active);
            builder.Property(x => x.IsMonthly)
                   .HasDefaultValue(false);
            builder.Property(x => x.UniversityId)
                   .HasMaxLength(20);
            builder.Property(x => x.UniversityName)
                   .HasMaxLength(100);
            builder.Property(x => x.Branch)
                   .HasMaxLength(20);
            builder.Property(x => x.ParentDepartment)
                   .HasMaxLength(20);
            builder.Property(x => x.EducationLevel)
                   .HasMaxLength(50);
            builder.Property(x => x.EducationInfo)
                   .HasMaxLength(50);
            builder.Property(x => x.Major)
                   .HasMaxLength(20);
            builder.Property(x => x.Site)
                   .HasMaxLength(3);
            builder.Property(x => x.SubjectType)
                   .HasMaxLength(50);
            builder.Property(x => x.SubSubjectType)
                   .HasMaxLength(50);
            builder.Property(x => x.FormatType)
                   .HasMaxLength(10);
            builder.Property(x => x.Scope)
                   .HasMaxLength(50);
            builder.Property(x => x.StatusAllocated)
                   .HasMaxLength(20);
            builder.Property(x => x.FsuAllocated)
                   .HasMaxLength(10);
            builder.Property(x => x.BUAllocated)
                   .HasMaxLength(10);
            builder.Property(x => x.LanguageSkill)
                   .HasMaxLength(50);
            builder.Property(x => x.UpdatedBy)
                   .HasMaxLength(20);
            builder.Property(x => x.Note)
                   .HasMaxLength(255);
            builder.Property(x => x.EmployeeValid)
                   .HasMaxLength(50);
            builder.Property(x => x.CourseValidOrSubjectType)
                   .HasMaxLength(50);
            builder.Property(x => x.CourseValidOrSubsubjectType)
                   .HasMaxLength(50);
            builder.Property(x => x.CourseValidOrFormatType)
                   .HasMaxLength(10);
            builder.Property(x => x.CourseValidOrScope)
                   .HasMaxLength(50);
        }
    }
}
