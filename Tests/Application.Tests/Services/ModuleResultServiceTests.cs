using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ModuleResultViewModels;
using Moq;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ModuleResultServiceTests : SetupTest
    {
        private readonly IModuleResultService _moduleResultService;

        public ModuleResultServiceTests()
        {
            _moduleResultService = new ModuleResultService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task CreateResultModuleTest_ShouldSaveChangeTrue()
        {
            //arrange
            var mockModel = _fixture.Build<CreateModuleResultViewModel>().Create();

            var mockObj = _mapperConfig.Map<ModuleResult>(mockModel);

            _unitOfWorkMock.Setup(e => e.ModuleResultRepository.AddAsync(mockObj));
            
            _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(1);

            //act
            var result = await _moduleResultService
                         .CreateModuleResultAsync(mockModel);

            //assert
            result.Should().BeTrue();

            _unitOfWorkMock.Verify(e => e.ModuleResultRepository
                                         .AddAsync(It.IsAny<ModuleResult>()), Times.Once());
            _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once());
        }

        [Fact]
        public async Task CreateResultModuleTest_ShouldSaveChangeFalse()
        {
            //arrange
            var mockModel = _fixture.Build<CreateModuleResultViewModel>().Create();

            var mockObj = _mapperConfig.Map<ModuleResult>(mockModel);

            _unitOfWorkMock.Setup(e => e.ModuleResultRepository.AddAsync(mockObj));
            
            _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(0);

            //act
            var result = await _moduleResultService
                         .CreateModuleResultAsync(mockModel);

            //assert
            result.Should().BeFalse();
            _unitOfWorkMock.Verify(e => e.ModuleResultRepository
                                         .AddAsync(It.IsAny<ModuleResult>()), Times.Once());
            _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once());
        }
        //[Fact]
        //public async Task UpdateModuleResultAsync_WhenModuleResultNull()
        //{
        //    //arrange

        //    var mockupdateModuleResultVM = _fixture.Build<UpdateQuizzAssignAVGViewModel>().Create();

        //    _unitOfWorkMock.Setup(e => e.ModuleResultRepository
        //                     .GetModuleResultByFillterAsync(
        //                        x => x.FresherId == mockupdateModuleResultVM.FresherId
        //                       && x.ModuleName == mockupdateModuleResultVM.ModuleName)).ReturnsAsync(new ModuleResult());
        //    //act
        //    await _moduleResultService.UpdateModuleResultAsync(mockupdateModuleResultVM);
        //}

        //[Fact]
        //public async Task UpdateScoreAsyncTest_ShouldReturnTrue()
        //{
        //    //arrange
        //    var mockModel = _fixture.Build<ModuleResult>().Create();
        //    var mockScoreList = _fixture.Build<Score>()
        //                                .With(e => e.TypeScore, TypeScoreEnum.Assignment)
        //                                .With(e => e.TypeScore, TypeScoreEnum.Quizz)
        //                                .CreateMany(10)
        //                                .ToList();
        //    _unitOfWorkMock
        //           .Setup(e => e.ModuleRepository.GetByIdAsync(It.IsAny<Guid>()))
        //           .ReturnsAsync(new Module());
        //    _unitOfWorkMock
        //                .Setup(e => e.ModuleResultRepository
        //                .GetModuleResultAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //                .ReturnsAsync(new ModuleResult());
        //    _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(1);

        //    //act
        //    var result = await _moduleResultService
        //                 .UpdateScoreAsync(It.IsAny<Guid>(),
        //                                   It.IsAny<Guid>(),
        //                                   mockScoreList);

        //    //assert
        //    result.Should().BeTrue();
        //    _unitOfWorkMock.Verify(e => e.ModuleResultRepository
        //                                 .Update(It.IsAny<ModuleResult>()), Times.Once());
        //    _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once());
        //}

        //[Fact]
        //public async Task UpdateFinalAuditScoreAsync_ShouldReturnFalse()
        //{
        //    //arrange
        //    _unitOfWorkMock
        //                .Setup(e => e.ModuleResultRepository
        //                .GetModuleResultAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
        //                .ReturnsAsync((ModuleResult?)null);
        //    //act
        //    var result = await _moduleResultService
        //                 .UpdateFinalAuditScoreAsync(It.IsAny<Guid>(),
        //                                             It.IsAny<Guid>(),
        //                                             It.IsAny<int>(),
        //                                             It.IsAny<int>());

        //    result.Should().BeFalse();
        //}
        //[Fact]
        //public async Task UpdateFinalAuditScoreAsync_ShouldReturnTrue()
        //{
        //    //arrange
        //    var mockFresherId = Guid.NewGuid();
        //    var mockModuleId = Guid.NewGuid();
        //    var mockAuditScore = 9.5;
        //    var mockPracticeScore = 7.8;

        //    _unitOfWorkMock
        //                .Setup(e => e.ModuleResultRepository
        //                .GetModuleResultAsync(mockFresherId, mockModuleId))
        //                .ReturnsAsync(new ModuleResult());
        //    _unitOfWorkMock
        //                .Setup(e => e.ModuleRepository.GetByIdAsync(It.IsAny<Guid>()))
        //                .ReturnsAsync(new Module());
        //    _unitOfWorkMock.Setup(e => e.SaveChangeAsync()).ReturnsAsync(1);

        //    //act
        //    var result = await _moduleResultService
        //                 .UpdateFinalAuditScoreAsync(mockFresherId,
        //                                             mockModuleId,
        //                                             mockAuditScore,
        //                                             mockPracticeScore);
        //    //assert
        //    result.Should().BeTrue();
        //    _unitOfWorkMock.Verify(e => e.ModuleResultRepository
        //                                 .Update(It.IsAny<ModuleResult>()), Times.Once());
        //    _unitOfWorkMock.Verify(e => e.SaveChangeAsync(), Times.Once());
        //}
    }
}
