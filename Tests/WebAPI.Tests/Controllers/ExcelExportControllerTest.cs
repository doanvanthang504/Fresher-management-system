using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Services
{
    public class ExcelExportControllerTest : SetupTest
    {
        private readonly MediaTypeHeaderValue _mediatypeHeader;
        private readonly ExportExcelController _exportExcelController;

        public ExcelExportControllerTest()
        {
            _exportExcelController = new ExportExcelController(_mockMEcelExportHistoryService.Object,
                                                              _mockMEcelExportDeliveryService.Object,
                                                              _mockMEcelExportChartService.Object,
                                                              _mockExcelExportScroreService.Object);
            _mediatypeHeader = new MediaTypeHeaderValue("application/octet-stream");
        }

        [Fact]
        public async Task ExportEmployeeTrainingHistory_ReturnLengthShouldBeHeigherThanZero()
        {
            var bytes = _fixture.Build<byte[]>().Create();
            var fileContentResult = new FileContentResult(bytes, _mediatypeHeader);
            var employeeTrainingHistoryOfCourseConfigurations = _fixture.Build<ExportCourseReportViewModel>().CreateMany(10).ToList();
            _mockMEcelExportHistoryService.Setup(x => x.ExportAsync(It.IsAny<List<ExportCourseReportViewModel>>()))
                    .ReturnsAsync(fileContentResult);
            var result = await _exportExcelController.ExportEmployeeTrainingHistoryAsync(employeeTrainingHistoryOfCourseConfigurations) as FileContentResult;
            var lenght = result.FileContents.Length;
            lenght.Should().NotBe(0);
        }

        [Fact]
        public async Task ExportEmployeeTrainingHistory_ReturnLengthShouldBeEqualZero()
        {
            var fileContentResult = new FileContentResult(new byte[] { }, _mediatypeHeader);
            var employeeTrainingHistoryOfCourseConfigurations = _fixture.Build<ExportCourseReportViewModel>().CreateMany(10).ToList();
            _mockMEcelExportHistoryService.Setup(x => x.ExportAsync(It.IsAny<List<ExportCourseReportViewModel>>()))
                    .ReturnsAsync(fileContentResult);
            var result = await _exportExcelController.ExportEmployeeTrainingHistoryAsync(employeeTrainingHistoryOfCourseConfigurations) as FileContentResult;
            var lenght = result.FileContents.Length;
            lenght.Should().Be(0);
        }

        [Fact]
        public async Task ExportEmployeeTrainingHistory_CallMethodShouldBeOnce()
        {
            //Arrange
            var employeeTrainingHistoryOfCourseConfigurations = _fixture.Build<ExportCourseReportViewModel>().CreateMany(10).ToList();

            //Act
            await _exportExcelController.ExportEmployeeTrainingHistoryAsync(employeeTrainingHistoryOfCourseConfigurations);

            //Assert
            _mockMEcelExportHistoryService.Verify(x => x.ExportAsync(employeeTrainingHistoryOfCourseConfigurations), Times.Once());
        }

        [Fact]
        public async Task ExportEmployeeTrainingDelivery_ReturnLengthShouldBeHeigherThanZero()
        {
            var bytes = _fixture.Build<byte[]>().Create();
            var fileContentResult = new FileContentResult(bytes, _mediatypeHeader);
            _mockMEcelExportDeliveryService.Setup(x => x.ExportAsync()).ReturnsAsync(fileContentResult);
            var result = await _exportExcelController.ExportEmployeeTrainingDeliveryAsync() as FileContentResult;
            var lenght = result.FileContents.Length;
            lenght.Should().NotBe(0);
        }

        [Fact]
        public async Task ExportEmployeeTrainingDelivery_ReturnLengthShouldBeEqualZero()
        {
            var fileContentResult = new FileContentResult(new byte[] { }, _mediatypeHeader);
            _mockMEcelExportDeliveryService.Setup(x => x.ExportAsync()).ReturnsAsync(fileContentResult);
            var result = await _exportExcelController.ExportEmployeeTrainingDeliveryAsync() as FileContentResult;
            var lenght = result.FileContents.Length;
            lenght.Should().Be(0);
        }

        [Fact]
        public async Task ExportEmployeeTrainingDelivery_CallMethodShouldBeOnce()
        {
            //Arrange

            //Act
            await _exportExcelController.ExportEmployeeTrainingDeliveryAsync();

            //Assert
            _mockMEcelExportDeliveryService.Verify(x => x.ExportAsync(), Times.Once());
        }
    }
}
