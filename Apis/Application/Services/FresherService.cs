using Application.Interfaces;
using AutoMapper;
using Domain.Enums;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class FresherService : IFresherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FresherService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> ChangeFresherStatusAsync(List<ChangeStatusFresherViewModel> changeStatusFresherViewModels)
        {
            foreach (var fresher in changeStatusFresherViewModels)
            {
                var fresherToUpdaeStatus = await _unitOfWork.FresherRepository.GetByIdAsync(fresher.Id);
                if (fresherToUpdaeStatus == null) throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_FRESHER);
                fresherToUpdaeStatus.Status = fresher.Status;
                _unitOfWork.FresherRepository.Update(fresherToUpdaeStatus);
            }
            var isSucccess = await _unitOfWork.SaveChangeAsync();

            if (isSucccess != changeStatusFresherViewModels.Count)
                throw new AppException(Constant.EXCEPTION_UPDATE_STATUS_FAIL, 400);
            return true;
        }

        public async Task<FresherViewModel> GetFresherByIdAsync(Guid fresherId)
        {
            if (fresherId == Guid.Empty)
                throw new AppNotFoundException(Constant.EXCEPTION_ID_FRESHER_EMPTY);
            var fresher = await _unitOfWork.FresherRepository.GetByIdAsync(fresherId);

            // check if not found fresher
            if (fresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_NOT_FOUND_FRESHER);
            return _mapper.Map<FresherViewModel>(fresher);
        }

    }
}
