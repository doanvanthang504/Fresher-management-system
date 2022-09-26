using Application.Interfaces;
using Application.Services;
using Domain.Tests;
using Hangfire;
using Hangfire.Common;
using Hangfire.States;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Tests.Services
{
    public class HangfireServiceTests  : SetupTest
    {
        private IHangfireService _hangfireService;
        private readonly Mock<IBackgroundJobClient> _backgroundJobClient;

        public HangfireServiceTests()
        {
            _backgroundJobClient = new Mock<IBackgroundJobClient>();
            _hangfireService = new HangfireService(_backgroundJobClient.Object);
        }

        [Fact]
        public void CreateFireAndForgetTask_ShouldBeEnqueued()
        {
            _hangfireService.CreateFireAndForgetTask(() => _chemicalServiceMock.Object.GetChemicalAsync());
            _backgroundJobClient.Verify(x => x.Create(
                    It.Is<Job>(job => job.Method.Name == "GetChemicalAsync"),
                    It.IsAny<EnqueuedState>()));
        }
    }
}
