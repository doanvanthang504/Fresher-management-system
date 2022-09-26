using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class FresherReportRepositoryTests : SetupTest
    {
        private readonly IFresherReportRepository _fresherReportRepository;

        public FresherReportRepositoryTests()
        {
            _fresherReportRepository = new FresherReportRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetMonthlyReportsByFilterAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<FresherReport>()
                                   .With(x => x.Account, "ThienDM")
                                   .CreateMany(10)
                                   .ToList();
            await _dbContext.FresherReports.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _fresherReportRepository.GetMonthlyReportsByFilterAsync(x => x.Account == "ThienDM");

            // assert
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetMonthlyReportsByFilterAsync_ShouldReturnNothing_IfFoundNothing()
        {
            // arrange

            // act
            var result = await _fresherReportRepository.GetMonthlyReportsByFilterAsync(x=>x.Account == "ThienDM5");

            // assert
            result.Should().BeEquivalentTo(new List<FresherReport>());
        }
    }
}
