using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.ViewModels.PlanInfomationViewModels;
using Global.Shared.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class PlanControllerTest : SetupTest
    {
        private readonly PlanController _planController;

        public PlanControllerTest()
        {
            _planController = new PlanController(_planServiceMock.Object);
        }

        [Fact]
        public async Task GetAllPlan_ShouldReturns_CorrectData()
        {
            //arrange
            var expectedResult = _fixture.Build<Pagination<PlanGetViewModel>>().Create();
            _planServiceMock.Setup(x => x.GetAllPlanAsync(0, 10)).ReturnsAsync(expectedResult);
            //Act
            var result = await _planController.GetAllPlan(0, 10);
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task AddItemPlan_ShoutldReturnData_IfSuccess()
        {
            //Arrange 
            var expectedResult = _fixture.Build<PlanGetViewModel>().Create();
            var planAddView = _fixture.Build<PlanAddViewModel>().Create();
            _planServiceMock.Setup(x => x.AddPlanAsync(planAddView)).ReturnsAsync(expectedResult);
            //Act
            var result = await _planController.AddPlan(planAddView);
            //Assert
            Assert.Equal(expectedResult, ((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task GetPlanById_ReturnCorrectData_IfFound()
        {
            //Arrange
            var expectedResult = _fixture.Build<PlanGetViewModel>().Create();
            _planServiceMock.Setup(x => x.GetPlanByIdAsync(It.IsAny<Guid>()))
                            .ReturnsAsync(expectedResult);
            //Act
            var result = await _planController.GetPlanById(It.IsAny<Guid>());
            //Assert
            Assert.Equal(expectedResult, ((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task UpdatePlan_ReturnCorrectData_IfSuccess()
        {
            //Arrange
            var expectedResult = _fixture.Build<PlanGetViewModel>().Create();
            var planUpdateView = _fixture.Build<PlanUpdateViewModel>().Create();
            _planServiceMock.Setup(x => x.UpdatePlanAsync(It.IsAny<Guid>(), planUpdateView))
                                         .ReturnsAsync(expectedResult);
            //Act
            var result = await _planController.UpdatePlan(It.IsAny<Guid>(), planUpdateView);
            //Assert
            Assert.Equal(expectedResult, ((OkObjectResult)result).Value);
        }

        [Fact]
        public async Task ChoicePlanForClassAsync_ReturnPlanInfomation_OfClass_IfSuccess()
        {
            //Arrange
            var mock = _fixture.Build<ChoosePlanForClassViewModel>()
                               .Create();
            var expectedResult = _fixture.Build<PlanInformationViewModel>().CreateMany(100).ToList();
            _planServiceMock.Setup(x => x.ChoosePlanForClassAsync(mock)).ReturnsAsync(expectedResult);
            //Act
            var result = await _planController.ChoosePlanForClassAsync(mock);
            //Assert
            ((OkObjectResult)result).Value.Should().BeEquivalentTo(expectedResult);
        }
    }
}
