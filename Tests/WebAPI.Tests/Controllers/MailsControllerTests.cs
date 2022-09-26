//using AutoFixture;
//using Domain.Tests;
//using FluentAssertions;
//using Global.Shared.Commons;
//using Global.Shared.Helpers;
//using Global.Shared.Settings.Mail;
//using Global.Shared.ViewModels.ChemicalsViewModels;
//using Global.Shared.ViewModels.MailViewModels;
//using Microsoft.Extensions.Configuration;
//using Moq;
//using WebAPI.Controllers;

//namespace WebAPI.Tests.Controllers
//{
//    public class MailsControllerTests : SetupTest
//    {
//        private readonly MailsController _mailsController;

//        public MailsControllerTests()
//        {
//            _mailsController = new MailsController(_mailServiceMock.Object);
//        }

//        [Fact]
//        public async Task Send_ShouldReturnCorrectData()
//        {
//            var mockRequest = _fixture
//                                .Build<MailRequestViewModel>()
//                                .With(m => m.MailType, "Welcome")
//                                .With(m => m.ToAddresses, "mock4ccount2@outlook.com")
//                                .Without(m => m.CCAddresses)
//                                .Create();

//            var userMailCredential = SetupMailService.CreateUserMailCredential(_configuration);

//            var mockMailBody = "Mock Mail";

//            var mailSettingMock = new MailSetting(userMailCredential.Address!, mockRequest, mockMailBody);
//            var mailMock = MailHelper.CreateMail(mailSettingMock);

//            var responseMock = _mapperConfig.Map<MailViewModel>(mailMock);

//            _mailServiceMock
//                .Setup(x => x.SendAsync(mockRequest))
//                .ReturnsAsync(responseMock);

//            var result = await _mailsController.Send(mockRequest);

//            result.Should().BeEquivalentTo(responseMock);

//            _mailServiceMock.Verify(x => x.SendAsync(mockRequest), Times.Once);
//        }
//    }
//}
