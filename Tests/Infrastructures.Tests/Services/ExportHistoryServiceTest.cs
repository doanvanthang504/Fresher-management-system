using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ExportExcelExtensions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Infrastructures.Services;
using Microsoft.Extensions.Primitives;
using Moq;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Services
{
    public class ExportHistoryServiceTest : SetupTest
    {

        private readonly ExcelExportHistoryService _exportHistoryService;
        private readonly SaveWorkBook _saveWorkBook;
        private static List<ExportCourseReportViewModel>? _fakeExportCourseReportViewModel;
        private static ExcelPackage? _excelPackage;

        public ExportHistoryServiceTest()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _excelPackage = new ExcelPackage();
            _saveWorkBook = new SaveWorkBook(_httpContextAccessorMock.Object);
            _exportHistoryService = new ExcelExportHistoryService(_currentTimeMock.Object, _saveWorkBook);
            _fakeExportCourseReportViewModel = _fixture.Build<ExportCourseReportViewModel>().CreateMany(3).ToList();
        }

        private static IEnumerable<object[]> DataTheoryTest()
        {
            yield return new object[] { _excelPackage, _fakeExportCourseReportViewModel };
            yield return new object[] { _excelPackage, null };
            yield return new object[] { null, _fakeExportCourseReportViewModel };
            yield return new object[] { null, null };
        }

        [Fact]
        public async Task Export_ReturnLengthShouldBeHigherThanZero()
        {
            //arrange
            var mediaTypeHeaderValue = new MediaTypeHeaderValue("application/octet-stream");
            _httpContextAccessorMock.Setup(x => x.HttpContext.Response.Headers.Add(It.IsAny<string>(), It.IsAny<StringValues>()));
            //Call private methods
            var addWorkSheetTemplateMethod = typeof(ExcelExportHistoryService)
                                                .GetMethod("AddWorkSheetTemplate", BindingFlags.NonPublic | BindingFlags.Instance);
            var addWorksheetOfCourse = typeof(ExcelExportHistoryService)
                                                .GetMethod("AddWorksheetOfCourse", BindingFlags.NonPublic | BindingFlags.Instance|BindingFlags.Static);
            
            object[] paramOfAddWorkSheetTemplateMethod = { _excelPackage };
            object[] paramOfAddWorksheetOfCourse = { _excelPackage, _fakeExportCourseReportViewModel };
           
            //act
            addWorkSheetTemplateMethod.Invoke(_exportHistoryService, paramOfAddWorkSheetTemplateMethod);
            addWorksheetOfCourse.Invoke(_exportHistoryService, paramOfAddWorksheetOfCourse);
           
            await _saveWorkBook.SaveFileAsync(_excelPackage, "filename");
            var result=await _exportHistoryService.ExportAsync(_fakeExportCourseReportViewModel);
            //assert
            (result.FileContents.Length > 0).Should().Be(true);

        }

        [Fact]
        public void Export_AddWorkSheetTemplateMethodShouldNotThrowException()
        {
            //arrange
            var addWorkSheetTemplateMethod = typeof(ExcelExportHistoryService)
                                                 .GetMethod("AddWorkSheetTemplate", BindingFlags.NonPublic | BindingFlags.Instance);
            object[] paramOfAddWorkSheetTemplateMethod = { _excelPackage };
            
            //act
            var action = () => addWorkSheetTemplateMethod.Invoke(_exportHistoryService, paramOfAddWorkSheetTemplateMethod);
            
            //assert
            action.Should().NotThrow();

        }

        [Fact]
        public void Export_AddWorkSheetTemplateMethodShouldThrowException()
        {
            //arrange
            var addWorkSheetTemplateMethod = typeof(ExcelExportHistoryService)
                                                 .GetMethod("AddWorkSheetTemplate", BindingFlags.NonPublic | BindingFlags.Instance);

            //act
            var action = () => addWorkSheetTemplateMethod.Invoke(_exportHistoryService, null);
            
            //assert    
            action.Should().Throw<TargetParameterCountException>();
        }

        [Theory]
        [MemberData(nameof(DataTheoryTest))]
        public void Export_AddWorksheetOfCourseMethodShouldThrowException(ExcelPackage excelPackage, List<ExportCourseReportViewModel>? data)
        {
            //arrange
            var addWorksheetOfCourse = typeof(ExcelExportHistoryService)
                                            .GetMethod("AddWorksheetOfCourse", BindingFlags.NonPublic | BindingFlags.Instance);
            object?[]? paramOfAddWorksheetOfCourse = { excelPackage, data };
          
            //act
            var action = () => addWorksheetOfCourse.Invoke(_exportHistoryService, paramOfAddWorksheetOfCourse);
          
            //assert    
            action.Should().Throw<Exception>();
        }

        [Fact]
        public void Export_AddWorksheetOfCourseMethodShouldNotThrowException()
        {
            //arrange
            object[] paramOfAddWorksheetOfCourse = { _excelPackage, _fakeExportCourseReportViewModel };
            var addWorksheetOfCourse = typeof(ExcelExportHistoryService)
                                .GetMethod("AddWorksheetOfCourse", BindingFlags.NonPublic | BindingFlags.Instance|BindingFlags.Static);
            //act
            var action = () => addWorksheetOfCourse.Invoke(_exportHistoryService, paramOfAddWorksheetOfCourse);
            
            //assert    
            action.Should().NotThrow();
        }
    }
}