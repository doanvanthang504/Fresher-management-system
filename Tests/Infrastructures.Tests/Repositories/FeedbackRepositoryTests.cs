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
    public class FeedbackRepositoryTests : SetupTest
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackRepositoryTests()
        {
            _feedbackRepository = new FeedbackRepository(
                                                _dbContext,
                                                _currentTimeMock.Object,
                                                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<Feedback>()
                                    .Without(x => x.FeedbackQuestions)
                                    .Without(x => x.FeedbackResults)
                                    .With(x => x.IsDeleted, false)
                                    .CreateMany(10)
                                    .ToList();
            await _dbContext.Feedbacks.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _feedbackRepository.GetAllAsync();

            // assert
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task SearchAsync_WithFilterTitle_ShouldReturnCorrectData()
        {
            // arrange
            var mockTitleBeAssigned = _fixture.Build<Feedback>()
                                                .Without(x => x.FeedbackResults)
                                                .Without(x => x.FeedbackQuestions)
                                                .With(x => x.IsDeleted, false)
                                                .With(x => x.Title, "Title Test")
                                                .CreateMany(10)
                                                .ToList();
            var mockData = new List<Feedback>(mockTitleBeAssigned);
            var mockDataSalt = _fixture.Build<Feedback>()
                                        .Without(x => x.FeedbackQuestions)
                                        .Without(x => x.FeedbackResults)
                                        .With(x => x.IsDeleted, false)
                                        .CreateMany(90)
                                        .ToList();
            mockData.AddRange(mockDataSalt);
            var mockQuery = _fixture.Build<SearchFeedbackViewModel>()
                                    .With(x => x.Title, "Title T")
                                    .With(x => x.PageIndex, 0)
                                    .With(x => x.PageSize, 100)
                                    .Without(x => x.CreateBy)
                                    .Without(x => x.CreationDate)
                                    .Create();
            await _dbContext.Feedbacks.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();
            var mockResultPagination = _fixture.Build<Pagination<Feedback>>()
                                    .With(x => x.Items, mockTitleBeAssigned)
                                    .With(x => x.PageIndex, 0)
                                    .With(x => x.PageSize, 100)
                                    .With(x => x.TotalItemsCount, 10)
                                    .Create();
            // act
            var result = await _feedbackRepository.SearchAsync(mockQuery);

            // assert
            result.Should().BeEquivalentTo(mockResultPagination);
        }

        [Fact]
        public async Task SearchAsync_WithFilterCreateDate_ShouldReturnCorrectData()
        {
            // arrange
            var currentTime = _currentTimeMock.Object.GetCurrentTime();
            var mockTitleBeAssigned = _fixture.Build<Feedback>()
                                                .Without(x => x.FeedbackQuestions)
                                                .Without(x => x.FeedbackResults)
                                                .With(x => x.IsDeleted, false)
                                                .With(x => x.CreationDate, currentTime)
                                                .CreateMany(10)
                                                .ToList();
            var mockData = new List<Feedback>(mockTitleBeAssigned);
            var mockDataSalt = _fixture.Build<Feedback>()
                                        .Without(x => x.FeedbackQuestions)
                                        .Without(x => x.FeedbackResults)
                                        .With(x => x.IsDeleted, false)
                                        .With(x => x.CreationDate, currentTime.AddDays(1))
                                        .CreateMany(90)
                                        .ToList();
            mockData.AddRange(mockDataSalt);

            await _dbContext.Feedbacks.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var mockQuery = _fixture.Build<SearchFeedbackViewModel>()
                        .With(x => x.CreationDate, currentTime)
                        .With(x => x.PageIndex, 0)
                        .With(x => x.PageSize, 100)
                        .Without(x => x.CreateBy)
                        .Without(x => x.Title)
                        .Create();
            var mockResultPagination = _fixture.Build<Pagination<Feedback>>()
                                    .With(x => x.Items, mockTitleBeAssigned)
                                    .With(x => x.PageIndex, 0)
                                    .With(x => x.PageSize, 100)
                                    .With(x => x.TotalItemsCount, 10)
                                    .Create();
            // act
            var result = await _feedbackRepository.SearchAsync(mockQuery);

            // assert
            result.Should().BeEquivalentTo(mockResultPagination);
        }
    }
}
