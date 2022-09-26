using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.ReminderViewModels
{
    public class ReminderViewModel
    {
        public Guid Id { get; set; }

        public string Subject  { get; set; }

        public DateTimeOffset ReminderTime1 { get; set; }

        public DateTimeOffset? ReminderTime2 { get; set; }

        public ReminderType ReminderType { get; set; }

        public string Description { get; set; }

        public string ReminderEmail { get; set; }

        public int SentReminderTime { get; set; }
    }
}
