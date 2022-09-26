using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.AttendancesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ReportAttendanceService : IReportAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        private const int WEIGHTED_NUMBER_PERMISSION = 2;
        private const int DISCIPLINARY_100_POINT = 100;
        private const int DISCIPLINARY_80_POINT = 80;
        private const int DISCIPLINARY_60_POINT = 60;
        private const int DISCIPLINARY_50_POINT = 50;
        private const int DISCIPLINARY_20_POINT = 20;
        private const int DISCIPLINARY_0_POINT = 0;
        private const int NO_PERMISSION_RATE_0 = 0;

        private const int NOT_PRESENT_5_PERCENT = 5;
        private const int NOT_PRESENT_20_PERCENT = 20;
        private const int NOT_PRESENT_30_PERCENT = 30;
        private const int NOT_PRESENT_50_PERCENT = 50;
        private const int NO_PERMISSION_RATE_20_PERCENT = 20;
        private const int ONE_HUNDRED_PERCENT = 100;
        private const int ZERO_PERCENT_SUM_OF_VIOLATION = 0;


        public ReportAttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<ReportAttendanceViewModel>> GetAllReportAttendanceAsync()
        {
            var reportAttendances = await _unitOfWork.ReportAttendanceRepository.GetAllAsync();
            var result = _mapper.Map<List<ReportAttendanceViewModel>>(reportAttendances);
            return result;
        }

        public async Task<List<ReportAttendanceViewModel>> CreateReportAttendanceByClass(CreateReportAttendanceViewModel request)
        {
            var fresherList = await _unitOfWork.FresherRepository.GetFresherByClassIdAsync(request.ClassId);
            var isExist = await _unitOfWork.ReportAttendanceRepository.ExistAnyAsync(x => fresherList.Select(fresher => fresher.Id).Contains(x.FresherId)
                                                                                          && x.MonthAttendance == request.Month
                                                                                          && x.YearAttendance == request.Year);

            if (isExist)
            {
                throw new AppException(Constant.REPORT_ALREADY_EXIST, Constant.STATUS_CONFLICT);
            }

            var reportAttendances = await CalculateAttendanceStatisticsAsync(request);
            await _unitOfWork.ReportAttendanceRepository.AddRangeAsync(reportAttendances);
            var isSuccess = await _unitOfWork.SaveChangeAsync() == reportAttendances.Count;

            if (isSuccess)
            {
                return _mapper.Map<List<ReportAttendanceViewModel>>(reportAttendances);
            }

            throw new AppArgumentInvalidException(Constant.CAN_NOT_CREATE_REPORT_NOTICE);
        }

        private async Task<List<ReportAttendance>> CalculateAttendanceStatisticsAsync(CreateReportAttendanceViewModel request)
        {
            var attendanceStatistics = await GetAttendanceStatisticsAsync(request);

            if(attendanceStatistics.Count == 0)
            {
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_ATTENDANCE);
            }

            var reportAttendanceList = new List<ReportAttendance>();
            foreach (var item in attendanceStatistics)
            {   
                var sumOfAbsent = item.NumberOfAbsent + item.NumberOfAbsent2;       
                var sumOfLateInEarlyOut = item.NumberOfLateInEarlyOut + item.NumberOfLateInEarlyOut2;
                var sumOfNoPermission = item.NumberOfNoPermission + item.NumberOfNoPermission2;

                var sumOfViolation = sumOfAbsent + sumOfLateInEarlyOut;
                var noPermissionRate = sumOfViolation == ZERO_PERCENT_SUM_OF_VIOLATION ? NO_PERMISSION_RATE_0 : Math.Round(((double)sumOfNoPermission / sumOfViolation) * ONE_HUNDRED_PERCENT);
                var notPresentTime = ((((double)sumOfLateInEarlyOut / WEIGHTED_NUMBER_PERMISSION) + sumOfAbsent) / item.NumberOfAttendanceCheckTimes) * ONE_HUNDRED_PERCENT;
                //var notAttendanceRate = ((double)notPresentTime / item1.NumberOfAttendanceCheckTimes) * 100;
                var disciplinaryPoint = GetDisciplinaryPoint(notPresentTime, noPermissionRate);
                var reportAttendance = new ReportAttendance
                {
                    FresherId = item.FresherId,
                    NumberOfAbsent = item.NumberOfAbsent,
                    NumberOfLateInEarlyOut = item.NumberOfLateInEarlyOut,
                    NoPermissionRate = noPermissionRate,
                    DisciplinaryPoint = disciplinaryPoint,
                    MonthAttendance = item.MonthAttendance,
                    YearAttendance = item.YearAttendance
                };
                reportAttendanceList.Add(reportAttendance);
            }
            return reportAttendanceList;
        }

        private async Task<List<ReportAttedanceStatisticViewModel>> GetAttendanceStatisticsAsync(CreateReportAttendanceViewModel request)
        {
            var attendanceStatus = new StatusAttendanceViewModel();
            var attendances = await _unitOfWork.AttendanceRepository.GetAllAttendanceByFilterAsync(x => x.Fresher.ClassFresherId == request.ClassId
                                                                                                        && x.AttendDate1.Month == request.Month
                                                                                                        && x.AttendDate1.Year == request.Year);

            if (attendances.Count == 0)
            {
                throw new AppException(Constant.EXCEPTION_NOT_FOUND_ATTENDANCE, 404);
            }

            var attendancesByMonth = attendances.GroupBy(x => new
                                                 {
                                                     x.AttendDate1.Year,
                                                     x.AttendDate1.Month,
                                                     x.FresherId,
                                                 })
                                                 .Select(report => new ReportAttedanceStatisticViewModel
                                                 {
                                                     FresherId = report.Key.FresherId,
                                                     MonthAttendance = report.Key.Month,
                                                     YearAttendance = report.Key.Year,
                                                     NumberOfAbsent = report.Where(status => attendanceStatus.AbsentStatuses.Contains(status.Status1))
                                                                            .Count(),
                                                     NumberOfLateInEarlyOut = report.Where(status => attendanceStatus.LateInEarlyOutStatuses.Contains(status.Status1))
                                                                                    .Count(),
                                                     NumberOfNoPermission = report.Where(status => attendanceStatus.NoPermissionStatuses.Contains(status.Status1))
                                                                                  .Count(),
                                                     NumberOfAbsent2 = report.Where(status => attendanceStatus.AbsentStatuses.Contains(status.Status2))
                                                                             .Count(),
                                                     NumberOfLateInEarlyOut2 = report.Where(status => attendanceStatus.LateInEarlyOutStatuses.Contains(status.Status2))
                                                                                     .Count(),
                                                     NumberOfNoPermission2 = report.Where(status => attendanceStatus.NoPermissionStatuses.Contains(status.Status2))
                                                                                   .Count(),
                                                     NumberOfAttendanceCheckTimes = report.Count() * WEIGHTED_NUMBER_PERMISSION
                                                 })
                                                 .ToList();
            return attendancesByMonth;
        }

        private double GetDisciplinaryPoint(double notPresentTime, double noPermissionRate)
        {
            //var result = notPresentTime <= 5 ? DISCIPLINARY_100_POINT
            //             : notPresentTime <= 20 ? DISCIPLINARY_80_POINT
            //             : notPresentTime <= 30 ? DISCIPLINARY_60_POINT
            //             : notPresentTime < 50 ? DISCIPLINARY_50_POINT
            //             : notPresentTime >= 50 && noPermissionRate < 20 ? DISCIPLINARY_20_POINT
            //             : 0;

            if (notPresentTime <= NOT_PRESENT_5_PERCENT)
            {
                return DISCIPLINARY_100_POINT;
            }

            if (notPresentTime <= NOT_PRESENT_20_PERCENT)
            {
                return DISCIPLINARY_80_POINT;
            }

            if (notPresentTime <= NOT_PRESENT_30_PERCENT)
            {
                return DISCIPLINARY_60_POINT;
            }

            if (notPresentTime < NOT_PRESENT_50_PERCENT)
            {
                return DISCIPLINARY_50_POINT;
            }

            if (notPresentTime >= NOT_PRESENT_50_PERCENT && noPermissionRate < NO_PERMISSION_RATE_20_PERCENT)
            {
                return DISCIPLINARY_20_POINT;
            }

            return DISCIPLINARY_0_POINT;
        }
    }
}
