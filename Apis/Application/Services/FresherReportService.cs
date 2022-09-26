using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Extensions;
using Global.Shared.Helpers;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ReportsViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FresherReportService : IFresherReportService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        //private readonly IMonthResultService _monthResultService;
        private Expression<Func<FresherReport, bool>> _expression;
        private Expression<Func<FresherReport, bool>>? tempEpression;

        public FresherReportService(IUnitOfWork unitOfWork, 
                                    IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _expression = x => x.IsMonthly;
            //_monthResultService = monthResultService;
        }

        public async Task<List<ExportCourseReportViewModel>> GetMonthlyReportsByFilterAsync
            (GetFresherReportFilterViewModel filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            if (!string.IsNullOrEmpty(filter.CourseCode))
            {
                tempEpression = x => x.CourseCode == filter.CourseCode;
                _expression = ExpressionHelper<FresherReport>
                                .ExpressionCombineAndAlso(_expression, tempEpression);
            }
            if (!string.IsNullOrEmpty(filter.Account))
            {
                tempEpression = x => x.Account == filter.Account;
                _expression = ExpressionHelper<FresherReport>
                                .ExpressionCombineAndAlso(_expression, tempEpression);
            }
            if (filter.Month != null)
            {
                tempEpression = x => x.FromDate.Month == filter.Month;
                _expression = ExpressionHelper<FresherReport>
                                .ExpressionCombineAndAlso(_expression, tempEpression);
            }
            if (filter.Year != null)
            {
                tempEpression = x => x.FromDate.Year == filter.Year;
                _expression = ExpressionHelper<FresherReport>
                                .ExpressionCombineAndAlso(_expression, tempEpression);
            }

            var reports = await _unitOfWork.FresherReportRepository
                                           .GetMonthlyReportsByFilterAsync(_expression);

            var result = _mapper.Map<List<ExportCourseReportViewModel>>(reports);
            return result;
        }

        public async Task<string> UpdateMonthlyReportAsync(Guid reportId, UpdateFresherReportViewModel updateInput)
        {
            if (reportId == Guid.Empty)
            {
                throw new ArgumentException(Constant.ID_CAN_NOT_EMPTY_NOTICE);
            }
            if (updateInput == null)
            {
                throw new ArgumentNullException(nameof(updateInput));
            }

            var fresherReport = await _unitOfWork.FresherReportRepository
                                                 .GetByIdAsync(reportId);
            if (fresherReport == null)
            {
                return Constant.RETURN_NULL_NOTICE;
            }

            var updatedFresherReport = _mapper.Map(updateInput, fresherReport);
            _unitOfWork.FresherReportRepository
                       .Update(updatedFresherReport);
            var isSavedSuccessfully = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSavedSuccessfully)
            {
                return Constant.CAN_NOT_UPDATE_REPORT_NOTICE;
            }

            return Constant.UPDATE_REPORT_SUCCESSFULLY_NOTICE;
        }

        public async Task<string> CreateMonthlyReportAsync(string courseCode)
        {
            if (string.IsNullOrEmpty(courseCode))
            {
                throw new ArgumentException(Constant.COURSECODE_CAN_NOT_EMPTY_NOTICE);
            }

            var classFresher = await _unitOfWork.ClassFresherRepository
                                                .GetClassFresherByClassCodeAsync(courseCode);
            if (classFresher == null)
            {
                return Constant.RETURN_NULL_NOTICE;
            }

            var resultReport = _mapper.Map<IEnumerable<FresherReport>>(classFresher)
                                      .ToList();

            await _unitOfWork.FresherReportRepository
                             .AddRangeAsync(resultReport);

            var isSavedSuccessfully = await _unitOfWork.SaveChangeAsync() >= resultReport.Count;
            if (!isSavedSuccessfully)
            {
                return Constant.CAN_NOT_CREATE_REPORT_NOTICE;
            }

            return Constant.CREATE_REPORT_SUCCESSFULLY_NOTICE;
        }

        public async Task<List<ExportCourseReportViewModel>?> GenerateFresherReportAsync
            (Guid adminId, bool isMonthly, int amount = 0)
        {
            if (adminId == Guid.Empty)
            {
                throw new ArgumentException(Constant.ID_CAN_NOT_EMPTY_NOTICE);
            }

            var admin = await _unitOfWork.UserRepository
                                         .GetByIdAsync(adminId);

            if(admin == null)
            {
                throw new Exception(Constant.RETURN_NULL_NOTICE);
            }

            var adminName = admin.Username;

            var classFreshers = await _unitOfWork.ClassFresherRepository
                                                 .GetClassFresherByAdminNameAsync(adminName);
            if (classFreshers == null)
            {
                return null;
            }
            var resultListReport = new List<ExportCourseReportViewModel>();
            for (var i = 0; i < classFreshers.Count; i++)
            {
                var resultReports = _mapper.Map<IEnumerable<ExportCourseReportViewModel>>
                                                (classFreshers[i])
                                           .ToList();
                resultListReport.AddRange(resultReports);
            }


            if (isMonthly)
            {
                var fromDate = DateTimeHelper.GetStartDateOfMonth();
                var toDate = DateTimeHelper.GetEndDateOfMonth();

                //var list = _monthResultService.GetMonthResultByClassId(classFreshers[0].Id,
                //                                                       fromDate.Month,
                //                                                       fromDate.Year);

                resultListReport.ForEach(r => r.FromDate = fromDate);
                resultListReport.ForEach(r => r.ToDate = toDate);
            }
            else
            {
                resultListReport.ForEach(r => r.FromDate = DateTimeHelper.GetMonday());
                resultListReport.ForEach(r => r.ToDate = DateTimeHelper.GetSunday());
            }

            if(amount > 0 && amount < resultListReport.Count)
            {
                return resultListReport.Take(amount).ToList();
            }

            return resultListReport;
        }

        public async Task<List<ExportCourseReportViewModel>> GetWeeklyFresherReportsByFilterAsync
            (GetWeeklyFresherReportFilterViewModel filter)
        {
            if (filter == null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            _expression = x => !x.IsMonthly;

            if (!string.IsNullOrEmpty(filter.CourseCode))
            {
                tempEpression = x => x.CourseCode == filter.CourseCode;
                _expression = ExpressionHelper<FresherReport>.ExpressionCombineAndAlso(_expression, tempEpression);
            }
            if (!string.IsNullOrEmpty(filter.FromDate))
            {
                var realFromDate = DateOnly.ParseExact(filter.FromDate, Constant.DATE_TIME_FORMAT_MMddyyyy, CultureInfo.InvariantCulture);
                tempEpression = x => x.FromDate >= realFromDate;
                _expression = ExpressionHelper<FresherReport>.ExpressionCombineAndAlso(_expression, tempEpression);
            }
            if (!string.IsNullOrEmpty(filter.ToDate))
            {
                var realToDate = DateOnly.ParseExact(filter.ToDate, Constant.DATE_TIME_FORMAT_MMddyyyy, CultureInfo.InvariantCulture);
                tempEpression = x => x.FromDate <= realToDate;
                _expression = ExpressionHelper<FresherReport>.ExpressionCombineAndAlso(_expression, tempEpression);
            }

            var reports = await _unitOfWork.FresherReportRepository.GetWeeklyFresherReportsByFilterAsync(_expression);

            var result = _mapper.Map<List<ExportCourseReportViewModel>>(reports);
            return result;

        }

        public async Task<string> CreateWeeklyFresherReportAsync(CreateWeeklyFresherReportViewModel createInput)
        {
            if (createInput == null)
            {
                throw new ArgumentNullException(nameof(createInput));
            }

            var fresherList = await _unitOfWork.FresherRepository.GetFresherByClassCodeAsync(createInput.CourseCode);

            if (fresherList.Count == 0)
            {
                return string.Format("Course don't have any fresher right now.");
            }

            var fromDate = DateTime.ParseExact(createInput.FromDate, Constant.DATE_TIME_FORMAT_MMddyyyy, CultureInfo.InvariantCulture);

            var monday = fromDate.AddDays((fromDate.Day - (int)fromDate.DayOfWeek + 1) - fromDate.Day).ToDateOnly();
            var sunday = fromDate.AddDays((fromDate.Day - (int)fromDate.DayOfWeek + 7) - fromDate.Day).ToDateOnly();

            foreach (var fresher in fresherList)
            {
                var rrCode = fresher.RRCode.Split(".");

                var weeklyFresherReport = new FresherReport()
                {
                    Account = fresher.AccountName,
                    Name = fresher.LastName + " " + fresher.FirstName,
                    EducationInfo = null,
                    UniversityId = fresher.University,
                    UniversityName = null,
                    Major = fresher.Major,
                    UniversityGraduationDate = fresher.Graduation,
                    UniversityGPA = fresher.GPA,
                    EducationLevel = null,
                    Branch = rrCode[0] + "." + rrCode[1],
                    ParentDepartment = rrCode[2] + "." + rrCode[3],
                    Site = rrCode[1],
                    CourseCode = fresher.ClassCode,
                    CourseName = "Công nghệ thông tin",
                    SubjectType = null,
                    SubSubjectType = null,
                    FormatType = "Offline",
                    Scope = "Company",
                    FromDate = monday,
                    ToDate = sunday,
                    LearningTime = 480,
                    Status = StatusFresherEnum.Active,
                    ToeicGrade = fresher.English,
                    UpdatedBy = null,
                    UpdatedDate = null,
                    Note = null,
                    IsMonthly = false
                };
                await _unitOfWork.FresherReportRepository.AddFresherReportAsync(weeklyFresherReport);

            }
            var savedRecords = await _unitOfWork.SaveChangeAsync();

            if (savedRecords == 0)
            {
                return string.Format("Cannot create weekly fresher reports. Saved {0} record(s).", savedRecords);
            }

            return string.Format("Saved {0} record(s).", savedRecords);
        }

        public async Task<string> UpdateWeeklyFresherReportAsync(Guid reportId, UpdateWeeklyFresherReportViewModel updateInput)
        {
            if (reportId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(reportId));
            }

            if (updateInput == null)
            {
                throw new ArgumentNullException(nameof(updateInput));
            }

            var fresherReport = await _unitOfWork.FresherReportRepository.GetByIdAsync(reportId);

            if (fresherReport == null)
            {
                return string.Format("Record not found. Input id: {0}", reportId);
            }

            var updatedFresherReport = _mapper.Map(updateInput, fresherReport);

            _unitOfWork.FresherReportRepository.UpdateFresherReport(updatedFresherReport);

            var savedRecords = await _unitOfWork.SaveChangeAsync();

            if (savedRecords == 0)
            {
                return string.Format("Cannot update weekly fresher reports. Saved {0} record(s).", savedRecords);
            }

            return string.Format("Updated {0} record(s).", savedRecords);
        }
    }
}
