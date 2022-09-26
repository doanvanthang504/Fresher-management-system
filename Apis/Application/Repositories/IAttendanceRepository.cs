using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IAttendanceRepository : IGenericRepository<Attendance>
    {
        Task<List<Attendance>> GetAllAttendanceByFilterAsync(Expression<Func<Attendance, bool>> expression);

        Task<Attendance?> GetAttendanceByFilterAsync(Expression<Func<Attendance, bool>> expression);
    }
}
