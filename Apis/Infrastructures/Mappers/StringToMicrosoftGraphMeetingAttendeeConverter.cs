using AutoMapper;
using Global.Shared.ViewModels.MeetingRequestViewModels.Internal;

namespace Infrastructures.Mappers
{
    internal class StringToMicrosoftGraphMeetingAttendeeConverter
        : ITypeConverter<string, MicrosoftGraphMeetingAttendee>
    {
        public MicrosoftGraphMeetingAttendee Convert(
            string source,
            MicrosoftGraphMeetingAttendee destination,
            ResolutionContext context)
        {
            return new MicrosoftGraphMeetingAttendee
            {
                EmailAddress = new MicrosoftGraphMeetingAttendeeEmail
                {
                    Address = source
                }
            };
        }
    }
}
