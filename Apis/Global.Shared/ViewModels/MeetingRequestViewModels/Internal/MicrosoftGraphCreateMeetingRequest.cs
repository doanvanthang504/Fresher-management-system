namespace Global.Shared.ViewModels.MeetingRequestViewModels.Internal
{
    public class MicrosoftGraphCreateMeetingRequest
    {
        public string Subject { get; set; }

        public MicrosoftGraphMeetingDateTime Start { get; set; }

        public MicrosoftGraphMeetingDateTime End { get; set; }

        public bool IsReminderOn { get => true; }

        public bool IsOnlineMeeting { get => true; }

        public MicrosoftGraphMeetingAttendee[] Attendees { get; set; }
    }
}
