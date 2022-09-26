using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Commons;
using Global.Shared.Helpers;
using Global.Shared.ModelExport.ModelExportConfiguration;
using Global.Shared.ViewModels.ReportsViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class FresherReportServiceTests : SetupTest
    {
        private readonly IFresherReportService _fresherReportService;
        private readonly IFresherService _fresherService;

        public FresherReportServiceTests()
        {
            _fresherService = new FresherService(_unitOfWorkMock.Object, _mapperConfig);
            _fresherReportService = new FresherReportService(_unitOfWorkMock.Object, _mapperConfig);
        }

        [Fact]
        public async Task GetMonthlyReportsByFilterAsync_ShouldReturnData()
        {
            //arrange
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .CreateMany(10)
                                             .ToList();
            var expectedResult = _mapperConfig.Map<List<ExportCourseReportViewModel>>(mockRepositoryData);
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetFresherReportFilterViewModel>()
                                                              .With(x => x.Month, 5)
                                                              .Create();

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetMonthlyReportsByFilterAsync(
                    It.IsAny<Expression<Func<FresherReport, bool>>>()))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService
                                    .GetMonthlyReportsByFilterAsync(mockGetFresherReportFilterViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetMonthlyReportsByFilterAsync(
                                It.IsAny<Expression<Func<FresherReport, bool>>>()),
                                Times.Once());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetMonthlyReportsByFilterAsync_ShouldThrowException_IfInputIsNull()
        {
            //arrange
            var mockGetFresherReportFilterViewModel = (GetFresherReportFilterViewModel?)null;

            //act
            var result = () => _fresherReportService
                                    .GetMonthlyReportsByFilterAsync(mockGetFresherReportFilterViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetMonthlyReportsByFilterAsync(
                                It.IsAny<Expression<Func<FresherReport, bool>>>()),
                                Times.Never());

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldReturnNotice_IfSuccess()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .With(x => x.Id, mockFresherId)
                                             .Create();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.Update(mockUpdatedFresherReport));

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(1);

            //act
            var result = await _fresherReportService.UpdateMonthlyReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .Update(mockUpdatedFresherReport), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().BeEquivalentTo(Constant.UPDATE_REPORT_SUCCESSFULLY_NOTICE);
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldThrowArgumentNullException_IfUpdateInputIsNull()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = (UpdateFresherReportViewModel?)null;
            var mockRepositoryData = It.IsAny<FresherReport>();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            //act
            var result = () => _fresherReportService.UpdateMonthlyReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .Update(mockUpdatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldThrowArgumentException_IfIdIsEmptyGuid()
        {
            //arrange
            var mockFresherId = Guid.Empty;
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = It.IsAny<FresherReport>();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            //act
            var result = () => _fresherReportService.UpdateMonthlyReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .Update(mockUpdatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            await result.Should().ThrowAsync<ArgumentException>()
                                 .WithMessage(Constant.ID_CAN_NOT_EMPTY_NOTICE);
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldReturnNotice_IfFoundNothing()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = (FresherReport?)null;
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService.UpdateMonthlyReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .Update(mockUpdatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            result.Should().BeEquivalentTo(Constant.RETURN_NULL_NOTICE);
        }

        [Fact]
        public async Task UpdateMonthlyReportAsync_ShouldReturnNotice_IfCantUpdate()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .With(x => x.Id, mockFresherId)
                                             .Create();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);
            var expectedResult = _mapperConfig.Map<ExportCourseReportViewModel>(mockUpdatedFresherReport);


            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.Update(mockUpdatedFresherReport));

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(0);

            //act
            var result = await _fresherReportService.UpdateMonthlyReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .Update(mockUpdatedFresherReport), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().BeEquivalentTo(Constant.CAN_NOT_UPDATE_REPORT_NOTICE);
        }

        [Fact]
        public async Task GetWeeklyFresherReportsByFilterAsync_ShouldReturnData()
        {
            //arrange
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .CreateMany(10)
                                             .ToList();
            var expectedResult = _mapperConfig.Map<List<ExportCourseReportViewModel>>(mockRepositoryData);
            var mockGetFresherReportFilterViewModel = _fixture.Build<GetWeeklyFresherReportFilterViewModel>()
                                                              .With(x => x.FromDate, "05/01/2022")
                                                              .With(x => x.ToDate, "05/31/2022")
                                                              .Create();

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetWeeklyFresherReportsByFilterAsync(
                    It.IsAny<Expression<Func<FresherReport, bool>>>()))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService
                                    .GetWeeklyFresherReportsByFilterAsync(mockGetFresherReportFilterViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetWeeklyFresherReportsByFilterAsync(
                                It.IsAny<Expression<Func<FresherReport, bool>>>()),
                                Times.Once());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GetWeeklyFresherReportsByFilterAsync_ShouldThrowException_IfInputIsNull()
        {
            //arrange
            var mockGetFresherReportFilterViewModel = (GetWeeklyFresherReportFilterViewModel?)null;

            //act
            var result = () => _fresherReportService
                                    .GetWeeklyFresherReportsByFilterAsync(mockGetFresherReportFilterViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetWeeklyFresherReportsByFilterAsync(
                                It.IsAny<Expression<Func<FresherReport, bool>>>()),
                                Times.Never());

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task CreateWeeklyFresherReportAsync_ShouldThrowArgumentNullException_IfCreateInputIsNull()
        {
            //arrange
            var mockCreateFresherReportViewModel = (CreateWeeklyFresherReportViewModel?)null;
            var mockRepositoryData = It.IsAny<FresherReport>();
            var mockCreatedFresherReport = _mapperConfig.Map(mockCreateFresherReportViewModel,
                                                             mockRepositoryData);

            //act
            var result = () => _fresherReportService.CreateWeeklyFresherReportAsync(
                                                            mockCreateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .AddFresherReportAsync(mockCreatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateWeeklyFresherReportAsync_ShouldThrowArgumentNullException_IfReportIdIsNull()
        {
            //arrange
            var mockFresherId = Guid.Empty;
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateWeeklyFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = It.IsAny<FresherReport>();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            //act
            var result = () => _fresherReportService.UpdateWeeklyFresherReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .UpdateFresherReport(mockUpdatedFresherReport), Times.Never());

        }

        [Fact]
        public async Task CreateMonthlyReportAsync_ShouldThrowArgumentException_IfCourseCodeIsNullOrEmpty()
        {
            //arrange
            var emptyCourseCode = string.Empty;

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(0);

            //act
            var result = () => _fresherReportService.CreateMonthlyReportAsync(emptyCourseCode);

            //assert
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            await result.Should()
                        .ThrowAsync<ArgumentException>()
                        .WithMessage(Constant.COURSECODE_CAN_NOT_EMPTY_NOTICE);
        }

        [Fact]
        public async Task CreateMonthlyReportAsync_ShouldReturnNotice_IfCantFindClass()
        {
            //arrange
            var mockCourseCode = _fixture.Build<string>().Create();
            var mockRepositoryData = (ClassFresher?)null;

            _unitOfWorkMock
                .Setup(x => x.ClassFresherRepository.GetClassFresherByClassCodeAsync(mockCourseCode))
                .ReturnsAsync(mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(0);

            //act
            var result = await _fresherReportService.CreateMonthlyReportAsync(mockCourseCode);

            //assert
            _unitOfWorkMock
                .Verify(x => x.ClassFresherRepository
                              .GetClassFresherByClassCodeAsync(mockCourseCode), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            result.Should().BeEquivalentTo(Constant.RETURN_NULL_NOTICE);
        }

        [Fact]
        public async Task UpdateWeeklyFresherReportAsync_ShouldThrowArgumentNullException_IfUpdateInputIsNull()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = (UpdateWeeklyFresherReportViewModel?)null;
            var mockRepositoryData = It.IsAny<FresherReport>();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            //act
            var result = () => _fresherReportService.UpdateWeeklyFresherReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .UpdateFresherReport(mockUpdatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            await result.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task UpdateWeeklyFresherReportAsync_ShouldReturnNotice_IfNotFound()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateWeeklyFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = (FresherReport?)null;
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService.UpdateWeeklyFresherReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .UpdateFresherReport(mockUpdatedFresherReport), Times.Never());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Never());

            result.Should().BeEquivalentTo(string.Format("Record not found. Input id: {0}", mockFresherId));
        }

        [Fact]
        public async Task UpdateWeeklyFresherReportAsync_ShouldReturnNotice_IfCannotUpdate()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateWeeklyFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .With(x => x.Id, mockFresherId)
                                             .Create();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.UpdateFresherReport(mockUpdatedFresherReport));

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(0);

            //act
            var result = await _fresherReportService.UpdateWeeklyFresherReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .UpdateFresherReport(mockUpdatedFresherReport), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().BeEquivalentTo(
                string.Format("Cannot update weekly fresher reports. Saved {0} record(s).", 0));
        }

        [Fact]
        public async Task UpdateWeeklyFresherReportAsync_ShouldReturnNotice_IfSuccess()
        {
            //arrange
            var mockFresherId = Guid.NewGuid();
            var mockUpdateFresherReportViewModel = _fixture.Build<UpdateWeeklyFresherReportViewModel>()
                                                           .Create();
            var mockRepositoryData = _fixture.Build<FresherReport>()
                                             .With(x => x.Id, mockFresherId)
                                             .Create();
            var mockUpdatedFresherReport = _mapperConfig.Map(mockUpdateFresherReportViewModel,
                                                             mockRepositoryData);
            var expectedSavedRecord = 1;

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.GetByIdAsync(mockFresherId))
                .ReturnsAsync(mockRepositoryData);

            _unitOfWorkMock
                .Setup(x => x.FresherReportRepository.UpdateFresherReport(mockUpdatedFresherReport));

            _unitOfWorkMock
                .Setup(x => x.SaveChangeAsync())
                .ReturnsAsync(expectedSavedRecord);

            //act
            var result = await _fresherReportService.UpdateWeeklyFresherReportAsync(
                                                            mockFresherId,
                                                            mockUpdateFresherReportViewModel);

            //assert
            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .GetByIdAsync(mockFresherId), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.FresherReportRepository
                              .UpdateFresherReport(mockUpdatedFresherReport), Times.Once());

            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());

            result.Should().BeEquivalentTo(
                string.Format("Updated {0} record(s).", expectedSavedRecord));
