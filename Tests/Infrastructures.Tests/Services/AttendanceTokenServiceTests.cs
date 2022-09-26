using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.AttendancesViewModels;
using System;

namespace Infrastructures.Tests.Services
{
    public class AttendanceTokenServiceTests:SetupTest
    {
        private readonly IAttendanceTokenService _attendanceTokenService;

        public AttendanceTokenServiceTests()
        {
            _attendanceTokenService = new AttendanceTokenService(_configuration, _currentTimeMock.Object);
        }

        [Fact]
        public void GenerateAttendanceToken_ReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<GenerateAttendanceTokenViewModel>().Create();

            //act
            var result = _attendanceTokenService.GenerateAttendanceTokenURL(mock);

            // assert
            result.Should().NotBeNull();
        }

        [Fact]
        public void VerifyAttendanceToken_ReturnCorrectData()
        {
            //act
            var mockTakeAttendanceModel = _fixture.Build<GenerateAttendanceTokenViewModel>()
                                                  .With(x=>x.TypeAttendance,1)
                                                  .With(x=>x.ExpiredLinkMinutes, 30)
                                                  .Create();

            var mock = _attendanceTokenService.GenerateAttendanceTokenURL(mockTakeAttendanceModel);

            //assert
            var result = _attendanceTokenService.VerifyAttendanceToken(mock);

            // assert
            result.Should().BeTrue();
        }
    }
}
