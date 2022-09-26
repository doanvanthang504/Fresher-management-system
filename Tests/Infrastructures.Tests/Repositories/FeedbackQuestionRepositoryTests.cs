using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.FeedbackViewModels;
using Infrastructures.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class FeedbackQuestionRepositoryTests : SetupTest
    {
        private readonly IFeedbackQuestionRepository _feedbackQuestionRepository;

        public FeedbackQuestionRepositoryTests()
        {
            _feedbackQuestionRepository = new FeedbackQuestionRepository(
                                                _dbContext,
                                                _currentTimeMock.Object,
                                                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<FeedbackQuestion>()
                                    .Without(x => x.FeedbackResults)
                                    .Without(x => x.Feedback)
                                    .Without(x=>x.FeedbackAnswers)
                                    .With(x => x.IsDeleted, false)
                                    .CreateMany(10)
                                    .ToList();
            await _dbContext.FeedbackQuestions.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _feedbackQuestionRepository.GetAllAsync();

            // assert
            result.Should().BeEquivalentTo(mockData);
        }

        //[Fact]
        //public async Task SearchAsync_WithFilterTitle_ShouldReturnCorrectData()
        //{
        //    // arrange

        //    var mockTitleBeAssigned = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.Feedback)
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.FeedbackAnswers)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .With(x => x.Title, "Title Test")
        //                                        .CreateMany(10)
        //                                        .ToList();
        //    var mockData = new List<FeedbackQuestion>(mockTitleBeAssigned);
        //    var mockResultPagination = _fixture.Build<Pagination<FeedbackQuestion>>()
        //                            .With(x => x.Items, mockTitleBeAssigned)
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.TotalItemsCount, 10)
        //                            .Create();
        //    var mockDataSalt = _fixture.Build<FeedbackQuestion>()
        //                                .Without(x => x.FeedbackResults)
        //                                .Without(x => x.Feedback)
        //                                .Without(x => x.FeedbackAnswers)
        //                                .With(x => x.IsDeleted, false)
        //                                .CreateMany(90)
        //                                .ToList();
        //    mockData.AddRange(mockDataSalt);
        //    var mockQuery = _fixture.Build<SearchFeedbackQuestionViewModel>()
        //                            .With(x => x.Title, "Title T")
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .Without(x => x.CreateBy)
        //                            .Without(x => x.CreationDate)
        //                            .Without(x => x.FeedbackId)
        //                            .Create();
        //    await _dbContext.FeedbackQuestions.AddRangeAsync(mockData);
        //    await _dbContext.SaveChangesAsync();

        //    // act
        //    var result = await _feedbackQuestionRepository.SearchAsync(mockQuery);

        //    // assert
        //    result.Should().BeEquivalentTo(mockResultPagination);
        //}

        //[Fact]
        //public async Task SearchAsync_WithFilterCreateDate_ShouldReturnCorrectData()
        //{
        //    // arrange
        //    var currentTime = _currentTimeMock.Object.GetCurrentTime();
        //    var mockTitleBeAssigned = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.Feedback)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .With(x => x.CreationDate, currentTime)
        //                                        .CreateMany(10)
        //                                        .ToList();
        //    var mockData = new List<FeedbackQuestion>(mockTitleBeAssigned);
        //    var mockDataSalt = _fixture.Build<FeedbackQuestion>()
        //                                .Without(x => x.FeedbackResults)
        //                                .Without(x => x.Feedback)
        //                                .With(x => x.IsDeleted, false)
        //                                .With(x => x.CreationDate, currentTime.AddDays(1))
        //                                .CreateMany(90)
        //                                .ToList();
        //    var mockResultPagination = _fixture.Build<Pagination<FeedbackQuestion>>()
        //                            .With(x => x.Items, mockTitleBeAssigned)
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.TotalItemsCount, 10)
        //                            .Create();
        //    mockData.AddRange(mockDataSalt);

        //    await _dbContext.FeedbackQuestions.AddRangeAsync(mockData);
        //    await _dbContext.SaveChangesAsync();

        //    var mockQuery = _fixture.Build<SearchFeedbackQuestionViewModel>()
        //                .With(x => x.CreationDate, currentTime)
        //                .With(x => x.PageIndex, 0)
        //                .With(x => x.PageSize, 100)
        //                .Without(x => x.CreateBy)
        //                .Without(x => x.Title)
        //                .Without(x => x.FeedbackId)
        //                .Create();
        //    // act
        //    var result = await _feedbackQuestionRepository.SearchAsync(mockQuery);

        //    // assert
        //    result.Should().BeEquivalentTo(mockResultPagination);
        //}

        //[Fact]
        //public async Task SearchAsync_WithFilterFeedbackId_ShouldReturnCorrectData()
        //{
        //    // arrange
        //    var mockFeedback = _fixture.Build<Feedback>()
        //                  .Without(x => x.FeedbackQuestions)
        //                  .Without(x => x.FeedbackResults)
        //                  .With(x => x.IsDeleted, false)
        //                  .Create();
        //    var mockFeedbackIdBeAssigned = _fixture.Build<FeedbackQuestion>()
        //                            .With(x => x.FeedbackId, mockFeedback.Id)
        //                            .With(x => x.IsDeleted, false)
        //                            .With(x => x.Title, "Title Test")
        //                            .Without(x => x.FeedbackResults)
        //                            .Without(x => x.Feedback)
        //                            .CreateMany(10)
        //                            .ToList();
        //    var mockData = new List<FeedbackQuestion>(mockFeedbackIdBeAssigned);
        //    var mockDataSalt = _fixture.Build<FeedbackQuestion>()
        //                            .Without(x => x.FeedbackResults)
        //                            .Without(x => x.Feedback)
        //                            .With(x => x.IsDeleted, false)
        //                            .CreateMany(90)
        //                            .ToList();
        //    mockData.AddRange(mockDataSalt);
        //    var mockQuery = _fixture.Build<SearchFeedbackQuestionViewModel>()
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.FeedbackId, mockFeedback.Id)
        //                            .Without(x => x.Title)
        //                            .Without(x => x.CreateBy)
        //                            .Without(x => x.CreationDate)
        //                            .Create();
        //    await _dbContext.Feedbacks.AddAsync(mockFeedback);
        //    await _dbContext.FeedbackQuestions.AddRangeAsync(mockData);
        //    await _dbContext.SaveChangesAsync();

        //    mockFeedbackIdBeAssigned = mockFeedbackIdBeAssigned.Select(x => new FeedbackQuestion
        //    {
        //        Id = x.Id,
        //        Content = x.Content,
        //        FeedbackId = x.FeedbackId,
        //        Title = x.Title,
        //        CreationDate = x.CreationDate,
        //        CreatedBy = x.CreatedBy,
        //        DeleteBy = x.DeleteBy,
        //        DeletionDate = x.DeletionDate,
        //        Description = x.Description,
        //        IsDeleted = x.IsDeleted,
        //        ModificationBy = x.ModificationBy,
        //        ModificationDate = x.ModificationDate
        //    }).ToList();
        //    var mockResultPagination = _fixture.Build<Pagination<FeedbackQuestion>>()
        //                            .With(x => x.Items, mockFeedbackIdBeAssigned)
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.TotalItemsCount, 10)
        //                            .Create();
        //    // act
        //    var result = await _feedbackQuestionRepository.SearchAsync(mockQuery);

        //    // assert
        //    result.Should().BeEquivalentTo(mockResultPagination);
        //}
    }
}
