using Application.Repositories;
using Domain.Tests;
using Infrastructures.Repositories;

namespace Infrastructures.Tests.Repositories
{
    public class TopicRepositoryTests : SetupTest
    {
        private readonly ITopicRepository _topicRepository;

        public TopicRepositoryTests()
        {
            _topicRepository = new TopicRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }
        //[Fact]
        //public async Task GetByModuleId_Test()
        //{
        //    var mocks = _fixture.Build<Topic>().CreateMany(50).ToList();
        //    await _dbContext.Topics.ToListAsync();
        //    var result = await _topicRepository.GetByModuleId(It.IsAny<Guid>());
        //    result.Should().BeEquivalentTo(mocks);

        //}
    }
}
