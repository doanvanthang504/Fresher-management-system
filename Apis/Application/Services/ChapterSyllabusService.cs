using Application.Interfaces;
using Application.SeedData;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ChapterSyllabusService : IChapterSyllabusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChapterSyllabusService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        //public async Task SeedData()
        //{
        //    var dataChapter = DataInitializer.SeedData<ChapterSyllabus>(Constant.DATA_CHAPTERSYLLABUS);
        //    await _unitOfWork.ChapterSyllabusRepository.AddRangeAsync(dataChapter);
        //    //Seed dataLectureChapter
        //    var dataLectureChapter = DataInnitializer.SeedData<LectureChapter>(Constant.DATA_LECTURECHAPTER);
        //    await _unitOfWork.LectureChapterRepository.AddRangeAsync(dataLectureChapter);
        //    await _unitOfWork.SaveChangeAsync();
        //}
        public async Task<ChapterSyllabusViewModel> AddChapterSyllabusAsync(ChapterSyllabusAddViewModel chapterSyllabusAddViewModel)
        {
            var chapterSyllabus = _mapper.Map<ChapterSyllabus>(chapterSyllabusAddViewModel);
            await _unitOfWork.ChapterSyllabusRepository.AddAsync(chapterSyllabus);
            var isSaveSuccessfully = await _unitOfWork.SaveChangeAsync() == 1;
            if (!isSaveSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var chapterSyllabusViewModel = _mapper.Map<ChapterSyllabusViewModel>(chapterSyllabus);
            return chapterSyllabusViewModel;
        }
        public async Task<IEnumerable<ChapterSyllabusViewModel>> GetChapterSyllabusByTopicIdAsync(Guid topicId)
        {
            var sortingConditionQueue = new SortingConditionQueue<ChapterSyllabus>();
            var sortCodition = new SortingCondition<ChapterSyllabus>(x => x.Order);
            var sortByOrder = sortingConditionQueue.Add(sortCodition);
            var chapterSyllabus = await _unitOfWork.ChapterSyllabusRepository
                                                   .FindAsync(x => x.TopicId == topicId, sortByOrder, x => x.LectureChapters);
            var chapterSyllabusViewModel = _mapper.Map<List<ChapterSyllabusViewModel>>(chapterSyllabus);
            return chapterSyllabusViewModel;
        }
        public async Task<ChapterSyllabusViewModel> GetChapterSyllabusByIdAsync(Guid id)
        {
            var chapterSyllabus = await _unitOfWork.ChapterSyllabusRepository.GetByIdAsync(id);
            var chapterSyllabusViewModel = _mapper.Map<ChapterSyllabusViewModel>(chapterSyllabus);
            return chapterSyllabusViewModel;
        }
        public async Task<ChapterSyllabusViewModel> UpdateChapterSyllabusAsync(
                                                Guid chapterSyllabusId, ChapterSyllabusAddViewModel chapterSyllabusAddViewModel)
        {
            var chapterSyllabus = await _unitOfWork.ChapterSyllabusRepository.GetByIdAsync(chapterSyllabusId);
            if (chapterSyllabus == null)
            {
                throw new AppException(Constant.EXCEPTION_CHAPTER_SYLLABUS_NOT_FOUND, 404);
            }  
            chapterSyllabus = _mapper.Map(chapterSyllabusAddViewModel, chapterSyllabus);
            _unitOfWork.ChapterSyllabusRepository.Update(chapterSyllabus);
            var isSaveSuccessfully = await _unitOfWork.SaveChangeAsync() == 1;
            if (!isSaveSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }  
            var chapterSyllabusViewModel = _mapper.Map<ChapterSyllabusViewModel>(chapterSyllabus);
            return chapterSyllabusViewModel;
        }
    }
}
