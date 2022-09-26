using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class ChemicalRepositoryTests : SetupTest
    {
        private readonly IChemicalRepository _chemicalRepository;

        public ChemicalRepositoryTests()
        {
            _chemicalRepository = new ChemicalRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task ChemicalRepository_Should_ReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<Chemical>().With(e => e.IsDeleted, false).CreateMany(10).ToList();
            await _dbContext.Chemicals.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _chemicalRepository.GetAllAsync();

            // assert
            result.Should().BeEquivalentTo(mockData);
        }
    }
}
