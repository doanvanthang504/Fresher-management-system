using Application;
using Application.Repositories;
using System.Threading.Tasks;

namespace Infrastructures
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;
        private readonly IChemicalRepository _chemicalRepository;
        private readonly IReminderRepository _reminderRepository;
        private readonly IScoreRepository _scoreRepository;
        private readonly IAuditManagementRepository _auditManagementRepository;
        private readonly IAuditorRepository _auditorRepository;
        private readonly IQuestionManagementRepository _questionManagementRepository;
        private readonly IFresherRepository _fresherRepository;
        private readonly IClassFresherRepository _classFresherRepository;
        private readonly IFresherReportRepository _fresherReportRepository;
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly IReportAttendanceRepository _reportAttendanceRepository;
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IFeedbackQuestionRepository _feedbackQuestionRepository;
        private readonly IFeedbackAnswerRepository _feedbackAnswerRepository;
        private readonly IFeedbackResultRepository _feedbackResultRepository;
        private readonly IPlanRepository _planRepository;
        private readonly ITopicRepository _topicRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly IPlanInformationRepository _planInformationRepository;
        private readonly IChapterSyllabusRepository _chapterSyllabusRepository;
        private readonly ILectureChapterRepository _lectureChapterRepository;
        private readonly ISyllabusDetailRepository _syllabusDetailRepository;
        private readonly IModuleResultRepository _moduleResultRepository;

        public UnitOfWork(
            AppDbContext appDbContext, 
            IUserRepository userRepository,
            IChemicalRepository chemicalRepository, 
            IFresherRepository fresherRepository,
            IReminderRepository reminderRepository,
            IAuditManagementRepository auditManagementRepository,
            IAuditorRepository auditorRepository,
            IQuestionManagementRepository questionManagementRepository,
	        IFresherReportRepository fresherReportRepository,
            IFeedbackRepository feedbackRepository,
            IFeedbackQuestionRepository feedbackQuestionRepository,
            IFeedbackResultRepository feedbackResultRepository,
            IFeedbackAnswerRepository feedbackAnswerRepository,
            IClassFresherRepository classFresherRepository,
            IScoreRepository scoreRepository,
            IAttendanceRepository attendanceRepository,
            IReportAttendanceRepository reportAttendanceRepository,
            IPlanRepository planRepository,
            ITopicRepository topicRepository,
            IModuleRepository moduleRepository,
            IPlanInformationRepository planInformationRepository, 
            IChapterSyllabusRepository chapterSyllabusRepository, 
            ILectureChapterRepository lectureChapterRepository,
            ISyllabusDetailRepository syllabusDetailRepository, 
            IModuleResultRepository moduleResultRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
            _chemicalRepository = chemicalRepository;
            _reminderRepository = reminderRepository;
            _scoreRepository = scoreRepository;
            _auditManagementRepository = auditManagementRepository;
            _auditorRepository = auditorRepository;
            _questionManagementRepository = questionManagementRepository;
            _attendanceRepository = attendanceRepository;
            _classFresherRepository = classFresherRepository;
            _fresherRepository = fresherRepository;
            _reminderRepository = reminderRepository;
            _scoreRepository = scoreRepository;
            _fresherReportRepository = fresherReportRepository;
            _attendanceRepository = attendanceRepository;
            _reportAttendanceRepository = reportAttendanceRepository;
            _feedbackRepository = feedbackRepository;
            _feedbackQuestionRepository = feedbackQuestionRepository;
            _feedbackResultRepository = feedbackResultRepository;
            _feedbackAnswerRepository = feedbackAnswerRepository;
            _planRepository = planRepository;
            _topicRepository = topicRepository;
            _moduleRepository = moduleRepository;
            _planInformationRepository = planInformationRepository;
            _chapterSyllabusRepository = chapterSyllabusRepository;
            _lectureChapterRepository = lectureChapterRepository;
            _syllabusDetailRepository = syllabusDetailRepository;
            _moduleResultRepository = moduleResultRepository;
            _feedbackRepository = feedbackRepository;
        }

        public IUserRepository UserRepository { get => _userRepository; }
        public IAuditorRepository AuditorRepository { get => _auditorRepository; }
        public IChemicalRepository ChemicalRepository { get => _chemicalRepository; }
        public IReminderRepository ReminderRepository { get => _reminderRepository; }
        public IScoreRepository ScoreRepository { get => _scoreRepository; }
        public IAuditManagementRepository AuditManagementRepository { get => _auditManagementRepository; }
        public IQuestionManagementRepository QuestionManagementRepository { get => _questionManagementRepository; }
        public IAttendanceRepository AttendanceRepository { get => _attendanceRepository; }
        public IReportAttendanceRepository ReportAttendanceRepository { get => _reportAttendanceRepository; }
        public IClassFresherRepository ClassFresherRepository { get => _classFresherRepository; }
        public IFresherRepository FresherRepository { get => _fresherRepository; }
        public IScoreRepository scoreRepository { get => _scoreRepository; }
        public IFresherReportRepository FresherReportRepository { get => _fresherReportRepository; }
        public IFeedbackRepository FeedbackRepository { get => _feedbackRepository; }
        public IFeedbackQuestionRepository FeedbackQuestionRepository { get => _feedbackQuestionRepository; }
        public IFeedbackResultRepository FeedbackResultRepository { get => _feedbackResultRepository; }
        public IFeedbackAnswerRepository FeedbackAnswerRepository { get => _feedbackAnswerRepository; }
        public IPlanRepository PlanRepository { get => _planRepository; }
        public ITopicRepository TopicRepository { get => _topicRepository; }
        public IModuleRepository ModuleRepository { get => _moduleRepository; }
        public IPlanInformationRepository PlanInformationRepository
        {
            get => _planInformationRepository;
        }
        public IChapterSyllabusRepository ChapterSyllabusRepository { get => _chapterSyllabusRepository; }
        public ILectureChapterRepository LectureChapterRepository { get => _lectureChapterRepository; }
        public ISyllabusDetailRepository SyllabusDetailRepository { get => _syllabusDetailRepository; }
        public IModuleResultRepository ModuleResultRepository { get => _moduleResultRepository; }

        public async Task<int> SaveChangeAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }

        public void BeginTransaction()
        {
            _appDbContext.Database.BeginTransaction();
        }

        public void Commit()
        {
            _appDbContext.Database.CommitTransaction();
        }
    }
}
