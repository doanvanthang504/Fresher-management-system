using Application;
using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Global.Shared.Helpers;
using Infrastructures.Mappers;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructures
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructuresService(
            this IServiceCollection services,
            IConfiguration configuration,
            IWebHostEnvironment environment)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IScoreService, ScoreService>();
            services.AddScoped<IScoreRepository, ScoreRepository>();
            services.AddScoped<IModuleResultService, ModuleResultService>();
            services.AddScoped<IModuleResultRepository, ModuleResultRepository>();
            services.AddScoped<IAttendanceRepository, AttendanceRepository>();
            services.AddScoped<IReportAttendanceRepository, ReportAttendanceRepository>();
            services.AddScoped<IChemicalService, ChemicalService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IMeetingRequestService, MeetingRequestService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IChemicalRepository, ChemicalRepository>();
            services.AddScoped<IFresherReportService, FresherReportService>();
            services.AddScoped<IFresherReportRepository, FresherReportRepository>();
            services.AddScoped<IReminderRepository, ReminderRepository>();
            services.AddScoped<IFresherReportService, FresherReportService>();
            services.AddScoped<IFresherReportRepository, FresherReportRepository>();
            services.AddScoped<ICronJobService, CronJobService>();
            services.AddScoped<IHangfireService, HangfireService>();
            services.AddScoped<IFresherService, FresherService>();
            services.AddScoped<IClassFresherService, ClassFresherService>();
            services.AddScoped<IClassFresherRepository, ClassFresherRepository>();
            services.AddScoped<IFresherRepository, FresherRepository>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<IAttendanceTokenService, AttendanceTokenService>();
            services.AddScoped<IReportAttendanceService, ReportAttendanceService>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackQuestionRepository, FeedbackQuestionRepository>();
            services.AddScoped<IFeedbackResultRepository, FeedbackResultRepository>();
            services.AddScoped<IFeedbackAnswerRepository, FeedbackAnswerRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IAuditorRepository, AuditorRepository>();
            services.AddScoped<IAuditorService, AuditorService>();
            services.AddScoped<IAuditManagementService, AuditManagementService>();
            services.AddScoped<IAuditManagementRepository, AuditManagementRepository>();
            services.AddScoped<IQuestionManagementService, QuestionManagementService>();
            services.AddScoped<IQuestionManagementRepository, QuestionManagementRepository>();
            services.AddSingleton<ICurrentTime, CurrentTime>();
            services.AddScoped<IMailTemplateManager, MailTemplateManager>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IPlanRepository, PlanRepository>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<IPlanService, PlanService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<IPlanInformationRepository, PlanInformationRepository>();
            services.AddScoped<IPlanInformationService, PlanInformationService>();
            services.AddScoped<IChapterSyllabusRepository, ChapterSyllabusRepository>();
            services.AddScoped<IChapterSyllabusService, ChapterSyllabusService>();
            services.AddScoped<ILectureChapterRepository, LectureChapterRepository>();
            services.AddScoped<ILectureChapterService, LectureChapterService>();
            services.AddScoped<ISyllabusDetailRepository, SyllabusDetailRepository>();
            services.AddScoped<ISyllabusDetailService, SyllabusDetailService>();
            services.AddScoped<IMonthResultService, MonthResultService>();
            if (environment.IsDevelopment())
            {
                services.AddDbContext<AppDbContext>(
                    option => option.UseInMemoryDatabase("test"));
            }
            else
            {
                var connectionString = configuration.GetRequiredSection("CONNECTION_STRING").Value;
                services.AddDbContext<AppDbContext>(
                    option => option.UseSqlServer(connectionString));
            }
            services.AddAutoMapper(typeof(MapperConfigurationsProfile).Assembly);
            services.AddSingleton<IOAuth2AccessTokenAcquirer, MicrosoftAccessTokenAcquirer>();
            services.AddScoped<IReminderService, ReminderService>();
            return services;
        }
    }
}
