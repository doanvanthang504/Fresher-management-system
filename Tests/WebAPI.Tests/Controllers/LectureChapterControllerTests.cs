using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.LectureChapterViewModels;
using Moq;
using System;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{

    public class LectureChapterControllerTests : SetupTest
    {
        private readonly LectureChaptersController _lectureChaptersController;
        public LectureChapterControllerTests()
        {
            _lectureChaptersController = new LectureChaptersController(_lectureChapterServiceMock.Object);
        }
        [Fact]
        public async Task AddLectureChapter_Should_Return_ViewModel_Correct()
        {
            //Arrange
            var expectedResult = _fixture.Build<LectureChapterViewModel>().Create();
            var lectureChapterViewModel = _fixture.Build<LectureChapterAddViewModel>().Create();
            _lectureChapterServiceMock.Setup(x => x.AddLectureChapterAsync(lectureChapterViewModel))
                                      .ReturnsAsync(expectedResult);
            //Act
            var result = await _lectureChaptersController.AddLectureChapter(lectureChapterViewModel);
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task GetLectureChapterById_Should_Return_ViewModel_Correct()
        {
            //Arrange
            var expectedResult = _fixture.Build<LectureChapterViewModel>().Create();
            _lectureChapterServiceMock.Setup(x => x.GetLectureChapterByIdAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(expectedResult);
            //Act
            var result = await _lectureChaptersController.GetLectureChapterById(It.IsAny<Guid>());
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }
        [Fact]
        public async Task GetLectureChapterByChapterId_Should_Returns_List_Data_Correct()
        {
            //Arrange
            var expectedResultList = _fixture.Build<LectureChapterViewModel>().CreateMany(10);
            _lectureChapterServiceMock.Setup(x => x.GetLectureChapterByChapterIdAsync(It.IsAny<Guid>()))
                                      .ReturnsAsync(expectedResultList);
            //Act
            var result = await _lectureChaptersController.GetLectureChapterByChapterId(It.IsAny<Guid>());
            //Assert
            result.Should().BeEquivalentTo(expectedResultList);
        }
        [Fact]
        public async Task UpdateLectureChapter_Should_Returns_Data_Correct()
        {
            //Arrange
            var expectedResult = _fixture.Build<LectureChapterViewModel>().Create();
            var lectureChapterAddViewModel = _fixture.Build<LectureChapterAddViewModel>().Create();
            _lectureChapterServiceMock.Setup(x => x.UpdateLectureChapterAsync(It.IsAny<Guid>(), lectureChapterAddViewModel))
                                      .ReturnsAsync(expectedResult);
            //Act
            var result = await _lectureChaptersController.UpdateLectureChapter(It.IsAny<Guid>(), lectureChapterAddViewModel);
            //Assert
            Assert.Equal(expectedResult, result.Value);
        }

    }
}
