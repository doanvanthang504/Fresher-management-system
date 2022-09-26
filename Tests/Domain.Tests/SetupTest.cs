using Application;
using Application.Interfaces;
using Application.Repositories;
using AutoFixture;
using AutoMapper;
using Infrastructures;
using Infrastructures.Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;

namespace Domain.Tests
{
    public class SetupTest : IDisposable
    {
        protected readonly IMapper _mapperConfig;
        protected readonly Fixture _fixture;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<IChemicalService> _chemicalServiceMock;

        protected readonly Mock<IAuditManagementService> _auditManagementServiceMock;
        protected readonly Mock<IAuditManagementRepository> _auditManagementRepositoryMock;
        protected readonly Mock<IAuditorService> _auditorServiceMock;
        protected readonly Mock<IAuditorRepository> _auditorRepositoryMock;
        protected readonly Mock<IQuestionManagementRepository> _questionManagementRepositoryMock;
        protected readonly Mock<IQuestionManagementService> _questionManagementServiceMock;


        protected readonly Mock<IMailService> _mailServiceMock;

        protected readonly Mock<IUserRepository> _userRepositoryMock;
        protected readonly Mock<IChemicalRepository> _chemicalRepositoryMock;
        protected readonly Mock<IClaimsService> _claimsServiceMock;
        protected readonly Mock<ICurrentTime> _currentTimeMock;


        protected readonly Mock<IReminderRepository> _reminderRepositoryMock;
        protected readonly Mock<IReminderService> _reminderService;

        protected readonly Mock<IExcelExportHistoryService> _mockMEcelExportHistoryService;
        protected readonly Mock<IExcelExportDeliveryService> _mockMEcelExportDeliveryService;
        protected readonly Mock<IImportDataFromExcelFileService> _importDataServiceMock;
        protected readonly Mock<IExcelExportChartService> _mockMEcelExportChartService;
        protected readonly Mock<IExcelExportScroreService> _mockExcelExportScroreService;
        protected readonly Mock<IFresherRepository> _fresherRepositoryMock;
        protected readonly Mock<IFresherService> _fresherServiceMock;
        protected readonly Mock<IClassFresherRepository> _classFresherRepositoryMock;
        protected readonly Mock<IClassFresherService> _classFresherServiceMock;
        protected readonly Mock<IScoreRepository> _scoreRepositoryMock;
        protected readonly Mock<IAttendanceService> _attendanceServiceMock;
        protected readonly Mock<IAttendanceRepository> _attendanceRepositoryMock;
        protected readonly Mock<IAttendanceTokenService> _attendanceTokenServiceMock;
        protected readonly Mock<IReportAttendanceRepository> _reportAttendanceRepositoryMock;
        protected readonly Mock<IModuleResultRepository> _moduleResultRepositoryMock;
        protected readonly Mock<IModuleResultService> _moduleResultServiceMock;
        protected readonly IConfiguration _configuration;
        protected readonly Mock<IFeedbackRepository> _feedbackRepositoryMock;
        protected readonly Mock<IFeedbackQuestionRepository> _feedbackQuestionRepositoryMock;
        protected readonly Mock<IFeedbackResultRepository> _feedbackResultRepositoryMock;
        protected readonly Mock<IFeedbackAnswerRepository> _feedbackAnswerRepositoryMock;
        protected readonly Mock<IFeedbackService> _feedbackServiceMock;
        protected readonly AppDbContext _dbContext;
        protected readonly Mock<IFresherReportRepository> _fresherReportRepositoryMock;
        protected readonly Mock<IFresherReportService> _fresherReportServiceMock;
        protected readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;

        #region PlanProgram_Setup_Mock
        protected readonly Mock<ITopicService> _topicServiceMock;
        protected readonly Mock<ITopicRepository> _topicRepositoryMock;
        protected readonly Mock<IPlanRepository> _planRepositoryMock;
        protected readonly Mock<IPlanService> _planServiceMock;
        protected readonly Mock<IModuleRepository> _moduleRepositoryMock;
        protected readonly Mock<IModuleService> _moduleServiceMock;
        protected readonly Mock<IPlanInformationRepository> _planInforRepositoryMock;
        protected readonly Mock<IPlanInformationService> _planInformationServiceMock;
        protected readonly Mock<IChapterSyllabusRepository> _chapterSyllabusRepositoryMock;
        protected readonly Mock<IChapterSyllabusService> _chapterSyllabusServiceMock;
        protected readonly Mock<ILectureChapterRepository> _lectureChapterRepositoryMock;
        protected readonly Mock<ILectureChapterService> _lectureChapterServiceMock;
        protected readonly Mock<ISyllabusDetailRepository> _syllabusDetailRepositoryMock;
        #endregion


