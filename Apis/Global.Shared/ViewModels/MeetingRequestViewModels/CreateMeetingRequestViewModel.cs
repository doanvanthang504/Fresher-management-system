using System;

namespace Global.Shared.ViewModels.MeetingRequestViewModels
{
    public class CreateMeetingRequestViewModel
    {
        public string OrganizerEmail { get; set; }

        public string Subject { get; set; }

        public DateTimeOffset Start { get; set; }

        public DateTimeOffset End { get; set; }
        
        public string[] Attendees { get; set; }
    }
}
