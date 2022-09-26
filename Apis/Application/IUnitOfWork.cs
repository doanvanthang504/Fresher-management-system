using Application.Repositories;
using System.Threading.Tasks;

namespace Application
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }

        public IChemicalRepository ChemicalRepository { get; }

        public IAuditManagementRepository AuditManagementRepository { get; }

        public IAuditorRepository AuditorRepository { get; }

        public IQuestionManagementRepository QuestionManagementRepository { get; }

        public IClassFresherRepository ClassFresherRepository { get; }

        public IFresherRepository FresherRepository { get; }

        public IFresherReportRepository FresherReportRepository { get; }

        public IFeedbackRepository FeedbackRepository { get; }

        public IFeedbackQuestionRepository FeedbackQuestionRepository { get; }

        public IFeedbackAnswerRepository FeedbackAnswerRepository { get; }

        public IFeedbackResultRepository FeedbackResultRepository { get; }

        public IReminderRepository ReminderRepository { get; }

        public IScoreRepository ScoreRepository { get; }

        public IAttendanceRepository AttendanceRepository { get; }

        public IReportAttendanceRepository ReportAttendanceRepository { get; }

        public IPlanRepository PlanRepository { get; }

        public ITopicRepository TopicRepository { get; }

        public IModuleRepository ModuleRepository { get; }

        public IPlanInformationRepository PlanInformationRepository { get; }

        public IChapterSyllabusRepository ChapterSyllabusRepository { get; }

        public ILectureChapterRepository LectureChapterRepository { get; }

        public ISyllabusDetailRepository SyllabusDetailRepository { get; }

        public IModuleResultRepository ModuleResultRepository { get; }

        public Task<int> SaveChangeAsync();

        void BeginTransaction();

        void Commit();
    }
}
