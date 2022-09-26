using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Settings;
using Global.Shared.Settings.Mail;
using Global.Shared.Settings.Reminder;
using Global.Shared.ViewModels.ReminderViewModels;
using Moq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ReminderServiceTests : SetupTest
    {
        private readonly IReminderService _reminderService;
        private readonly RootSetting _rootSetting;
        private readonly ReminderSettings _reminderSettings;

        public ReminderServiceTests()
        {
            _rootSetting = new RootSetting()
            {
                SmtpClientSetting = new SmtpClientSetting
                {
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true,
                    Host = "smtp-mail.outlook.com",
                    Port = 587,
                    UseDefaultCredentials = false
                }
            };
            _reminderSettings = new ReminderSettings
            {
                AuditReminderTime = new AuditReminderTime()
            };

            _reminderService = new ReminderService(_unitOfWorkMock.Object, _mapperConfig, _mailServiceMock.Object, _reminderSettings);
        }

        [Fact]
        public async Task CreateReminderAsync_ShouldCreateSucceededWhenParameterTrue()
        {
            //arrange
            var mocks = _fixture.Build<CreateReminderViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.ReminderRepository.AddAsync(It.IsAny<Reminder>()))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //act
            var result = await _reminderService.CreateReminderAsync(mocks);

            //assert
            _unitOfWorkMock.Verify(
                x => x.ReminderRepository.AddAsync(It.IsAny<Reminder>()), Times.Once());

            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().NotBeNull();
        }

        [Fact]
        public async Task CreateReminderAsync_ShouldCreateFailedWhenParameterFalse()
        {
            //arrange
            var mocks = _fixture.Build<CreateReminderViewModel>().Create();

            _unitOfWorkMock.Setup(
                x => x.ReminderRepository.AddAsync(It.IsAny<Reminder>())).Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

            //act
            var result = await _reminderService.CreateReminderAsync(mocks);

            //assert
            _unitOfWorkMock.Verify(
                x => x.ReminderRepository.AddAsync(It.IsAny<Reminder>()), Times.Once());

            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().BeNull();
        }
    }
}