        public SetupTest()
        {
            _configuration = new ConfigurationBuilder()
                                        .SetBasePath(SetupWebAPIPath.GetBasePath())
                                        .AddJsonFile("testsettings.json")
                                        .Build();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperConfigurationsProfile());
                mc.AddProfile(new ClassFresherConfigurationsProfile());
                mc.AddProfile(new MailConfigurationsProfile());
                mc.AddProfile(new FresherConfigurationsProfile());
                mc.AddProfile(new AttendanceConfigurationProfile());
            });
            _mapperConfig = mappingConfig.CreateMapper();
            _fixture = new Fixture();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _chemicalServiceMock = new Mock<IChemicalService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _fresherRepositoryMock = new Mock<IFresherRepository>();
            _classFresherRepositoryMock = new Mock<IClassFresherRepository>();
            _classFresherServiceMock = new Mock<IClassFresherService>();
            _fresherServiceMock = new Mock<IFresherService>();
            _claimsServiceMock = new Mock<IClaimsService>();
            _currentTimeMock = new Mock<ICurrentTime>();
            #region Audit
            _auditManagementServiceMock = new Mock<IAuditManagementService>();
            _auditManagementRepositoryMock = new Mock<IAuditManagementRepository>();
            _questionManagementRepositoryMock = new Mock<IQuestionManagementRepository>();
            _questionManagementServiceMock = new Mock<IQuestionManagementService>();
            _auditorRepositoryMock = new Mock<IAuditorRepository>();
            _auditorServiceMock = new Mock<IAuditorService>();
            #endregion
            _chemicalRepositoryMock = new Mock<IChemicalRepository>();
            _fresherReportRepositoryMock = new Mock<IFresherReportRepository>();
            _fresherReportServiceMock = new Mock<IFresherReportService>();
            _reminderRepositoryMock = new Mock<IReminderRepository>();
            _reminderService = new Mock<IReminderService>();
            _feedbackAnswerRepositoryMock = new Mock<IFeedbackAnswerRepository>();
            _fresherReportServiceMock = new Mock<IFresherReportService>();
            _mockMEcelExportHistoryService = new Mock<IExcelExportHistoryService>();
            _mockMEcelExportDeliveryService = new Mock<IExcelExportDeliveryService>();
            _mockMEcelExportChartService = new Mock<IExcelExportChartService>();
            _scoreRepositoryMock = new Mock<IScoreRepository>();
            _attendanceServiceMock = new Mock<IAttendanceService>();
            _attendanceRepositoryMock = new Mock<IAttendanceRepository>();
            _attendanceTokenServiceMock = new Mock<IAttendanceTokenService>();
            _reportAttendanceRepositoryMock = new Mock<IReportAttendanceRepository>();
            _feedbackRepositoryMock = new Mock<IFeedbackRepository>();
            _feedbackQuestionRepositoryMock = new Mock<IFeedbackQuestionRepository>();
            _feedbackResultRepositoryMock = new Mock<IFeedbackResultRepository>();
            _feedbackServiceMock = new Mock<IFeedbackService>();
            _classFresherServiceMock = new Mock<IClassFresherService>();
            _fresherServiceMock = new Mock<IFresherService>();
            _syllabusDetailRepositoryMock = new Mock<ISyllabusDetailRepository>();
            _lectureChapterRepositoryMock = new Mock<ILectureChapterRepository>();
            _chapterSyllabusRepositoryMock = new Mock<IChapterSyllabusRepository>();
            _mockExcelExportScroreService = new Mock<IExcelExportScroreService>();
            #region PlanProgram_Mock
            _planRepositoryMock = new Mock<IPlanRepository>();
            _planServiceMock = new Mock<IPlanService>();
            _moduleRepositoryMock = new Mock<IModuleRepository>();
            _moduleServiceMock = new Mock<IModuleService>();
            _topicRepositoryMock = new Mock<ITopicRepository>();
            _topicServiceMock = new Mock<ITopicService>();
            _planInforRepositoryMock = new Mock<IPlanInformationRepository>();
            _planInformationServiceMock= new Mock<IPlanInformationService>();
            _chapterSyllabusRepositoryMock =  new Mock<IChapterSyllabusRepository>();
            _chapterSyllabusServiceMock = new Mock<IChapterSyllabusService>();
            _lectureChapterRepositoryMock = new Mock<ILectureChapterRepository>();
            _lectureChapterServiceMock = new Mock<ILectureChapterService>();
            _syllabusDetailRepositoryMock = new Mock<ISyllabusDetailRepository>();
            #endregion
            _mailServiceMock = new Mock<IMailService>();

            _moduleResultRepositoryMock = new Mock<IModuleResultRepository>();
            _moduleResultServiceMock = new Mock<IModuleResultService>();

            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _importDataServiceMock = new Mock<IImportDataFromExcelFileService>();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _dbContext = new AppDbContext(options);

            _currentTimeMock.Setup(x => x.GetCurrentTime()).Returns(DateTime.UtcNow);
            _claimsServiceMock.Setup(x => x.CurrentUserId).Returns(Guid.Empty);

            _fixture.Customizations.Add(new RandomDateOnlySequenceGenerator());
            _fixture.Customizations.Add(new RandomNullableDateOnlySequenceGenerator());
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
