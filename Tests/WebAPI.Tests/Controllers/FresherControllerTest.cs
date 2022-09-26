using AutoFixture;
using Domain.Enums;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class FresherControllerTest : SetupTest
    {
        private readonly FresherController _fresherController;
        public FresherControllerTest()
        {
            _fresherController = new FresherController(_fresherServiceMock.Object);
        }

        [Fact]
        public async Task GetFreshertById_ShouldReturnCorrectData_IfSuccess()
        {
            //arrage
            Guid id = Guid.NewGuid();
            _fresherServiceMock.Setup(x => x.GetFresherByIdAsync(id))
                .ReturnsAsync(new FresherViewModel { Id = id });
            //Act
            var result = await _fresherController.GetFresherByIdAsync(id);
            //assert
            //result.Id.Should().Be(id);
            _fresherServiceMock.Verify(
               x => x.GetFresherByIdAsync(id), Times.Once());
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task GetFreshertById_ShouldReturnThrowExceptionNotFound()
        {
            //arrage
            Guid id = Guid.NewGuid();
            _fresherServiceMock.Setup(x => x.GetFresherByIdAsync
            (It.IsAny<Guid>())).Callback(() =>
            throw new AppException(Constant.EXCEPTION_NOT_FOUND_FRESHER));
            //Act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _fresherController.GetFresherByIdAsync(id));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_NOT_FOUND_FRESHER);
        }

        [Fact]
        public async Task ChangStatusFresher_ShouldReturnThrowExceptionUpdateFail()
        {
            //arrage
            var listChangeStatusFresher = _fixture.Build<List<ChangeStatusFresherViewModel>>().Create();
            _fresherServiceMock.Setup(x => x.ChangeFresherStatusAsync(listChangeStatusFresher)).Callback(() =>
                throw new AppException(Constant.EXCEPTION_UPDATE_STATUS_FAIL));
            //act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _fresherController.ChangStatusFresherAsync(listChangeStatusFresher));
            //assert
            Assert.Equal(ex.Message, Constant.EXCEPTION_UPDATE_STATUS_FAIL);
        }

        [Fact]
        public async Task ChangStatusFresher_ShouldReturnThrowException()
        {
            //arrage
            var listChangeStatusFresher = _fixture.Build<List<ChangeStatusFresherViewModel>>().Create();
            _fresherServiceMock.Setup(x => x.ChangeFresherStatusAsync(listChangeStatusFresher)).Callback(() =>
                throw new AppException(Constant.EXCEPTION_NOT_FOUND_FRESHER));
            //act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _fresherController.ChangStatusFresherAsync(listChangeStatusFresher));
            //assert
            Assert.Equal(ex.Message, Constant.EXCEPTION_NOT_FOUND_FRESHER);
        }

        [Fact]
        public async Task ChangStatusFresher_ShouldReturnCorrectData()
        {
            //arrage
            var listChangeStatusFresher = _fixture.Build<List<ChangeStatusFresherViewModel>>().Create();
            _fresherServiceMock.Setup(x => x.ChangeFresherStatusAsync(listChangeStatusFresher)).ReturnsAsync(true);

            //act
            var result = await _fresherController.ChangStatusFresherAsync(listChangeStatusFresher);
            //assert
            _fresherServiceMock.Verify(
               x => x.ChangeFresherStatusAsync(listChangeStatusFresher), Times.Once());
            var actionResult = result as OkObjectResult;
            actionResult.Should().NotBeNull();
            var obj = actionResult.Value;
            obj.Should().NotBeNull();
        }
    }
}
