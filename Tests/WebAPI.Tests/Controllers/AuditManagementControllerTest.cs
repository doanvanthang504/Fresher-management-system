using AutoFixture;
using Domain.Entities;
using Domain.Enums;
using Domain.Tests;
using Global.Shared.ViewModels;
using Global.Shared.ViewModels.AuditManagementViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Controllers;

namespace WebAPI.Tests.Controllers
{
    public class AuditManagementControllerTest : SetupTest
    {
        private readonly AuditResultController _auditManagementController;
        public AuditManagementControllerTest()
        {
            _auditManagementController = new AuditResultController(_auditManagementServiceMock.Object);
        }     
        
       
        [Fact]
        public async Task UpdateAuditForStudent()
        {
            // arrange
            var auditDTO = new UpdateAuditViewModel
            {
                AuditorId = "string",
                QuestionQ1 = "string",
                CommentQ1 = "string",
                EvaluateQ1 = Evaluate.Improve,
                QuestionQ2 = "string",
                CommentQ2 = "string",
                EvaluateQ2 = Evaluate.Improve,
                QuestionQ3 = "string",
                CommentQ3 = "string",
                EvaluateQ3 = Evaluate.Improve,
                QuestionQ4 = "string",
                CommentQ4 = "string",
                EvaluateQ4 = Evaluate.Improve,
                PracticeComment = "string",
                PracticeScore = 0,             
                AuditComment = "string"
            };
            var responUpdateAuditForStudent = new AuditManagementResponse
            {
                Data = auditDTO,
                Success = true,
                Message = "Ok"
            };
            _auditManagementServiceMock.Setup(x => x.UpdateAuditForStudentAsync(It.IsAny<Guid>(),auditDTO))
                                       .ReturnsAsync(responUpdateAuditForStudent);
            // act
            var result = await _auditManagementController.UpdateAuditForStudent(It.IsAny<Guid>(),auditDTO);

            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responUpdateAuditForStudent, resultObj!.Value);
        }
        [Fact]
        public async Task GetAuditByAuditor()
        {
            // arrange
            var list = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(100);
            var listAuditMap = _mapperConfig.Map<List<AuditResult>>(list);
            var responGetAuditByAuditor = new AuditManagementResponse
            {
                Data = listAuditMap,
                Success = true,
                Message = "Ok"
            };
            _auditManagementServiceMock.Setup(x => x.GetAuditByClassIdAsync(It.IsAny<Guid>()))
                                       .ReturnsAsync(responGetAuditByAuditor);

            // act
            var result = await _auditManagementController.GetAuditByAuditor(It.IsAny<Guid>());
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetAuditByAuditor, resultObj!.Value);
        }
        [Fact] 
        public async Task AddAduditorForPlanAudit()
        {
            // arrange
            var listAuditor = _fixture.Build<AuditorViewModel>().CreateMany(100).ToList();
            var listAuditorMap = _mapperConfig.Map<List<Auditor>>(listAuditor);
            var responAddAduditorForPlanAudit = new AuditManagementResponse
            {
                Data = listAuditorMap,
                Success = true,
                Message = "Ok"
            };
            var id = new Guid();
            var nameModule = "sql";
            _auditManagementServiceMock.Setup(x => x.AddAuditorForPlanAuditAsync(It.IsAny<GetAuditAndAuditorViewModel>())).ReturnsAsync(responAddAduditorForPlanAudit);
            
            // act
            var result = await _auditManagementController.AddAduditorForPlanAudit(It.IsAny<GetAuditAndAuditorViewModel>());
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(resultObj!.Value, responAddAduditorForPlanAudit);
        }       
        [Fact]
        public async Task DeleteAuditResultAsync()
        {
            // arrange
            var auditResult = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).Create();
            auditResult.IsDeleted = false;
            var responDeleteAuditResultAsync = new AuditManagementResponse
            {
                Data = auditResult,
                Message = "Success",
                Success = true
            };
            _auditManagementRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(auditResult);
            _auditManagementServiceMock.Setup(x => x.DeleteAuditResultAsync(It.IsAny<Guid>())).ReturnsAsync(responDeleteAuditResultAsync);

            // act
            var result = await _auditManagementController.DeleteAuditResult(auditResult.Id);
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responDeleteAuditResultAsync, resultObj!.Value);            
        }
        [Fact]
        public async Task GetPlanAudit()
        {
            // arrange
            var auditResult = _fixture.Build<ClassFresher>().Without(x => x.Freshers).Create();
            var responGetPlanAudit = new AuditManagementResponse
            {
                Data = auditResult,
                Message = "Success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.GetAllAuditPlansAsync()).ReturnsAsync(responGetPlanAudit);

            // act
            var result = await _auditManagementController.GetPlanAudit();
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetPlanAudit, resultObj!.Value);
        }
        [Fact]
        public async Task GetDetailPlanAuditAsync()
        {
            // arrange
            var auditViewModel = _fixture.Build<GetAuditByClassIdAndNumberAuditViewModel>().Create();
            var resultReturn = _fixture.Build<GetDetailPlanAuditViewModel>().CreateMany(100).ToList();
            var responGetDetailPlanAuditAsync = new AuditManagementResponse
            {
                Data = resultReturn,
                Message = "Success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.GetDetailPlanAuditAsync(auditViewModel)).ReturnsAsync(responGetDetailPlanAuditAsync);

            // act
            var result = await _auditManagementController.GetDetailPlanAuditAsync(auditViewModel);
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetDetailPlanAuditAsync, resultObj!.Value);
        }
        [Fact]
        public async Task GetAllAuditAsync()
        {
            // arrange
            var auditor = _fixture.Build<Auditor>().CreateMany(100);
            var responGetAllAuditAsync = new AuditManagementResponse
            {
                Data = auditor,
                Message = "Get success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.GetAllAuditorAsync()).ReturnsAsync(responGetAllAuditAsync);

            // act
            var result = await _auditManagementController.GetAllAuditAsync();
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetAllAuditAsync, resultObj!.Value);
        }
        [Fact]
        public async Task GetFresherInClass()
        {
            // arrange
            var listFresher = _fixture.Build<FresherViewModel>().CreateMany(100).ToList();
            var responGetFresherInClass = new AuditManagementResponse
            {
                Data = listFresher,
                Message = "get success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.GetAllFresherIdInClassAsync(It.IsAny<Guid>())).ReturnsAsync(responGetFresherInClass);

            // act
            var result = await _auditManagementController.GetFresherInClass(It.IsAny<Guid>());
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetFresherInClass, resultObj!.Value);
        }
        [Fact]
        public async Task CreatePlanAuditForMemberInClassAsync()
        {

            // arrange
            var createPlanAudit = _fixture.Build<CreateAuditViewModel>().Create();
            var responCreatePlan = new AuditManagementResponse
            {
                Data = createPlanAudit,
                Message = "Create success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.CreatePlanAuditForMemberInClassAsync(createPlanAudit)).ReturnsAsync(responCreatePlan);

            // act
            var result = await _auditManagementController.CreatePlanAuditForMemberInClassAsync(createPlanAudit);
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responCreatePlan, resultObj!.Value);
        }
        [Fact]
        public async Task GetCountAuditorOfClassAudit()
        {
            // arrange

            var get = _fixture.Build<GetAuditByClassIdAndNumberAuditViewModel>().Create();
            var responGetCountAuditor = new AuditManagementResponse
            {
                Data = 100,
                Message = "Get success",
                Success = true
            };
            _auditManagementServiceMock.Setup(x => x.CountAuditorOfClassAsync(get)).ReturnsAsync(responGetCountAuditor);

            // act
            var result = await _auditManagementController.GetCountAuditorOfClassAudit(get);
            var resultObj = result as ObjectResult;

            // assert
            Assert.Equal(responGetCountAuditor, resultObj!.Value);
        }
    }
}
