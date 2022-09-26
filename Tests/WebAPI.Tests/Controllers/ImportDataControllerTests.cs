using Domain.Tests;
using FluentAssertions;
using Global.Shared.ViewModels.ImportViewModels;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class ImportDataControllerTests : SetupTest
    {
        private readonly ImportDataController _importDataController;

        public ImportDataControllerTests()
        {
            _importDataController = new ImportDataController(_importDataServiceMock.Object);
        }

        [Fact]
        public void GetDataFromImportFile_ShouldReturnCorrectData()
        {

            // arrange
            _importDataServiceMock.Setup(
                x => x.GetDataFromRECExcelFileAsync(It.IsAny<IFormFile>())).ReturnsAsync(new PackageReponseFromRECFileImportViewModel());
            // act
            var result = _importDataController.GetDataFromImportedRECFile(It.IsAny<IFormFile>());
            
            _importDataServiceMock.Verify(
                x => x.GetDataFromRECExcelFileAsync(
                    It.IsAny<IFormFile>()), Times.Once());
            // assert
            result.Should().BeOfType<Task<PackageReponseFromRECFileImportViewModel>>();

        }
    }
}
