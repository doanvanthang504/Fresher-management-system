using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class FeedbackControllerTests : SetupTest
    {
        private readonly FeedbackController _feedbackController;

        public FeedbackControllerTests()
        {
            _feedbackController = new FeedbackController(_feedbackServiceMock.Object);
        }

        [Fact]
        public async Task CreateFeedbackQuestionAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackQuestionViewModel>().Create();
            var mockCreateFeedbackQuestionViewModel = _fixture.Build<CreateFeedbackQuestionViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.CreateFeedbackQuestionAsync(
                                                            It.IsAny<CreateFeedbackQuestionViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController.CreateFeedbackQuestionAsync(
                                                            mockCreateFeedbackQuestionViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.CreateFeedbackQuestionAsync(
                    It.Is<CreateFeedbackQuestionViewModel>(x => x.Equals(mockCreateFeedbackQuestionViewModel))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

       
         [Fact]
        public async Task CreateFeedbackResultAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackResultViewModel>().Create();
            var mockCreateFeedbackResultViewModel = _fixture.Build<CreateFeedbackResultViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.CreateFeedbackResultAsync(
                                                            It.IsAny<CreateFeedbackResultViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController.CreateFeedbackResultAsync(
                                                            mockCreateFeedbackResultViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.CreateFeedbackResultAsync(
                    It.Is<CreateFeedbackResultViewModel>(x => x.Equals(mockCreateFeedbackResultViewModel))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task CreateFeedbackAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackViewModel>().Create();
            var mockCreateFeedbackViewModel = _fixture.Build<CreateFeedbackViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.CreateFeedbackAsync(
                                                            It.IsAny<CreateFeedbackViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController.CreateFeedbackAsync(
                                                            mockCreateFeedbackViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.CreateFeedbackAsync(
                    It.Is<CreateFeedbackViewModel>(x => x.Equals(mockCreateFeedbackViewModel))),
                    Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task UpdateFeedbackAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackViewModel>().Create();
            var mockUpdateFeedbackViewModel = _fixture.Build<UpdateFeedbackViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.UpdateFeedbackAsync(
                                                            It.IsAny<UpdateFeedbackViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController.UpdateFeedbackAsync(
                                                            mockUpdateFeedbackViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.UpdateFeedbackAsync(
                    It.Is<UpdateFeedbackViewModel>(x => x.Equals(mockUpdateFeedbackViewModel))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task UpdateFeedbackQuestionAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackQuestionViewModel>().Create();
            var mockUpdateFeedbackViewModel = _fixture.Build<UpdateFeedbackQuestionViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.UpdateFeedbackQuestionAsync(
                                                            It.IsAny<UpdateFeedbackQuestionViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController.UpdateFeedbackQuestionAsync(
                                                            mockUpdateFeedbackViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.UpdateFeedbackQuestionAsync(
                    It.Is<UpdateFeedbackQuestionViewModel>(x => x.Equals(mockUpdateFeedbackViewModel))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task GetFeedbackByIdAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackViewModel>().Create();

            // arrange
            _feedbackServiceMock.Setup(x => x.GetFeedbackByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(mocks);

            var guid = Guid.NewGuid();
            // act
            var result = await _feedbackController.GetFeedbackByIdAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.GetFeedbackByIdAsync(
                    It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        } 
        
        [Fact]
        public async Task GetFeedbackResultByIdAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackResultViewModel>()
                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.GetFeedbackResultByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(mocks);

            var guid = Guid.NewGuid();
            // act
            var result = await _feedbackController.GetFeedbackResultByIdAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.GetFeedbackResultByIdAsync(
                    It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task GetFeedbackQuestionByIdAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackQuestionViewModel>().Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.GetFeedbackQuestionByIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(mocks);

            var guid = Guid.NewGuid();
            // act
            var result = await _feedbackController.GetFeedbackQuestionByIdAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.GetFeedbackQuestionByIdAsync(
                    It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }

        [Fact]
        public async Task GetAllResultOfFeedbackAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FeedbackResultViewModel>().CreateMany(100).ToList();
            // arrange
            _feedbackServiceMock.Setup(x => x.GetAllResultOfFeedbackByFeedbackIdAsync(It.IsAny<Guid>()))
                                .ReturnsAsync(mocks);

            var guid = Guid.NewGuid();
            // act
            var result = await _feedbackController.GetAllResultOfFeedbackAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.GetAllResultOfFeedbackByFeedbackIdAsync(
                    It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(mocks);
        }
        
        [Fact]
        public async Task SearchFeedbackQuestionAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<Pagination<FeedbackQuestionViewModel>>().Create();
            var mockSearchFeedbackQuestionViewModel = _fixture.Build<SearchFeedbackQuestionViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.SearchFeedbackQuestionAsync(
                                                It.IsAny<SearchFeedbackQuestionViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController
                                    .SearchFeedbackQuestionAsync(mockSearchFeedbackQuestionViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.SearchFeedbackQuestionAsync(It.Is<SearchFeedbackQuestionViewModel>(
                                                        x => x.Equals(mockSearchFeedbackQuestionViewModel))),
                Times.Once());

            result.As<OkObjectResult>()
                    .Value
                    .Should()
                    .BeEquivalentTo(mocks);
        } 
        
        [Fact]
        public async Task SearchFeedbackResultAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<Pagination<FeedbackResultViewModel>>().Create();
            var mockSearchFeedbackQuestionViewModel = _fixture.Build<SearchFeedbackResultViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.SearchFeedbackResultAsync(
                                                It.IsAny<SearchFeedbackResultViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController
                                    .SearchFeedbackResultAsync(mockSearchFeedbackQuestionViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.SearchFeedbackResultAsync(It.Is<SearchFeedbackResultViewModel>(
                                                        x => x.Equals(mockSearchFeedbackQuestionViewModel))),
                Times.Once());

            result.As<OkObjectResult>()
                    .Value
                    .Should()
                    .BeEquivalentTo(mocks);
        } 
        
        [Fact]
        public async Task SearchFeedbackAsync_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<Pagination<FeedbackViewModel>>().Create();
            var mockSearchFeedbackQuestionViewModel = _fixture.Build<SearchFeedbackViewModel>()
                                                                .Create();
            // arrange
            _feedbackServiceMock.Setup(x => x.SearchFeedbackAsync(
                                                It.IsAny<SearchFeedbackViewModel>()))
                                .ReturnsAsync(mocks);

            // act
            var result = await _feedbackController
                                    .SearchFeedbackAsync(mockSearchFeedbackQuestionViewModel);

            // assert
            _feedbackServiceMock.Verify(
                x => x.SearchFeedbackAsync(It.Is<SearchFeedbackViewModel>(
                                                        x => x.Equals(mockSearchFeedbackQuestionViewModel))),
                Times.Once());

            result.As<OkObjectResult>()
                    .Value
                    .Should()
                    .BeEquivalentTo(mocks);
        }    
        
        [Fact]
        public async Task DeleteFeedbackAsync_ShouldReturnCorrectData()
        {
            var guid = Guid.NewGuid();
            // arrange
            _feedbackServiceMock.Setup(x => x.DeleteFeedbackAsync(guid))
                                .Returns(Task.CompletedTask);

            // act
            var result = await _feedbackController.DeleteFeedbackAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.DeleteFeedbackAsync(It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

        }

        [Fact]
        public async Task DeleteFeedbackQuestionAsync_ShouldReturnCorrectData()
        {
            var guid = Guid.NewGuid();
            // arrange
            _feedbackServiceMock.Setup(x => x.DeleteFeedbackQuestionAsync(guid))
                                .Returns(Task.CompletedTask);

            // act
            var result = await _feedbackController.DeleteFeedbackQuestionAsync(guid);

            // assert
            _feedbackServiceMock.Verify(
                x => x.DeleteFeedbackQuestionAsync(It.Is<Guid>(x => x.Equals(guid))),
                Times.Once());

        }
    }
}
