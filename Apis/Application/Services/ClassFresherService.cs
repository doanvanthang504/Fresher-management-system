using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ClassFresherService : IClassFresherService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImportDataFromExcelFileService _importDataService;

        public ClassFresherService(IUnitOfWork unitOfWork, IMapper mapper, IImportDataFromExcelFileService importDataService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _importDataService = importDataService;
        }

        public async Task<ClassFresherViewModel> UpdateClassFresherAfterImportedAsync(
            UpdateClassFresherInfoViewModel updateClassFresherViewModel)
        {
            var classFresher = await _unitOfWork.ClassFresherRepository.GetByIdAsync(updateClassFresherViewModel.Id);

            if (classFresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);
             var classFresherUpdate = _mapper.Map(updateClassFresherViewModel, classFresher);

            _unitOfWork.ClassFresherRepository.Update(classFresherUpdate);
            // check save is success
            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSuccess) throw new AppException(Constant.EXCEPTION_CREATE_CLASS_FAIL, 400);

            return _mapper.Map<ClassFresherViewModel>(classFresher);
        }

        private async Task ImportClassFreshersAndFreshersAtFirstTime(
            List<ClassFresher> listClassFresher,
            List<Fresher> listFresher)
        {
            List<string> rrCodeExited = new();
            var count = 0;
            foreach (var classFresher in listClassFresher)
            {
                if (classFresher.RRCode != null)
                {
                    var checkClassFresherIfExisted = await _unitOfWork.ClassFresherRepository.CheckExistedClassAsync(classFresher.RRCode);
                    if (checkClassFresherIfExisted)
                    {
                        rrCodeExited.Add(classFresher.ClassCode);
                        count++;
                    }
                    else
                    {
                        await _unitOfWork.ClassFresherRepository.AddAsync(classFresher);
                        classFresher.Id = Guid.NewGuid();
                        var listFresherRR = listFresher.Where(p => p.RRCode.Equals(classFresher.RRCode));
                        foreach (var fresher in listFresherRR)
                        {
                            fresher.ClassFresherId = classFresher.Id;
                            fresher.Email = fresher.AccountName + Constant.DOMAIN_EMAIL_FSOFT;
                            await _unitOfWork.FresherRepository.AddAsync(fresher);
                        }
                    }
                }
            }
            if (count > 0)
            {
                var stringRRCodeExited = string.Join(", ", rrCodeExited);
                throw new AppException($"{Constant.LIST_CLASS_EXITED}: {stringRRCodeExited}");
            }
        }

        private List<ClassFresherViewModel> GetResultOfClassFresherAfterImport(List<ClassFresher> listClassFresher, List<Fresher> listFresher)
        {
            List<ClassFresherViewModel> resultClassFresher = new();
            var listClassFresherAfterImport = _mapper.Map<List<ClassFresherViewModel>>(listClassFresher);
            var listFresherAfterImport = _mapper.Map<List<FresherViewModel>>(listFresher);

            foreach (var classFresher in listClassFresherAfterImport)
            {
                List<FresherViewModel> fresherViewModel = new();
                foreach (var fresherImport in listFresherAfterImport)
                {
                    if (fresherImport.ClassCode.Equals(classFresher.ClassCode))
                    {
                        fresherViewModel.Add(fresherImport);
                    }
                }
                classFresher.Freshers = fresherViewModel;
                resultClassFresher.Add(classFresher);
            }
            return resultClassFresher;
        }

        public async Task<List<ClassFresherViewModel>> CreateClassFresherFromImportedExcelFile(IFormFile fileExcel)
        {
            var getPackageReponse = await _importDataService.GetDataFromRECExcelFileAsync(fileExcel);
            if (getPackageReponse == null)
                throw new AppException(Constant.IMPORT_FAIL, 400);

            //luu classFresher
            var listClassFresherViewModel = _mapper.Map<List<ClassFresherViewModel>>(getPackageReponse.ListClassImportViewModel);
            var listClassFresher = _mapper.Map<List<ClassFresher>>(listClassFresherViewModel);

            // luu fresher
            var listFresherViewModel = _mapper.Map<List<FresherViewModel>>(getPackageReponse.ListFresherImportViewModel);
            var listFresher = _mapper.Map<List<Fresher>>(listFresherViewModel);
            await ImportClassFreshersAndFreshersAtFirstTime(listClassFresher, listFresher);

            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSuccess) throw new AppException(Constant.IMPORT_FAIL, 400);

            return GetResultOfClassFresherAfterImport(listClassFresher, listFresher);

        }

        public async Task<Pagination<ClassFresherViewModel>> GetAllClassFreshersPagingsionAsync(PaginationRequest request)
        {
            var listClass = await _unitOfWork.ClassFresherRepository.ToPagination(request.PageIndex, request.PageSize);

            // check if not found class fresher
            if (listClass == null)
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);

            return _mapper.Map<Pagination<ClassFresherViewModel>>(listClass);
        }

        public async Task<ClassFresherViewModel> GetClassFresherByClassIdAsync(Guid classFresherId)
        {
            var classFresher = await _unitOfWork.ClassFresherRepository.GetByIdAsync(classFresherId);

            if (classFresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);

            return _mapper.Map<ClassFresherViewModel>(classFresher);
        }

        public async Task<ClassFresherViewModel> GetClassWithFresherByClassIdAsync(Guid classFresherId)
        {
            var classFresher = await _unitOfWork.ClassFresherRepository.GetClassWithFresherByClassIdAsync(classFresherId);

            if (classFresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);

            return _mapper.Map<ClassFresherViewModel>(classFresher);
        }

        public async Task<ClassFresherViewModel> UpdateClassFresher(UpdateClassFresherViewModel updateClassFresherViewModel)
        {
            var classFresher = await _unitOfWork.ClassFresherRepository.GetByIdAsync(updateClassFresherViewModel.Id);

            if (classFresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_CLASS_NOT_FOUND);

            var classFresherUpdate = _mapper.Map(updateClassFresherViewModel, classFresher);
            _unitOfWork.ClassFresherRepository.Update(classFresherUpdate);

            var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;

            // check save is success   
            if (!isSuccess)
                throw new AppException(Constant.EXCEPTION_UPDATE_CLASS_FAIL);

            return _mapper.Map<ClassFresherViewModel>(classFresher);
        }

        public async Task<List<FresherViewModel>> GetFreshersByClassCodeAsync(string classCode)
        {
            var listFresher = await _unitOfWork.FresherRepository.GetFresherByClassCodeAsync(classCode);

            if (listFresher == null)
                throw new AppNotFoundException(Constant.EXCEPTION_LIST_FRESHER_NOT_FOUND);

            return _mapper.Map<List<FresherViewModel>>(listFresher);
        }

        public async Task<List<string>> GetAllClassCodeAsync()
        {
            return await _unitOfWork.ClassFresherRepository.GetAllClassCodeAsync();
        }

        public async Task<bool> DeleteClassFresherAsync (Guid classFresherId)
        {
            var classFresher = await _unitOfWork.ClassFresherRepository.GetByIdAsync(classFresherId);
            if (classFresher != null)
            {
                _unitOfWork.ClassFresherRepository.SoftRemove(classFresher);

                var listFresher = await _unitOfWork.FresherRepository.GetFresherByClassCodeAsync(classFresher.ClassCode);

                _unitOfWork.FresherRepository.SoftRemoveRange(listFresher);
                var isSuccess = await _unitOfWork.SaveChangeAsync() > 0;
                if (isSuccess)
                    return true;
            }
            return false;
        }
    }
}
