using Domain.Enums;
using System;

namespace Global.Shared.ViewModels.ReminderViewModels
{
    public class CreateReminderViewModel
    {
        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTimeOffset EventTime { get; set; }

        public ReminderType ReminderType { get; set; }

        public string ReminderEmail { get; set; }
    }
}

