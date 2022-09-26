using Application.Interfaces;
using Global.Shared.Extensions;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CronJobService : ICronJobService
    {
        private readonly IChemicalService _chemicalService;
        private readonly IReminderService _reminderService;
        private readonly ICurrentTime _currentTime;
        private readonly IHangfireService _hangfireService;

        public CronJobService(IChemicalService chemicalService,
            IReminderService reminderService,
            ICurrentTime currentTime,
            IHangfireService hangfireService)
        {
            _chemicalService = chemicalService;
            _reminderService = reminderService;
            _currentTime = currentTime;
            _hangfireService = hangfireService;
        }

        public async Task AutoCreateReminderAsync()
        {
            var eventTime = _currentTime.GetCurrentTime().ToDateOnly();
            var reminders = await _reminderService.GetReminderAsync(eventTime, true);
            foreach (var reminder in reminders)
            {
                var currentTime = _currentTime.GetCurrentTime();
                TimeSpan delayedTime = TimeSpan.Zero;
                if (!reminder.ReminderTime2.HasValue)
                {
                    delayedTime = reminder.ReminderTime1 - currentTime;
                }
                else
                {
                    var reminderTime = reminder.SentReminderTime == 0
                                            ? reminder.ReminderTime1
                                            : reminder.ReminderTime2!.Value;
                    delayedTime = reminderTime - currentTime;
                }
                if (delayedTime > TimeSpan.Zero)
                {
                    _hangfireService.CreateDelayedTask(
                                            () => _reminderService.SendReminderMailAsync(reminder),
                                            delayedTime);
                }
            }
        }

        public async Task AutoGetChemicalAsync()
        {
            await _chemicalService.GetChemicalAsync();
        }

        public async Task AutoGetChemicalPagingsionAsync()
        {
            await _chemicalService.GetChemicalPagingsionAsync();
        }
    }
}
