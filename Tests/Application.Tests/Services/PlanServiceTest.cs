using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.PlanViewModels;
using Moq;
using System;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class PlanServiceTest : SetupTest
    {
        private readonly IPlanService _planService;

        public PlanServiceTest()
        {
            _planService = new PlanService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturn_ViewModel()
        {
            //Arrange
            var expectedResult = _fixture.Build<Pagination<PlanGetViewModel>>()
                                         .Create();
            var mocks = _mapperConfig.Map<Pagination<Plan>>(expectedResult);
            _unitOfWorkMock.Setup(x => x.PlanRepository.FindAsync(null, null, 0, 10)).ReturnsAsync(mocks);
            //Act
            var result = await _planService.GetAllPlanAsync(0, 10);
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task GetPlanByIdAsync_ShouldReturn_CorrectData_IfFound()
        {
            //Arrange
            var expectedResult = _fixture.Build<PlanGetViewModel>().Create();
            var mock = _mapperConfig.Map<Plan>(expectedResult);
            _unitOfWorkMock.Setup(x => x.PlanRepository.FindAsync(It.IsAny<Guid>(), x => x.Modules)).ReturnsAsync(mock);
            //Act
            var result = await _planService.GetPlanByIdAsync(It.IsAny<Guid>());
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task AddItemPlanAsync_ShouldReturnData_IfSuccess()
        {
            //Arrange
            var expectedResult = _fixture.Build<PlanAddViewModel>()
                                        .Create();
            var mock = _mapperConfig.Map<Plan>(expectedResult);
            _unitOfWorkMock.Setup(x => x.PlanRepository.AddAsync(mock)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //Act
            var result = await _planService.AddPlanAsync(expectedResult);
            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdatePlanAsync_ShouldReturnData_IfSuccess()
        {
            //Arrange
            var planUpdateView = _fixture.Build<PlanUpdateViewModel>().Create();
            var mockplan = _mapperConfig.Map<Plan>(planUpdateView);
            var expectedResult = _mapperConfig.Map<PlanGetViewModel>(mockplan);
            _unitOfWorkMock.Setup(x => x.PlanRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(mockplan);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //Act
            var result = await _planService.UpdatePlanAsync(It.IsAny<Guid>(), planUpdateView);
            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
