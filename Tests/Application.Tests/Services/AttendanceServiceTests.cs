using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AttendancesViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class AttendanceServiceTests : SetupTest
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceServiceTests()
        {
            _attendanceService = new AttendanceService(_unitOfWorkMock.Object, 
                                                       _mapperConfig, 
                                                       _attendanceTokenServiceMock.Object, 
                                                       _currentTimeMock.Object, 
                                                       _mailServiceMock.Object);
        }

        [Fact]

        public async Task UpdateAttendanceAsync_ShouldReturnData_WhenSuccessSaved()
        {
            //arrange
            var mock = _fixture.Build<UpdateAttendanceViewModel>().Create();
            var mockAttendance = _fixture.Build<Attendance>().Without(x => x.Fresher).Create();
            _unitOfWorkMock.Setup(x => x.AttendanceRepository
                                        .GetByIdAsync(mock.AttendanceId))
                                        .ReturnsAsync(mockAttendance);
            _unitOfWorkMock.Setup(x => x.AttendanceRepository.Update(mockAttendance));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //act
            var result = await _attendanceService.UpdateAttendanceAsync(mock);
            //assert
            _unitOfWorkMock.Verify(x => x.AttendanceRepository
                                      .Update(It.IsAny<Attendance>()),
                                      Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(),
                                         Times.Once());
        }


        [Fact]
        public async Task GetAllAttendanceByFresherIdAsync_ShouldReturnData()
        {
            //arrange
            var mockDate = DateTime.Now;
            var mocks = _fixture.Build<Attendance>().Without(x => x.Fresher)
                                                    .With(x => x.AttendDate1, mockDate)
                                                    .With(x => x.AttendDate2, mockDate)
                                                    .CreateMany(100).ToList();
            var expectedResult = _mapperConfig.Map<List<AttendanceViewModel>>(mocks);
            _unitOfWorkMock.Setup(x => x.AttendanceRepository.GetAllAttendanceByFilterAsync
                                                             (It.IsAny<Expression<Func<Attendance, bool>>>()))
                                                             .ReturnsAsync(mocks);

            //act
            var result = await _attendanceService.GetAllAttendanceByFresherIdAsync(It.IsAny<FilterAttendanceViewModel>());
            //assert
            _unitOfWorkMock.Verify(x => x.AttendanceRepository
                                         .GetAllAttendanceByFilterAsync
                                         (It.IsAny<Expression<Func<Attendance, bool>>>()), Times.Once());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllAttendanceByClassIdAsync_ShouldReturnData()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockDate = DateTime.Now;
            var mockClassRequest = _fixture.Build<FilterAttendanceViewModel>().With(x => x.Month, mockDate.Month)
                                                                              .With(x => x.Year, mockDate.Year)
                                                                              .Create();
            var mockFreshers = _fixture.Build<Fresher>().Without(x=>x.ClassFresher)
                                                        .Without(x=>x.Attendances)
                                                        .Without(x => x.ModuleResults)
                                                        .With(x=>x.ClassFresherId, mockClassRequest.Id)
                                                        .With(x=>x.Id, mockFresherId)
                                                        .CreateMany(5).ToList();
            var mockAttendances = _fixture.Build<Attendance>().Without(x=>x.Fresher)
                                                              .With(x => x.FresherId, mockFresherId)
                                                              .CreateMany(5).ToList();
            var mockClass = _fixture.Build<ClassFresher>().Without(x=>x.Freshers).Create();
            mockFreshers[0].Attendances= mockAttendances;
            mockClass.Freshers = mockFreshers;
            var expectedResult = _mapperConfig.Map<List<FresherAttendancesViewModel>>(mockClass.Freshers);
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetClassIncludeFreshersAttendancesByIdAsync(mockClassRequest.Id, mockDate.Month
                                                                                            , mockDate.Year)).ReturnsAsync(mockClass);
            //act
            var result = await _attendanceService.GetAllAttendanceByClassIdAsync(mockClassRequest);

            //assert
            _unitOfWorkMock.Verify(x => x.ClassFresherRepository
                                         .GetClassIncludeFreshersAttendancesByIdAsync(mockClassRequest.Id, mockDate.Month
                                                                                            , mockDate.Year), 
                                         Times.Once());
                                    

            result.Should().BeEquivalentTo(expectedResult);

        }

        [Fact]
        public async Task GetAllAttendanceByClassIdAsync_ShouldThrowException_IfInputIsNull()
        {
            //arrange
            var mockDate = DateTime.Now;
            var mockClassRequest = _fixture.Build<FilterAttendanceViewModel>()
                                                                               .Create();
          
            var mockClass = _fixture.Build<ClassFresher>().Without(x => x.Freshers).Create();
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetClassIncludeFreshersAttendancesByIdAsync(
                                                                    mockClassRequest.Id, 
                                                                    mockClassRequest.Month, 
                                                                    mockClassRequest.Year)).ThrowsAsync(new AppException());
            //act
            var result = () => _attendanceService.GetAllAttendanceByClassIdAsync(mockClassRequest);

            //assert
            _unitOfWorkMock.Verify(x => x.ClassFresherRepository
                                         .GetClassIncludeFreshersAttendancesByIdAsync(mockClassRequest.Id, mockDate.Month
                                                                                            , mockDate.Year),
                                         Times.Never());
            await result.Should().ThrowAsync<AppException>();
        }
    }
}
