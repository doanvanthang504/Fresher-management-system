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
    public class FeedbackResultRepositoryTests  : SetupTest
    {
        private readonly IFeedbackResultRepository _feedbackResultRepository;
        public FeedbackResultRepositoryTests()
        {
            _feedbackResultRepository = new FeedbackResultRepository(
                                                _dbContext,
                                                _currentTimeMock.Object,
                                                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_ReturnCorrectData()
        {
            // arrange
            var mockData = _fixture.Build<FeedbackResult>()
                                    .Without(x => x.Feedback)
                                    .Without(x => x.FeedbackQuestion)
                                    .With(x => x.IsDeleted, false)
                                    .CreateMany(10)
                                    .ToList();
            await _dbContext.FeedbackResults.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _feedbackResultRepository.GetAllAsync();

            // assert
            result.Should().BeEquivalentTo(mockData);
        }
        
        [Fact]
        public async Task SearchAsync_WithFilterCreateDate_ShouldReturnCorrectData()
        {
            // arrange
            var currentTime = _currentTimeMock.Object.GetCurrentTime();
            var mockCreationDateBeAssigned = _fixture.Build<FeedbackResult>()
                                                .Without(x => x.Feedback)                                  
                                                .Without(x => x.FeedbackQuestion)
                                                .With(x => x.IsDeleted, false)
                                                .With(x => x.CreationDate, currentTime)
                                                .CreateMany(10)
                                                .ToList();
            var mockData = new List<FeedbackResult>(mockCreationDateBeAssigned);
            var mockDataSalt = _fixture.Build<FeedbackResult>()
                                        .Without(x => x.Feedback)
                                        .Without(x => x.FeedbackQuestion)
                                        .With(x => x.IsDeleted, false)
                                        .With(x=>x.CreationDate, currentTime.AddDays(1))
                                        .CreateMany(90)
                                        .ToList();
            mockData.AddRange(mockDataSalt);

            await _dbContext.FeedbackResults.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            var mockQuery = _fixture.Build<SearchFeedbackResultViewModel>()
                        .With(x => x.CreationDate, currentTime)
                        .With(x => x.PageIndex, 0)
                        .With(x => x.PageSize, 100)
                        .Without(x => x.AccountName)
                        .Without(x => x.QuestionId)
                        .Create();
            var mockResultPagination = _fixture.Build<Pagination<FeedbackResult>>()
                                    .With(x => x.Items, mockCreationDateBeAssigned)
                                    .With(x => x.PageIndex, 0)
                                    .With(x => x.PageSize, 100)
                                    .With(x => x.TotalItemsCount, 10)
                                    .Create();
            // act
            var result = await _feedbackResultRepository.SearchAsync(mockQuery);

            // assert
            result.Should().BeEquivalentTo(mockResultPagination);
        }

        //[Fact]
        //public async Task SearchAsync_WithFilterFeedbackId_ShouldReturnCorrectData()
        //{
        //    // arrange
        //    var mockFeedBackQuestion = _fixture.Build<FeedbackQuestion>()
        //                                        .Without(x => x.FeedbackResults)
        //                                        .Without(x => x.Feedback)
        //                                        .With(x => x.IsDeleted, false)
        //                                        .Create();
        //    var mockQuestionIdBeAssigned = _fixture.Build<FeedbackResult>()
        //                            .With(x => x.QuestionId, mockFeedBackQuestion.Id)
        //                            .With(x => x.IsDeleted, false)
        //                            .Without(x => x.Feedback)
        //                            .Without(x => x.FeedbackQuestion)
        //                            .CreateMany(10)
        //                            .ToList();
        //    var mockData = new List<FeedbackResult>(mockQuestionIdBeAssigned);
        //    var mockDataSalt = _fixture.Build<FeedbackResult>()
        //                            .Without(x => x.FeedbackQuestion)
        //                            .Without(x => x.Feedback)
        //                            .With(x => x.IsDeleted, false)
        //                            .CreateMany(90)
        //                            .ToList();
        //    mockData.AddRange(mockDataSalt);
        //    var mockQuery = _fixture.Build<SearchFeedbackResultViewModel>()
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.QuestionId, mockFeedBackQuestion.Id)
        //                            .Without(x => x.AccountName)
        //                            .Without(x => x.CreationDate)
        //                            .Create();
        //    await _dbContext.FeedbackQuestions.AddAsync(mockFeedBackQuestion);
        //    await _dbContext.FeedbackResults.AddRangeAsync(mockData);
        //    await _dbContext.SaveChangesAsync();

        //    mockQuestionIdBeAssigned = mockQuestionIdBeAssigned.Select(x => new FeedbackResult
        //    {
        //        Id = x.Id,
        //        Content = x.Content,
        //        FeedbackId = x.FeedbackId,
        //        QuestionId = x.QuestionId,
        //        QuestionTitle = x.QuestionTitle,
        //        AccountFresherId = x.AccountFresherId,
        //        AccountName = x.AccountName,
        //        Fullname = x.Fullname,
        //        Note = x.Note,
        //        CreationDate = x.CreationDate,
        //        CreatedBy = x.CreatedBy,
        //        DeleteBy = x.DeleteBy,
        //        DeletionDate = x.DeletionDate,
        //        IsDeleted = x.IsDeleted,
        //        ModificationBy = x.ModificationBy,
        //        ModificationDate = x.ModificationDate
        //    }).ToList();
        //    var mockResultPagination = _fixture.Build<Pagination<FeedbackResult>>()
        //                            .With(x => x.Items, mockQuestionIdBeAssigned)
        //                            .With(x => x.PageIndex, 0)
        //                            .With(x => x.PageSize, 100)
        //                            .With(x => x.TotalItemsCount, 10)
        //                            .Create();
        //    // act
        //    var result = await _feedbackResultRepository.SearchAsync(mockQuery);

        //    // assert
        //    result.Should().BeEquivalentTo(mockResultPagination);
        //}
    }
}
