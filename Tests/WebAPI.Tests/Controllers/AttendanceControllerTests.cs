using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class AttendanceControllerTests : SetupTest
    {
        private readonly AttendanceController _attendanceController;

        public AttendanceControllerTests()
        {
            _attendanceController = new AttendanceController(_attendanceServiceMock.Object);
        }

        [Fact]
        public async Task SendAttendanceEmailToFresher_ShouldReturnCorrectData()
        {
            var mockMoodelRequest = _fixture.Build<GenerateAttendanceClassTokenViewModel>().Create();
            var mockReponse = true;

            //arrange
            _attendanceServiceMock.Setup(
                x => x.SendAttendanceEmailToFresherAsync(mockMoodelRequest)).ReturnsAsync(mockReponse);

            //act
            var result = await _attendanceController.SendAttendanceEmailToFresher(mockMoodelRequest);

            //assert
            _attendanceServiceMock.Verify(
                x => x.SendAttendanceEmailToFresherAsync(mockMoodelRequest), Times.Once());

            result.Should().BeTrue();
        }

        [Fact]
        public async Task SendAttendanceEmailToFresher_ShouldThrowException()
        {
            var mockMoodelRequest = _fixture.Build<GenerateAttendanceClassTokenViewModel>().Create();

            //arrange
            _attendanceServiceMock.Setup(x => x.SendAttendanceEmailToFresherAsync(mockMoodelRequest))
                                  .Callback(() => throw new AppException("Send Email Fail!", 404));

            //act
            var exception = await Assert.ThrowsAsync<AppException>
                (async () => await _attendanceController.SendAttendanceEmailToFresher(mockMoodelRequest));

            //assert
            Assert.Equal("Send Email Fail!", exception.Message);
        }

        [Fact]
        public async Task TakeAttendance_ShouldReturnCorrectData()
        {
            var mockRequest = _fixture.Build<string>().Create();
            var mockReponse = true;

            //arrange
            _attendanceServiceMock.Setup(
                x => x.TakeAttendanceAsync(mockRequest)).ReturnsAsync(mockReponse);

            //act
            var result = await _attendanceController.TakeAttendance(mockRequest);

            //assert
            _attendanceServiceMock.Verify(
                x => x.TakeAttendanceAsync(mockRequest), Times.Once());

            result.Should().BeTrue();
        }

        [Fact]
        public async Task TakeAttendance_ShouldThrowException()
        {
            var mockRequest = _fixture.Build<string>().Create();

            //arrange
            _attendanceServiceMock.Setup(x => x.TakeAttendanceAsync(mockRequest))
                                  .Callback(() => throw new AppException(Constant.INVALID_LINK, 404));

            //act
            var exception = await Assert.ThrowsAsync<AppException>
                (async () => await _attendanceController.TakeAttendance(mockRequest));

            //assert
            Assert.Equal(Constant.INVALID_LINK, exception.Message);
        }

        [Fact]
        public async Task UpdateAttendance_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<UpdateAttendanceViewModel>().Create();
            var mockModelResponse = _fixture.Build<AttendanceViewModel>().Create();
            //arrange
            _attendanceServiceMock.Setup(
                x => x.UpdateAttendanceAsync(mockModelRequest)).ReturnsAsync(mockModelResponse);

            //act
            var result = await _attendanceController.UpdateAttendance(mockModelRequest);

            //assert
            _attendanceServiceMock.Verify( 
                x => x.UpdateAttendanceAsync(mockModelRequest), Times.Once());

            result.Should().BeEquivalentTo(mockModelResponse);
        }

        [Fact]
        public async Task GetAllAttendanceByFresherIdAsync_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<FilterAttendanceViewModel>().Create();
            var mockModelResponse = _fixture.Build<AttendanceViewModel>().CreateMany(20).ToList();

            //arrange
            _attendanceServiceMock.Setup(
                x => x.GetAllAttendanceByFresherIdAsync(mockModelRequest)).ReturnsAsync(mockModelResponse);

            //act
            var result = await _attendanceController.ListAttendanceOfFresher(mockModelRequest);

            //assert
            _attendanceServiceMock.Verify(
                x => x.GetAllAttendanceByFresherIdAsync(mockModelRequest), Times.Once());

            result.Should().BeEquivalentTo(mockModelResponse);
        }

        [Fact]
        public async Task ListAttendanceOfFresherByClass_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<FilterAttendanceViewModel>().Create();
            var mockModelResponse = _fixture.Build<FresherAttendancesViewModel>().CreateMany(20).ToList();

            //arrange
            _attendanceServiceMock.Setup(
                x => x.GetAllAttendanceByClassIdAsync(mockModelRequest)).ReturnsAsync(mockModelResponse);

            //act
            var result = await _attendanceController.ListAttendanceOfFresherByClass(mockModelRequest);

            //assert
            _attendanceServiceMock.Verify(x => x.GetAllAttendanceByClassIdAsync(mockModelRequest), Times.Once());

            result.Should().BeEquivalentTo(mockModelResponse);
        }
       
    }
}



