using Domain.Entities;
using Infrastructures.ValueConverters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Chemical> Chemicals { get; set; }

        public DbSet<Reminder> Reminders { get; set; }

        public DbSet<Plan> Plans { get; set; }

        public DbSet<PlanInformation> PlanInformations { get; set; }

        public DbSet<Topic> Topics { get; set; }

        public DbSet<Module> Modules { get; set; }

        public DbSet<ClassFresher> ClassFreshers { get; set; }

        public DbSet<Fresher> Freshers { get; set; }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<ReportAttendance> ReportAttendances { get; set; }

        public DbSet<AuditResult> Audits { get; set; }

        public DbSet<Auditor> Auditors { get; set; }

        public DbSet<FresherReport> FresherReports { get; set; }

        public DbSet<Score> Scores { get; set; }

        public DbSet<QuestionManagement> QuestionManagements { get; set; }

        public DbSet<ChapterSyllabus> ChapterSyllabuses { get; set; }

        public DbSet<LectureChapter> LectureChapters { get; set; }

        public DbSet<SyllabusDetail> SyllabusDetails { get; set; }

        public DbSet<ModuleResult> ModuleResults { get; set; }

        public DbSet<MonthResult> MonthsResults { get; set; }

        public DbSet<Feedback> Feedbacks { get; set; }

        public DbSet<FeedbackQuestion> FeedbackQuestions { get; set; }
        
        public DbSet<FeedbackResult> FeedbackResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            configurationBuilder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
        }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}
