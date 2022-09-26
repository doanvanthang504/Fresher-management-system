using Application.Interfaces;
using Application.Repositories;
using Application.Services;
using Domain.Entities;
using Domain.Tests;
using FluentAssertions;
using Global.Shared.Helpers;
using Infrastructures;
using Infrastructures.Repositories;
using Infrastructures.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Diagnostics;
using WebAPI.Middlewares;
using WebAPI.Services;

namespace WebAPI.Tests
{
    public class DependencyInjectionTests
    {
        private readonly ServiceProvider _serviceProvider;

        public DependencyInjectionTests()
        {
            var configuration = new ConfigurationBuilder()
                                        .SetBasePath(SetupWebAPIPath.GetBasePath())
                                        .AddJsonFile("testsettings.json")
                                        .Build();

            var service = new ServiceCollection();

            var webHostEnvironmentMock = new Mock<IWebHostEnvironment>();
            webHostEnvironmentMock.SetupGet(e => e.EnvironmentName)
                .Returns("Development");
            var webHostEnvironment = webHostEnvironmentMock.Object;

            service.AddSingleton<IConfiguration>(configuration);
            service.AddWebAPIService();
            service.AddInfrastructuresService(configuration, webHostEnvironment);
            service.AddRootSetting(configuration);
            service.AddSingleton(webHostEnvironment);
            service.AddDbContext<AppDbContext>(
                option => option.UseInMemoryDatabase("test"));

            _serviceProvider = service.BuildServiceProvider();
        }

        [Fact]
        public void DependencyInjectionTests_ServiceShouldResolveCorrectly()
        {
            var currentTimeServiceResolved = _serviceProvider.GetRequiredService<ICurrentTime>();
            var claimsServiceServiceResolved = _serviceProvider.GetRequiredService<IClaimsService>();
            var userMailCredentialServiceResolved = _serviceProvider.GetRequiredService<IUserMailCredentialService>();
            var exceptionMiddlewareResolved = _serviceProvider.GetRequiredService<ExceptionMiddleware>();
            var performanceMiddleware = _serviceProvider.GetRequiredService<PerformanceMiddleware>();
            var stopwatchResolved = _serviceProvider.GetRequiredService<Stopwatch>();
            var mailServiceResolved = _serviceProvider.GetRequiredService<IMailService>();
            var mailTemplateManagerResolved = _serviceProvider.GetRequiredService<IMailTemplateManager>();
            var chemicalRepositoryResolved = _serviceProvider.GetRequiredService<IChemicalRepository>();
            var chemicalServiceResolved = _serviceProvider.GetRequiredService<IChemicalService>();
            var importFileRepositoryResolved = _serviceProvider.GetRequiredService<IImportDataFromExcelFileService>();
            var importDataServiceResolved = _serviceProvider.GetRequiredService<IImportDataFromExcelFileService>();
            var genericRepositoryResolved = _serviceProvider.GetRequiredService<IGenericRepository<Chemical>>();
            var feedbackRepositoryResolved = _serviceProvider.GetRequiredService<IFeedbackRepository>();
            var feedbackQuestionRepositoryResolved = _serviceProvider.GetRequiredService<IFeedbackQuestionRepository>();
            var feedbackResultRepositoryResolved = _serviceProvider.GetRequiredService<IFeedbackResultRepository>();
            var feedbackServiceResolved = _serviceProvider.GetRequiredService<IFeedbackService>();

            currentTimeServiceResolved.GetType().Should().Be(typeof(CurrentTime));
            claimsServiceServiceResolved.GetType().Should().Be(typeof(ClaimsService));
            userMailCredentialServiceResolved.GetType().Should().Be(typeof(UserMailCredentialService));
            exceptionMiddlewareResolved.GetType().Should().Be(typeof(ExceptionMiddleware));
            performanceMiddleware.GetType().Should().Be(typeof(PerformanceMiddleware));
            stopwatchResolved.GetType().Should().Be(typeof(Stopwatch));
            mailServiceResolved.GetType().Should().Be(typeof(MailService));
            mailTemplateManagerResolved.GetType().Should().Be(typeof(MailTemplateManager));
            chemicalRepositoryResolved.GetType().Should().Be(typeof(ChemicalRepository));
            chemicalServiceResolved.GetType().Should().Be(typeof(ChemicalService));
            importFileRepositoryResolved.GetType().Should().Be(typeof(ImportDataFromExcelFileService));
            importDataServiceResolved.GetType().Should().Be(typeof(ImportDataFromExcelFileService));
            genericRepositoryResolved.GetType().Should().Be(typeof(GenericRepository<Chemical>));
            feedbackRepositoryResolved.GetType().Should().Be(typeof(FeedbackRepository));
            feedbackQuestionRepositoryResolved.GetType().Should().Be(typeof(FeedbackQuestionRepository));
            feedbackResultRepositoryResolved.GetType().Should().Be(typeof(FeedbackResultRepository));
            feedbackServiceResolved.GetType().Should().Be(typeof(FeedbackService));
        }
    }
}
