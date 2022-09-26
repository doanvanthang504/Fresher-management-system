using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ChapterSyllabusViewModels;
using Moq;
using System;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class ChapterSyllabusControllerTests : SetupTest
    {
        private readonly ChapterSyllabusesController _chapterSyllabusesController;
        public ChapterSyllabusControllerTests()
        {
            _chapterSyllabusesController = new ChapterSyllabusesController(_chapterSyllabusServiceMock.Object);
        }
        [Fact]
        public async Task AddChapterSyllabus_Should_Return_Correct_Data()
        {
            //Arrange
            var chapterSyllabusAddViewModel = _fixture.Build<ChapterSyllabusAddViewModel>().Create();
            var expectedResult = _fixture.Build<ChapterSyllabusViewModel>().Create();
            _chapterSyllabusServiceMock.Setup(x => x.AddChapterSyllabusAsync(chapterSyllabusAddViewModel))
                                       .ReturnsAsync(expectedResult);
            //Act
            var result = await _chapterSyllabusesController.AddChapterSyllabus(chapterSyllabusAddViewModel);
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task GetChapterSyllabusById_Should_Return_Correct_ListData()
        {
            //Arrange
            var expectedResult = _fixture.Build<ChapterSyllabusViewModel>().Create();
            _chapterSyllabusServiceMock.Setup(x => x.GetChapterSyllabusByIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(expectedResult);
            //Act
            var result = await _chapterSyllabusesController.GetChapterSyllabusById(It.IsAny<Guid>());
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task GetChapterSyllabusByTopicIdAsync_Should_Returns_List_Correct_Data()
        {
            //Arrange
            var expectedResultList = _fixture.Build<ChapterSyllabusViewModel>().CreateMany(10);
            _chapterSyllabusServiceMock.Setup(x => x.GetChapterSyllabusByTopicIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(expectedResultList);
            //Act
            var result = await _chapterSyllabusesController.GetChapterSyllabusByTopicId(It.IsAny<Guid>());
            //Assert
            result.Should().BeEquivalentTo(expectedResultList);
        }
        [Fact]
        public async Task UpdateChapterSyllabusAsync_Should_Returns_Correct_Data()
        {
            //Arrange
            var expectedResult = _fixture.Build<ChapterSyllabusViewModel>().Create();
            var chapterSyllabusViewModel = _fixture.Build<ChapterSyllabusAddViewModel>().Create();
            _chapterSyllabusServiceMock.Setup(x => x.UpdateChapterSyllabusAsync(It.IsAny<Guid>(),
                                                    chapterSyllabusViewModel))
                                       .ReturnsAsync(expectedResult);
            //Act
            var result = await _chapterSyllabusesController.UpdateChapterSyllabus(It.IsAny<Guid>(), chapterSyllabusViewModel);
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }
    }
}
