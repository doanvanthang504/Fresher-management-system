namespace Global.Shared.ViewModels.MeetingRequestViewModels.Internal
{
    public class MicrosoftGraphMeetingAttendee
    {
        public MicrosoftGraphMeetingAttendeeEmail EmailAddress { get; set; }

        // we don't make it as static field because
        // system.text.json package which is used in json serialize process does not include static field
        public string Type { get => "required"; }
    }
}
