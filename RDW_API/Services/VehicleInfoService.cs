using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RDW_API.Configurations;
using RDW_API.Interfaces;
using RDW_API.Models;

namespace RDW_API.Services
{
    /// <summary>
    /// Vehicle Info service
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="httpClient"></param>
    public class VehicleInfoService(IOptions<RDWOptions> configuration, HttpClient httpClient) : IVehicleInfoService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly RDWOptions _configuration = configuration.Value;

        /// <summary>
        /// Gets a list of vehicles based on the input parameters
        /// </summary>
        /// <returns></returns>
        public async Task<List<VehicleInfo>?> GetVehicles(string? make = null, string? registration = null, string? type = null, int? offset = 0)
        {
            // Base API URL
            string apiUrl = $"{_configuration.BaseUrl}?";


            // Add URL parameters based on input
            // These can be updated to work like the registration `if statement`, if searching for `make` and `type` should not be strict on all characters being there
            if (!string.IsNullOrEmpty(make))
            {
                apiUrl += $"&$where=lower(merk)=lower('{make}')";
            }

            if (!string.IsNullOrEmpty(registration))
            {
                apiUrl += $"{(apiUrl.Contains("$where") ? "AND" : "&$where=")} lower(kenteken) like lower('%25{registration}%25')";
            }

            if (!string.IsNullOrEmpty(type))
            {
                apiUrl += $"{(apiUrl.Contains("$where") ? "AND" : "&$where=")} lower(voertuigsoort)=lower('{type}')";
            }

            if(offset > 0)
            {
                apiUrl += $"&$offset={offset}";
            }

            apiUrl += "&$order=kenteken ASC";

            _httpClient.DefaultRequestHeaders.Add("X-App-Token", _configuration.AppToken);

            var responseJson = await _httpClient.GetStringAsync(apiUrl);

            List<VehicleInfo>? response = JsonConvert.DeserializeObject<List<VehicleInfo>>(responseJson);

            return response;
        }
    }

}
