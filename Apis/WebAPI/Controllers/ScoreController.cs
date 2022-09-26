using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.ViewModels.ScoreViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ScoreController : BaseController
    {
        private readonly IScoreService _scoreService;

        public ScoreController(IScoreService scoreService)
        {
            _scoreService = scoreService;
        }

        [HttpGet("fresher/{fresherId}/module/{moduleName}/{typeScore}")]
        public async Task<IList<Score>?> GetScoreByTypeScoreAndFresherIdAndModuleIdAsync(
                                            Guid fresherId,
                                            string moduleName,
                                            TypeScoreEnum typeScore)
        {
            return await _scoreService.GetScoreByTypeScoreAndFresherIdAndModuleNameAsync(typeScore, fresherId, moduleName);
        }

        [HttpGet("class/{classId}/module/{moduleName}")]
        public async Task<IList<ScoreViewModel>?> GetAllScoreByModule(Guid classId, string moduleName)
        {
            return await _scoreService.GetListScoreByModuleAsync(classId, moduleName);
        }

        [HttpPost]
        public async Task<IList<ScoreViewModel?>> CreateScores([FromBody] List<CreateScoreViewModel>
                                                                     createScoresViewModel)
        {
            return await _scoreService.CreateListScoreAsync(createScoresViewModel);
        }
        [HttpPost]
        public async Task<IList<ScoreViewModel?>> CreateScoresFromFileExcel(IFormFile fileExcel)
        {
            return await _scoreService.ImportListScoreAsync(fileExcel);
        }
        [HttpPut]
        public async Task<ScoreViewModel?> UpdateScore(UpdateScoreViewModel updateScoreViewModel)
        {
            return await _scoreService.UpdateScoreAsync(updateScoreViewModel);
        }

        [HttpGet]
        public IActionResult GetEnumScore()
        {
            var scoreEnum = _scoreService.GetTypeScoreEnum();

            return Ok(scoreEnum);
        }
    }
}
