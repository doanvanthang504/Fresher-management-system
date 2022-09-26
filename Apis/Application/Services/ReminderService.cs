using Application.Hangfire.ClientFilters;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Global.Shared.Extensions;
using Global.Shared.Helpers;
using Global.Shared.Settings.Reminder;
using Global.Shared.ViewModels.ReminderViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Services
{
    [OnceTimeJobClientFilter]
    public class ReminderService : IReminderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMailService _mailService;
        private readonly ReminderSettings _reminderSettings;

        public ReminderService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IMailService mailService,
            ReminderSettings reminderSettings)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _mailService = mailService;
            _reminderSettings = reminderSettings;
        }

        public async Task<ReminderViewModel?> CreateReminderAsync(CreateReminderViewModel reminderViewModel)
        {
            var reminder = _mapper.Map<Reminder>(reminderViewModel);
            if (reminderViewModel.ReminderType == ReminderType.Audit)
            {
                reminder.ReminderTime1 = reminderViewModel.EventTime.AddDays(_reminderSettings.AuditReminderTime.TheFirstTime);
                reminder.ReminderTime2 = reminderViewModel.EventTime.AddDays(_reminderSettings.AuditReminderTime.TheSecondTime);
            }
            else if (reminderViewModel.ReminderType == ReminderType.ContractTransfer)
            {
                reminder.ReminderTime1 = reminderViewModel.EventTime.AddDays(_reminderSettings.ContractTransferReminderTime);
                reminder.ReminderTime2 = null;
            }
            else if (reminderViewModel.ReminderType == ReminderType.Custom)
            {
                reminder.ReminderTime1 = reminderViewModel.EventTime.AddDays(_reminderSettings.CustomReminderTime.TheFirstTime);
                reminder.ReminderTime2 = reminderViewModel.EventTime.AddDays(_reminderSettings.CustomReminderTime.TheSecondTime);
            }
            await _unitOfWork.ReminderRepository.AddAsync(reminder);
            var affectedRows = await _unitOfWork.SaveChangeAsync();
            if (affectedRows > 0)
                return _mapper.Map<ReminderViewModel>(reminder);
            return null;
        }

        public async Task<IList<ReminderViewModel>> GetReminderAsync(DateOnly? eventTime, bool shouldExceptSentReminder)
        {
            IList<Reminder> reminders = new List<Reminder>();
           
            
            // if eventTime has value, filter the results
            Expression<Func<Reminder, bool>> expression =
                (Reminder e) => 
                    !eventTime.HasValue
                        || e.ReminderTime1.Date == eventTime.Value.ToDateTime()
                        || (e.ReminderTime2.HasValue && e.ReminderTime2.Value.Date == eventTime.Value.ToDateTime());

            // call from hangfire
            if (shouldExceptSentReminder)
            {
                // reminder is sent max twice times
                Expression<Func<Reminder, bool>> reminderTimeExpression = 
                    (Reminder e) => 
                        e.ReminderType == ReminderType.ContractTransfer
                            ? e.SentReminderTime < 1
                            : e.SentReminderTime < 2;

                expression = ExpressionHelper<Reminder>.ExpressionCombineAndAlso(expression, reminderTimeExpression);
            }

            reminders = await _unitOfWork.ReminderRepository.FindAsync(expression, null);

            return _mapper.Map<IList<ReminderViewModel>>(reminders);
        }

        [OnceTimeJobClientFilter]
        public async Task SendReminderMailAsync(ReminderViewModel reminder)
        {
            using MailMessage mailMessage = new();
            mailMessage.Subject = reminder.Subject;
            mailMessage.Body = reminder.Description;
            mailMessage.To.Add(reminder.ReminderEmail);

            var result = await _mailService.SendAsync(mailMessage);
            if (!result.IsError)
            {
                await UpdateReminderStatusAsync(reminder);
            }
        }

        public async Task UpdateReminderStatusAsync(ReminderViewModel reminder)
        {
            reminder.SentReminderTime += 1;
            var reminderEntity = _mapper.Map<Reminder>(reminder);
            _unitOfWork.ReminderRepository.Update(reminderEntity);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}