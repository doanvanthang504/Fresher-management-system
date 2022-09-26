using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.ModuleViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class ModuleServiceTest : SetupTest
    {
        private readonly IModuleService _moduleService;

        public ModuleServiceTest()
        {
            _moduleService = new ModuleService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task AddModuleAsync_ReturnsData_IfSuccess()
        {
            //Arrange
            var expectedResult = _fixture.Build<ModuleAddViewModel>().Create();
            var mock = _mapperConfig.Map<Module>(expectedResult);
            _unitOfWorkMock.Setup(x => x.ModuleRepository.AddAsync(mock)).Returns(Task.CompletedTask);
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //Act
            var result = await _moduleService.AddModuleAsync(expectedResult);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetAllModuleAsync_Returns_ListDataCorrect()
        {
            //Arrange
            var expectedResult = _fixture.Build<Pagination<ModuleViewModel>>().Create();
            var mock = _mapperConfig.Map<Pagination<Module>>(expectedResult);
            _unitOfWorkMock.Setup(x => x.ModuleRepository.FindAsync(null, null, 0, 10)).ReturnsAsync(mock);
            //Act
            var result = await _moduleService.GetAllModuleAsync(0, 10);
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsData_IfFound()
        {
            //Arrange
            var expectedResult = _fixture.Build<ModuleViewModel>().Create();
            var mock = _mapperConfig.Map<Module>(expectedResult);
            _unitOfWorkMock.Setup(x => x.ModuleRepository.FindAsync(It.IsAny<Guid>(), x => x.Topics)).ReturnsAsync(mock);
            //Act
            var result = await _moduleService.GetModuleByIdAsync(It.IsAny<Guid>());
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task GetByPlanIdAsync_ReturnsListData_IfFound()
        {
            //Arrange
            var expectedResult = _fixture.Build<ModuleViewModel>().CreateMany(10).ToList();
            var mock = _mapperConfig.Map<List<Module>>(expectedResult);
            _unitOfWorkMock.Setup(x => x.ModuleRepository.GetModuleByPlanId(It.IsAny<Guid>())).ReturnsAsync(mock);
            //Act
            var result = await _moduleService.GetModuleByPlanIdAsync(It.IsAny<Guid>());
            //Assert
            result.Should().BeEquivalentTo(expectedResult);
        }
        [Fact]
        public async Task UpdateModuleAsync_Must_CallOneTime_SaveChange()
        {
            //Arrange
            var expectedResult = _fixture.Build<ModuleUpdateViewModel>().Create();
            _unitOfWorkMock.Setup(x => x.ModuleRepository.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Module());
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            //Act
            var result = await _moduleService.UpdateModuleAsync(It.IsAny<Guid>(), expectedResult);
            //Assert
            _unitOfWorkMock.Verify(x => x.SaveChangeAsync(), Times.Once());
        }
    }
}
