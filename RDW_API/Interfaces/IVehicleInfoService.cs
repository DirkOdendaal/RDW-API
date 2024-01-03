using RDW_API.Models;

namespace RDW_API.Interfaces
{
    /// <summary>
    /// Vehicle info service interface
    /// </summary>
    public interface IVehicleInfoService
    {
        /// <summary>
        /// Get vehicle info by make, registration and type
        /// </summary>
        /// <param name="make">Vehicle Brand</param>
        /// <param name="registration">Vehicle Registration</param>
        /// <param name="type">Type of Vehicle</param>
        /// <param name="offset">Number of record offset</param>
        /// <returns>List of Vehicles matching criteria</returns>
        Task<List<VehicleInfo>?> GetVehicles(string? make = null, string? registration = null, string? type = null, int? offset = 0);
    }
}
