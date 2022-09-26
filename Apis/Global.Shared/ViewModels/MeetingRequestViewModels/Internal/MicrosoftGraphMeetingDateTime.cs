using System;

namespace Global.Shared.ViewModels.MeetingRequestViewModels.Internal
{
    public class MicrosoftGraphMeetingDateTime
    {
        public DateTimeOffset DateTime { get; set; }

        // we don't make it as static field because
        // system.text.json package which is used in json serialize process does not include static field

        // we make the the datetime instance in here is in utc timezone
        // microsoft graph will take care the localization/globaliazion process for us
        public string TimeZone { get => "UTC"; }
    }
}
