using Application.Interfaces;
using Global.Shared.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class TopicsController : BaseController
    {
        private readonly ITopicService _topicService;

        public TopicsController(ITopicService topicService)
        {
            _topicService = topicService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTopic(int pageIndex = 0, int pageSize = 10)
        {
            return Ok(await _topicService.GetAllTopicAsync(pageIndex, pageSize));
        }
        [HttpGet("{topicId}")]
        public async Task<IActionResult> GetTopicById([FromRoute] Guid topicId)
        {
            return Ok(await _topicService.GetTopicByIdAsync(topicId));
        }

        [HttpGet("{moduleId}")]
        public async Task<IActionResult> GetTopicByModuleId([FromRoute] Guid moduleId)
        {
            return Ok(await _topicService.GetTopicByModuleIdAsync(moduleId));
        }

        [HttpPost]
        public async Task<IActionResult> AddTopic(CreateTopicViewModel createTopicViewModel)
        {
            return Ok(await _topicService.AddTopicAsync(createTopicViewModel));
        }
        [HttpPut("{topicId}")]
        public async Task<IActionResult> UpdateTopic([FromRoute] Guid topicId, UpdateTopicViewModel topic)
        {
            return Ok(await _topicService.UpdateTopicAsync(topicId, topic));
        }
    }
}
