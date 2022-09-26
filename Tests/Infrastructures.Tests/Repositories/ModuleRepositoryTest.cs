using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using Infrastructures.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class ModuleRepositoryTest : SetupTest
    {
        private IModuleRepository _moduleRepository;

        public ModuleRepositoryTest()
        {
            _moduleRepository = new ModuleRepository(
                                        _dbContext,
                                        _currentTimeMock.Object,
                                        _claimsServiceMock.Object
                                        );
            //This code is needed to support recursion(de quy)
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public async Task GetModuleByPlanId_ShouldReturns_ListData_IfFound()
        {
            //Arrange
            _fixture.Customize<Module>(x => x.Without(o => o.Topics));
            var mocks = _fixture.Build<Module>()
                                .With(e => e.IsDeleted, false)
                                .CreateMany(100)
                                .ToList();
            await _dbContext.Modules.AddRangeAsync(mocks);
            await _dbContext.SaveChangesAsync();
            //Act
            var result = await _moduleRepository.GetModuleByPlanId(mocks[0].PlanId);
            //Assert
            Assert.Single(result);
        }
    }
    
}
