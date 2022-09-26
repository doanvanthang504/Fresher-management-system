using Application.Interfaces;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ImportViewModels;
using Infrastructures.Services;
using Microsoft.AspNetCore.Http;
using Moq;

namespace Infrastructures.Tests.Services
{
    public class ImportDataServiceTests : SetupTest
    {
        private readonly IImportDataFromExcelFileService _importDataService;

        public ImportDataServiceTests()
        {
            _importDataService = new ImportDataFromExcelFileService(_unitOfWorkMock.Object);
        }

        [Fact]
        public async void GetDataFromFileExcelAsync_ShouldReturnNull_WhenExcelFileIsNull()
        {

            // act
            IFormFile? mock = null;
            var result = await _importDataService.GetDataFromRECExcelFileAsync(mock);

            _importDataServiceMock.Verify(
                x => x.CreateClassCodeFromPackageDataReponseAsync(
                    It.IsAny<PackageReponseFromRECFileImportViewModel>()), Times.Never());
            // assert
            result.Should().BeNull();
        }

        [Fact]
        public async void GetDataFromFileExcelAsync_ShouldBeNull_WhenExcelMapperCanNotReadTheExcelFile()
        {

            // act
            IFormFile? mock = It.IsNotNull<IFormFile>();
            var result = await _importDataService.GetDataFromRECExcelFileAsync(mock);

            _importDataServiceMock.Verify(
                x => x.CreateClassCodeFromPackageDataReponseAsync(
                    It.IsAny<PackageReponseFromRECFileImportViewModel>()), Times.Never());
            // assert
            result.Should().BeNull();
        }

    }
}
