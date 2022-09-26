using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.LectureChapterViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LectureChapterService : ILectureChapterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LectureChapterService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<LectureChapterViewModel> AddLectureChapterAsync(LectureChapterAddViewModel lectureChapterView)
        {
            //Add LectureChapter
            var lectureChapter = _mapper.Map<LectureChapter>(lectureChapterView);
            await _unitOfWork.LectureChapterRepository.AddAsync(lectureChapter);
            var chapter = await _unitOfWork.ChapterSyllabusRepository
                                           .FindAsync(lectureChapter.ChapterSyllabusId, 
                                                      x => x.Topic, 
                                                      x => x.Topic.Module);
            
            if (chapter == null)
            {
                throw new AppException(Constant.EXCEPTION_CHAPTER_SYLLABUS_NOT_FOUND, 404);
            }
            if (chapter.Topic == null)
            {
                throw new AppException(Constant.EXCEPTION_TOPIC_NOT_FOUND, 404);
            }
            if (chapter.Topic.Module == null)
            {
                throw new AppException(Constant.EXCEPTION_MODULE_NOT_FOUND, 404);
            }

            //update duration of Chapter
            chapter.Duration += lectureChapter.Duration;
            _unitOfWork.ChapterSyllabusRepository.Update(chapter);

            //update duration of Topic
            chapter.Topic.Duration += lectureChapter.Duration;
            _unitOfWork.TopicRepository.Update(chapter.Topic);
            
            //update duration of Module
            chapter.Topic.Module.DurationTotal += lectureChapter.Duration;
            _unitOfWork.ModuleRepository.Update(chapter.Topic.Module);
            
            //Check SaveChange
            var rowsAffect = await _unitOfWork.SaveChangeAsync();
            if (rowsAffect != 4)
            {
                throw new Exception(Constant.EXCEPTION_SAVECHANGE_FAILED);
            }
            var lectureChapterViewModel = _mapper.Map<LectureChapterViewModel>(lectureChapter);
            return lectureChapterViewModel;
        }
        public async Task<IEnumerable<LectureChapterViewModel>> GetLectureChapterByChapterIdAsync(Guid chapterId)
        {
            var sortingConditionQueue = new SortingConditionQueue<LectureChapter>();
            var sortCondition = new SortingCondition<LectureChapter>(x => x.Order);
            var sortByOrder = sortingConditionQueue.Add(sortCondition);

            var lectureChapters = await _unitOfWork.LectureChapterRepository
                                                   .FindAsync(x => x.ChapterSyllabusId == chapterId, sortByOrder);
            var lectureChaptersViewModel = _mapper.Map<List<LectureChapterViewModel>>(lectureChapters);
            return lectureChaptersViewModel;
        }
        public async Task<LectureChapterViewModel> GetLectureChapterByIdAsync(Guid lectureChapterId)
        {
            var lectureChapter = await _unitOfWork.LectureChapterRepository.GetByIdAsync(lectureChapterId);
            var lectureChapterViewModel = _mapper.Map<LectureChapterViewModel>(lectureChapter);
            if (lectureChapterViewModel == null)
            {
                throw new AppException(Constant.EXCEPTION_LECTURE_NOT_FOUND, 404);
            }
            return lectureChapterViewModel;
        }
        public async Task<LectureChapterViewModel> UpdateLectureChapterAsync(
                                     Guid lectureChapterId, LectureChapterAddViewModel lectureChapterAddView)
        {
            var lectureChapter = await _unitOfWork.LectureChapterRepository.GetByIdAsync(lectureChapterId);
            if (lectureChapter == null)
            {
                throw new AppException(Constant.EXCEPTION_LECTURE_NOT_FOUND, 404);
            }
            var oldDurationLecture = lectureChapter.Duration;
            var newDurationLecture = lectureChapterAddView.Duration;
            lectureChapter = _mapper.Map(lectureChapterAddView, lectureChapter);
            _unitOfWork.LectureChapterRepository.Update(lectureChapter);

            //get chapter with related table of lecture
            var chapter = await _unitOfWork.ChapterSyllabusRepository
                                           .FindAsync(lectureChapter.ChapterSyllabusId, 
                                                      x => x.Topic, 
                                                      x => x.Topic.Module);
            if (chapter == null)
            {
                throw new AppException(Constant.EXCEPTION_CHAPTER_SYLLABUS_NOT_FOUND, 404);
            }
            if (chapter.Topic == null)
            {
                throw new AppException(Constant.EXCEPTION_TOPIC_NOT_FOUND, 404);
            }
            if (chapter.Topic.Module == null)
            {
                throw new AppException(Constant.EXCEPTION_MODULE_NOT_FOUND, 404);
            }

            //update duration chapter
            chapter.Duration = chapter.Duration - oldDurationLecture + newDurationLecture;
            _unitOfWork.ChapterSyllabusRepository.Update(chapter);

            //update duration topic
            chapter.Topic.Duration = chapter.Topic.Duration - oldDurationLecture + newDurationLecture;
            _unitOfWork.TopicRepository.Update(chapter.Topic);
            
            //update duration module
            chapter.Topic.Module.DurationTotal = chapter.Topic.Module.DurationTotal - oldDurationLecture + newDurationLecture;
            _unitOfWork.ModuleRepository.Update(chapter.Topic.Module);
            
            //check SaveChangeAsync
            var isSaveSuccessfully = await _unitOfWork.SaveChangeAsync() == 4;
            if (!isSaveSuccessfully)
            {
                throw new AppException(Constant.EXCEPTION_SAVECHANGE_FAILED, 500);
            }
            var lectureChapterViewModel = _mapper.Map<LectureChapterViewModel>(lectureChapter);
            return lectureChapterViewModel;
        }
    }
}
