using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ModuleResultViewModels;
using Global.Shared.ViewModels.ScoreViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ScoreService : IScoreService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IModuleResultService _moduleResultService;
        private readonly IImportDataFromExcelFileService _importDataFromExcelFileService;
        public ScoreService(IUnitOfWork unitOfWork, IMapper mapper,
                            IModuleResultService moduleResultService,
                            IImportDataFromExcelFileService importDataFromExcelFileService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _moduleResultService = moduleResultService;
            _importDataFromExcelFileService = importDataFromExcelFileService;
        }

        private async Task RunUpdateMouduleResultAsync(RunUpdateScoreViewModel runUpdateScoreViewModel, bool isCreate)
        {
            var scores = await _unitOfWork.ScoreRepository.GetScoresByFillterAsync
                                                        (x => x.TypeScore == runUpdateScoreViewModel.TypeScore
                                                        && x.FresherId == runUpdateScoreViewModel.FresherId
                                                        && x.ModuleName == runUpdateScoreViewModel.ModuleName
                                                        , x => x.ModuleScore);
            if (isCreate)
            {
                scores.Add(runUpdateScoreViewModel.ModuleScore);
            }

            var updateQuizzAssignAVGViewModel = _mapper.Map<UpdateQuizzAssignAVGViewModel>(runUpdateScoreViewModel);

            updateQuizzAssignAVGViewModel.Scores = scores;

            await _moduleResultService.UpdateModuleResultAsync(updateQuizzAssignAVGViewModel);

        }

        public async Task<IList<ScoreViewModel?>> CreateListScoreAsync(List<CreateScoreViewModel> createScoresViewModel)
        {
            var scores = _mapper.Map<IList<Score>>(createScoresViewModel);

            await _unitOfWork.ScoreRepository.AddRangeAsync(scores);

            foreach (var createScoreViewModel in createScoresViewModel)
            {
                if (createScoreViewModel.TypeScore == TypeScoreEnum.Assignment ||
                    createScoreViewModel.TypeScore == TypeScoreEnum.Quizz)
                {
                    var moduleResult = await _unitOfWork.ModuleResultRepository
                                                  .GetModuleResultByFillterAsync
                                 (x => x.FresherId == createScoreViewModel.FresherId
                                 && x.ModuleName == createScoreViewModel.ModuleName);

                    if (moduleResult == null)
                    {
                        var createModuleResultViewModel = _mapper.Map<CreateModuleResultViewModel>(createScoreViewModel);

                        await _moduleResultService.CreateModuleResultAsync(createModuleResultViewModel);
                    }

                    var runUpdateScoreViewModel = _mapper.Map<RunUpdateScoreViewModel>(createScoreViewModel);

                    await RunUpdateMouduleResultAsync(runUpdateScoreViewModel, true);
                }
            }

            await _unitOfWork.SaveChangeAsync();

            return _mapper.Map<IList<ScoreViewModel?>>(scores);
        }

        public async Task<IList<ScoreViewModel?>> ImportListScoreAsync(IFormFile fileExcel)
        {
            var scoresImport = _importDataFromExcelFileService.GetScoreFromExcelFile(fileExcel);

            if (scoresImport == null)
            {
                throw new AppException(Constant.IMPORT_FAIL);
            }

            var scores = _mapper.Map<List<CreateScoreViewModel>>(scoresImport);

            return await CreateListScoreAsync(scores);
        }

        public async Task<ScoreViewModel?> UpdateScoreAsync(UpdateScoreViewModel updateScoreViewModel)
        {
            var score = await _unitOfWork.ScoreRepository
                                           .GetByIdAsync(updateScoreViewModel.Id);
            if (score == null) return null;

            var scoreMap = _mapper.Map(updateScoreViewModel, score);

            _unitOfWork.ScoreRepository.Update(scoreMap);

            await _unitOfWork.SaveChangeAsync();

            if (scoreMap.TypeScore == TypeScoreEnum.Assignment ||
                    scoreMap.TypeScore == TypeScoreEnum.Quizz)
            {
                var runUpdateScoreViewModel = _mapper.Map<RunUpdateScoreViewModel>(scoreMap);

                await RunUpdateMouduleResultAsync(runUpdateScoreViewModel, false);

                await _unitOfWork.SaveChangeAsync();
            }

            return _mapper.Map<ScoreViewModel>(scoreMap);

        }

        public async Task<IList<ScoreViewModel>?> GetListScoreByModuleAsync(Guid classId, string moduleName)
        {
            var scores = await _unitOfWork.ScoreRepository.GetScoresByFillterAsync
                                      (x => x.ClassId == classId && x.ModuleName == moduleName);

            var scoreViewModel = _mapper.Map<List<ScoreViewModel>>(scores);

            return scoreViewModel;
        }

        public async Task<IList<Score>?> GetScoreByTypeScoreAndFresherIdAndModuleNameAsync(TypeScoreEnum typeScoreEnum,
                                                                            Guid fresherId, string moduleName)
        {
            var result = await _unitOfWork.ScoreRepository.GetScoresByFillterAsync
                                                            (x => x.TypeScore == typeScoreEnum
                                                            && x.FresherId == fresherId
                                                            && x.ModuleName == moduleName);

            return result;
        }
        public List<ScoreTypeViewModel> GetTypeScoreEnum()
        {
            var typeScoresEnum = new List<ScoreTypeViewModel>();

            foreach (var scoreEnum in Enum.GetValues(typeof(TypeScoreEnum)))
            {
                typeScoresEnum.Add(new ScoreTypeViewModel()
                {
                    Key = (int)scoreEnum,
                    Value = scoreEnum.ToString()
                });
            }
            return typeScoresEnum;
        }
    }
}
