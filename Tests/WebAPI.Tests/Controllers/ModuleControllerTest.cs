using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.ModuleViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class ModuleControllerTest : SetupTest
    {
        private readonly ModuleController _moduleController;

        public ModuleControllerTest()
        {
            _moduleController = new ModuleController(_moduleServiceMock.Object);
        }

        [Fact]
        public async Task GetAllModule_ShouldReturns_ListDataCorrect()
        {

            //arrange
            var expectedResult = _fixture.Build<Pagination<ModuleViewModel>>().Create();
            _moduleServiceMock.Setup(x => x.GetAllModuleAsync(0, 10)).ReturnsAsync(expectedResult);
            //Act
            var result = await _moduleController.GetAllModule(0, 10);
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetModuleById_ShouldReturns_CorrectData()
        {
            //Arrange 
            var expectedResult = _fixture.Build<ModuleViewModel>().Create();
            _moduleServiceMock.Setup(x => x.GetModuleByIdAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(expectedResult);
            //Act
            var result = await _moduleController.GetModuleById(It.IsAny<Guid>());
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetModuleByPlanId_ShouldReturns_ListData_IfFound()
        {
            //Arrange 
            var expectedResult = _fixture.Build<ModuleViewModel>().CreateMany(10).ToList();
            _moduleServiceMock.Setup(x => x.GetModuleByPlanIdAsync(It.IsAny<Guid>()))
                              .ReturnsAsync(expectedResult);
            //Act
            var result = await _moduleController.GetModuleByPlanId(It.IsAny<Guid>());
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task AddModule_ShouldReturns_Data_IfSuccess()
        {
            //Arrange 
            var moduleAddView = _fixture.Build<ModuleAddViewModel>().Create();
            var expectedResult = _fixture.Build<ModuleViewModel>().Create();
            _moduleServiceMock.Setup(x => x.AddModuleAsync(moduleAddView))
                              .ReturnsAsync(expectedResult);
            //Act
            var result = await _moduleController.AddModule(moduleAddView);
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task UpdateModule_ShouldReturns_Data_IfSuccess()
        {
            //Arrange 
            var moduleUpdateView = _fixture.Build<ModuleUpdateViewModel>().Create();
            var expectedResult = _fixture.Build<ModuleViewModel>().Create();
            _moduleServiceMock.Setup(x => x.UpdateModuleAsync(It.IsAny<Guid>(), moduleUpdateView))
                              .ReturnsAsync(expectedResult);
            //Act
            var result = await _moduleController.UpdateModule(It.IsAny<Guid>(), moduleUpdateView);
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}
