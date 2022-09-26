using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ScoreViewModels;
using Moq;

namespace Application.Tests.ServicesTest
{
    public class ScoreServiceTest : SetupTest
    {
    //    private readonly IScoreService _scoreService;

    //    public ScoreServiceTest()
    //    {
    //        _scoreService = new ScoreService(_unitOfWorkMock.Object,
    //                                         _mapperConfig,
    //                                         _moduleResultServiceMock.Object);
    //    }

    //    [Fact]
    //    public async Task CreateScoreAsyncTest_ShouldCreateSuccess()
    //    {
    //        //arrange
    //        var mockModelCreateList = _fixture.Build<CreateScoreViewModel>()
    //                                      .With
    //                                       (
    //                                           e => e.TypeScore,
    //                                           TypeScoreEnum.Assignment
    //                                       )
    //                                      .CreateMany(10)
    //                                      .ToList();

    //        var scoreMockList = _mapperConfig.Map<List<Score>>(mockModelCreateList);

    //        _unitOfWorkMock.Setup(e => e.ScoreRepository
    //                                    .AddRangeAsync(scoreMockList)
    //                             ).Returns(Task.CompletedTask);

    //        _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(1);
    //        _unitOfWorkMock.Setup(e => e.ModuleResultRepository
    //                                    .GetModuleResultAsync
    //                                        (
    //                                          It.IsAny<Guid>(),
    //                                          It.IsAny<Guid>()
    //                                        )
    //                             ).ReturnsAsync((ModuleResult?)null);

    //        _moduleResultServiceMock.Setup(e => e.CreateModuleResultAsync
    //                                             (
    //                                                 It.IsAny<Guid>(),
    //                                                 It.IsAny<Guid>()
    //                                             )
    //                                      ).ReturnsAsync(true);

    //        var scoreModelMock = _mapperConfig.Map<List<ScoreViewModel>>(scoreMockList);

    //        //act
    //        var result = await _scoreService.CreateScoresAsync(mockModelCreateList);

    //        //assert
    //        _unitOfWorkMock.Verify(e => e.ScoreRepository
    //                                     .AddRangeAsync(It.IsAny<List<Score>>()), Times.Once);
    //        _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once);

    //        _unitOfWorkMock.Verify(e => e.ModuleResultRepository
    //                                     .GetModuleResultAsync
    //                                         (
    //                                            It.IsAny<Guid>(),
    //                                            It.IsAny<Guid>()
    //                                         ), Times.Exactly(10));

    //        _moduleResultServiceMock.Verify(e => e.CreateModuleResultAsync
    //                                               (
    //                                                    It.IsAny<Guid>(),
    //                                                    It.IsAny<Guid>()
    //                                               ), Times.Exactly(10));
    //    }

    //    [Fact]
    //    public async Task CreateScoreAsyncTest_ShouldSaveChangeFalse()
    //    {
    //        //arrange
    //        var mockModelCreateList = _fixture.Build<CreateScoreViewModel>()
    //                                      .With
    //                                       (
    //                                           e => e.TypeScore,
    //                                           TypeScoreEnum.Assignment
    //                                       )
    //                                      .CreateMany(10)
    //                                      .ToList();

    //        var scoreMockList = _mapperConfig.Map<List<Score>>(mockModelCreateList);

    //        _unitOfWorkMock.Setup(e => e.ScoreRepository
    //                                    .AddRangeAsync(scoreMockList)
    //                             ).Returns(Task.CompletedTask);

    //        _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(0);

    //        //act
    //        var result = await _scoreService.CreateScoresAsync(mockModelCreateList);

    //        //assert
    //        result.Should().BeNull();

    //        _unitOfWorkMock.Verify(e => e.ScoreRepository
    //                                     .AddRangeAsync(It.IsAny<List<Score>>()), Times.Once);
    //        _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once);
    //        _unitOfWorkMock.Verify(e => e.ModuleResultRepository
    //                                     .GetModuleResultAsync
    //                                         (
    //                                            It.IsAny<Guid>(),
    //                                            It.IsAny<Guid>()
    //                                         ), Times.Never());

    //        _moduleResultServiceMock.Verify(e => e.CreateModuleResultAsync
    //                                               (
    //                                                    It.IsAny<Guid>(),
    //                                                    It.IsAny<Guid>()
    //                                               ), Times.Never());
    //    }

    //    [Fact]
    //    public async Task UpdateScoreAsyncTest_ShouldReturnNull()
    //    {
    //        var mockModelUpdate = _fixture.Build<UpdateScoreViewModel>().Create();
    //        _unitOfWorkMock.Setup(e => e.ScoreRepository.GetByIdAsync(It.IsAny<Guid>()))
    //                                                           .ReturnsAsync((Score?)null);
    //        var result = await _scoreService
    //                            .UpdateScoreAsync(mockModelUpdate);
    //        result.Should().BeNull();
    //    }

    //    [Fact]
    //    public async Task UpdateScoreAsyncTest_UpdateSuccess()
    //    {
    //        //arrange
    //        var mockObj = _fixture.Build<Score>().Create();
    //        var mockModelUpdate = _fixture.Build<UpdateScoreViewModel>().Create();

    //        _unitOfWorkMock.Setup(e => e.ScoreRepository
    //                                    .GetByIdAsync(It.IsAny<Guid>()))
    //                                    .ReturnsAsync(mockObj);

    //        var a = _mapperConfig.Map(mockModelUpdate, mockObj);
    //        _unitOfWorkMock.Setup(e => e.ScoreRepository.Update(a));
    //        _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(1);

    //        var mockViewModel = _mapperConfig.Map<ScoreViewModel>(a);

    //        //act
    //        var result = await _scoreService
    //                            .UpdateScoreAsync(mockModelUpdate);

    //        //assert
    //        result.Should().BeEquivalentTo(mockViewModel);
    //        _unitOfWorkMock.Verify(e => e.ScoreRepository.Update
    //                                                         (It.IsAny<Score>()),
    //                                                      Times.Once());
    //        _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once);
    //    }

    //    [Fact]
    //    public async Task UpdateScoreAsyncTest_ShouldSaveChangeFail()
    //    {
    //        //arrange
    //        var mockObj = new Score();
    //        var mockModelUpdate = _fixture.Build<UpdateScoreViewModel>().Create();

    //        _unitOfWorkMock.Setup(e => e.ScoreRepository
    //                                    .GetByIdAsync(It.IsAny<Guid>()))
    //                                    .ReturnsAsync(mockObj);

    //        _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(0);

    //        //act
    //        var result = await _scoreService
    //                            .UpdateScoreAsync(mockModelUpdate);
    //        //assert
    //        result.Should().BeNull();
    //        _unitOfWorkMock.Verify(e => e.ScoreRepository.Update
    //                                                        (It.IsAny<Score>()),
    //                                                      Times.Once());
    //        _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once);
    //    }
    }
}
