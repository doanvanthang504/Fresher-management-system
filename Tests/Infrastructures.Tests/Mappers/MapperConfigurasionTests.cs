using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ChemicalsViewModels;
using Global.Shared.ViewModels.ReportsViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructures.Tests.Mappers
{
    public class MapperConfigurasionTests : SetupTest
    {
        [Fact]
        public void TestMapper()
        {
            //arrange
            var chemicalMock = _fixture.Build<Chemical>().Create();
            var fresherReport = _fixture.Build<FresherReport>().Create();
            var updateFresherReport = _fixture.Build<UpdateFresherReportViewModel>().Create();
            var fresherReportList = _fixture.Build<FresherReport>().CreateMany(10).ToList();

            //act
            var result = _mapperConfig.Map<ChemicalViewModel>(chemicalMock);
            var exportCourseReports = _mapperConfig.Map<List<ExportCourseReportViewModel>>(fresherReportList);
            var updatedFresherReport = _mapperConfig.Map(updateFresherReport, fresherReport);
            var updatedFresherReportForReturningToFe = _mapperConfig.Map<ExportCourseReportViewModel>(fresherReport);

            //assert
            result._Id.Should().Be(chemicalMock.Id.ToString());
            exportCourseReports.Should().HaveCount(10);
            updatedFresherReport.Id.Should().Be(fresherReport.Id.ToString());
            updatedFresherReportForReturningToFe.Id.Should().Be(fresherReport.Id.ToString());
        }
    }
}
