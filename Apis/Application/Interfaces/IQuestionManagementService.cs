using Domain.Entities;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Global.Shared.ViewModels.QuestionManagementViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IQuestionManagementService
    {
        public Task<AuditManagementResponse> AddRangeAsync(PostQuestionViewModel postQuestionViewModel);
        public Task<AuditManagementResponse> GetAllQuestionInPlanAuditAsync(GetQuestionToServer getQuestionToSerrver);
    }
}
