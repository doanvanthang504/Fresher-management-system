using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.Tests
{
    public class ProgramTests
    {
        private readonly HttpClient _httpClient;
        private readonly WebApplicationFactory<Program> _application;

        public ProgramTests()
        {
            _application = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseEnvironment("Development");
                });
            //.WithWebHostBuilder(builder =>
            //{
            //    builder.ConfigureServices(services =>
            //    {
            //        services.AddHealthChecks();
            //    });
            //});
            
            _httpClient = _application.CreateClient();
        }

        [Fact]
        public async Task Program_HealthCheck_ShouldReturnHealthResult()
        {
            var response = await _httpClient.GetStringAsync("/healthchecks");
            response.Should().Be("Healthy");
        }
    }
}
