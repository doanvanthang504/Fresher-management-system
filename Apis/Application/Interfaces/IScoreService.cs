using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels.ScoreViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IScoreService
    {
        Task<IList<ScoreViewModel?>> CreateListScoreAsync(List<CreateScoreViewModel> createScoresViewModel);
        Task<IList<ScoreViewModel?>> ImportListScoreAsync(IFormFile fileExcel);

        Task<ScoreViewModel?> UpdateScoreAsync(UpdateScoreViewModel updateScoreViewModel);
                                                                   
        Task<IList<ScoreViewModel>?> GetListScoreByModuleAsync(Guid classId, string moduleName);

        Task<IList<Score>?> GetScoreByTypeScoreAndFresherIdAndModuleNameAsync(
                                                            TypeScoreEnum typeScoreEnum,
                                                            Guid fresherId, string moduleName);
        List<ScoreTypeViewModel> GetTypeScoreEnum();


    }
}
