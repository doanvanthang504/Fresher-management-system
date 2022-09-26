using AutoFixture;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ReportsViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class FresherReportControllerTests: SetupTest
    {
        private readonly FresherReportController _fresherReportController; 
        public FresherReportControllerTests()
        {
            _fresherReportController = new FresherReportController
                                                (_fresherReportServiceMock.Object);
        }

        [Fact]
        public async Task GetMonthlyReportByFilterAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetFresherReportFilterViewModel>()
                                                             .With(x => x.Month, 5)
                                                             .Create();
            var mockData = _fixture.Build<ExportCourseReportViewModel>().CreateMany(10).ToList();

            _fresherReportServiceMock
                .Setup(x => x.GetMonthlyReportsByFilterAsync(mockGetFresherReportFilterViewModel))
                .ReturnsAsync(mockData);

            // act
            var result = await _fresherReportController
                                .GetMonthlyReportsByFilterAsync(mockGetFresherReportFilterViewModel);

            // assert
            _fresherReportServiceMock
                .Verify(x => x.GetMonthlyReportsByFilterAsync(
                                    mockGetFresherReportFilterViewModel),
                                    Times.Once());

            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetMonthlyReportByFilterAsync_ShouldReturnNothing_IfServiceReturnNothing()
        {
            // arrange
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetFresherReportFilterViewModel>()
                                                              .With(x => x.Month, 5)
                                                              .Create();

            _fresherReportServiceMock
                .Setup(x => x.GetMonthlyReportsByFilterAsync(mockGetFresherReportFilterViewModel))
                .ReturnsAsync(new List<ExportCourseReportViewModel>());

            // act
            var result = await _fresherReportController.GetMonthlyReportsByFilterAsync(
                                                            mockGetFresherReportFilterViewModel);

            // assert
            _fresherReportServiceMock
                .Verify(x => x.GetMonthlyReportsByFilterAsync(
                                    mockGetFresherReportFilterViewModel),
                                    Times.Once());

            result.Should().BeEquivalentTo(new List<ExportCourseReportViewModel>());
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldReturnNotice_IfInputValidateTrue()
        {
            // arrange
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateFresherReportViewModel>().Create();
            var mockId = Guid.NewGuid();
            var expectedResult = It.IsAny<string>();

            _fresherReportServiceMock
                .Setup(x => x.UpdateMonthlyReportAsync(mockId, mockUpdateFresherReportViewModel))
                .ReturnsAsync(expectedResult);

            // act
            var result = await _fresherReportController.UpdateMonthlyReportAsync(
                                                            mockId,
                                                            mockUpdateFresherReportViewModel);

            // assert
            _fresherReportServiceMock
                .Verify(x => x.UpdateMonthlyReportAsync(
                                    mockId,
                                    mockUpdateFresherReportViewModel),
                                    Times.Once());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetWeeklyFresherReportAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetWeeklyFresherReportFilterViewModel>()
                                                              .Create();
            var mockData = _fixture.Build<ExportCourseReportViewModel>().CreateMany(10).ToList();

            _fresherReportServiceMock
                .Setup(x => x.GetWeeklyFresherReportsByFilterAsync(mockGetFresherReportFilterViewModel))
                .ReturnsAsync(mockData);

            // act
            var result = await _fresherReportController
                                .GetWeeklyFresherReportAsync(mockGetFresherReportFilterViewModel);

            // assert
            _fresherReportServiceMock
                .Verify(x => x.GetWeeklyFresherReportsByFilterAsync(
                                    mockGetFresherReportFilterViewModel),
                                    Times.Once());

            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetWeeklyFresherReportAsync_ShouldReturnNothing_IfServiceReturnNothing()
        {
            // arrange
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetWeeklyFresherReportFilterViewModel>()
                                                              .Create();

            _fresherReportServiceMock
                .Setup(x => x.GetWeeklyFresherReportsByFilterAsync(mockGetFresherReportFilterViewModel))
                .ReturnsAsync(new List<ExportCourseReportViewModel>());

            // act
            var result = await _fresherReportController.GetWeeklyFresherReportAsync(
                                                            mockGetFresherReportFilterViewModel);

            // assert
            _fresherReportServiceMock
                .Verify(x => x.GetWeeklyFresherReportsByFilterAsync(
                                    mockGetFresherReportFilterViewModel),
                                    Times.Once());

            result.Should().BeEquivalentTo(new List<ExportCourseReportViewModel>());
        }

        //[Fact]
        //public async Task GenerateFresherReportAsync_ShouldReturnNull_IfServiceReturnNull()
        //{
        //    // arrange
        //    var mockListExportCourseReportViewModel = (List<ExportCourseReportViewModel>?)null;
        //    var anyBool = It.IsAny<bool>();
        //    var anyAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");

        //    _fresherReportServiceMock
        //        .Setup(x => x.GenerateFresherReportAsync(anyAdminId, anyBool, 0))
        //        .ReturnsAsync(mockListExportCourseReportViewModel);

        //    // act
        //    var result = await _fresherReportController.GenerateFresherReportAsync(anyBool);

        //    // assert
        //    _fresherReportServiceMock
        //        .Verify(x => x.GenerateFresherReportAsync(
        //                            anyAdminId,
        //                            anyBool,
        //                            0),
        //                            Times.Once());

        //    result.Should().BeEquivalentTo(mockListExportCourseReportViewModel);
        //}

        //[Fact]
        //public async Task GenerateFresherReportAsync_ShouldReturnData_IfSuccess()
        //{
        //    // arrange
        //    var mockListExportCourseReportViewModel = _fixture.Build<ExportCourseReportViewModel>()
        //                                                      .CreateMany(10)
        //                                                      .ToList();
        //    var anyBool = It.IsAny<bool>();
        //    var anyAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");

        //    _claimsServiceMock
        //        .Setup(x => x.CurrentUserId)
        //        .Returns(anyAdminId);
        //    _fresherReportServiceMock
        //        .Setup(x => x.GenerateFresherReportAsync(anyAdminId, anyBool, 0))
        //        .ReturnsAsync(mockListExportCourseReportViewModel);
        //    _claimsServiceMock
        //        .Setup(x => x.CurrentUserId)
        //        .Returns(anyAdminId);

        //    // act
        //    var result = await _fresherReportController.GenerateFresherReportAsync(anyBool);

        //    // assert
        //    _fresherReportServiceMock
        //        .Verify(x => x.GenerateFresherReportAsync(
        //                            anyAdminId,
        //                            anyBool,
        //                            0),
        //                            Times.Once());

        //    result.Should().BeEquivalentTo(mockListExportCourseReportViewModel);
        //}

        //[Fact]
        //public async Task GenerateFresherReportAsync_ShouldReturnFixedAmountData_IfSuccess()
        //{
        //    // arrange
        //    var mockListExportCourseReportViewModel = _fixture.Build<ExportCourseReportViewModel>()
        //                                                      .CreateMany(10)
        //                                                      .Take(6)
        //                                                      .ToList();
        //    var anyBool = It.IsAny<bool>();
        //    var anyAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");

        //    _claimsServiceMock
        //        .Setup(x => x.CurrentUserId)
        //        .Returns(anyAdminId);
        //    _fresherReportServiceMock
        //        .Setup(x => x.GenerateFresherReportAsync(anyAdminId, anyBool, 6))
        //        .ReturnsAsync(mockListExportCourseReportViewModel);
        //    _claimsServiceMock
        //        .Setup(x => x.CurrentUserId)
        //        .Returns(anyAdminId);

        //    // act
        //    var result = await _fresherReportController.GenerateFresherReportAsync(anyBool, 6);

        //    // assert
        //    _fresherReportServiceMock
        //        .Verify(x => x.GenerateFresherReportAsync(
        //                            anyAdminId,
        //                            anyBool,
        //                            6),
        //                            Times.Once());

        //    result.Should().BeEquivalentTo(mockListExportCourseReportViewModel);
        //}
    }
}
