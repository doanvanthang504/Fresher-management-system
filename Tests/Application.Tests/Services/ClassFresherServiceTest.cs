using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Exceptions;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.ClassFresherViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ClassFresherServiceTest : SetupTest
    {
        private readonly IClassFresherService _classFresherService;

        public ClassFresherServiceTest()
        {
            _classFresherService = new ClassFresherService(_unitOfWorkMock.Object, _mapperConfig, _importDataServiceMock.Object);
        }

        [Fact]
        public async Task GetClassFresherPagingsionAsync_ShouldReturnCorrectDataWhenDidNotPassTheParameters()
        {
            //arrange
            var request = new PaginationRequest { PageIndex = 0, PageSize = 10 };
            var mockData = new Pagination<ClassFresher>
            {
                Items = _fixture.Build<ClassFresher>().Without(x => x.Freshers).CreateMany(100).ToList(),
                PageIndex = 0,
                PageSize = 100,
                TotalItemsCount = 100
            };
            var expectedResult = _mapperConfig.Map<Pagination<ClassFresher>>(mockData);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.ToPagination(0, 10)).ReturnsAsync(mockData);

            //act
            var result = await _classFresherService.GetAllClassFreshersPagingsionAsync(request);

            //assert
            _unitOfWorkMock.Verify(x => x.ClassFresherRepository.ToPagination(0, 10), Times.Once());
        }

        [Fact]
        public async Task GetAllClassFreshersPagingsionAsync_ShouldThrowExceptionClassNotFound()
        {
            // arrange
            var request = new PaginationRequest { PageIndex = 0, PageSize = 10 };
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.ToPagination(0, 10))
                .Callback(() => throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND)); ;

            //act
            var result = await Assert.ThrowsAsync<AppException>(
                async () => await _classFresherService.GetAllClassFreshersPagingsionAsync(request));

            //assert
            result.Message.Should().Be(Constant.EXCEPTION_CLASS_NOT_FOUND);
        }


        [Fact]
        public async Task GetClassFresherByIdAsync_ShoudReturnCorrectData()
        {
            //arrange
            var mock = _fixture.Build<ClassFresher>()
                .Without(x => x.Freshers)
                .Create();

            var expectedResult = _mapperConfig.Map<ClassFresherViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(mock);

            //act
            var result = await _classFresherService
                .GetClassFresherByClassIdAsync(It.IsAny<Guid>());

            // assert
            _unitOfWorkMock.Verify(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>()), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetClassFresherByIdAsync_ShouldThrowExceptionNotFound()
        {
            // arrange
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>()))
                .Callback(() => throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND)); ;

            //act
            var result = await Assert.ThrowsAsync<AppException>(
                async () => await _classFresherService.GetClassFresherByClassIdAsync(It.IsAny<Guid>()));

            //assert
            Assert.Equal(result.Message, Constant.EXCEPTION_CLASS_NOT_FOUND);

        }

        [Fact]
        public async Task GetClassFresherByIdAsync_ShoudReturnNullData()
        {
            //arrange
            var mock = (ClassFresher?)null;
            Guid id = Guid.NewGuid();
            var expectedResult = _mapperConfig.Map<ClassFresherViewModel>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(id)).Callback(() =>
           throw new AppException($"CLassFresher with id = {id} not found"));
            //Act
            var ex = await Assert.ThrowsAsync<AppException>(async () => await _classFresherService.GetClassFresherByClassIdAsync(id));
            //assert
            Assert.Equal(ex.Message, $"CLassFresher with id = {id} not found");

        }

        [Fact]
        public async Task UpdateClassFresher_ShouldReturnTrueIfUpdateSuccess()
        {
            //arrange 
            var mock = _fixture.Build<UpdateClassFresherViewModel>().Create();

            var expectedResult = _mapperConfig.Map<ClassFresher>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.Update(expectedResult));
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);

            //act
            var result = await _classFresherService.UpdateClassFresher(mock);

            //assert
            _unitOfWorkMock
                .Verify(
                    x => x.ClassFresherRepository.Update(It.IsAny<ClassFresher>()),
                    Times.Once());
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());
        }

        [Fact]
        public async Task UpdateClassFresher_ShouldReturnFalseIfUpdateFail()
        {
            //arrange 
            var mock = _fixture.Build<UpdateClassFresherViewModel>().Create();

            var expectedResult = _mapperConfig.Map<ClassFresher>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.Update(expectedResult));
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

            //act
            var result = await Assert
                            .ThrowsAsync<AppException>(
                                () => _classFresherService.UpdateClassFresher(mock));

            //assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Message.Should().BeEquivalentTo(Constant.EXCEPTION_UPDATE_CLASS_FAIL);
        }

        [Fact]
        public async Task UpdateClassFresher_ShouldThrowExceptionNotFoundClass()
        {
            //arrange 
            var mockClass = _fixture.Build<UpdateClassFresherViewModel>().Create();

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>()))
                .Callback(() => throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND));

            //act
            var result = await Assert.ThrowsAsync<AppException>(
                () => _classFresherService.UpdateClassFresher(mockClass));

            //assert
            result.Message.Should().BeEquivalentTo(Constant.EXCEPTION_CLASS_NOT_FOUND);
        }

        [Fact]
        public async Task GetFresherByClassCode_ShouldReturnCorrectData()
        {
            //arrange
            var mocks = _fixture.Build<Fresher>().Without(x => x.ModuleResults).Without(x => x.ClassFresher).Without(y => y.Attendances)
                .CreateMany(10)
                .ToList();
            var expectedResult = _mapperConfig.Map<List<FresherViewModel>>(mocks);

            _unitOfWorkMock
                .Setup(x => x.FresherRepository
                .GetFresherByClassCodeAsync("HCM22_FR_NET_05"))
                .ReturnsAsync(mocks);

            //act
            var results = await _classFresherService
                .GetFreshersByClassCodeAsync("HCM22_FR_NET_05");

            //assert
            _unitOfWorkMock.Verify(
                x => x.FresherRepository.GetFresherByClassCodeAsync("HCM22_FR_NET_05")
                , Times.Once());
            results.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllClassCodeAsync_ShouldReturnCorrectData()
        {
            //arrange
            var listClassCode = _fixture.Build<ClassFresher>().Without(x => x.Freshers).CreateMany(10).Select(x => x.ClassCode).ToList();
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetAllClassCodeAsync()).ReturnsAsync(listClassCode);

            //Act
            var results = await _classFresherService.GetAllClassCodeAsync();

            //Assert
            _unitOfWorkMock.Verify(x => x.ClassFresherRepository.GetAllClassCodeAsync(), Times.Once());
            results.Should().BeEquivalentTo(listClassCode);
        }

        [Fact]
        public async Task UpdateClassFresherAfterImportedAsync_ShouldReturnTrueIfUpdateSuccess()
        {
            //arrange 
            var mock = _fixture.Build<UpdateClassFresherInfoViewModel>().Without(x => x.Freshers).Create();

            var expectedResult = _mapperConfig.Map<ClassFresher>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.Update(expectedResult));
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);

            //act
            var result = await _classFresherService.UpdateClassFresherAfterImportedAsync(mock);

            //assert
            _unitOfWorkMock
                .Verify(
                    x => x.ClassFresherRepository.Update(It.IsAny<ClassFresher>()),
                    Times.Once());
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());
        }
        [Fact]
        public async Task UpdateClassFresherAfterImportedAsync_ShouldThrowExceptionNotFoundClass()
        {
            //arrange      
            var mock = _fixture.Build<UpdateClassFresherInfoViewModel>().Without(x => x.Freshers).Create();

            var s = _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>()))//.Returns<Exception>(null);
            .Callback(() => throw new AppException(Constant.EXCEPTION_CLASS_NOT_FOUND));

            //act
            var result = await Assert.ThrowsAsync<AppException>(async () => await _classFresherService.UpdateClassFresherAfterImportedAsync(mock));

            //assert
            result.Message.Should().Be(Constant.EXCEPTION_CLASS_NOT_FOUND);
        }

        [Fact]
        public async Task UpdateClassFresherAfterImportedAsync__ShouldThrowExceptionFail()
        {
            //arrange 
            var mock = _fixture.Build<UpdateClassFresherInfoViewModel>().Without(x => x.Freshers).Create();

            var expectedResult = _mapperConfig.Map<ClassFresher>(mock);

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.Update(expectedResult));
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(expectedResult);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

            //act
            var result = await Assert
                            .ThrowsAsync<AppException>(
                                () => _classFresherService.UpdateClassFresherAfterImportedAsync(mock));

            //assert            
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Message.Should().BeEquivalentTo(Constant.EXCEPTION_CREATE_CLASS_FAIL);
        }
    }
}
