using Global.Shared.ViewModels.LectureChapterViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ILectureChapterService
    {
        Task<LectureChapterViewModel> AddLectureChapterAsync(LectureChapterAddViewModel lectureChapter);
        Task<LectureChapterViewModel> GetLectureChapterByIdAsync(Guid lectureChapterId);
        Task<IEnumerable<LectureChapterViewModel>> GetLectureChapterByChapterIdAsync(Guid chapterId);
        Task<LectureChapterViewModel> UpdateLectureChapterAsync(Guid lectureChapterId, 
                                                                LectureChapterAddViewModel lectureChapter);
    }
}
