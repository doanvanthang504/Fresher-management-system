using Application.Interfaces;
using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ExportExcelExtensions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Infrastructures.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Services
{

    public class ExportDeliveryServiceTest : SetupTest
    {

        private readonly ExcelExportDeliveryService _exportDeliveryService;
        private readonly SaveWorkBook _saveWorkBook;
        private static ExcelPackage? _excelPackage;

        public ExportDeliveryServiceTest()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _excelPackage = new ExcelPackage();
            _saveWorkBook = new SaveWorkBook(_httpContextAccessorMock.Object);
            _exportDeliveryService = new ExcelExportDeliveryService(_currentTimeMock.Object, _saveWorkBook);
        }

        [Fact]
        public async Task Export_ExportMothodReturnLengthShouldBeHigherThanZero()
        {
            //arrange
            _httpContextAccessorMock.Setup(x => x.HttpContext.Response.Headers.Add(It.IsAny<string>(), It.IsAny<StringValues>()));

            var addWorkSheetTemplateMethod = typeof(ExcelExportDeliveryService)
                                                 .GetMethod("AddAllWorkSheet", BindingFlags.NonPublic | BindingFlags.Instance);

            object[] paramOfAddWorkSheetTemplateMethod = { _excelPackage };
            addWorkSheetTemplateMethod.Invoke(_exportDeliveryService, paramOfAddWorkSheetTemplateMethod);
            //act

            await _saveWorkBook.SaveFileAsync(_excelPackage, "filename");
            var data = await _exportDeliveryService.ExportAsync();
            //assert
            (data.FileContents.Length > 0).Should().Be(true);
        }
       
        [Fact]
        public void Export_AddAllWorkSheetMethodShouldNotThrowException()
        {
            //arrange
            var addAllWorkSheetMethod = typeof(ExcelExportDeliveryService)
                                                 .GetMethod("AddAllWorkSheet", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] paramOfAddAllWorkSheetMethod = { _excelPackage };

            //act
            var action = () => addAllWorkSheetMethod.Invoke(_exportDeliveryService, paramOfAddAllWorkSheetMethod);

            //assert
            action.Should().NotThrow();

        }

        [Fact]
        public void Export_AddWorkSheetTemplateMethodShouldThrowException()
        {
            //arrange
            var addWorkSheetTemplateMethod = typeof(ExcelExportDeliveryService)
                                                 .GetMethod("AddAllWorkSheet", BindingFlags.NonPublic | BindingFlags.Instance);

            //act
            var action = () => addWorkSheetTemplateMethod.Invoke(_exportDeliveryService, null);

            //assert    
            action.Should().Throw<TargetParameterCountException>();
        }
    }
}