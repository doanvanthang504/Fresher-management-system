using Global.Shared.ViewModels.MeetingRequestViewModels;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IMeetingRequestService
    {
        Task<string> CreateMeetingRequestAsync(
            CreateMeetingRequestViewModel createMeetingRequest,
            IDeviceCodeNotifier deviceCodeNotifier);
    }
}
