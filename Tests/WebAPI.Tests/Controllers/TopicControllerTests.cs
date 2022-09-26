using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using Global.Shared.Commons;
using Global.Shared.ViewModels.TopicViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class TopicControllerTests: SetupTest
    {
        private readonly TopicsController _topicsController;

        public TopicControllerTests()
        {
            _topicsController = new TopicsController(_topicServiceMock.Object);
        }

        [Fact]
        public async Task GetAllTopic_Test_ShouldReturnCorrentData()
        {
            //arrange
            var mocks = _fixture.Build<Pagination<TopicViewModel>>().Create();
            _topicServiceMock.Setup(x => x.GetAllTopicAsync(0,10)).ReturnsAsync(mocks);
            var result = await _topicsController.GetAllTopic(0,10);
            Assert.Equal(mocks, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task GetTopicById_Test_ShouldReturnCorrentData()
        {
            var mocks = _fixture.Build<TopicViewModel>().Create();
            var topic = _mapperConfig.Map<Topic>(mocks);
            _topicServiceMock.Setup(x => x.GetTopicByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mocks);
            var result = await _topicsController.GetTopicById(It.IsAny<Guid>());
            Assert.Equal(mocks, ((ObjectResult)result).Value);
        }

        [Fact]
        public async Task CreateTopic_Test_CompareTwoReturnDataTypes_WhenSuccessSaved()
        {
            var topicModel = _fixture.Build<CreateTopicViewModel>().Create();
            var topic = _mapperConfig.Map<Topic>(topicModel);
            var expectedResult = _mapperConfig.Map<TopicViewModel>(topic);
            _topicServiceMock.Setup(x => x.AddTopicAsync(topicModel)).ReturnsAsync(expectedResult);
            var result = await _topicsController.AddTopic(topicModel);
            Assert.Equal(expectedResult, ((ObjectResult)result).Value);
        }
        [Fact]
        public async Task UpdateTopic_Test_ShouldReturnCorrentData_WhenSuccessSaved()
        {
            var topicUpdateModel = _fixture.Build<UpdateTopicViewModel>().Create();
            var topic = _mapperConfig.Map<Topic>(topicUpdateModel);
            var expectedResult = _mapperConfig.Map<TopicViewModel>(topic);
            _topicServiceMock.Setup(x => x.UpdateTopicAsync(It.IsAny<Guid>(), topicUpdateModel)).ReturnsAsync(expectedResult);
            var result = await _topicsController.UpdateTopic(It.IsAny<Guid>(), topicUpdateModel);
            Assert.Equal(expectedResult, ((ObjectResult)result).Value);
        }
        [Fact]
        public async Task GetTopicByModule_Test_ShouldReturnCorrentData()
        {
            var topic = _fixture.Build<TopicViewModel>().CreateMany(10).ToList();
            var topicModule = _mapperConfig.Map<List<Topic>>(topic);
            _topicServiceMock.Setup(x => x.GetTopicByModuleIdAsync(It.IsAny<Guid>())).ReturnsAsync(topic);
            var result = await _topicsController.GetTopicByModuleId(It.IsAny<Guid>());
            Assert.Equal(topic, ((ObjectResult)result).Value);
        }
    }
}
