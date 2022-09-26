using Application.Repositories;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using System;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class PlanInformationRepositoryTest : SetupTest
    {
        private IPlanInformationRepository _planInfoRepository;
        public PlanInformationRepositoryTest()
        {
            _planInfoRepository = new PlanInformationRepository(
                                    _dbContext,
                                    _currentTimeMock.Object,
                                    _claimsServiceMock.Object
                                    );
        }

        [Fact]
        public async Task GetByClassIdAsync_ShouldReturns_ListCorrectData()
        {
            //Act
            var results = await _planInfoRepository.GetByClassIdAsync(Guid.Empty);
            //Assert
            results.Should().BeEmpty();
        }
    }
}
