using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class FresherServiceTest : SetupTest
    {
        private readonly IFresherService _fresherService;
        public FresherServiceTest()
        {
            _fresherService = new FresherService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetFresherByIdAsync_ShoudReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<Fresher>()
                .Without(x => x.ClassFresher)
                .Without(x => x.ModuleResults)
                .Without(x => x.Attendances)
                .Create();
            var id = Guid.NewGuid();

            var expectedResult = _mapperConfig.Map<FresherViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(id))
                .ReturnsAsync(mock);

            //act
            var result = await _fresherService
                .GetFresherByIdAsync(id);

            // assert
            _unitOfWorkMock.Verify(x => x.FresherRepository.GetByIdAsync(id), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetFresherByIdAsync_ShoudReturnNullData()
        {
            //arrange
            var mock = (Fresher?)null;
            Guid id = Guid.NewGuid();
            var expectedResult = _mapperConfig.Map<FresherViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(id)).Callback(() =>
           throw new AppException(Constant.EXCEPTION_NOT_FOUND_FRESHER));
            //act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _fresherService.GetFresherByIdAsync(id));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_NOT_FOUND_FRESHER);
        }

        [Fact]
        public async Task ChangeFresherStatus_shouldReturnCorrectData()
        {
            //arrage
            int count = 0;
            var listChangeStatusFresher = _fixture.Build<ChangeStatusFresherViewModel>().CreateMany(10).ToList();
            foreach (var fresher in listChangeStatusFresher)
            {
                var fresherExpected = _mapperConfig.Map<Fresher>(fresher);
                _unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(fresher.Id)).ReturnsAsync(fresherExpected);
            }
            _unitOfWorkMock.Setup(x => x.FresherRepository.Update(It.IsAny<Fresher>())).Callback<Fresher>(f => count++).Verifiable();
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(10);
            //act
            var result = await _fresherService.ChangeFresherStatusAsync(listChangeStatusFresher);
            //assert
            Assert.True(result);
            _unitOfWorkMock.Verify(x => x.FresherRepository.Update(It.IsAny<Fresher>()), Times.Exactly(count));
        }

        [Fact]
        public async Task ChangeFresherStatus_shouldReturnThrowExceptionWithIdNotFound()
        {
            //arrage
            var listChangeStatusFresher = _fixture.Build<ChangeStatusFresherViewModel>().CreateMany(1).ToList();

            foreach (var fresher in listChangeStatusFresher)
            {
                _unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(It.IsAny<Guid>())).Callback(() =>
                                                                throw new AppException(Constant.EXCEPTION_NOT_FOUND_FRESHER));
                //_unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(fresher.Id)).ReturnsAsync(fresherExpected);
            }
            //act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _fresherService.ChangeFresherStatusAsync(listChangeStatusFresher));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_NOT_FOUND_FRESHER);
        }

        [Fact]
        public async Task ChangeFresherStatus_shouldReturnThrowExceptionWithSaveChangeFail()
        {
            //arrage
            var listChangeStatusFresher = _fixture.Build<ChangeStatusFresherViewModel>().CreateMany(1).ToList();

            foreach (var fresher in listChangeStatusFresher)
            {
                var fresherExpected = _mapperConfig.Map<Fresher>(fresher);
                _unitOfWorkMock.Setup(x => x.FresherRepository.GetByIdAsync(fresher.Id)).ReturnsAsync(fresherExpected);
            }
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);
            //act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _fresherService.ChangeFresherStatusAsync(listChangeStatusFresher));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_UPDATE_STATUS_FAIL);
        }

    }
}
