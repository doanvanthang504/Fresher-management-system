using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class FeedbackServiceTests : SetupTest
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackServiceTests()
        {
            _feedbackService = new FeedbackService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetFeedbackByIdAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<Feedback>().Without(x => x.FeedbackQuestions).Without(x => x.FeedbackResults).Create();
            var expectedResult = _mapperConfig.Map<FeedbackViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.GetByIdAsync(mock.Id)).ReturnsAsync(mock);

            //act
            var result = await _feedbackService.GetFeedbackByIdAsync(mock.Id);

            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository.GetByIdAsync(mock.Id), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        //[Fact]
        //public async Task GetFeedbackQuestionByIdAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mock = _fixture.Build<FeedbackQuestion>()
        //                        .Without(x => x.Feedback)
        //                        .Without(x => x.FeedbackResults)
        //                        .Create();
        //    var expectedResult = _mapperConfig.Map<FeedbackQuestionViewModel>(mock);

        //    _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.GetByIdAsync(mock.Id)).ReturnsAsync(mock);

        //    //act
        //    var result = await _feedbackService.GetFeedbackQuestionByIdAsync(mock.Id);

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.GetByIdAsync(mock.Id), Times.Once());
        //    result.Should().BeEquivalentTo(expectedResult);
        //}

        [Fact]
        public async Task GetFeedbackResultByIdAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<FeedbackResult>().Without(x => x.Feedback).Without(x => x.FeedbackQuestion).Create();
            var expectedResult = _mapperConfig.Map<FeedbackResultViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.FeedbackResultRepository.GetByIdAsync(mock.Id)).ReturnsAsync(mock);

            //act
            var result = await _feedbackService.GetFeedbackResultByIdAsync(mock.Id);

            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackResultRepository.GetByIdAsync(mock.Id), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CreateFeedbackQuestionAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mockCreateFeedbackQuestionViewModel = _fixture.Build<CreateFeedbackQuestionViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.AddAsync(It.IsAny<FeedbackQuestion>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(1);
            //act
            var result = await _feedbackService
                                .CreateFeedbackQuestionAsync(mockCreateFeedbackQuestionViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.AddAsync(It.IsAny<FeedbackQuestion>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateFeedbackAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mockCreateFeedbackViewModel = _fixture.Build<CreateFeedbackViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.AddAsync(It.IsAny<Feedback>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(1);
            //act
            var result = await _feedbackService
                                .CreateFeedbackAsync(mockCreateFeedbackViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository.AddAsync(It.IsAny<Feedback>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateFeedbackResultAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mockCreateFeedbackResultViewModel = _fixture.Build<CreateFeedbackResultViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackResultRepository.AddAsync(It.IsAny<FeedbackResult>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(1);
            //act
            var result = await _feedbackService
                                .CreateFeedbackResultAsync(mockCreateFeedbackResultViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackResultRepository.AddAsync(It.IsAny<FeedbackResult>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        }

        //[Fact]
        //public async Task UpdateFeedbackQuestionAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.Feedback)
        //                                        .Create();
        //    var mockUpdateFeedbackQuestionViewModel = _fixture.Build<UpdateFeedbackQuestionViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.Update(It.IsAny<FeedbackQuestion>()));
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackQuestionRepository
        //                .GetByIdAsync(mockUpdateFeedbackQuestionViewModel.FeedbackId))
        //                .ReturnsAsync(mockFeedBackQuestion);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);

        //    _mapperConfig.Map(mockUpdateFeedbackQuestionViewModel, mockFeedBackQuestion);
        //    var expectedResult = _mapperConfig.Map<FeedbackQuestionViewModel>(mockFeedBackQuestion);

        //    //act
        //    var result = await _feedbackService
        //                        .UpdateFeedbackQuestionAsync(mockUpdateFeedbackQuestionViewModel);
        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.Update(It.IsAny<FeedbackQuestion>()), Times.Once());
        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //    result.Should().BeEquivalentTo(expectedResult);
        //}

        [Fact]
        public async Task UpdateFeedbackAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mockFeedBack = _fixture.Build<Feedback>()
                                                .Without(x => x.FeedbackQuestions)
                                                .Without(x => x.FeedbackResults)
                                                .Create();
            var mockUpdateFeedbackViewModel = _fixture.Build<UpdateFeedbackViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.Update(It.IsAny<Feedback>()));
            _unitOfWorkMock.Setup(
                x => x.FeedbackRepository
                        .GetByIdAsync(mockUpdateFeedbackViewModel.FeedbackId))
                        .ReturnsAsync(mockFeedBack);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(1);

            _mapperConfig.Map(mockUpdateFeedbackViewModel, mockFeedBack);
            var expectedResult = _mapperConfig.Map<FeedbackViewModel>(mockFeedBack);

            //act
            var result = await _feedbackService
                                .UpdateFeedbackAsync(mockUpdateFeedbackViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository.Update(It.IsAny<Feedback>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        //[Fact]
        //public async Task DeleteFeedbackAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mockFeedBack = _fixture.Build<Feedback>()
        //                                        .Without(x => x.FeedbackQuestions)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackRepository
        //                .GetByIdAsync(mockFeedBack.Id))
        //                .ReturnsAsync(mockFeedBack);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);
        //    //act
        //    await _feedbackService.DeleteFeedbackAsync(mockFeedBack.Id);

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackRepository.SoftRemove(It.IsAny<Feedback>()), Times.Once());
        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //}

        //[Fact]
        //public async Task DeleteFeedbackQuestionAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.Feedback)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackQuestionRepository
        //                .GetByIdAsync(mockFeedBackQuestion.Id))
        //                .ReturnsAsync(mockFeedBackQuestion);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);
        //    //act
        //    await _feedbackService.DeleteFeedbackQuestionAsync(mockFeedBackQuestion.Id);

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.SoftRemove(It.IsAny<FeedbackQuestion>()), Times.Once());
        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //}

        [Fact]
        public async Task SearchFeedbackAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mocksFeedBackWithTitleIsAssigned = _fixture.Build<Feedback>()
                                                .Without(x => x.FeedbackQuestions)
                                                .Without(x => x.FeedbackResults)
                                                .With(x => x.Title, "Title Test")
                                                .CreateMany(10)
                                                .ToList();
            var listMock = new List<Feedback>(mocksFeedBackWithTitleIsAssigned);
            var mocksFeedBack = _fixture.Build<Feedback>()                            
                                        .Without(x => x.FeedbackQuestions)
                                        .Without(x => x.FeedbackResults)
                                        .CreateMany(90);
            var mockResultPagination = _fixture.Build<Pagination<Feedback>>()
                                                .With(x => x.Items, mocksFeedBackWithTitleIsAssigned)
                                                .With(x => x.PageIndex, 0)
                                                .With(x => x.PageSize, 10)
                                                .With(x => x.TotalItemsCount, 10)
                                                .Create();
            var expectedResult = _mapperConfig.Map<Pagination<FeedbackViewModel>>(mockResultPagination);
            listMock.AddRange(mocksFeedBack);
            var mockSearchFeedbackViewModel = _fixture.Build<SearchFeedbackViewModel>()
                                .Without(x => x.CreationDate)
                                .Without(x => x.CreateBy)
                                .With(x => x.Title, "Title")
                                .Create();
            _unitOfWorkMock.Setup(x => x.FeedbackRepository.SearchAsync(mockSearchFeedbackViewModel))
                        .ReturnsAsync(mockResultPagination);
            //act
            var result = await _feedbackService.SearchFeedbackAsync(mockSearchFeedbackViewModel);

            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository.SearchAsync(mockSearchFeedbackViewModel), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        //[Fact]
        //public async Task SearchFeedbackQuestionAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mocksFeedBackWithQuestionTitleIsAssigned = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.Feedback)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.Title, "Title Test")
        //                                        .CreateMany(10)
        //                                        .ToList();
        //    var listMock = new List<FeedbackQuestion>(mocksFeedBackWithQuestionTitleIsAssigned);
        //    var mocksFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                .Without(x => x.Feedback)
        //                                .Without(x => x.FeedbackResults)
        //                                .CreateMany(90);
        //    var mockResultPagination = _fixture.Build<Pagination<FeedbackQuestion>>()
        //                            .With(x => x.Items, mocksFeedBackWithQuestionTitleIsAssigned)
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 10)
        //                            .With(x => x.TotalItemsCount, 10)
        //                            .Create();
        //    var expectedResult = _mapperConfig.Map<Pagination<FeedbackQuestionViewModel>>(mockResultPagination);
        //    listMock.AddRange(mocksFeedBackQuestion);
        //    var mockSearchFeedbackQuestionViewModel = _fixture.Build<SearchFeedbackQuestionViewModel>()
        //                                                        .Without(x => x.CreationDate)
        //                                                        .Without(x => x.CreateBy)
        //                                                        .With(x => x.Title, "Title")
        //                                                        .Create();
        //    _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.SearchAsync(mockSearchFeedbackQuestionViewModel))
        //                .ReturnsAsync(mockResultPagination);
        //    //act
        //    var result = await _feedbackService.SearchFeedbackQuestionAsync(mockSearchFeedbackQuestionViewModel);

        //    //assert
        //    _unitOfWorkMock.Verify(
        //                    x => x.FeedbackQuestionRepository.SearchAsync(mockSearchFeedbackQuestionViewModel),
        //                    Times.Once()
        //    );
        //    result.Should().BeEquivalentTo(expectedResult);
        //}

        [Fact]
        public async Task SearchFeedbackResultAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mocksFeedBackWithAccountNameIsAssigned = _fixture.Build<FeedbackResult>()
                                                .Without(x => x.Feedback)
                                                .Without(x => x.FeedbackQuestion)
                                                .With(x => x.AccountName, "Account Test")
                                                .CreateMany(10)
                                                .ToList();
            var listMock = new List<FeedbackResult>(mocksFeedBackWithAccountNameIsAssigned);
            var mocksFeedBack = _fixture.Build<FeedbackResult>()
                                        .Without(x => x.Feedback)
                                        .Without(x => x.FeedbackQuestion)
                                        .CreateMany(90);
            var mockResultPagination = _fixture.Build<Pagination<FeedbackResult>>()
                                    .With(x => x.Items, mocksFeedBackWithAccountNameIsAssigned)
                                    .With(x => x.PageIndex, 0)
                                    .With(x => x.PageSize, 10)
                                    .With(x => x.TotalItemsCount, 10)
                                    .Create();
            var expectedResult = _mapperConfig.Map<Pagination<FeedbackResultViewModel>>(
                                                    mockResultPagination);
            listMock.AddRange(mocksFeedBack);
            var mockSearchFeedbackResultViewModel = _fixture.Build<SearchFeedbackResultViewModel>()
                                .Without(x => x.CreationDate)
                                .Without(x => x.QuestionId)
                                .With(x => x.AccountName, "Account Test")
                                .Create();
            _unitOfWorkMock.Setup(x => x.FeedbackResultRepository.SearchAsync(mockSearchFeedbackResultViewModel))
                        .ReturnsAsync(mockResultPagination);
            //act
            var result = await _feedbackService.SearchFeedbackResultAsync(mockSearchFeedbackResultViewModel);

            //assert
            _unitOfWorkMock.Verify(
                            x => x.FeedbackResultRepository.SearchAsync(mockSearchFeedbackResultViewModel),
                            Times.Once()
            );
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task CreateFeedbackAsync_ShouldSaveChangeAsyncFailed()
        {
            //arrange
            var mockCreateFeedbackViewModel = _fixture.Build<CreateFeedbackViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.AddAsync(It.IsAny<Feedback>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(0);
            //act
            var result = await _feedbackService
                                .CreateFeedbackAsync(mockCreateFeedbackViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository.AddAsync(It.IsAny<Feedback>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateFeedbackResultAsync_ShouldSaveChangeAsyncFailed()
        {
            //arrange
            var mockCreateFeedbackResultViewModel = _fixture.Build<CreateFeedbackResultViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackResultRepository.AddAsync(It.IsAny<FeedbackResult>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(0);
            //act
            var result = await _feedbackService
                                .CreateFeedbackResultAsync(mockCreateFeedbackResultViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackResultRepository.AddAsync(It.IsAny<FeedbackResult>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateFeedbackQuestionAsync_ShouldSaveChangeAsyncFailed()
        {
            //arrange
            var mockCreateFeedbackQuestionViewModel = _fixture.Build<CreateFeedbackQuestionViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.AddAsync(It.IsAny<FeedbackQuestion>()))
                            .Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
                            .ReturnsAsync(0);
            //act
            var result = await _feedbackService
                                .CreateFeedbackQuestionAsync(mockCreateFeedbackQuestionViewModel);
            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.AddAsync(It.IsAny<FeedbackQuestion>()), Times.Once());
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeNull();
        }

        //[Fact]
        //public async Task UpdateFeedbackQuestionAsync_ShouldNotFoundFeedbackQuestion()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.Feedback)
        //                                        .Create();
        //    var mockUpdateFeedbackQuestionViewModel = _fixture.Build<UpdateFeedbackQuestionViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.Update(It.IsAny<FeedbackQuestion>())); _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);

        //    _mapperConfig.Map(mockUpdateFeedbackQuestionViewModel, mockFeedBackQuestion);
        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(() => _feedbackService
        //                        .UpdateFeedbackQuestionAsync(mockUpdateFeedbackQuestionViewModel));
        //    //assert
        //    Assert.Equal($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_QUESTION} {mockFeedBackQuestion.Id}", ex.Message);
        //}

        //[Fact]
        //public async Task UpdateFeedbackAsync_ShouldNotFoundFeedback()
        //{
        //    //arrange
        //    var mockFeedBack = _fixture.Build<Feedback>()
        //                                        .Without(x => x.FeedbackQuestions)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Create();
        //    var mockUpdateFeedbackViewModel = _fixture.Build<UpdateFeedbackViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.FeedbackRepository.Update(It.IsAny<Feedback>()));
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);

        //    _mapperConfig.Map(mockUpdateFeedbackViewModel, mockFeedBack);
        //    var expectedResult = _mapperConfig.Map<FeedbackViewModel>(mockFeedBack);

        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(() => _feedbackService
        //                        .UpdateFeedbackAsync(mockUpdateFeedbackViewModel));
        //    //assert
        //    Assert.Equal($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {mockFeedBack.Id}", ex.Message);
        //}

        //[Fact]
        //public async Task UpdateFeedbackQuestionAsync_ShouldReturnNullable()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.Feedback)
        //                                        .Create();
        //    var mockUpdateFeedbackQuestionViewModel = _fixture.Build<UpdateFeedbackQuestionViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.FeedbackQuestionRepository.Update(It.IsAny<FeedbackQuestion>()));
        //    _unitOfWorkMock.Setup(
        //       x => x.FeedbackQuestionRepository
        //               .GetByIdAsync(mockUpdateFeedbackQuestionViewModel.FeedbackId))
        //               .ReturnsAsync(mockFeedBackQuestion);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(0);

        //    _mapperConfig.Map(mockUpdateFeedbackQuestionViewModel, mockFeedBackQuestion);
        //    //act
        //    var result = await _feedbackService
        //                          .UpdateFeedbackQuestionAsync(mockUpdateFeedbackQuestionViewModel);

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackQuestionRepository.Update(mockFeedBackQuestion), Times.Once());
        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //    result.Should().BeNull();
        //}

        //[Fact]
        //public async Task UpdateFeedbackAsync_ShouldReturnNullable()
        //{
        //    //arrange
        //    var mockFeedBack = _fixture.Build<Feedback>()
        //                                        .Without(x => x.FeedbackQuestions)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Create();
        //    var mockUpdateFeedbackViewModel = _fixture.Build<UpdateFeedbackViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.FeedbackRepository.Update(It.IsAny<Feedback>()));
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackRepository
        //                .GetByIdAsync(mockUpdateFeedbackViewModel.FeedbackId))
        //                .ReturnsAsync(mockFeedBack);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(0);

        //    _mapperConfig.Map(mockUpdateFeedbackViewModel, mockFeedBack);
        //    var expectedResult = _mapperConfig.Map<FeedbackViewModel>(mockFeedBack);

        //    //act
        //    var result = await _feedbackService
        //                          .UpdateFeedbackAsync(mockUpdateFeedbackViewModel);

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.FeedbackRepository.Update(mockFeedBack), Times.Once());
        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //    result.Should().BeNull();
        //}

        //[Fact]
        //public async Task DeleteFeedbackAsync_ShouldNotFoundFeedback()
        //{
        //    //arrange
        //    var mockFeedBack = _fixture.Build<Feedback>()
        //                                        .Without(x => x.FeedbackQuestions)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackRepository
        //                .GetByIdAsync(mockFeedBack.Id));
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);

        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(
        //                            () => _feedbackService.DeleteFeedbackAsync(mockFeedBack.Id));
        //    //assert
        //    Assert.Equal($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {mockFeedBack.Id}", ex.Message);
        //}

        //[Fact]
        //public async Task DeleteFeedbackQuestionAsync_ShouldNotFoundFeedbackQuestion()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.Feedback)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackQuestionRepository
        //                .GetByIdAsync(mockFeedBackQuestion.Id));
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(1);
        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(
        //                            () => _feedbackService.DeleteFeedbackQuestionAsync(mockFeedBackQuestion.Id));
        //    //assert
        //    Assert.Equal($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK_QUESTION} {mockFeedBackQuestion.Id}", ex.Message);
        //}

        //[Fact]
        //public async Task DeleteFeedbackAsync_ShouldNotRemoveObject()
        //{
        //    //arrange
        //    var mockFeedBack = _fixture.Build<Feedback>()
        //                                        .Without(x => x.FeedbackQuestions)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackRepository
        //                .GetByIdAsync(mockFeedBack.Id))
        //        .ReturnsAsync(mockFeedBack);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(0);

        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(
        //                            () => _feedbackService.DeleteFeedbackAsync(mockFeedBack.Id));
        //    //assert
        //    Assert.Equal($"Can't remove this object", ex.Message);
        //}

        //[Fact]
        //public async Task DeleteFeedbackQuestionAsync_ShouldNotRemoveObject()
        //{
        //    //arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.Feedback)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    _unitOfWorkMock.Setup(
        //        x => x.FeedbackQuestionRepository
        //                .GetByIdAsync(mockFeedBackQuestion.Id))
        //        .ReturnsAsync(mockFeedBackQuestion);
        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync())
        //                    .ReturnsAsync(0);
        //    //act
        //    var ex = await Assert.ThrowsAsync<Exception>(
        //                            () => _feedbackService.DeleteFeedbackQuestionAsync(mockFeedBackQuestion.Id));
        //    //assert
        //    Assert.Equal(Constant.EXCEPTION_REMOVE_FAILED, ex.Message);
        //}

        [Fact]
        public async Task GetAllResultOfFeedbackAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<Feedback>()
                                .Without(x => x.FeedbackQuestions)
                                .Without(x => x.FeedbackResults)
                                .Create();
            var mockFeedbackResults = _fixture.Build<FeedbackResult>()
                                                .Without(x => x.FeedbackQuestion)
                                                .Without(x => x.Feedback)
                                                .With(x=>x.FeedbackId, mock.Id)
                                                .CreateMany(10)
                                                .ToList();
            var expectedResult = _mapperConfig.Map<List<FeedbackResultViewModel>>(mockFeedbackResults);

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.GetByIdAsync(mock.Id))
                            .ReturnsAsync(mock);   
            
            _unitOfWorkMock.Setup(x => x.FeedbackResultRepository
                                        .GetAllResultOfFeedbackAsync(mock.Id))
                            .ReturnsAsync(mockFeedbackResults);

            //act
            var result = await _feedbackService.GetAllResultOfFeedbackByFeedbackIdAsync(mock.Id);

            //assert
            _unitOfWorkMock.Verify(x => x.FeedbackRepository
                                            .GetByIdAsync(mock.Id), Times.Once());
            _unitOfWorkMock.Verify(x => x.FeedbackResultRepository
                                            .GetAllResultOfFeedbackAsync(mock.Id), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllResultOfFeedbackAsync_ShouldNotFoundFeedback()
        {
            //arrange
            var mock = _fixture.Build<Feedback>()
                                .Without(x => x.FeedbackQuestions)
                                .Without(x => x.FeedbackResults)
                                .Create();
            var mockFeedbackResults = _fixture.Build<FeedbackResult>()
                                                .Without(x => x.FeedbackQuestion)
                                                .Without(x => x.Feedback)
                                                .With(x => x.FeedbackId, mock.Id)
                                                .CreateMany(10)
                                                .ToList();
            var expectedResult = _mapperConfig.Map<List<FeedbackResultViewModel>>(mockFeedbackResults);

            _unitOfWorkMock.Setup(x => x.FeedbackRepository.GetByIdAsync(mock.Id));

            //act
            var ex = await Assert.ThrowsAsync<Exception>(
                            () => _feedbackService.GetAllResultOfFeedbackByFeedbackIdAsync(mock.Id));
            //assert
            Assert.Equal($"{Constant.EXCEPTION_NOT_FOUND_FEEDBACK} {mock.Id}", ex.Message);
        }
    }
}
