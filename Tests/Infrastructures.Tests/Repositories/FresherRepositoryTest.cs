using Application.Repositories;
using AutoFixture;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Infrastructures.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructures.Tests.Repositories
{
    public class FresherRepositoryTest : SetupTest
    {
        private readonly IFresherRepository _fresherRepository;

        public FresherRepositoryTest()
        {
            _fresherRepository = new FresherRepository(
                _dbContext,
                _currentTimeMock.Object,
                _claimsServiceMock.Object);
        }

        [Fact]
        public async Task GetFresherByClassCodeAsync_ShouldReturnCorrectData()
        {
            //Arrange
            var classCode = "classcode";
            var mockFresher = _fixture.Build<Fresher>()
                                      .With(x => x.ClassCode, "classCode")
                                      .Without(x => x.ClassFresher)
                                      .Without(x => x.Attendances)
                                      .Without(x => x.ModuleResults)
                                      .With(x => x.IsDeleted, false)
                                      .CreateMany(2)
                                      .ToList();

            await _dbContext.Freshers.AddRangeAsync(mockFresher);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _fresherRepository.GetFresherByClassCodeAsync(classCode);

            //Assert
            result.Should().BeEquivalentTo(mockFresher);
        }

        [Fact]
        public async Task GetFresherByClassCodeAsync_ShouldReturnNoThing()
        {
            //Arrange
            var classCode = "cc";
            var mockFresher = new List<Fresher>()
            {
                new Fresher()
                {
                    ClassCode = classCode,
                    ClassFresherId = Guid.NewGuid(),
                    Email = "aaa@gmail.com",
                    AccountName = "som",
                    FirstName = "",
                    LastName = "",
                    Major = "",
                    University = ""
                },
                new Fresher()
                {
                    ClassCode = classCode,
                    ClassFresherId = Guid.NewGuid(),
                    Email = "aaa@gmail.com",
                    AccountName = "som",
                    FirstName = "",
                    LastName = "",
                    Major = "",
                    University=""

                }, new Fresher()
                {
                    ClassCode = classCode,
                    ClassFresherId = Guid.NewGuid(),
                    Email = "aaa@gmail.com",
                    AccountName = "som",
                    FirstName = "",
                    LastName = "",
                    Major = "",
                    University = ""

                }
            };
            await _dbContext.Freshers.AddRangeAsync(mockFresher);
            await _dbContext.SaveChangesAsync();
            //Act
            var result = await _fresherRepository.GetFresherByClassCodeAsync("");

            //Assert
            result.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task CheckExistedFresherByAccountNameAsync_ShouldReturnTrue()
        {
            //Arrange
            var accountName = "bangnt9";
            var mockFresher = _fixture.Build<Fresher>().With(x => x.AccountName, accountName)
                .Without(x => x.ClassFresher).Without(x => x.Attendances).Without(x => x.ModuleResults).CreateMany(10);

            await _dbContext.Freshers.AddRangeAsync(mockFresher);
            await _dbContext.SaveChangesAsync();

            //Act
            var result = await _fresherRepository.CheckExistedFresherByAccountNameAsync(accountName);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task CheckExistedFresherByAccountNameAsync_ShouldReturnFalse()
        {
            //Arrange
            var accountName = _fixture.Build<string>().Create();

            //Act
            var result = await _fresherRepository.CheckExistedFresherByAccountNameAsync(accountName);

            //Assert
            Assert.False(result);
        }
    }
}
