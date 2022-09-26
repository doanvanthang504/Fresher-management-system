using Global.Shared.ViewModels.LectureChapterViewModels;
using System;
using System.Collections.Generic;

namespace Global.Shared.ViewModels.ChapterSyllabusViewModels
{
    public class ChapterSyllabusViewModel : ChapterSyllabusAddViewModel
    {
        public Guid Id { get; set; }

        public ICollection<LectureChapterViewModel>? LectureChapters { get; set; }
    }
}
