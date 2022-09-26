using Application.Interfaces;
using Global.Shared.ViewModels.ReminderViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class RemindersController : BaseController
    {
        private readonly IReminderService _reminderService;
        
        public RemindersController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpGet]
        public async Task<IList<ReminderViewModel>> GetReminder([FromQuery] DateOnly? eventTime)
        {
            return await _reminderService.GetReminderAsync(eventTime);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReminder([FromBody] CreateReminderViewModel createReminderViewModel)
        {
            if (createReminderViewModel != null)
            {
                var result = await _reminderService.CreateReminderAsync(createReminderViewModel);
                if (result != null )
                {
                    return Ok(result);
                }
            }
            return BadRequest();
        }
    }
}
