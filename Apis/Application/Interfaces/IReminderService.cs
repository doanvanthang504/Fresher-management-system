using Global.Shared.ViewModels.ReminderViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IReminderService
    {
        Task<ReminderViewModel?> CreateReminderAsync(CreateReminderViewModel reminderViewModel);

        Task SendReminderMailAsync(ReminderViewModel reminderViewModel);

        Task<IList<ReminderViewModel>> GetReminderAsync(DateOnly? eventTime, bool shouldExceptSentReminder = false);
    }
}
