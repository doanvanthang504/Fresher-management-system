using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IReportAttendanceRepository : IGenericRepository<ReportAttendance>
    {
        Task<List<ReportAttendance>> GetReportAttendanceListByClassAsync(List<Guid> freshers, int month, int year);
    }
}
