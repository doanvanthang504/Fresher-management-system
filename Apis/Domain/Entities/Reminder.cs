using Domain.Enums;
using System;

namespace Domain.Entities
{
    public  class Reminder : BaseEntity
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTimeOffset ReminderTime1 { get; set; }

        public DateTimeOffset? ReminderTime2 { get; set; }

        public int SentReminderTime { get; set; }

        public ReminderType ReminderType { get; set; }

        public string ReminderEmail { get; set; }
    }
}
