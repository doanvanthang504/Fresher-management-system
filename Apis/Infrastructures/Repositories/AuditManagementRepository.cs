using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class AuditManagementRepository : GenericRepository<AuditResult>, IAuditManagementRepository
    {
        public AuditManagementRepository(AppDbContext chemicalDbContext,
            ICurrentTime timeService,
            IClaimsService claimsService)
            : base(chemicalDbContext,
                  timeService,
                  claimsService)
        {
        }

        public async Task<List<double>> GetScoreFromEvaluateAsync(List<Evaluate> ListScoreEnum)
        {
            var listScore = new List<double>();
            foreach(var item in ListScoreEnum)
            {
                switch (item)
                {
                    case Evaluate.DontPass:
                        listScore.Add(44.8);
                        break;
                    case Evaluate.Improve:
                        listScore.Add(63);
                        break;
                    case Evaluate.Pass:
                        listScore.Add(77);
                        break;
                    case Evaluate.Good:
                        listScore.Add(87.5);
                        break;
                    case Evaluate.Excellence:
                        listScore.Add(100);
                        break;
                    default:
                        listScore.Add(0);
                        break;
                }
            }
            return listScore;
        }
        public async Task<List<AuditResult>> GetAuditbyClassIdAsync(Guid classId)
        {
            var listAuditGroupBy = await _dbSet.Where(a => a.ClassFresherId == classId)
                                                                    .GroupBy(x => x.DateStart)
                                                                    .Select(x => new AuditResult
                                                                    {
                                                                        DateStart = x.Key,
                                                                        Id = x.Where(p => p.DateStart == x.Key)
                                                                              .Select(p => p.Id)
                                                                              .FirstOrDefault(),
                                                                        ClassFresherId = x.Where(p => p.DateStart == x.Key)
                                                                                          .Select(p => p.ClassFresherId)
                                                                                          .FirstOrDefault(),
                                                                        ModuleName = x.Where(p => p.DateStart == x.Key)
                                                                                      .Select(p => p.ModuleName).FirstOrDefault(),
                                                                        NumberAudit = x.Where(p => p.DateStart == x.Key)
                                                                                       .Select(p => p.NumberAudit).FirstOrDefault()
                                                                    }).ToListAsync();
            return listAuditGroupBy;
        }
        
        public async Task<int> GetClassFreshersAsync(Guid classId)
        {
            int listClass = await _dbSet.GroupBy(x => x.DateStart).CountAsync();
            return listClass;            
        }

        public async Task<List<AuditResult>> GetPlanAuditByClassIdAndNameModuelAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var listAuditResult = await _dbSet.Where(x => x.ClassFresherId == auditViewModel.classId && 
                                                     x.ModuleName == auditViewModel.nameModule && 
                                                     x.NumberAudit == auditViewModel.numberAudit)
                                              .ToListAsync();
            return listAuditResult;
        }
        public async Task<List<Guid>> GetAuditorByClassModuleAsync(GetAuditByClassIdAndNumberAuditViewModel auditViewModel)
        {
            var listAuditOfClassModule = new List<Auditor>();
            var listAuditorIdOfClass = await _dbSet.Where(x => x.ClassFresherId == auditViewModel.classId
                                                     && x.NumberAudit == auditViewModel.numberAudit
                                                     && x.ModuleName == auditViewModel.nameModule
                                                     && x.AuditorId != "4bdda332-168e-44e4-915c-ae29f048f288")
                                                   .GroupBy(x => x.AuditorId)
                                                   .Select(x => new
                                                   {
                                                     AuditorId = x.Key.ToString()
                                                   }).ToListAsync();
            if(listAuditorIdOfClass == null)
            {
                return null;
            }
            else
            {
                var listAuditorIdOfClassModule = new List<Guid>();
                foreach (var item in listAuditorIdOfClass)
                {
                    listAuditorIdOfClassModule.Add(Guid.Parse(item.AuditorId));
                }
                return listAuditorIdOfClassModule;
            }
        }

        public async Task<int> GetNumberAuditorOfAuditAsync(GetAuditByClassIdAndNumberAuditViewModel getAuditByClassIdAnd)
        {
            int numberAuditor = await _dbSet.Where(x => x.ClassFresherId.Equals(getAuditByClassIdAnd.classId) &&
                                                   x.ModuleName.Equals(getAuditByClassIdAnd.nameModule) &&
                                                   x.NumberAudit.Equals(getAuditByClassIdAnd.numberAudit))
                                            .GroupBy(x => x.AuditorId)
                                            .CountAsync();
            return numberAuditor;
        }
    }
}
