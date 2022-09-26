using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels.TopicViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class TopicServiceTests : SetupTest
    {
        private readonly ITopicService _topicService;

        public TopicServiceTests()
        {
            _topicService = new TopicService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetAllTopic_Test_ShouldReturnCorrentData()
        {
            var mocks = _fixture.Build<Pagination<TopicViewModel>>().Create();
            var topicListModel = _mapperConfig.Map<Pagination<Topic>>(mocks);
            _unitOfWorkMock.Setup(x => x.TopicRepository.FindAsync(null, null, 0, 10)).ReturnsAsync(topicListModel);
            var result = await _topicService.GetAllTopicAsync(0, 10);
            result.Should().BeEquivalentTo(mocks);
        }
        [Fact]
        public async Task GetTopicById_Test_ShouldReturnCorrentData()
        {
            var mocks = _fixture.Build<TopicViewModel>().Create();
            var topicModel = _mapperConfig.Map<Topic>(mocks);
            _unitOfWorkMock.Setup(x => x.TopicRepository.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(topicModel);
            var result = await _topicService.GetTopicByIdAsync(It.IsAny<Guid>());
            result.Should().BeEquivalentTo(mocks);
        }
        [Fact]
        public async Task GetTopicByModule_Test_ShouldReturnCorrentData()
        {
            var mocks = _fixture.Build<TopicViewModel>().CreateMany(100).ToList();
            var topicList = _mapperConfig.Map<List<Topic>>(mocks);
            _unitOfWorkMock.Setup(x => x.TopicRepository.GetByModuleId(It.IsAny<Guid>()))
                .ReturnsAsync(topicList);
            var result = await _topicService.GetTopicByModuleIdAsync(It.IsAny<Guid>());
            result.Should().BeEquivalentTo(mocks);
        }
        [Fact]
        public async Task AddTopic_Test_CompareTwoReturnDataTypes_WhenSuccessSaved()
        {
            //Arrange
            var mocks = _fixture.Build<CreateTopicViewModel>().Create();
            var mockModule = _fixture.Build<Module>()
                                     .Without(x => x.Topics)
                                     .Without(x => x.Plan)
                                     .Create();
            var topic = _mapperConfig.Map<Topic>(mocks);
            var expectedResult = _mapperConfig.Map<TopicViewModel>(topic);
            _unitOfWorkMock.Setup(x => x.ModuleRepository.GetByIdAsync(mocks.ModuleId))
                           .ReturnsAsync(mockModule);
            _unitOfWorkMock.Setup(x => x.TopicRepository.AddAsync(topic));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);

            //Act
            var result = await _topicService.AddTopicAsync(mocks);
            result.Id = expectedResult.Id;

            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task AddTopic_Test_MustThrowAppException_WhenModuleReturnsNull()
        {
            //Arrange
            var mocks = _fixture.Build<CreateTopicViewModel>().Create();
            var mockModule = (Module?)null;
            _unitOfWorkMock.Setup(x => x.ModuleRepository.GetByIdAsync(mocks.ModuleId))
                           .ReturnsAsync(mockModule);

            //Act
            var result = () => _topicService.AddTopicAsync(mocks);

            //Assert
            await result.Should().ThrowAsync<AppException>()
                                 .WithMessage(Constant.EXCEPTION_MODULE_NOT_FOUND);
        }

        [Fact]
        public async Task AddTopic_Test_MustThrowAppException_WhenCantSave()
        {
            //Arrange
            var mocks = _fixture.Build<CreateTopicViewModel>().Create();
            var mockModule = _fixture.Build<Module>()
                                     .Without(x => x.Topics)
                                     .Without(x => x.Plan)
                                     .Create();
            var topic = _mapperConfig.Map<Topic>(mocks);

            _unitOfWorkMock.Setup(x => x.ModuleRepository.GetByIdAsync(mocks.ModuleId))
                           .ReturnsAsync(mockModule);
            _unitOfWorkMock.Setup(x => x.TopicRepository.AddAsync(topic));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

            //Act
            var result = () => _topicService.AddTopicAsync(mocks);

            //Assert
            await result.Should().ThrowAsync<AppException>()
                                 .WithMessage(Constant.EXCEPTION_SAVECHANGE_FAILED);
        }

        [Fact]
        public async Task UpdateTopic_Test_ShouldReturnCorrentData_WhenSuccessSaved()
        {
            //Arrange
            var mocks = _fixture.Build<UpdateTopicViewModel>().Create();
            var mockTopicId = Guid.NewGuid();
            var mockTopic = _fixture.Build<Topic>()
                                    .With(x => x.Id, mockTopicId)
                                    .Without(x => x.Module)
                                    .Without(x => x.ChapterSyllabuses)
                                    .Create();
            var mockUpdatedTopic = _mapperConfig.Map(mocks, mockTopic);
            var expectedResult = _mapperConfig.Map<TopicViewModel>(mockUpdatedTopic);

            _unitOfWorkMock.Setup(x => x.TopicRepository.GetByIdAsync(mockTopicId))
                           .ReturnsAsync(mockTopic);
            _unitOfWorkMock.Setup(x => x.TopicRepository.Update(mockUpdatedTopic));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);

            //Act
            var resultUpdate = await _topicService.UpdateTopicAsync(mockTopicId, mocks);
            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            _unitOfWorkMock.Verify(x => x.TopicRepository
                                         .GetByIdAsync(mockTopicId), Times.Once());
            _unitOfWorkMock.Verify(x => x.TopicRepository
                                         .Update(mockUpdatedTopic), Times.Once());
            resultUpdate.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateTopic_Test_MustThrowAppException_WhenTopicReturnsNull()
        {
            //Arrange
            var mocks = _fixture.Build<UpdateTopicViewModel>().Create();
            var mockTopicId = Guid.NewGuid();
            var mockTopic = (Topic?)null;

            _unitOfWorkMock.Setup(x => x.TopicRepository.GetByIdAsync(mockTopicId))
                           .ReturnsAsync(mockTopic);

            //Act
            var resultUpdate = () =>  _topicService.UpdateTopicAsync(mockTopicId, mocks);

            //Assert
            await resultUpdate.Should().ThrowAsync<AppException>()
                                       .WithMessage(Constant.EXCEPTION_TOPIC_NOT_FOUND);
        }

        [Fact]
        public async Task UpdateTopic_Test_MustThrowAppException_WhenCantSave()
        {
            //Arrange
            var mocks = _fixture.Build<UpdateTopicViewModel>().Create();
            var mockTopicId = Guid.NewGuid();
            var mockTopic = _fixture.Build<Topic>()
                                    .With(x => x.Id, mockTopicId)
                                    .Without(x => x.Module)
                                    .Without(x => x.ChapterSyllabuses)
                                    .Create();
            var mockUpdatedTopic = _mapperConfig.Map(mocks, mockTopic);

            _unitOfWorkMock.Setup(x => x.TopicRepository.GetByIdAsync(mockTopicId))
                           .ReturnsAsync(mockTopic);
            _unitOfWorkMock.Setup(x => x.TopicRepository.Update(mockUpdatedTopic));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

            //Act
            var resultUpdate = () => _topicService.UpdateTopicAsync(mockTopicId, mocks);

            //Assert
            await resultUpdate.Should().ThrowAsync<AppException>()
                                       .WithMessage(Constant.EXCEPTION_SAVECHANGE_FAILED);
        }
    }
}
