using Application.Interfaces;
using Application.Services;
using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Settings.Reminder;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Global.Shared.ViewModels.FresherViewModels;
using Global.Shared.ViewModels.ReminderViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class AuditResultServiceTest : SetupTest
    {
        private readonly IAuditManagementService _auditManagementService;
        private readonly IReminderService _reminderService;
        private readonly ReminderSettings _reminderSettings;

        public AuditResultServiceTest()
        {
            _auditManagementService = new AuditManagementService(_unitOfWorkMock.Object, _mapperConfig, _reminderService!);
            _reminderSettings = new ReminderSettings
            {
                AuditReminderTime = new AuditReminderTime()
            };

            _reminderService = new ReminderService(_unitOfWorkMock.Object, _mapperConfig, _mailServiceMock.Object, _reminderSettings);
        }
        // get audit by class id
        [Fact]
        public async Task GetAuditByClassIdAsync()
        {
            //arrange
            var mocks = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(100).ToList();
            var expectedResult = _mapperConfig.Map<List<GetPlanAuditViewModel>>(mocks);

            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetAuditbyClassIdAsync(It.IsAny<Guid>())).ReturnsAsync(mocks);

            //act
            var result = await _auditManagementService.GetAuditByClassIdAsync(It.IsAny<Guid>());

            //assert
            _unitOfWorkMock.Verify(x => x.AuditManagementRepository.GetAuditbyClassIdAsync(It.IsAny<Guid>()), Times.Once());
            result.Data.Should().BeEquivalentTo(expectedResult);
        }
        // update audit for student
        [Fact]
        public async Task UpdateAuditForStudentAsync()
        {
            // arrange
            var id = Guid.NewGuid();
            var updateAuditViewModel = _fixture.Build<UpdateAuditViewModel>().Create();
            var auditResult = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).Create();
            
            var listScoreAudit = new List<double>()
            {
                72, 84, 100, 99
            };
            var listEvaluate = new List<Evaluate>
            {
                    (Evaluate)updateAuditViewModel.EvaluateQ1!,
                    (Evaluate)updateAuditViewModel.EvaluateQ2!,
                    (Evaluate)updateAuditViewModel.EvaluateQ3!,
                    (Evaluate)updateAuditViewModel.EvaluateQ4!
            };
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetScoreFromEvaluateAsync(listEvaluate)).ReturnsAsync(listScoreAudit);
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetByIdAsync(id)).ReturnsAsync(auditResult);
            auditResult = _mapperConfig.Map(updateAuditViewModel, auditResult);
            var auditUpdate = _mapperConfig.Map<AuditResult>(auditResult);
            
            _unitOfWorkMock
                .Setup(x => x.AuditManagementRepository.Update(auditResult));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            var responUpdateAuditResult = new AuditManagementResponse
            {
                Data = auditResult,
                Success = true,
                Message = "Update success"
            };
            //act
            var result = await _auditManagementService.UpdateAuditForStudentAsync(id, updateAuditViewModel);

            //assert
            _unitOfWorkMock
                .Verify(
                    x => x.AuditManagementRepository.Update(auditResult),
                    Times.Once());
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());
        }
        // delete plan audit
        [Fact]
        public async Task DeleteAuditResultAsync()
        {
            // arrange
            var auditResult = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).Create();
            var id = Guid.NewGuid();
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetByIdAsync(id)).ReturnsAsync(auditResult);
            auditResult.IsDeleted = true;
            var result = _mapperConfig.Map<AuditResult>(auditResult);
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.Update(result));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);

            // act
            await _auditManagementService.DeleteAuditResultAsync(id);

            //assert
            _unitOfWorkMock
                .Verify(
                    x => x.AuditManagementRepository.Update(auditResult),
                    Times.Once());
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());
        }
        // add auditor for plan audit
        [Fact]
        public async Task AddAuditorForPlanAudit()
        {
            // arrange
            var get = _fixture.Build<GetAuditAndAuditorViewModel>().Create();
            var auditResult = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(10).ToList();
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetPlanAuditByClassIdAndNameModuelAsync(get
                                                                                .GetAuditByClassIdAndNumberAuditViewModel))
                                                                                .ReturnsAsync(auditResult);   
            var listAuditResultMap = _mapperConfig.Map<List<AuditResult>>(auditResult);
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.UpdateRange(listAuditResultMap));
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(auditResult.Count);

            //act
            await _auditManagementService.AddAuditorForPlanAuditAsync(get);

            // assert
            _unitOfWorkMock
                .Verify(
                    x => x.AuditManagementRepository.UpdateRange(listAuditResultMap),
                    Times.Once());
            _unitOfWorkMock
                .Verify(x => x.SaveChangeAsync(), Times.Once());
        }
        // get plan audit
        [Fact]
        public async Task GetPlanAuditAsync()
        {
            // arrange
            var listClass = _fixture.Build<ClassFresher>().Without(x => x.Freshers).CreateMany(3).ToList();
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetAllAsync()).ReturnsAsync(listClass);
            var listPlanAuditViewModel = _mapperConfig.Map<List<ClassViewModel>>(listClass);
            var responGetPlanAudit = new AuditManagementResponse
            {
                Data = listPlanAuditViewModel,
                Message = "Get success",
                Success = true
            };

            // act
            var result = await _auditManagementService.GetAllAuditPlansAsync();

            // assert
            result.Should().BeEquivalentTo(responGetPlanAudit);
        }
        // get detail plan audit
        [Fact]
        public async Task GetDetailPlanAuditAsync()
        {
            // arrange
            var auditViewModel = _fixture.Build<GetAuditByClassIdAndNumberAuditViewModel>().Create();
            var listIdAuditor = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid()
            };
            var _dbSet = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(5).ToList();
            for (int i = 0; i < listIdAuditor.Count; i++)
            {
                _dbSet[i].AuditorId = listIdAuditor[i].ToString();
                _dbSet[i].ClassFresherId = auditViewModel.classId;
                _dbSet[i].NumberAudit = auditViewModel.numberAudit;
                _dbSet[i].ModuleName = auditViewModel.nameModule;
            }
            var listAuditor = _fixture.Build<Auditor>().CreateMany(5).ToList();
            for (int i = 0; i < listIdAuditor.Count; i++)
            {
                listAuditor[i].Id = Guid.Parse(_dbSet[i].AuditorId.ToString());
            }
            for (int i = 0; i < listAuditor.Count; i++)
            {
                _unitOfWorkMock.Setup(x => x.AuditorRepository.GetByIdAsync(listAuditor[i].Id)).ReturnsAsync(listAuditor[i]);
            }
            var listFresher = _fixture.Build<Fresher>().Without(x => x.ClassFresher)
                                                       .Without(x => x.Attendances)
                                                       .Without(x => x.ModuleResults)
                                                       .CreateMany(5)
                                                       .ToList();
            for (int i = 0; i < listFresher.Count; i++)
            {
                listFresher[i].ClassFresherId = auditViewModel.classId;
            }
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetAllAsync()).ReturnsAsync(_dbSet);
            _unitOfWorkMock.Setup(x => x.FresherRepository.GetAllAsync()).ReturnsAsync(listFresher);
            var detailPlanAudit = _fixture.Build<GetDetailPlanAuditViewModel>().CreateMany(5).ToList();

            // act
            var result = await _auditManagementService.GetDetailPlanAuditAsync(auditViewModel);
            var responGetDetailPlanAudit = new AuditManagementResponse
            {
                Data = result.Data,
                Message = "Get success",
                Success = true
            };

            // assert
            result.Should().BeEquivalentTo(responGetDetailPlanAudit);
        }
        // get all auditor return count > 0
        [Fact]
        public async Task GetAllAuditorAsync()
        {
            // arrange
            var listAuditor = _fixture.Build<Auditor>().CreateMany(100).ToList();
            _unitOfWorkMock.Setup(x => x.AuditorRepository.GetAllAsync()).ReturnsAsync(listAuditor);
            var responGetAllAuditor = new AuditManagementResponse
            {
                Data = listAuditor,
                Message = "Get data success",
                Success = true
            };

            // act
            var result = await _auditManagementService.GetAllAuditorAsync();

            // assert
            result.Should().BeEquivalentTo(responGetAllAuditor);
        }
        // test get all auditor return count  == 0
        [Fact]
        public async Task GetAllAuditorAsync_ReturnNotFound()
        {
            // arrange
            var listAuditor = _fixture.Build<Auditor>().CreateMany(0).ToList();
            _unitOfWorkMock.Setup(x => x.AuditorRepository.GetAllAsync()).ReturnsAsync(listAuditor);
            var responGetAllAuditor_NotFound = new AuditManagementResponse
            {
                Message = "Not hava data",
                Success = false
            };

            // act
            var result = await _auditManagementService.GetAllAuditorAsync();

            // assert
            result.Should().BeEquivalentTo(responGetAllAuditor_NotFound);
        }
        // test get all fresher in class true count > 0
        [Fact]
        public async Task GetAllFresherIdInClassAsync()
        {
            // arrange
            var listFresher = _fixture.Build<Fresher>()
                                      .Without(x => x.ClassFresher)
                                      .Without(x => x.Attendances)
                                      .Without(x => x.ModuleResults)
                                      .CreateMany(100).ToList();
            Guid idClass = Guid.NewGuid();
            foreach (var item in listFresher)
            {
                item.ClassFresherId = idClass;
            }
            _unitOfWorkMock.Setup(x => x.FresherRepository.FindAsync(x => x.ClassFresherId == idClass, null, null))
                                                          .ReturnsAsync(listFresher);
            var listFresherMap = _mapperConfig.Map<List<FresherViewModel>>(listFresher);
            
            var respon = new AuditManagementResponse
            {
                Data = listFresherMap,
                Success = true,
                Message = "Get data success"
            };

            // act
            var result = await _auditManagementService.GetAllFresherIdInClassAsync(idClass);

            // assert
            result.Should().BeEquivalentTo(respon);
        }
        // test create plan audit return true
        [Fact]
        public async Task CreatePlanAuditForMemberInClassAsync()
        {
            // arrange
            var listFresher = _fixture.Build<AddFresherViewModel>().CreateMany(5).ToList();
            var createPlanAudit = _fixture.Build<CreateAuditViewModel>().Without(x => x.fresherViewModels).Create();
            var datetime = DateTime.Now;
            createPlanAudit.DateStart = datetime.ToString();
            createPlanAudit.fresherViewModels = listFresher;
            int numberAudit = 0;
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetClassFreshersAsync(createPlanAudit.ClassFresherId))
                                                                  .ReturnsAsync(numberAudit);
            var classFresher = _fixture.Build<ClassFresher>()
                                       .Without(x => x.Freshers)
                                       .Create();
            createPlanAudit.ClassFresherId = classFresher.Id;
            var listAuditNew = new List<AddAuditViewModel>();
            var listAddAuditViewModel = _fixture.Build<AddAuditViewModel>().CreateMany(createPlanAudit.fresherViewModels.Count).ToList();
            for (int i = 0; i < createPlanAudit.fresherViewModels.Count; i++)
            {
                listAddAuditViewModel[i].ClassFresherId = createPlanAudit.ClassFresherId.ToString();
                listAddAuditViewModel[i].ModuleName = createPlanAudit.ModuleName;
                listAddAuditViewModel[i].FresherId = createPlanAudit.fresherViewModels[i].Id.ToString();
                listAddAuditViewModel[i].NumberAudit = byte.Parse((numberAudit + 1).ToString());
                listAddAuditViewModel[i].DateStart = DateTime.Parse(createPlanAudit.DateStart);
                listAddAuditViewModel[i].Status = false;
                listAuditNew.Add(listAddAuditViewModel[i]);
            }
            
            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(createPlanAudit.ClassFresherId))
                                                               .ReturnsAsync(classFresher);

            var lisAuditMapped = _mapperConfig.Map<List<AuditResult>>(listAuditNew);

            foreach(var item in lisAuditMapped)
            {
                _unitOfWorkMock.Setup(x => x.AuditManagementRepository.AddAsync(item));
            }

            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(lisAuditMapped.Count);

            var mocks = new CreateReminderViewModel
            {
                Description = "On the day " + datetime + " have audit meeting for class " + lisAuditMapped[0].ModuleName,
                Subject = "Audit module " + lisAuditMapped[0].ModuleName + " class " + classFresher.ClassName + " on " + datetime,
                EventTime = datetime,
                ReminderType = ReminderType.Audit,
                ReminderEmail = "tranvantiep2506@gmail.com"
            };
            var remider = _mapperConfig.Map<Reminder>(mocks);
            _unitOfWorkMock.Setup(x => x.ReminderRepository.AddAsync(remider))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            var result = await _reminderService.CreateReminderAsync(mocks);

            // act

            var resultCreatePlanAudit = await _auditManagementService.CreatePlanAuditForMemberInClassAsync(createPlanAudit);

            // assert

            result.Should().NotBeNull();

        }
        // test create plan audit return false
        [Fact]
        public async Task CreatePlanAuditForMemberInClassAsync_Fail()
        {
            // arrange
            
            var listFresher = _fixture.Build<AddFresherViewModel>().CreateMany(5).ToList();
            var createPlanAudit = _fixture.Build<CreateAuditViewModel>().Without(x => x.fresherViewModels).Create();
            var datetime = DateTime.Now;
            createPlanAudit.DateStart = datetime.ToString();
            createPlanAudit.fresherViewModels = listFresher;
            int numberAudit = 0;
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetClassFreshersAsync(createPlanAudit.ClassFresherId))
                                                                  .ReturnsAsync(numberAudit);
            var classFresher = _fixture.Build<ClassFresher>()
                                       .Without(x => x.Freshers)
                                       .Create();
            createPlanAudit.ClassFresherId = classFresher.Id;
            var listAuditNew = new List<AddAuditViewModel>();
            var listAddAuditViewModel = _fixture.Build<AddAuditViewModel>().CreateMany(createPlanAudit.fresherViewModels.Count).ToList();
            for (int i = 0; i < createPlanAudit.fresherViewModels.Count; i++)
            {
                listAddAuditViewModel[i].ClassFresherId = createPlanAudit.ClassFresherId.ToString();
                listAddAuditViewModel[i].ModuleName = createPlanAudit.ModuleName;
                listAddAuditViewModel[i].FresherId = createPlanAudit.fresherViewModels[i].Id.ToString();
                listAddAuditViewModel[i].NumberAudit = byte.Parse((numberAudit + 1).ToString());
                listAddAuditViewModel[i].DateStart = DateTime.Parse(createPlanAudit.DateStart);
                listAddAuditViewModel[i].Status = false;
                listAuditNew.Add(listAddAuditViewModel[i]);
            }

            _unitOfWorkMock.Setup(x => x.ClassFresherRepository.GetByIdAsync(createPlanAudit.ClassFresherId))
                                                               .ReturnsAsync(classFresher);
            var lisAuditMapped = _mapperConfig.Map<List<AuditResult>>(listAuditNew);
            for (int i = 0; i < lisAuditMapped.Count; i++)
            {
                if (i == lisAuditMapped.Count - 1)
                    continue;
                _unitOfWorkMock.Setup(x => x.AuditManagementRepository.AddAsync(lisAuditMapped[i]));
            }
            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(lisAuditMapped.Count);

            //arrange
            var mocks = new CreateReminderViewModel
            {
                Description = "On the day " + datetime + " have audit meeting for class " + lisAuditMapped[0].ModuleName,
                Subject = "Audit module " + lisAuditMapped[0].ModuleName + " class " + classFresher.ClassName + " on " + datetime,
                EventTime = datetime,
                ReminderType = ReminderType.Audit,
                ReminderEmail = "tranvantiep2506@gmail.com"
            };
            var remider = _mapperConfig.Map<Reminder>(mocks);
            _unitOfWorkMock.Setup(x => x.ReminderRepository.AddAsync(remider))
                .Returns(Task.CompletedTask);

            _unitOfWorkMock.Setup(x => x.SaveChangeAsync()).ReturnsAsync(1);
            var result = await _reminderService.CreateReminderAsync(mocks);

            // act
            var resultCreatePlanAudit = await _auditManagementService.CreatePlanAuditForMemberInClassAsync(createPlanAudit);

            // assert

            result.Should().NotBeNull();
            var respon = new AuditManagementResponse
            {
                Data = listAuditNew,
                Message = "Error while Savechange",
                Success = false
            };
            resultCreatePlanAudit.Message.Should().BeEquivalentTo(respon.Message);
        }
        // test count auditor of class 
        [Fact]
        public async Task CountAuditorOfClass()
        {
            // arrange
            var get = _fixture.Build<GetAuditByClassIdAndNumberAuditViewModel>().Create();
            var listAuditResult = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(100).ToList();
            var listAuditor = _fixture.Build<Auditor>().CreateMany(100).ToList();
            for(int i = 0; i < listAuditResult.Count; i++)
            {
                listAuditResult[i].AuditorId = listAuditor[i].Id.ToString();
                listAuditResult[i].ModuleName = get.nameModule;
                listAuditResult[i].NumberAudit = get.numberAudit;
                listAuditResult[i].ClassFresherId = get.classId;
            }
            _unitOfWorkMock.Setup(x => x.AuditManagementRepository.GetAllAsync()).ReturnsAsync(listAuditResult);
            for (int i = 0; i < listAuditResult.Count; i++)
            {
                _unitOfWorkMock.Setup(x => x.AuditorRepository.GetByIdAsync(listAuditor[i].Id)).ReturnsAsync(listAuditor[i]);
            }
            var responCountAuditorOfClass = new AuditManagementResponse
            {
                Data = listAuditor.Count,
                Message = "Get have " + listAuditor.Count + " auditor",
                Success = true
            };
            // act
            var result = await _auditManagementService.CountAuditorOfClassAsync(get);

            // assert
            result.Should().BeEquivalentTo(responCountAuditorOfClass);
        }
    }
}
