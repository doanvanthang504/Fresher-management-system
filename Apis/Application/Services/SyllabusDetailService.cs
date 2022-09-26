using Application.Interfaces;
using AutoMapper;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.SyllabusDetailViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SyllabusDetailService : ISyllabusDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SyllabusDetailService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByClassIdAsync(Guid classId)
        {
            var syllabusDetails = await _unitOfWork.SyllabusDetailRepository
                                                   .FindAsync(x => x.ClassId == classId, null);
            var syllabusDetailsViewModel = _mapper.Map<List<SyllabusDetailViewModel>>(syllabusDetails);

            return syllabusDetailsViewModel;
        }
        public async Task<IEnumerable<SyllabusDetailViewModel>> GetSyllabusDetailByPlanInformationIdAsync(Guid planInformationId)
        {
            var syllabusDetails = await _unitOfWork.SyllabusDetailRepository
                                                   .FindAsync(x => x.PlanInformationId == planInformationId, null);
            var syllabusDetailsViewModel = _mapper.Map<List<SyllabusDetailViewModel>>(syllabusDetails);

            return syllabusDetailsViewModel;
        }
        public async Task<SyllabusDetailViewModel> UpdateSyllabusDetailAsync(
                                                        Guid id, SyllabusDetailAddViewModel syllabusDetailAddView)
        {
            var syllabus = await _unitOfWork.SyllabusDetailRepository.GetByIdAsync(id);
            if (syllabus == null)
            {
                throw new AppException(Constant.EXCEPTION_SYLLABUS_DETAIL_NOT_FOUND, 404);
            }

            syllabus = _mapper.Map(syllabusDetailAddView, syllabus);
            _unitOfWork.SyllabusDetailRepository.Update(syllabus);

            var isSavedSuccessfully = await _unitOfWork.SaveChangeAsync() > 0;
            if (!isSavedSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var syllabusDetail = _mapper.Map<SyllabusDetailViewModel>(syllabus);

            return syllabusDetail;
        }
    }
}
