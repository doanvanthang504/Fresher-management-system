using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ModuleViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ModuleService : IModuleService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ModuleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<ModuleViewModel> AddModuleAsync(ModuleAddViewModel moduleAddViewModel)
        {
            var module = _mapper.Map<Module>(moduleAddViewModel);
            await _unitOfWork.ModuleRepository.AddAsync(module);
            var isSaveSuccessfully = await _unitOfWork.SaveChangeAsync() == 1;
            if (!isSaveSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var moduleViewModel = _mapper.Map<ModuleViewModel>(module);
            return moduleViewModel;
        }
        public async Task<Pagination<ModuleViewModel>> GetAllModuleAsync(int pageIndex, int pageSize)
        {
            var modules = await _unitOfWork.ModuleRepository.FindAsync(null, null, pageIndex, pageSize);
            var moduleGetAllViewModules = _mapper.Map<Pagination<ModuleViewModel>>(modules);
            return moduleGetAllViewModules;
        }
        public async Task<ModuleViewModel> GetModuleByIdAsync(Guid moduleId)
        {
            var module = await _unitOfWork.ModuleRepository.FindAsync(moduleId, x => x.Topics);
            var moduleGetByIdViewModule = _mapper.Map<ModuleViewModel>(module);
            return moduleGetByIdViewModule;
        }
        public async Task<List<ModuleViewModel>> GetModuleByPlanIdAsync(Guid planId)
        {
            var modules = await _unitOfWork.ModuleRepository.GetModuleByPlanId(planId);
            var moduleGetByPlanId = _mapper.Map<List<ModuleViewModel>>(modules);
            return moduleGetByPlanId;
        }
        public async Task<ModuleViewModel> UpdateModuleAsync(Guid moduleId, ModuleUpdateViewModel moduleUpdateViewModel)
        {
            var module = await _unitOfWork.ModuleRepository.GetByIdAsync(moduleId);
            if (module == null)
            {
                throw new AppException(Constant.EXCEPTION_MODULE_NOT_FOUND, 404);
            }
            module = _mapper.Map(moduleUpdateViewModel, module);
            //update item Module
            _unitOfWork.ModuleRepository.Update(module);
            var isSaveSuccessfully = await _unitOfWork.SaveChangeAsync() == 1;
            if (!isSaveSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var moduleViewModel = _mapper.Map<ModuleViewModel>(module);
            return moduleViewModel;
        }
    }
}
