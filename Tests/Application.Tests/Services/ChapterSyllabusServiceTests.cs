using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ChapterSyllabusServiceTests : SetupTest
    {
        private readonly IChapterSyllabusService _chapterSyllabusService;
        public ChapterSyllabusServiceTests()
        {
            _chapterSyllabusService = new ChapterSyllabusService(_unitOfWorkMock.Object, _mapperConfig);
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }
        [Fact]
        public async Task AddChapterSyllabusAsync_Return_ViewModel_Correct()
        {
            //Arrange
            var chapterSyllabusAddViewModel = _fixture.Build<ChapterSyllabusAddViewModel>().Create();
            var chapterSyllabus = _mapperConfig.Map<ChapterSyllabus>(chapterSyllabusAddViewModel);
            var expectedResult = _mapperConfig.Map<ChapterSyllabusViewModel>(chapterSyllabus);
            _unitOfWorkMock.Setup(x => x.ChapterSyllabusRepository.AddAsync(chapterSyllabus)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //Act
            var result= await _chapterSyllabusService.AddChapterSyllabusAsync(chapterSyllabusAddViewModel);
            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        }
        [Fact]
        public async Task GetChapterSyllabusByIdAsync_Return_Correct_Data()
        {
            //Arrange
            var chapterSyllabus = _fixture.Build<ChapterSyllabus>().Create();
            var expectedResult = _mapperConfig.Map<ChapterSyllabusViewModel>(chapterSyllabus);
            _unitOfWorkMock.Setup(x => x.ChapterSyllabusRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(chapterSyllabus);
            //Act
            var result = await _chapterSyllabusService.GetChapterSyllabusByIdAsync(It.IsAny<Guid>());
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
