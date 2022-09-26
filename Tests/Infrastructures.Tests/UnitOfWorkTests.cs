using Application;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Tests
{
    public class UnitOfWorkTests : SetupTest
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkTests()
        {
            _unitOfWork = new UnitOfWork(
                _dbContext,
                _userRepositoryMock.Object,
                _chemicalRepositoryMock.Object,
                _fresherRepositoryMock.Object,
                _reminderRepositoryMock.Object,
                _auditManagementRepositoryMock.Object,
                _auditorRepositoryMock.Object,
                _questionManagementRepositoryMock.Object,
                _fresherReportRepositoryMock.Object,
                _feedbackRepositoryMock.Object,
                _feedbackQuestionRepositoryMock.Object,
                _feedbackResultRepositoryMock.Object,
                _feedbackAnswerRepositoryMock.Object,
                _classFresherRepositoryMock.Object, 
                _scoreRepositoryMock.Object,
                _attendanceRepositoryMock.Object,
                _reportAttendanceRepositoryMock.Object,
                _planRepositoryMock.Object,
                _topicRepositoryMock.Object,
                _moduleRepositoryMock.Object,
                _planInforRepositoryMock.Object,
                _chapterSyllabusRepositoryMock.Object,
                _lectureChapterRepositoryMock.Object,
                _syllabusDetailRepositoryMock.Object,
                _moduleResultRepositoryMock.Object
                );
        }


        [Fact]
        public async Task TestUnitOfWork()
        {
            // arrange
            var mockData = _fixture.Build<Chemical>().CreateMany(10).ToList();

            var fresherReportListMockData = _fixture.Build<FresherReport>().CreateMany(10).ToList();
            var fresherReportMockData = _fixture.Build<FresherReport>().Create();
            var mockExpression = _fixture.Create<Expression<Func<FresherReport, bool>>>();

            _chemicalRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockData);
            _fresherReportRepositoryMock
                .Setup(x => x.GetMonthlyReportsByFilterAsync(mockExpression))
                .ReturnsAsync(fresherReportListMockData);

            var mockScore = _fixture.Build<Score>().CreateMany(10).ToList();

            _scoreRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockScore);

            var mockAudit = _fixture.Build<AuditResult>().Without(x => x.ClassFresher).CreateMany(10).ToList();

            _auditManagementRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockAudit);
            var mockQuestion = _fixture.Build<QuestionManagement>().CreateMany(10).ToList();

            _questionManagementRepositoryMock.Setup(x => x.GetAllAsync()).ReturnsAsync(mockQuestion);

            // act
            var items = await _unitOfWork.ChemicalRepository.GetAllAsync();

            var reportItemsByExpression = await _unitOfWork.FresherReportRepository
                                                           .GetMonthlyReportsByFilterAsync
                                                           (mockExpression);

            // assert
            items.Should().BeEquivalentTo(mockData);
            reportItemsByExpression.Should().BeEquivalentTo(fresherReportListMockData);
        }
    }
}