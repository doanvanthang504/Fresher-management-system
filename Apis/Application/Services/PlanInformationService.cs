using Application.Interfaces;
using AutoMapper;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PlanInformationService : IPlanInformationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanInformationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<PlanInformationViewModel>?> GetPlanDetailByClassIdAsync(Guid classId)
        {
            var planInfomations = await _unitOfWork.PlanInformationRepository.GetByClassIdAsync(classId);
            if (planInfomations.Count <= 0)
            {
                throw new AppException(Constant.EXCEPTION_PLANINFORMATION_NOT_FOUND, 404);
            }
            var planInfoViewModels = _mapper.Map<List<PlanInformationViewModel>>(planInfomations);

            return planInfoViewModels;
        }
        public async Task<List<WeightedNumberViewModel>> GetWeightedNumberOfModuleAsync(Guid classId)
        {
            var planInformation = await _unitOfWork.PlanInformationRepository
                                                   .GetByClassIdAsync(classId);
            var moduleOfPlan = _mapper.Map<List<WeightedNumberViewModel>>(planInformation);

            var result = moduleOfPlan
                                .GroupBy(m => new {
                                    m.ClassId,
                                    m.ModuleName,
                                    m.WeightedNumberQuizz,
                                    m.WeightedNumberAssignment,
                                    m.WeightedNumberFinal,
                                    moduleOfPlan
                                })
                                .Select(t => new WeightedNumberViewModel()
                                {
                                    ClassId = t.Key.ClassId,
                                    ModuleName = t.Key.ModuleName,
                                    WeightedNumberQuizz = t.Key.WeightedNumberQuizz,
                                    WeightedNumberAssignment = t.Key.WeightedNumberAssignment,
                                    WeightedNumberFinal = t.Key.WeightedNumberFinal,
                                    Duration = t.Sum(x => x.Duration)
                                });

            return result.ToList();
        }
        public async Task<WeightedNumberViewModel> UpdateWeightedNumberOfModuleAsync(
                                             Guid classId, WeightedNumberViewModel weightedNumberViewModel)
        {
            var planInformation = await _unitOfWork.PlanInformationRepository.GetByClassIdAsync(classId);
            for (int i = 0; i < planInformation.Count; i++)
            {
                if (planInformation[i].ModuleName == weightedNumberViewModel.ModuleName)
                {
                    planInformation[i].WeightedNumberQuizz = weightedNumberViewModel.WeightedNumberQuizz;
                    planInformation[i].WeightedNumberAssignment = weightedNumberViewModel.WeightedNumberAssignment;
                    planInformation[i].WeightedNumberFinal = weightedNumberViewModel.WeightedNumberFinal;
                    _unitOfWork.PlanInformationRepository.Update(planInformation[i]);
                }
            }
            var isSavedSuccessfully = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSavedSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }

            return weightedNumberViewModel;
        }
        public async Task<bool?> UpdatePlanInfoAsync(Guid planId, PlanInformationViewModel planInformation)
        {
            var planInfo = await _unitOfWork.PlanInformationRepository
                                            .GetByIdAsync(planId);
            if (planInfo == null)
            {
                throw new AppException(Constant.EXCEPTION_PLANINFORMATION_NOT_FOUND, 404);
            }
            planInfo = _mapper.Map(planInformation, planInfo);
            _unitOfWork.PlanInformationRepository.Update(planInfo);

            var isSavedSuccessfully = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSavedSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }

            return true;
        }
    }
}
