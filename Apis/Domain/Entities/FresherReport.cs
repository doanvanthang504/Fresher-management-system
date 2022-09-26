using Domain.Enums;
using System;

namespace Domain.Entities
{
    public class FresherReport : BaseEntity
    {
        public string? EmployeeId { get; set; }

        public string? NationalId { get; set; }

        public string Account { get; set; }

        public string Name { get; set; }

        public string? EducationInfo { get; set; }

        public string? UniversityId { get; set; }

        public string? UniversityName { get; set; }

        public string? Major { get; set; }

        public int? UniversityGraduationDate { get; set; }

        public double? UniversityGPA { get; set; }

        public string? EducationLevel { get; set; }

        public string? Branch { get; set; }

        public string? ParentDepartment { get; set; }

        public string? Site { get; set; }

        public string CourseCode { get; set; }

        public string CourseName { get; set; }

        public string? SubjectType { get; set; }

        public string? SubSubjectType { get; set; }

        public string? FormatType { get; set; }

        public string? Scope { get; set; }

        public DateOnly FromDate { get; set; }

        public DateOnly? ToDate { get; set; }

        public int? LearningTime { get; set; }

        public StatusFresherEnum Status { get; set; }

        public string? FinalGrade { get; set; }

        public ReportCompletionLevelEnum CompletionLevel { get; set; }

        public string? StatusAllocated { get; set; }

        public double? SalaryAllocated { get; set; }

        public string? FsuAllocated { get; set; }

        public string? BUAllocated { get; set; }

        public string? ToeicGrade { get; set; }

        public string? LanguageSkill { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTimeOffset? UpdatedDate { get; set; }

        public string? Note { get; set; }

        public string? EmployeeValid { get; set; }

        public ReportStatusEnum CourseStatus { get; set; }

        public string? CourseValidOrSubjectType { get; set; }

        public string? CourseValidOrSubsubjectType { get; set; }

        public string? CourseValidOrFormatType { get; set; }

        public string? CourseValidOrScope { get; set; }

        public DateOnly? CourseValidOrStartDate { get; set; }

        public DateOnly? CourseValidOrEndDate { get; set; }

        public int? CourseValidOrLearningTime { get; set; }

        public int? EndYear { get; set; }

        public int? EndMonth { get; set; }

        public bool IsMonthly { get; set; }
    }
}
