
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Helpers;

using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using Global.Shared.ViewModels.ChemicalsViewModels;
using Global.Shared.ViewModels.FeedbackViewModels;
using Global.Shared.ViewModels.ImportViewModels;
using Global.Shared.ViewModels.LectureChapterViewModels;
using Global.Shared.ViewModels.MeetingRequestViewModels;
using Global.Shared.ViewModels.MeetingRequestViewModels.Internal;

using Global.Shared.ViewModels.ModuleResultViewModels;
using Global.Shared.ViewModels.ModuleViewModels;
using Global.Shared.ViewModels.MonthResultViewModels;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Global.Shared.ViewModels.PlanViewModels;
using Global.Shared.ViewModels.ReminderViewModels;
using Global.Shared.ViewModels.ReportsViewModels;
using Global.Shared.ViewModels.ScoreViewModels;
using Global.Shared.ViewModels.SyllabusDetailViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Global.Shared.ViewModels.QuestionManagementViewModel;

using Global.Shared.ViewModels.TopicViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper.EquivalencyExpression;

namespace Infrastructures.Mappers
{
    public class MapperConfigurationsProfile : Profile
    {
        public MapperConfigurationsProfile()
        {
            CreateMap<CreateChemicalViewModel, Chemical>();
            CreateMap(typeof(Pagination<>), typeof(Pagination<>));
            CreateMap<Chemical, ChemicalViewModel>()
                .ForMember(dest => dest._Id, src => src.MapFrom(x => x.Id));

            CreateMap<ReminderViewModel, Reminder>().ReverseMap();
            CreateMap<CreateReminderViewModel, Reminder>().ReverseMap();


            CreateMap<CreateMeetingRequestViewModel, MicrosoftGraphCreateMeetingRequest>();
            CreateMap<DateTime, MicrosoftGraphMeetingDateTime>()
                .ConvertUsing<DateTimeToMicrosoftGraphDateTimeConverter>();
            CreateMap<string, MicrosoftGraphMeetingAttendee>()
                .ConvertUsing<StringToMicrosoftGraphMeetingAttendeeConverter>();
            #region Score
            CreateMap<Score, ScoreViewModel>().ReverseMap();
            CreateMap<Score, CreateScoreViewModel>().ReverseMap();
            CreateMap<Score, UpdateScoreViewModel>().ReverseMap();

            CreateMap<Fresher, ModuleResultViewModel>()
                .ForMember(dest => dest.FresherId, src => src.MapFrom(x => x.Id))
                .ForMember(dest => dest.FresherFirstName, src => src.MapFrom(x => x.FirstName))
                .ForMember(dest => dest.FresherLastName, src => src.MapFrom(x => x.LastName))
                .ForMember(dest => dest.FresherAccount, src => src.MapFrom(x => x.AccountName))
                .ForMember(dest => dest.ModuleName, src => src
                            .MapFrom(x => x.ModuleResults.Select(x => x.ModuleName).FirstOrDefault()))
                .ForMember(dest => dest.AssignmentAvgScore, src => src
                            .MapFrom(x => x.ModuleResults.Select(x => x.AssignmentAvgScore).FirstOrDefault()))
                .ForMember(dest => dest.QuizzAvgScore, src => src
                            .MapFrom(x => x.ModuleResults.Select(x => x.QuizzAvgScore).FirstOrDefault()))
                .ForMember(dest => dest.FinalAuditScore, src => src
                            .MapFrom(x => x.ModuleResults.Select(x => x.FinalAuditScore).FirstOrDefault()))
                .ForMember(dest => dest.FinalMark, src => src
                            .MapFrom(x => x.ModuleResults.Select(x => x.FinalMark).FirstOrDefault()));
            CreateMap<CreateScoreViewModel, ScoreImportViewModel>().ReverseMap();

            CreateMap<KeyValuePair<Guid, double>, MonthResultViewModel>()
                .ForMember(dest => dest.FresherId, src => src.MapFrom(src => src.Key))
                .ForMember(dest => dest.Gpa, src => src.MapFrom(src => src.Value))
                .ReverseMap();
            CreateMap<RunUpdateScoreViewModel, UpdateQuizzAssignAVGViewModel>().ReverseMap();
           
            CreateMap<CreateScoreViewModel, CreateModuleResultViewModel>().ReverseMap();
            
            CreateMap<CreateScoreViewModel, RunUpdateScoreViewModel>().ReverseMap();
            
            CreateMap<Score, RunUpdateScoreViewModel>().ReverseMap();
            
            CreateMap<ModuleResult, CreateModuleResultViewModel>().ReverseMap();
            CreateMap<CreateScoreViewModel, ScoreImportViewModel>().ReverseMap();
            #endregion

            #region AuditResult
            CreateMap<AuditResult, AuditManagementViewModel>().ReverseMap();
            CreateMap<AuditResult, AddAuditViewModel>().ReverseMap();
            CreateMap<AuditResult, UpdateAuditViewModel>().ReverseMap();
            CreateMap<AuditResult, UpdateAuditScoreViewModel>().ReverseMap();
            CreateMap<AuditResult, AuditManagementResponse>().ReverseMap();
            CreateMap<AuditResult, AddAuditorViewModel>().ReverseMap();
            CreateMap<AuditResult, PlanAuditViewModel>()
                        .ForMember(dest => dest.ClassName, src => src.MapFrom(x => x.ClassFresher.ClassName))
                        .ReverseMap();
            CreateMap<ClassFresher, ClassViewModel>().ReverseMap();
            CreateMap<AuditResult, GetPlanAuditViewModel>().ReverseMap();
            CreateMap<Auditor, AuditorViewModel>().ReverseMap();
            CreateMap<Auditor, CreatePlanAuditViewModel>().ReverseMap();
            CreateMap<QuestionManagement, QuestionManagementViewModel>().ReverseMap();
            CreateMap<QuestionManagement, GetQuestionViewModel>().ReverseMap();
            #endregion


            CreateMap<FresherReport, ExportCourseReportViewModel>();
            CreateMap<UpdateFresherReportViewModel, FresherReport>();

            #region Feedback
            CreateMap<CreateFeedbackQuestionViewModel, FeedbackQuestion>().ReverseMap();
            CreateMap<UpdateFeedbackQuestionViewModel, FeedbackQuestion>().ReverseMap();
            CreateMap<CreateFeedbackResultViewModel, FeedbackResult>().ReverseMap();
            CreateMap<CreateFeedbackViewModel, Feedback>().ReverseMap();
            CreateMap<FeedbackResult, FeedbackResultViewModel>().ReverseMap();
            CreateMap<UpdateFeedbackViewModel, Feedback>().ReverseMap();
            CreateMap<CreateFeedbackAnswerViewModel, FeedbackAnswer>().ReverseMap();

            CreateMap<FeedbackAnswer, UpdateFeedbackAnswerViewModel>().ForMember(desc => desc.AnswerId,
                                                                           src => src.MapFrom(x => x.Id))
                                                                .ReverseMap();

            CreateMap<FeedbackAnswer, FeedbackAnswerViewModel>().ForMember(desc => desc.AnswerId,
                                                                           src => src.MapFrom(x => x.Id))
                                                                .ReverseMap();

            CreateMap<Feedback, FeedbackViewModel>().ForMember(desc => desc.FeedbackId, 
                                                               src => src.MapFrom(x => x.Id))
                                                    .ReverseMap();

            CreateMap<FeedbackQuestion, FeedbackQuestionViewModel>().ForMember(desc => desc.QuestionId,
                                                                               src => src.MapFrom(x => x.Id))
                                                                    .ReverseMap();
            #endregion

            #region PlanProgram_mapper

            CreateMap<Topic, CreateTopicViewModel>().ReverseMap();
            CreateMap<Topic, UpdateTopicViewModel>().ReverseMap();
            CreateMap<Topic, TopicViewModel>().ReverseMap();

            CreateMap<Plan, PlanGetViewModel>().ReverseMap();
            CreateMap<PlanAddViewModel, Plan>().ReverseMap();
            CreateMap<Plan, PlanUpdateViewModel>().ReverseMap();

            CreateMap<ModuleAddViewModel, Module>();
            CreateMap<Module, ModuleViewModel>().ReverseMap();
            CreateMap<Module, ModuleUpdateViewModel>().ReverseMap();

            CreateMap<PlanInformation, PlanInformationViewModel>().ReverseMap();
            CreateMap<Topic, PlanInformationViewModel>()
                    .ForMember(dest => dest.ModuleName, src =>
                            src.MapFrom(x => x.Module.ModuleName))
                    .ForMember(dest => dest.PlanName, src =>
                                            src.MapFrom(x => x.Module.Plan.CourseName))
                    .ForMember(dest => dest.TopicName, src => src.MapFrom(x => x.Name))
                    //Ignoring the Id property of the destination type
                    .ForMember(dest => dest.Id, act => act.Ignore())
                    .ForMember(dest => dest.WeightedNumberAssignment, src => src.MapFrom(x => x.Module.WeightedNumberAssignment))
                    .ForMember(dest => dest.WeightedNumberQuizz, src => src.MapFrom(x => x.Module.WeightedNumberQuizz))
                    .ForMember(dest => dest.WeightedNumberFinal, src => src.MapFrom(x => x.Module.WeightedNumberFinal));
            CreateMap<PlanInformationViewModel, PlanInformation>();
            CreateMap<PlanInformation, WeightedNumberViewModel>();

            CreateMap<ChapterSyllabus, ChapterSyllabusViewModel>();
            CreateMap<ChapterSyllabusAddViewModel, ChapterSyllabus>();

            CreateMap<LectureChapter, LectureChapterViewModel>();
            CreateMap<LectureChapterAddViewModel, LectureChapter>();

            CreateMap<LectureChapter, SyllabusDetailAddViewModel>()
                    .ForMember(dest => dest.TopicId, src => src.MapFrom(x => x.ChapterSyllabus.TopicId))
                    .ForMember(dest => dest.TopicName, src => src.MapFrom(x => x.ChapterSyllabus.Topic.Name))
                    .ForMember(dest => dest.ChapterName, src => src.MapFrom(x => x.ChapterSyllabus.Name))
                    .ForMember(dest => dest.LectureName, src => src.MapFrom(x => x.Lecture));
            CreateMap<SyllabusDetailAddViewModel, SyllabusDetail>();
            CreateMap<SyllabusDetail, SyllabusDetailViewModel>();

            CreateMap<ChapterSyllabus, ChapterSyllabusViewModel>();
            CreateMap<ChapterSyllabusAddViewModel, ChapterSyllabus>();

            CreateMap<LectureChapter, LectureChapterViewModel>();
            CreateMap<LectureChapterAddViewModel, LectureChapter>();

            CreateMap<LectureChapter, SyllabusDetailAddViewModel>()
                    .ForMember(dest => dest.TopicId, src => src.MapFrom(x => x.ChapterSyllabus.TopicId))
                    .ForMember(dest => dest.TopicName, src => src.MapFrom(x => x.ChapterSyllabus.Topic.Name))
                    .ForMember(dest => dest.ChapterName, src => src.MapFrom(x => x.ChapterSyllabus.Name))
                    .ForMember(dest => dest.LectureName, src => src.MapFrom(x => x.Lecture));
            CreateMap<SyllabusDetailAddViewModel, SyllabusDetail>();
            CreateMap<SyllabusDetail, SyllabusDetailViewModel>();

            #endregion

            CreateMap<Fresher, FresherReport>()
                .ForMember(dest => dest.Account,
                           src => src.MapFrom(f => f.AccountName))
                .ForMember(dest => dest.Name,
                           src => src.MapFrom(f => f.FirstName + " " + f.LastName))
                .ForMember(dest => dest.UniversityId,
                           src => src.MapFrom(f => f.University))
                .ForMember(dest => dest.ToeicGrade,
                           src => src.MapFrom(f => f.English))
                .ForMember(dest => dest.UniversityGPA,
                           src => src.MapFrom(f => f.GPA))
                .ForMember(dest => dest.Branch,
                           src => src.MapFrom(f => StringHelper.Split(f.RRCode)[0]
                                                 + "."
                                                 + StringHelper.Split(f.RRCode)[1]))
                .ForMember(dest => dest.ParentDepartment,
                           src => src.MapFrom(f => StringHelper.Split(f.RRCode)[2]
                                                 + "."
                                                 + StringHelper.Split(f.RRCode)[3]));

            CreateMap<ClassFresher, FresherReport>()
                .ForMember(dest => dest.CourseCode,
                           src => src.MapFrom(c => c.ClassCode))
                .ForMember(dest => dest.CourseName,
                           src => src.MapFrom(c => c.ClassName))
                .ForMember(dest => dest.CourseValidOrStartDate,
                           src => src.MapFrom(c => c.StartDate))
                .ForMember(dest => dest.CourseValidOrEndDate,
                           src => src.MapFrom(c => c.EndDate))
                .ForMember(dest => dest.Site,
                           src => src.MapFrom(c => c.Location));

            CreateMap<ClassFresher, IEnumerable<FresherReport>>()
                .ConvertUsing<ClassFresherConverter>();

            CreateMap<Fresher, ExportCourseReportViewModel>()
                .ForMember(dest => dest.Account,
                           src => src.MapFrom(f => f.AccountName))
                .ForMember(dest => dest.Name,
                           src => src.MapFrom(f => f.FirstName + " " + f.LastName))
                .ForMember(dest => dest.UniversityId,
                           src => src.MapFrom(f => f.University))
                .ForMember(dest => dest.ToeicGrade,
                           src => src.MapFrom(f => f.English))
                .ForMember(dest => dest.UniversityGPA,
                           src => src.MapFrom(f => f.GPA))
                .ForMember(dest => dest.Branch,
                           src => src.MapFrom(f => StringHelper.Split(f.RRCode)[0]
                                                 + "."
                                                 + StringHelper.Split(f.RRCode)[1]))
                .ForMember(dest => dest.ParentDepartment,
                           src => src.MapFrom(f => StringHelper.Split(f.RRCode)[2]
                                                 + "."
                                                 + StringHelper.Split(f.RRCode)[3]));

            CreateMap<ClassFresher, ExportCourseReportViewModel>()
                .ForMember(dest => dest.CourseCode,
                           src => src.MapFrom(c => c.ClassCode))
                .ForMember(dest => dest.CourseName,
                           src => src.MapFrom(c => c.ClassName))
                .ForMember(dest => dest.CourseValidOrStartDate,
                           src => src.MapFrom(c => c.StartDate))
                .ForMember(dest => dest.CourseValidOrEndDate,
                           src => src.MapFrom(c => c.EndDate))
                .ForMember(dest => dest.Site,
                           src => src.MapFrom(c => c.Location));

            CreateMap<ClassFresher, IEnumerable<ExportCourseReportViewModel>>()
                .ConvertUsing<ExportReportConverter>();

            CreateMap<UpdateWeeklyFresherReportViewModel, FresherReport>();

            CreateMap<MonthResultViewModel, ExportCourseReportViewModel>()
                .ForMember(dest => dest.FinalGrade,
                           src => src.MapFrom(m => $"{m.Gpa}%"))
                .EqualityComparison((dto, entity) => dto.FresherId == entity.Id);
        }
    }
}
