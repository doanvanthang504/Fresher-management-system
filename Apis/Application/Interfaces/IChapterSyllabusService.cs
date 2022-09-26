using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IChapterSyllabusService
    {
        //Task SeedData();
        Task<ChapterSyllabusViewModel> AddChapterSyllabusAsync(ChapterSyllabusAddViewModel chapterSyllabus);
        Task<ChapterSyllabusViewModel> GetChapterSyllabusByIdAsync(Guid chapterSyllabusId);
        Task<IEnumerable<ChapterSyllabusViewModel>> GetChapterSyllabusByTopicIdAsync(Guid topicId);
        Task<ChapterSyllabusViewModel> UpdateChapterSyllabusAsync(Guid id, ChapterSyllabusAddViewModel chapterSyllabus);
    }
}
