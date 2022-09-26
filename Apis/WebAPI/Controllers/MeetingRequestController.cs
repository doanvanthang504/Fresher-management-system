using Application.Interfaces;
using Global.Shared.ViewModels.MeetingRequestViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    public class MeetingRequestController : BaseController
    {
        private readonly IMeetingRequestService _meetingRequestService;

        public MeetingRequestController(IMeetingRequestService meetingRequestService)
        {
            _meetingRequestService = meetingRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateMeetingRequestViewModel createMeetingRequest)
        {
            var consoleDeviceCodeNotifier = new WriteToConsoleDeviceCodeNotifier();
            var meetingUrl = await _meetingRequestService
                                        .CreateMeetingRequestAsync(
                                            createMeetingRequest,
                                            consoleDeviceCodeNotifier);
            return Ok(meetingUrl);
        }
    }

    // this class is created for testing create meeting request purpose only
    // in production environment, it have to be re-implemented by using email, sms or etc...
    // this's can be deleted safely if you are on production environment
    class WriteToConsoleDeviceCodeNotifier : IDeviceCodeNotifier
    {
        public Task OnDeviceCodeReceivedAsync(string signInUrl, string userCode)
        {
            Console.WriteLine($"Authentication link: {signInUrl}");
            Console.WriteLine($"User code: {userCode}");
            Console.WriteLine("Open the web browser and enter the link, user code above to authenticate");
            return Task.CompletedTask;
        }
    }
}