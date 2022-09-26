using Application.Interfaces;
using Domain.Entities;
using Global.Shared.ViewModels.FeedbackViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class FeedbackController : BaseController
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedbackQuestionAsync([FromBody] CreateFeedbackQuestionViewModel feedbackQuestion)
        {
            var result = await _feedbackService.CreateFeedbackQuestionAsync(feedbackQuestion);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedbackAnswerAsync([FromBody] CreateFeedbackAnswerViewModel feedbackAnswer)
        {
            var result = await _feedbackService.CreateFeedbackAnswerAsync(feedbackAnswer);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedbackResultAsync([FromBody] CreateFeedbackResultViewModel feedbackResult)
        {
            var result = await _feedbackService.CreateFeedbackResultAsync(feedbackResult);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedbackAsync(CreateFeedbackViewModel feedbackResult)
        {
            var result = await _feedbackService.CreateFeedbackAsync(feedbackResult);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedbackAsync([FromBody] UpdateFeedbackViewModel updateFeedback)
        {
            var result = await _feedbackService.UpdateFeedbackAsync(updateFeedback);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedbackQuestionAsync([FromBody] UpdateFeedbackQuestionViewModel updateFeedbackQuestion)
        {
            var result = await _feedbackService.UpdateFeedbackQuestionAsync(updateFeedbackQuestion);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFeedbackAnswerAsync([FromBody] UpdateFeedbackAnswerViewModel updateFeedbackAnswer)
        {
            var result = await _feedbackService.UpdateFeedbackAnswerAsync(updateFeedbackAnswer);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackByIdAsync(Guid feedBackId)
        {
            var result = await _feedbackService.GetFeedbackByIdAsync(feedBackId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackResultByIdAsync(Guid feedBackResultId)
        {
            var result = await _feedbackService.GetFeedbackResultByIdAsync(feedBackResultId);
            return Ok(result);
        }
      
        [HttpGet]
        public async Task<IActionResult> GetFeedbackAnswersByQuestionIdIdAsync(Guid questionId)
        {
            var result = await _feedbackService.GetFeedbackAnswerByConstrainIdAsync(typeof(FeedbackQuestion), questionId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackAnswersByFeedbackIdIdAsync(Guid feedBackId)
        {
            var result = await _feedbackService.GetFeedbackAnswerByConstrainIdAsync(typeof(Feedback), feedBackId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedbackQuestionByIdAsync(Guid questionId)
        {
            var result = await _feedbackService.GetFeedbackQuestionByIdAsync(questionId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllResultOfFeedbackAsync(Guid feedbackId)
        {
            var result = await _feedbackService.GetAllResultOfFeedbackByFeedbackIdAsync(feedbackId);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchFeedbackQuestionAsync([FromQuery] SearchFeedbackQuestionViewModel searchFeedbackQuestion)
        {
            var result = await _feedbackService.SearchFeedbackQuestionAsync(searchFeedbackQuestion);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchFeedbackResultAsync([FromQuery] SearchFeedbackResultViewModel searchFeedbackResult)
        {
            var result = await _feedbackService.SearchFeedbackResultAsync(searchFeedbackResult);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> SearchFeedbackAsync([FromQuery] SearchFeedbackViewModel searchFeedback)
        {
            var result = await _feedbackService.SearchFeedbackAsync(searchFeedback);
            return Ok(result);
        }

        [HttpDelete("{feedbackId}")]
        public async Task<IActionResult> DeleteFeedbackAsync([FromRoute] Guid feedbackId)
        {
            await _feedbackService.DeleteFeedbackAsync(feedbackId);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackQuestionAsync([FromRoute] Guid id)
        {
            await _feedbackService.DeleteFeedbackQuestionAsync(id);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedbackAnswerAsync([FromRoute] Guid id)
        {
            await _feedbackService.DeleteFeedbackAnswerAsync(id);
            return Ok();
        }

    }
}
