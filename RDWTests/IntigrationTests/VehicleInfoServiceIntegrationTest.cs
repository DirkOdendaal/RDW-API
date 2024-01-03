using Microsoft.Extensions.Options;
using RDW_API.Configurations;
using RDW_API.Models;
using RDW_API.Services;

namespace RDW_API_Test.VehicleService
{
    public class VehicleInfoServiceIntegrationTest
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<RDWOptions> _options;

        public VehicleInfoServiceIntegrationTest()
        {
            _httpClient = new HttpClient();
            _options = Options.Create(new RDWOptions
            {
                BaseUrl = "https://opendata.rdw.nl/resource/m9d7-ebf2.json",
                AppToken = "cLA5iK40X2g3K69ax3qvWVjJb" //This is a test token
            });
        }

        [Fact]
        public async Task GetVehicles_ReturnsExpectedResults()
        {
            // Arrange
            var vehicleInfoService = new VehicleInfoService(_options, _httpClient);

            // Act
            var vehicles = await vehicleInfoService.GetVehicles("Toyota", "", "");

            // Assert
            Assert.NotEmpty(vehicles);
        }
    }
}
