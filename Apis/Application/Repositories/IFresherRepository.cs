using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IFresherRepository : IGenericRepository<Fresher>
    {
        Task<List<Fresher>> GetFresherByClassCodeAsync(string classCode);

        Task<List<Fresher>> GetFresherByClassIdAsync(Guid classId, string moduleName);

        Task<bool> CheckExistedFresherByAccountNameAsync(string accountName);

        Task<List<Fresher>> GetFresherByClassIdAsync(Guid classId);
    }
}
