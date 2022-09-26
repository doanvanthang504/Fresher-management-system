using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class CLassFresherControllerTest : SetupTest
    {
        private readonly ClassFresherController _cLassFresherController;
        public CLassFresherControllerTest()
        {
            _cLassFresherController = new ClassFresherController(_classFresherServiceMock.Object);
        }

        [Fact]
        public async Task GetCLassFreshertById_ShouldReturnCorrectData_IfSuccess()
        {
            //arrage
            Guid id = Guid.NewGuid();
            _classFresherServiceMock.Setup(x => x.GetClassFresherByClassIdAsync(id))
                .ReturnsAsync(new ClassFresherViewModel { Id = id });
            //Act
            var result = await _cLassFresherController.GetClassFresherByIdAsync(id);
            //assert
            _classFresherServiceMock.Verify(
               x => x.GetClassFresherByClassIdAsync(id), Times.Once());           
            result.Should().NotBeNull();
            result.Should().NotBeNull();
        }


        [Fact]
        public async Task GetCLassFreshertById_ShouldReturnThrowException()
        {
            //arrage
            Guid id = Guid.NewGuid();
            _classFresherServiceMock.Setup(x => x.GetClassFresherByClassIdAsync
            (It.IsAny<Guid>())).Callback(() =>
            throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND));
            //Act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _cLassFresherController.GetClassFresherByIdAsync(id));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_CLASS_NOT_FOUND);
        }

        [Fact]
        public async Task GetAllClassFresherPagingsion_ShouldReturnCorrectData()
        {
            // arrange
            var request = new PaginationRequest { PageSize = 10, PageIndex = 0 };
            var mocks = _fixture.Build<Pagination<ClassFresherViewModel>>().Create();
            _classFresherServiceMock.Setup(
                x => x.GetAllClassFreshersPagingsionAsync(request)).ReturnsAsync(mocks);
            // act
            var result = await _cLassFresherController.GetAllClassFresherPagingsionAsync(request);

            // assert
            _classFresherServiceMock.Verify(
                x => x.GetAllClassFreshersPagingsionAsync(request), Times.Once());
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetAllClassFresherPagingsion_ShouldReturnThrowException()
        {
            // arrange
            var request = new PaginationRequest { PageSize = 10, PageIndex = 0 };
            _classFresherServiceMock.Setup(x => x.GetAllClassFreshersPagingsionAsync
             (request)).Callback(() =>
             throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND));
            //Act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _cLassFresherController.GetAllClassFresherPagingsionAsync(request));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_CLASS_NOT_FOUND);
        }

        [Fact]
        public async Task GetFresherByClassCode_ShouldReturnCorrectData()
        {
            var mocks = _fixture.Build<FresherViewModel>().CreateMany(10).ToList();
            var classCode = It.IsAny<string>();
            // arrange
            _classFresherServiceMock.Setup(
                x => x.GetFreshersByClassCodeAsync(classCode)).ReturnsAsync(mocks);

            // act
            var result = await _cLassFresherController.GetFresherByClassCodeAsync(classCode);

            // assert
            _classFresherServiceMock.Verify(
                x => x.GetFreshersByClassCodeAsync(classCode), Times.Once());
            result.Should().NotBeNull();
        }

        [Fact]
        public async Task GetFresherByClassCode_ShouldReturnThrowException()
        {
            // arrange
            var classCode = It.IsAny<string>();
            _classFresherServiceMock.Setup(x => x.GetFreshersByClassCodeAsync
             (classCode)).Callback(() =>
             throw new AppException(Constant.EXCEPTION_LIST_FRESHER_NOT_FOUND));
            //Act
            var ex = await Assert.ThrowsAsync<AppException>
                (async () => await _cLassFresherController.GetFresherByClassCodeAsync(classCode));
            //assert
            ex.Message.Should().Be(Constant.EXCEPTION_LIST_FRESHER_NOT_FOUND);
        }

        [Fact]
        public async Task CreateClassFresherFromImportFile_ShouldReturnCorrectData()
        {
            //arrage
            var mocks = _fixture.Build<ClassFresherViewModel>().Without(x => x.Freshers).CreateMany(10).ToList();

            _classFresherServiceMock.Setup(
               x => x.CreateClassFresherFromImportedExcelFile(It.IsAny<IFormFile>())).ReturnsAsync(mocks);
            // act
            var result = await _cLassFresherController.CreateClassFresherFromImportedFileAsync(It.IsAny<IFormFile>());

            // assert
            _classFresherServiceMock.Verify(
                x => x.CreateClassFresherFromImportedExcelFile(
                    It.IsAny<IFormFile>()), Times.Once());
            result.Should().NotBeNull();

        }

        [Fact]
        public async Task CreateClassFresherFromImportFile_ShouldReturnThrowException()
        {
            _classFresherServiceMock.Setup(
               x => x.CreateClassFresherFromImportedExcelFile(It.IsAny<IFormFile>())).Callback(() =>
                   throw new AppException(Constant.IMPORT_FAIL));

            // act
            var ex = await Assert.ThrowsAsync<AppException>(async () =>
                           await _cLassFresherController.CreateClassFresherFromImportedFileAsync(It.IsAny<IFormFile>()));
            //assert
            ex.Message.Should().Be(Constant.IMPORT_FAIL);
        }

        [Fact]
        public async Task UpdateClassFresherInfomation_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<UpdateClassFresherInfoViewModel>()
                           .Without(x => x.Freshers).Create();
            var mockModelResponse = _fixture.Build<ClassFresherViewModel>().Create();
            // arrange
            _classFresherServiceMock.Setup(
                x => x.UpdateClassFresherAfterImportedAsync(It.IsAny<UpdateClassFresherInfoViewModel>()))
                        .ReturnsAsync(mockModelResponse);
            //act
            var result = await _cLassFresherController.UpdateClassFresherInfomationAsync(mockModelRequest);
            //assert
            _classFresherServiceMock.Verify(
              x => x.UpdateClassFresherAfterImportedAsync(It.Is<UpdateClassFresherInfoViewModel>(
                  x => x.Equals(mockModelRequest))), Times.Once());
            var actionResult = result as OkObjectResult;
            actionResult.Should().NotBeNull();
            var obj = actionResult.Value;
            obj.Should().NotBeNull();
        }

        [Fact]
        public async Task UpdateClassFresher_ShouldReturnCorrectData()
        {
            var mockModelRequest = _fixture.Build<UpdateClassFresherViewModel>()
                           .Create();
            var mockModelResponse = _fixture.Build<ClassFresherViewModel>().Create();

            // arrange
            _classFresherServiceMock.Setup(
                x => x.UpdateClassFresher(It.IsAny<UpdateClassFresherViewModel>()))
                        .ReturnsAsync(mockModelResponse);

            //act
            var result = await _cLassFresherController.UpdateClassFresherAsync(mockModelRequest);

            //assert
            _classFresherServiceMock.Verify(
              x => x.UpdateClassFresher(It.Is<UpdateClassFresherViewModel>(
                  x => x.Equals(mockModelRequest))), Times.Once());

            var actionResult = result as OkObjectResult;

            actionResult.Should().NotBeNull();
            var obj = actionResult.Value;

             obj.Should().NotBeNull();            
        }


        [Fact]
        public async Task GetClassWithFresherByClassIdAsync_ShouldReturnCorrectData()
        {
            //arrange
            var idClass = Guid.NewGuid();
            var mockListFresher = _fixture.Build<FresherViewModel>().CreateMany(10).ToList();
            var mockModelResponse = _fixture.Build<ClassFresherViewModel>().With(x => x.Freshers, mockListFresher).With(x => x.Id , idClass).Create();

            _classFresherServiceMock.Setup(x => x.GetClassWithFresherByClassIdAsync(idClass)).ReturnsAsync(mockModelResponse);

            //art
            var result = await _cLassFresherController.GetClassWithFresherByClassIdAsync(idClass);

            //assert
            _classFresherServiceMock.Verify(x => x.GetClassWithFresherByClassIdAsync(idClass), Times.Once());

            result.Should().NotBeNull();           

            result.Should().BeEquivalentTo(mockModelResponse);

        }

        [Fact]
        public async Task GetAllClassCodeAsync_ShouldReturnCorrectData()
        {
            //arrange            
            var mockListClassCode = _fixture.Build<ClassFresherViewModel>().Without(x => x.Freshers)
                .CreateMany(10).Select(x => x.ClassCode).ToList();

            _classFresherServiceMock.Setup(x => x.GetAllClassCodeAsync()).ReturnsAsync(mockListClassCode);

            //act
            var result = await _cLassFresherController.GetAllClassCodeAsync();

            //assert

            _classFresherServiceMock.Verify(x => x.GetAllClassCodeAsync(), Times.Once());

            result.Should().NotBeNull();

            result.Should().BeEquivalentTo(mockListClassCode);            

        }

        [Fact]
        public async Task DeleteClassFresherAsync_ShouldReturnTrue()
        {
            //arrange 
            var idClass = Guid.NewGuid();
            var mockClassFresher = _fixture.Build<ClassFresherViewModel>().Without(x => x.Freshers).With(x => x.Id , idClass).Create();
            var mockResponse = true;

            _classFresherServiceMock.Setup(x => x.DeleteClassFresherAsync(idClass)).ReturnsAsync(mockResponse);

            //act
            var result = await _cLassFresherController.DeleteClassFresherAsync(idClass);

            //assert
            _classFresherServiceMock.Verify(x => x.DeleteClassFresherAsync(idClass), Times.Once());

            Assert.True(result);
        }
        [Fact]
        public async Task DeleteClassFresherAsync_ShouldReturnnotFoundClass()
        {
            //arrange 
            var idClass = Guid.NewGuid();
            var mockClassFresher = _fixture.Build<ClassFresherViewModel>().Without(x => x.Freshers).Create();
            var mockResponse = false;

            _classFresherServiceMock.Setup(x => x.DeleteClassFresherAsync(idClass)).ReturnsAsync(mockResponse);

            //act
            var result = await _cLassFresherController.DeleteClassFresherAsync(idClass);

            //assert
            _classFresherServiceMock.Verify(x => x.DeleteClassFresherAsync(idClass), Times.Once());

            Assert.False(result);
        }
    }
}
