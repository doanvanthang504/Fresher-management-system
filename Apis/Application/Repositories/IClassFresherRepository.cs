using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IClassFresherRepository  : IGenericRepository<ClassFresher>
    {
        /// <summary>
        /// Check a class is existed  by rr code
        /// </summary>
        /// <returns><see cref="bool"/> for status check. True if class's already existed, false if class's not existed</returns>   
        Task<bool> CheckExistedClassAsync(string rrCode);

        Task<ClassFresher> GetClassWithFresherByClassIdAsync(Guid classId);

        Task<List<string>> GetAllClassCodeAsync();

        Task<ClassFresher?> GetClassIncludeFreshersByIdAsync(Guid classId);

        Task<ClassFresher?> GetClassIncludeFreshersAttendancesByIdAsync(Guid classFresherId, int month, int year);
        
        Task<ClassFresher?> GetClassFresherByClassCodeAsync(string classCode);
        Task<List<ClassFresher>?> GetClassFresherByAdminNameAsync(string adminName);     
    }
}
    
