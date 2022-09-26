using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class ClassFresherRepositoryTests : SetupTest
    {
        private readonly IClassFresherRepository _classFresherRepository;

        public ClassFresherRepositoryTests()
        {
            _classFresherRepository = new ClassFresherRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetClassFresherByAdminNameAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockName = _fixture.Build<string>().Create();
            var mockData = _fixture.Build<ClassFresher>()
                                   .With(x => x.NameAdmin1, mockName)
                                   .Without(x => x.Freshers)
                                   .CreateMany(10)
                                   .ToList();

            await _dbContext.ClassFreshers.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _classFresherRepository.GetClassFresherByAdminNameAsync
                                            (mockName);

            // assert
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetClassFresherByAdminNameAsync_ShouldReturnNothing_IfFoundNothing()
        {
            //arrange
            var anyName = It.IsAny<string>();

            //act
            var result = await _classFresherRepository.GetClassFresherByAdminNameAsync(anyName);

            //assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetClassFresherByClassCodeAsync_ShouldReturnCorrectData()
        {
            // arrange
            var mockClassCode = _fixture.Build<string>().Create();
            var mockData = _fixture.Build<ClassFresher>()
                                   .With(x => x.ClassCode, mockClassCode)
                                   .Without(x => x.Freshers)
                                   .Create();

            await _dbContext.ClassFreshers.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _classFresherRepository.GetClassFresherByClassCodeAsync
                                            (mockClassCode);

            // assert
            result.Should().BeEquivalentTo(mockData);
        }

        [Fact]
        public async Task GetClassFresherByClassCodeAsync_ShouldReturnNothing_IfFoundNothing()
        {
            //arrange
            var anyName = It.IsAny<string>();

            //act
            var result = await _classFresherRepository.GetClassFresherByAdminNameAsync(anyName);

            //assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task GetClassWithFresherByClassIdAsync_ShouldReturnCorrectData()
        {
            //arrange
            Guid classId = Guid.NewGuid();
            var listFresher = _fixture.Build<Fresher>().Without(x => x.Attendances)
                .Without(y => y.ClassFresher).Without(x => x.ModuleResults).CreateMany(10).ToList();
            var mock = _fixture.Build<ClassFresher>().With(x => x.Freshers, listFresher).With(x => x.Id, classId).Create();

            await _dbContext.ClassFreshers.AddAsync(mock);
            await _dbContext.SaveChangesAsync();

            //act 
            var result = await _classFresherRepository.GetClassWithFresherByClassIdAsync(classId);

            //assert
            result.Should().BeEquivalentTo(mock);

        }

        [Fact]
        public async Task GetAllClassCodeAsync_ShouldReturnCorrectData()
        {
            //arrange
            var mockData = _fixture.Build<ClassFresher>()
                                  .Without(x => x.Freshers)
                                  .CreateMany(10)
                                  .ToList();
            var mockDataClassCode = mockData.Select(x => x.ClassCode);

            await _dbContext.ClassFreshers.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _classFresherRepository.GetAllClassCodeAsync();

            // assert
            result.Should().BeEquivalentTo(mockDataClassCode);
        }

        [Fact]
        public async Task GetAllClassCodeAsync_ShouldReturnNotThing()
        {
            //arrange
            var mockData = _fixture.Build<ClassFresher>()
                                  .Without(x => x.Freshers)
                                  .CreateMany(0)
                                  .ToList();
            var mockDataClassCode = mockData.Select(x => x.ClassCode);

            await _dbContext.ClassFreshers.AddRangeAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _classFresherRepository.GetAllClassCodeAsync();

            // assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CheckExistedClassAsync_ShouldReturnTrue()
        {
            //arrange
            var rrCode = _fixture.Build<string>().Create();
            var mockData = _fixture.Build<ClassFresher>()
                                  .With(x => x.RRCode, rrCode)
                                  .Without(x => x.Freshers)
                                  .Create();

            await _dbContext.ClassFreshers.AddAsync(mockData);
            await _dbContext.SaveChangesAsync();

            // act
            var result = await _classFresherRepository.CheckExistedClassAsync(rrCode);

            // assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckExistedClassAsync_ShouldReturnFalse()
        {
            //arrange
            var anyRRCode = It.IsAny<string>();

            // act
            var result = await _classFresherRepository.CheckExistedClassAsync(anyRRCode);

            // assert
            Assert.False(result);
        }
    }
}
