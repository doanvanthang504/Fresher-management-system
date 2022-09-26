using AutoMapper;
using Global.Shared.ViewModels.MeetingRequestViewModels.Internal;
using System;

namespace Infrastructures.Mappers
{
    public class DateTimeToMicrosoftGraphDateTimeConverter
        : ITypeConverter<DateTime, MicrosoftGraphMeetingDateTime>
    {
        public MicrosoftGraphMeetingDateTime Convert(
            DateTime source,
            MicrosoftGraphMeetingDateTime destination,
            ResolutionContext context)
        {
            return new MicrosoftGraphMeetingDateTime
            {
                DateTime = source
            };
        }
    }
}
