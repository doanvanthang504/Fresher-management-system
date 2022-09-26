using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class AttendanceRepositoryTests:SetupTest
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceRepositoryTests()
        {
            _attendanceRepository = new AttendanceRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task AttendanceRepository_GetListAttendanceByMonthAsync_ShoudReturnData()
        {
            //arrange
            var mockMonth = It.IsAny<DateTime>().Month;
            var mockYear = It.IsAny<DateTime>().Year;
            var mockId = Guid.NewGuid();
            var mockData = _fixture.Build<Attendance>()
                                   .Without(x => x.Fresher)
                                   .CreateMany(10)
                                   .Where(x=>x.AttendDate1.Month == mockMonth 
                                            && x.AttendDate1.Year==mockYear)
                                   .ToList();
            await _dbContext.Attendances.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            //act
            var result = await _attendanceRepository.GetAllAttendanceByFilterAsync(x => 
            x.FresherId == mockId &&
            x.AttendDate1.Month == mockMonth &&
            x.AttendDate1.Year == mockYear);

            //assert
            result.Should().BeEquivalentTo(mockData);
        }

         
        [Fact]
        public async Task AttendanceRepository_GetListAttendanceByMonthAsync_ShoudReturnNull()
        {
            //arrange
            var mockMonth = It.IsAny<DateTime>().Month;
            var mockYear = It.IsAny<DateTime>().Year;
            var mockId = Guid.NewGuid();
            var mockData = _fixture.Build<Attendance>()
                                   .Without(x => x.Fresher)
                                   .CreateMany(10)
                                   .Where(x => x.AttendDate1.Month != mockMonth
                                            && x.AttendDate1.Year != mockYear)
                                   .ToList();
            await _dbContext.Attendances.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            //act
            var result = await _attendanceRepository.GetAllAttendanceByFilterAsync(x =>
            x.FresherId == mockId &&
            x.AttendDate1.Month == mockMonth &&
            x.AttendDate1.Year == mockYear);

            //assert
            result.Should().BeNullOrEmpty();
        }
    }
}
