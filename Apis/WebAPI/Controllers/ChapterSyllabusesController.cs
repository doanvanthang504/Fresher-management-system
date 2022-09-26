using Application.Interfaces;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class ChapterSyllabusesController : BaseController
    {
        private readonly IChapterSyllabusService _chapterSyllabusService;
        public ChapterSyllabusesController(IChapterSyllabusService chapterSyllabusService)
        {
            _chapterSyllabusService = chapterSyllabusService;
        }
        //[HttpPost]
        //public async Task<IActionResult> SeedData()
        //{
        //    await _chapterSyllabusService.SeedData();
        //    return Ok();
        //}
        [HttpPost]
        public async Task<ActionResult<ChapterSyllabusViewModel>> AddChapterSyllabus(ChapterSyllabusAddViewModel chapterSyllabus)
        {
            var response = await _chapterSyllabusService.AddChapterSyllabusAsync(chapterSyllabus);
            return response;
        }
        [HttpGet("{chapterSyllabusId}")]
        public async Task<ActionResult<ChapterSyllabusViewModel>> GetChapterSyllabusById([FromRoute] Guid chapterSyllabusId)
        {
            return await _chapterSyllabusService.GetChapterSyllabusByIdAsync(chapterSyllabusId);
        }
        [HttpGet("{topicId}")]
        public async Task<IEnumerable<ChapterSyllabusViewModel>> GetChapterSyllabusByTopicId([FromRoute] Guid topicId)
        {
            return await _chapterSyllabusService.GetChapterSyllabusByTopicIdAsync(topicId);
        }
        [HttpPut("{chapterSyllabusId}")]
        public async Task<ActionResult<ChapterSyllabusViewModel>> UpdateChapterSyllabus(
                                             [FromRoute] Guid chapterSyllabusId, ChapterSyllabusAddViewModel chapterSyllabus)
        {
            return await _chapterSyllabusService.UpdateChapterSyllabusAsync(chapterSyllabusId, chapterSyllabus);
        }
    }
}
