using Application.Interfaces;
using Global.Shared.ViewModels.LectureChapterViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class LectureChaptersController : BaseController
    {
        private readonly ILectureChapterService _lectureChapterService;
        public LectureChaptersController(ILectureChapterService lectureChapterService)
        {
            _lectureChapterService = lectureChapterService;
        }
        [HttpPost]
        public async Task<ActionResult<LectureChapterViewModel>> AddLectureChapter(LectureChapterAddViewModel lectureChapter)
        {
            return await _lectureChapterService.AddLectureChapterAsync(lectureChapter);
        }
        [HttpGet("{lectureChapterId}")]
        public async Task<ActionResult<LectureChapterViewModel>> GetLectureChapterById([FromRoute] Guid lectureChapterId)
        {
            return await _lectureChapterService.GetLectureChapterByIdAsync(lectureChapterId);
        }
        [HttpGet("{chapterId}")]
        public async Task<IEnumerable<LectureChapterViewModel>> GetLectureChapterByChapterId([FromRoute] Guid chapterId)
        {
            return await _lectureChapterService.GetLectureChapterByChapterIdAsync(chapterId);
        }
        [HttpPut("{lectureChapterId}")]
        public async Task<ActionResult<LectureChapterViewModel>> UpdateLectureChapter(
                                                [FromRoute] Guid lectureChapterId, LectureChapterAddViewModel lectureChapter)
        {
            return await _lectureChapterService.UpdateLectureChapterAsync(lectureChapterId, lectureChapter);
        }
    }
}
