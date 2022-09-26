using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ReportsViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IFresherReportService
    {
        /// <summary>
        /// Get Fresher Report from the database base on "filter".
        /// Return a list entity which is queried from database 
        /// with specific properties define in "filter".
        /// If nothing specify, return all records.
        /// Throw ArgumentException if "filter" is null.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<List<ExportCourseReportViewModel>> GetMonthlyReportsByFilterAsync
            (GetFresherReportFilterViewModel filter);

        /// <summary>
        /// Update FresherReport record in database base on its Id.
        /// Return notice string if update successfully.
        /// Throw ArgumentException if "reportId" is Empty.
        /// Throw ArgumentException if "updateInput" is null.
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="updateInput"></param>
        /// <returns></returns>
        Task<string> UpdateMonthlyReportAsync
            (Guid reportId, UpdateFresherReportViewModel updateInput);

        /// <summary>
        /// Create new FresherReport base on "courseCode".
        /// Return notice string if create successfully
        /// Throw ArgumentException if "courseCode" is null or Empty.
        /// </summary>
        /// <param name="courseCode"></param>
        /// <returns></returns>
        Task<string> CreateMonthlyReportAsync
            (string courseCode);

        /// <summary>
        /// Generate Monthly/Weekly FresherReport base on "adminId".
        /// Return a List of ExportCourseReportViewModel.
        /// Return null if can't generate report.
        /// Throw ArgumentException if "adminId" is Empty.
        /// </summary>
        /// <param name="adminId"></param>
        /// <param name="isMonthly"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        Task<List<ExportCourseReportViewModel>?> GenerateFresherReportAsync
            (Guid adminId, bool isMonthly, int amount = 0);

        Task<List<ExportCourseReportViewModel>> GetWeeklyFresherReportsByFilterAsync
            (GetWeeklyFresherReportFilterViewModel filter);

        Task<string> CreateWeeklyFresherReportAsync
            (CreateWeeklyFresherReportViewModel createInput);

        Task<string> UpdateWeeklyFresherReportAsync
            (Guid reportId, UpdateWeeklyFresherReportViewModel updateInput);
    }
}
