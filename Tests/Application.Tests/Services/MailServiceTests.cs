using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.ChemicalsViewModels;
using Global.Shared.ViewModels.MailViewModels;
using Infrastructures.Services;
using Moq;

namespace Application.Tests.Services
{
    public class MailServiceTests : SetupTest
    {
        private readonly IMailService _mailService;

        public MailServiceTests()
        {
            //_mailService = new MailService(_unitOfWorkMock.Object, _claimsServiceMock.Object, _mapperConfig);
        }

        //[Fact]
        //public void Send_ShouldReturnCorrectData()
        //{
        //    var request = new MailRequestViewModel
        //    {
        //        MailType = "Welcome"
        //    };

        //    //var responseMock = _mapperConfig.Map<MailViewModel>(mock);

        //    var result = _mailService.Send(request);

        //    result.Should().BeEquivalentTo(responseMock);

        //    _mailServiceMock.Verify(x => x.Send(mock), Times.Once);
        //}

        //[Fact]
        //public void Send_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mocks = _fixture.Build<Chemical>().CreateMany(100).ToList();
        //    var expectedResult = _mapperConfig.Map<List<ChemicalViewModel>>(mocks);

        //    _unitOfWorkMock.Setup(x => x.ChemicalRepository.GetAllAsync()).ReturnsAsync(mocks);

        //    //act
        //    var result = await _chemicalService.GetChemicalAsync();

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.ChemicalRepository.GetAllAsync(), Times.Once());
        //    result.Should().BeEquivalentTo(expectedResult);
        //}


        //[Fact]
        //public async Task CreateChemicalAsync_ShouldReturnCorrentData_WhenSuccessSaved()
        //{
        //    //arrange
        //    var mocks = _fixture.Build<CreateChemicalViewModel>().Create();

        //    _unitOfWorkMock.Setup(x => x.ChemicalRepository.AddAsync(It.IsAny<Chemical>()))
        //        .Returns(Task.CompletedTask);

        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
        //    //act
        //    var result = await _chemicalService.CreateChemicalAsync(mocks);

        //    //assert
        //    _unitOfWorkMock.Verify(
        //        x => x.ChemicalRepository.AddAsync(It.IsAny<Chemical>()), Times.Once());

        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        //}

        //[Fact]
        //public async Task CreateChemicalAsync_ShouldReturnNull_WhenFailedSave()
        //{
        //    //arrange
        //    var mocks = _fixture.Build<CreateChemicalViewModel>().Create();

        //    _unitOfWorkMock.Setup(
        //        x => x.ChemicalRepository.AddAsync(It.IsAny<Chemical>())).Returns(Task.CompletedTask);

        //    _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(0);

        //    //act
        //    var result = await _chemicalService.CreateChemicalAsync(mocks);

        //    //assert
        //    _unitOfWorkMock.Verify(
        //        x => x.ChemicalRepository.AddAsync(It.IsAny<Chemical>()), Times.Once());

        //    _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());

        //    result.Should().BeNull();
        //}

        //[Fact]
        //public async Task GetChemicalPagingsionAsync_ShouldReturnCorrectDataWhenDidNotPassTheParameters()
        //{
        //    //arrange
        //    var mockData = new Pagination<Chemical>
        //    {
        //        Items = _fixture.Build<Chemical>().CreateMany(100).ToList(),
        //        PageIndex = 0,
        //        PageSize = 100,
        //        TotalItemsCount = 100
        //    };
        //    var expectedResult = _mapperConfig.Map<Pagination<Chemical>>(mockData);

        //    _unitOfWorkMock.Setup(x => x.ChemicalRepository.ToPagination(0, 10)).ReturnsAsync(mockData);

        //    //act
        //    var result = await _chemicalService.GetChemicalPagingsionAsync();

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.ChemicalRepository.ToPagination(0, 10), Times.Once());
        //}

        //[Fact]
        //public async Task GetChemicalAsync_ShouldReturnCorrectData()
        //{
        //    //arrange
        //    var mocks = _fixture.Build<Chemical>().CreateMany(100).ToList();
        //    var expectedResult = _mapperConfig.Map<List<ChemicalViewModel>>(mocks);

        //    _unitOfWorkMock.Setup(x => x.ChemicalRepository.GetAllAsync()).ReturnsAsync(mocks);

        //    //act
        //    var result = await _chemicalService.GetChemicalAsync();

        //    //assert
        //    _unitOfWorkMock.Verify(x => x.ChemicalRepository.GetAllAsync(), Times.Once());
        //    result.Should().BeEquivalentTo(expectedResult);
        //}
    }
}