//            result.Should().BeEquivalentTo(Constant.RETURN_NULL_NOTICE);
        }

        //[Fact]
        //public async Task CreateMonthlyReportAsync_ShouldReturnNotice_IfCantSaveChanges()
        //{
        //    //arrange
        //    var mockCourseCode = _fixture.Build<string>().Create();
        //    var mockFresherData = _fixture.Build<Fresher>()
        //                                  .With(f => f.ClassCode, mockCourseCode)
        //                                  .With(f => f.RRCode, "FSO.HCM.FHO.FA.G0.SG_2022.57_3")
        //                                  .Without(f => f.ClassFresher)
        //                                  .CreateMany(5)
        //                                  .ToList();
        //    var mockRepositoryData = _fixture.Build<ClassFresher>()
        //                                     .With(x => x.ClassCode, mockCourseCode)
        //                                     .With(x => x.Freshers, mockFresherData)
        //                                     .Create();
        //    var mockCreatedFresherReport = _mapperConfig.Map<IEnumerable<FresherReport>>(mockRepositoryData)
        //                                                .ToList();

        //    _unitOfWorkMock
        //        .Setup(x => x.ClassFresherRepository.GetClassFresherByClassCodeAsync(mockCourseCode))
        //        .ReturnsAsync(mockRepositoryData);

        //    _unitOfWorkMock
        //        .Setup(x => x.FresherReportRepository.AddRangeAsync(mockCreatedFresherReport))
        //        .Returns(Task.CompletedTask);

        //    _unitOfWorkMock
        //        .Setup(x => x.SaveChangeAsync())
        //        .ReturnsAsync(1);

        //    //act
        //    var result = await _fresherReportService.CreateMonthlyReportAsync(mockCourseCode);

        //    //assert
        //    _unitOfWorkMock
        //        .Verify(x => x.ClassFresherRepository
        //                      .GetClassFresherByClassCodeAsync(mockCourseCode), Times.Once());

        //    _unitOfWorkMock
        //        .Verify(x => x.FresherReportRepository
        //                      .AddRangeAsync(mockCreatedFresherReport), Times.Once());

        //    _unitOfWorkMock
        //        .Verify(x => x.SaveChangeAsync(), Times.Once());

        //    result.Should().BeEquivalentTo(Constant.CAN_NOT_CREATE_REPORT_NOTICE);
        //}

        [Fact]
        public async Task GenerateFresherReportAsync_ShouldThrowArgumentException_IfAdminIdIsEmpty()
        {
            //arrange
            var emptyAdminId = Guid.Empty;
            var anyBoolean = It.IsAny<bool>();

            //act
            var result = () => _fresherReportService
                                    .GenerateFresherReportAsync(emptyAdminId, anyBoolean);

            //assert
            await result.Should()
                        .ThrowAsync<ArgumentException>()
                        .WithMessage(Constant.ID_CAN_NOT_EMPTY_NOTICE);
        }

        [Fact]
        public async Task GenerateFresherReportAsync_ShouldReturnNull_IfCantFindClass()
        {
            //arrange
            var mockAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");
            var mockAdmin = _fixture.Build<User>()
                                    .With(x=>x.Id, mockAdminId)
                                    .Create();
            var mockAdminName = mockAdmin.Username;
            var mockRepositoryData = (List<ClassFresher>?)null;
            var anyBoolean = It.IsAny<bool>();


            _unitOfWorkMock
                .Setup(x => x.UserRepository.GetByIdAsync(mockAdminId))
                .ReturnsAsync(mockAdmin);
            _unitOfWorkMock
                .Setup(x => x.ClassFresherRepository.GetClassFresherByAdminNameAsync(mockAdminName))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService
                                    .GenerateFresherReportAsync(mockAdminId, anyBoolean);

            //assert
            _unitOfWorkMock
                .Verify(x => x.ClassFresherRepository
                              .GetClassFresherByAdminNameAsync(mockAdminName), Times.Once());
            _unitOfWorkMock
                .Verify(x => x.UserRepository
                              .GetByIdAsync(mockAdminId), Times.Once());

            result.Should().BeEquivalentTo((List<ExportCourseReportViewModel>?)null);
        }

        [Fact]
        public async Task GenerateFresherReportAsync_ShouldReturnListReport_IfSuccess()
        {
            //arrange
            var mockAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");
            var mockAdmin = _fixture.Build<User>()
                                    .With(x => x.Id, mockAdminId)
                                    .Create();
            var mockAdminName = mockAdmin.Username;
            var anyGuid = Guid.NewGuid();
            var mockCourseCode = _fixture.Build<string>().Create();
            var mockFresherData = _fixture.Build<Fresher>()
                                          .With(f => f.ClassCode, mockCourseCode)
                                          .With(f => f.RRCode, "FSO.HCM.FHO.FA.G0.SG_2022.57_3")
                                          .Without(x => x.ModuleResults)
                                          .Without(f => f.ClassFresher)
                                          .Without(f => f.Attendances)
                                          .CreateMany(5)
                                          .ToList();
            var mockRepositoryData = _fixture.Build<ClassFresher>()
                                             .With(x => x.NameAdmin1, mockAdminName)
                                             .With(x => x.ClassCode, mockCourseCode)
                                             .With(x => x.Freshers, mockFresherData)
                                             .CreateMany(2)
                                             .ToList();
            var expectedResult = new List<ExportCourseReportViewModel>();
            for(var i = 0; i< mockRepositoryData.Count; i++)
            {
                var mapperItems = _mapperConfig.Map<IEnumerable<ExportCourseReportViewModel>>
                                                                (mockRepositoryData[i])
                                               .ToList();
                expectedResult.AddRange(mapperItems);
            }
            var anyBoolean = It.IsAny<bool>();
            if (anyBoolean)
            {
                expectedResult.ForEach(r => r.FromDate = DateTimeHelper.GetStartDateOfMonth());
                expectedResult.ForEach(r => r.ToDate = DateTimeHelper.GetEndDateOfMonth());
            }
            else
            {
                expectedResult.ForEach(r => r.FromDate = DateTimeHelper.GetMonday());
                expectedResult.ForEach(r => r.ToDate = DateTimeHelper.GetSunday());
            }

            _unitOfWorkMock
                .Setup(x => x.UserRepository.GetByIdAsync(mockAdminId))
                .ReturnsAsync(mockAdmin);
            _unitOfWorkMock
                .Setup(x => x.ClassFresherRepository.GetClassFresherByAdminNameAsync(mockAdminName))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService
                                    .GenerateFresherReportAsync(mockAdminId, anyBoolean);
            result.ForEach(x => x.Id = anyGuid);
            expectedResult.ForEach(x => x.Id = anyGuid);

            //assert
            _unitOfWorkMock
                .Verify(x => x.ClassFresherRepository
                              .GetClassFresherByAdminNameAsync(mockAdminName), Times.Once());
            _unitOfWorkMock
                .Verify(x => x.UserRepository
                              .GetByIdAsync(mockAdminId), Times.Once());

            result.Should().BeEquivalentTo(expectedResult);
        }

        [Fact]
        public async Task GenerateFresherReportAsync_ShouldReturnListReportWithFixedAmount_IfSuccess()
        {
            //arrange
            var mockAdminId = Guid.Parse("540de93c-0f2e-4cfc-9ffd-91cb5317c1ab");
            var mockAdmin = _fixture.Build<User>()
                                    .With(x => x.Id, mockAdminId)
                                    .Create();
            var mockAdminName = mockAdmin.Username;
            var anyGuid = Guid.NewGuid();
            var mockCourseCode = _fixture.Build<string>().Create();
            var mockFresherData = _fixture.Build<Fresher>()
                                          .With(f => f.ClassCode, mockCourseCode)
                                          .With(f => f.RRCode, "FSO.HCM.FHO.FA.G0.SG_2022.57_3")
                                          .Without(f => f.ClassFresher)
                                          .Without(f => f.Attendances)
                                          .Without(f => f.ModuleResults)
                                          .CreateMany(5)
                                          .ToList();
            var mockRepositoryData = _fixture.Build<ClassFresher>()
                                             .With(x => x.NameAdmin1, mockAdminName)
                                             .With(x => x.ClassCode, mockCourseCode)
                                             .With(x => x.Freshers, mockFresherData)
                                             .CreateMany(2)
                                             .ToList();
            var expectedResult = new List<ExportCourseReportViewModel>();
            for (var i = 0; i < mockRepositoryData.Count; i++)
            {
                var mapperItems = _mapperConfig.Map<IEnumerable<ExportCourseReportViewModel>>
                                                                (mockRepositoryData[i])
                                               .ToList();
                expectedResult.AddRange(mapperItems);
            }
            var anyBoolean = It.IsAny<bool>();
            if (anyBoolean)
            {
                expectedResult.ForEach(r => r.FromDate = DateTimeHelper.GetStartDateOfMonth());
                expectedResult.ForEach(r => r.ToDate = DateTimeHelper.GetEndDateOfMonth());
            }
            else
            {
                expectedResult.ForEach(r => r.FromDate = DateTimeHelper.GetMonday());
                expectedResult.ForEach(r => r.ToDate = DateTimeHelper.GetSunday());
            }

            _unitOfWorkMock
                .Setup(x => x.UserRepository.GetByIdAsync(mockAdminId))
                .ReturnsAsync(mockAdmin);
            _unitOfWorkMock
                .Setup(x => x.ClassFresherRepository.GetClassFresherByAdminNameAsync(mockAdminName))
                .ReturnsAsync(mockRepositoryData);

            //act
            var result = await _fresherReportService
                                    .GenerateFresherReportAsync(mockAdminId, anyBoolean, 6);
            result.ForEach(x => x.Id = anyGuid);
            expectedResult.ForEach(x => x.Id = anyGuid);
            var realExpectedResult = expectedResult.Take(6).ToList();

            //assert
            _unitOfWorkMock
                .Verify(x => x.ClassFresherRepository
                              .GetClassFresherByAdminNameAsync(mockAdminName), Times.Once());
            _unitOfWorkMock
                .Verify(x => x.UserRepository
                              .GetByIdAsync(mockAdminId), Times.Once());

            result.Should().BeEquivalentTo(realExpectedResult);
        }
    }
}
