using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ModuleResultViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ModuleResultService : IModuleResultService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ModuleResultService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        private async Task<double> CalculateFinalAuditAsync(UpdateFinalAuditViewModel updateFinalAuditViewModel)
        {
            var planInformation = await _unitOfWork.PlanInformationRepository
                                                    .GetByModuleNameAndClassIdAsync
                                                    (updateFinalAuditViewModel.ClassId, 
                                                    updateFinalAuditViewModel.ModuleName);

            if (planInformation == null) return 0;

            var finalAudit = (updateFinalAuditViewModel.PracticeScore * planInformation.WeightedNumberPractice) + 
                             (updateFinalAuditViewModel.AuditScore * planInformation.WeightedNumberAudit);

            return Math.Round(finalAudit, 2);
        }

        private static double CalculateScoresAVG( IList<double> scores )
        {
            var avgScore = scores.Average();

            return Math.Round(avgScore, 2);
        }

        private async Task<double> CalculateFinalMarkAsync(ModuleResult moduleResult)
        {
            var planInformation = await _unitOfWork.PlanInformationRepository
                                        .GetByModuleNameAndClassIdAsync(moduleResult.ClassId, moduleResult.ModuleName);
            
            if(planInformation == null) return 0;

            double finalMark = (double)((moduleResult.AssignmentAvgScore * planInformation.WeightedNumberAssignment) +
                                        (moduleResult.QuizzAvgScore * planInformation.WeightedNumberQuizz) 
                                      + (moduleResult.FinalAuditScore * planInformation.WeightedNumberFinal));

            return Math.Round(finalMark, 2);
        }
        public async Task<bool> CreateModuleResultAsync(CreateModuleResultViewModel createModuleResultViewModel)
        {
            var moduleResult = _mapper.Map<ModuleResult>(createModuleResultViewModel);
          
            await _unitOfWork.ModuleResultRepository.AddAsync(moduleResult);

            return await _unitOfWork.SaveChangeAsync() > 0;
        }
        
        public async Task UpdateModuleResultAsync(UpdateQuizzAssignAVGViewModel updateModuleResultViewModel)
        {
            var moduleResult = await _unitOfWork.ModuleResultRepository
                                                .GetModuleResultByFillterAsync
                                 (x => x.FresherId == updateModuleResultViewModel.FresherId
                               && x.ModuleName == updateModuleResultViewModel.ModuleName);
           
            if (moduleResult == null)
            {
                throw new AppException(Constant.EXCEPTION_UPDATE_MODULERESULT_FAIL);
            }
            
            switch (updateModuleResultViewModel.TypeScore)
            {
                case TypeScoreEnum.Assignment:
                    moduleResult.AssignmentAvgScore = CalculateScoresAVG(updateModuleResultViewModel.Scores);
                    break;
                case TypeScoreEnum.Quizz:
                    moduleResult.QuizzAvgScore = CalculateScoresAVG(updateModuleResultViewModel.Scores);
                    break;
            }

            var finalMark = await CalculateFinalMarkAsync(moduleResult);

            moduleResult.FinalMark = finalMark;

            _unitOfWork.ModuleResultRepository.Update(moduleResult);
        }

        public async Task<bool> UpdateModuleResultAsync(UpdateFinalAuditViewModel updateFinalAuditViewModel)
        {
            var moduleResult = await _unitOfWork.ModuleResultRepository
                                                .GetModuleResultByFillterAsync
                                 ( x=> x.FresherId == updateFinalAuditViewModel.FresherId
                                && x.ModuleName == updateFinalAuditViewModel.ModuleName);

            if (moduleResult == null)
            {
                var moduleResultNew = new ModuleResult
                {
                    FresherId = updateFinalAuditViewModel.FresherId,
                    ClassId = updateFinalAuditViewModel.ClassId,
                    ModuleName = updateFinalAuditViewModel.ModuleName,
                    AssignmentAvgScore = 0,
                    QuizzAvgScore = 0,
                    FinalAuditScore = await CalculateFinalAuditAsync(updateFinalAuditViewModel),
                };

                moduleResultNew.FinalMark = await CalculateFinalMarkAsync(moduleResultNew);

                await _unitOfWork.ModuleResultRepository.AddAsync(moduleResultNew);

                return await _unitOfWork.SaveChangeAsync() > 0;
            }
            
            moduleResult.FinalAuditScore = await CalculateFinalAuditAsync(updateFinalAuditViewModel);

            moduleResult.FinalMark = await CalculateFinalMarkAsync(moduleResult);

            _unitOfWork.ModuleResultRepository.Update(moduleResult);

            return await _unitOfWork.SaveChangeAsync() > 0;
        }

        public async Task<IList<ModuleResultViewModel>> GetModuleResultByClassIdAndModuleNameAsync(
                                                                    Guid classId, string moduleName)
        {
            var freshers = await _unitOfWork.FresherRepository.GetFresherByClassIdAsync
                                        (classId, moduleName);

            var moduleResultsVM = _mapper.Map<IList<ModuleResultViewModel>>(freshers);

            for (int i = 0; i < moduleResultsVM.Count; i++)
            {
                moduleResultsVM[i].Rank = GetRank(moduleResultsVM[i].FinalMark);
            }

            return moduleResultsVM;
        }
        private static string GetRank(double? finalMark)
        {
            return finalMark switch
            {
                double n when (n < 60) => RankEnum.D.ToString(),
                double n when (n < 72) => RankEnum.C.ToString(),
                double n when (n < 86) => RankEnum.B.ToString(),
                double n when (n < 93) => RankEnum.A.ToString(),
                double n when (n < 100) => RankEnum.APlus.ToString(),
                _ => RankEnum.NotYet.ToString()
            };
        }
    }
}
