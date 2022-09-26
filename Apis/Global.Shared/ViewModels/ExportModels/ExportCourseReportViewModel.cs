using Domain.Enums;
using OfficeOpenXml.Attributes;
using System;
using System.ComponentModel;

namespace Global.Shared.ModelExport.ModelExportConfiguration
{
    public class ExportCourseReportViewModel
    {
        [EpplusIgnore]
        public Guid Id { get; set; }

        [Description("Empl. ID")]
        public string? EmployeeId { get; set; }

        [Description("National ID")]
        public string? NationalId { get; set; }
        
        public string Account { get; set; }
        
        public string Name { get; set; }


        [Description("Education Info")]
        public string? EducationInfo { get; set; }


        [Description("University ID")]
        public string? UniversityId { get; set; }


        [Description("University Name")]
        public string? UniversityName { get; set; }

        [Description("Faculty/Major")]
        public string? Major { get; set; }

        [Description("University graduation date")]
        public int? UniversityGraduationDate { get; set; }

        [Description("University GPA")]
        public double? UniversityGPA { get; set; }

        [Description("Education Level")]
        public string? EducationLevel { get; set; }
        
        public string? Branch { get; set; }

        [Description("Parent department")]
        public string? ParentDepartment { get; set; }

        public string? Site { get; set; }

        [Description("Course code")]
        public string CourseCode { get; set; }

        [Description("Course name")]
        public string CourseName { get; set; }

        [Description("Subject type")]
        public string? SubjectType { get; set; }

        [Description("Sub-subject type")]
        public string? SubSubjectType { get; set; }
        
        [Description("Format type")]
        public string? FormatType { get; set; }

        public string? Scope { get; set; }

        [Description("From date")]
        public DateOnly? FromDate { get; set; }

        [Description("To date")]
        public DateOnly? ToDate { get; set; }

        [Description("Learning time (hrs)")]
        public int? LearningTime { get; set; }

        public StatusFresherEnum Status { get; set; }

        [Description("Final grade")] 
        public string? FinalGrade { get; set; }

        [Description("Completion level")]
        public ReportCompletionLevelEnum CompletionLevel { get; set; }

        [Description("Status Allocated")]
        public string? StatusAllocated { get; set; }

        [Description("Salary Allocated")]
        public double? SalaryAllocated { get; set; }

        [Description("Fsu Allocated")]
        public string? FsuAllocated { get; set; }

        [Description("BU Allocated")]
        public string? BUAllocated { get; set; }

        [Description("Toeic grade")]
        public string? ToeicGrade { get; set; }

        [Description("Trình độ ngoại ngữ")]
        public string? LanguageSkill { get; set; }

        [Description("Updated by")]
        public string? UpdatedBy { get; set; }

        [Description("Updated date")]
        public DateTimeOffset? UpdatedDate { get; set; }

        public string? Note { get; set; }

        [Description("Employee valid")]
        public string? EmployeeValid { get; set; }

        [Description("Course status")]
        public StatusFresherEnum CourseStatus { get; set; }

        [Description("Course valid/ Subject type")]
        public string? CourseValidOrSubjectType { get; set; }

        [Description("Course valid/ Sub-subject type")]
        public string? CourseValidOrSubsubjectType { get; set; }

        [Description("Course valid/ Format type")]
        public string? CourseValidOrFormatType { get; set; }

        [Description("Course valid/ Scope")]
        public string? CourseValidOrScope { get; set; }

        [Description("Course valid/ Start date")]
        public DateOnly? CourseValidOrStartDate { get; set; }

        [Description("Course valid/ End date")]
        public DateOnly? CourseValidOrEndDate { get; set; }

        [Description("Course valid/ Learning time (hrs)")]
        public int? CourseValidOrLearningTime { get; set; }

        [Description("End Year")]
        public int? EndYear { get; set; }

        [Description("End Month")]
        public int? EndMonth { get; set; }
    }
}
