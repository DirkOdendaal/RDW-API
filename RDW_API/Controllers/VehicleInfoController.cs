using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RDW_API.Interfaces;
using RDW_API.Models;

namespace RDW_API.Controllers
{
    /// <summary>
    /// Vehicle Info Controller
    /// </summary>
    /// <param name="vehicleInfoService">The service for retrieving vehicle information</param>
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleInfoController(IVehicleInfoService vehicleInfoService) : ControllerBase
    {
        private readonly IVehicleInfoService _vehicleInfoService = vehicleInfoService;

        /// <summary>
        /// Get List of Vehicles based on paramaters given in ASC order based on registration
        /// </summary>
        /// <param name="make">Optional. Filter by vehicle make</param>
        /// <param name="registration">Optional. Filter by vehicle registration. Filters based on partail registration number as well.</param>
        /// <param name="type">Optional. Filter by vehicle type</param>
        /// <param name="offset">Optional. Number of records to offset by. Default : 0</param>
        /// <returns>List of vehicle information</returns>
        /// <response code="200">Returns the list of vehicles</response>
        /// <response code="404">If no vehicles are found</response>
        /// <response code="500">If there is an internal server error</response>
        [HttpGet]
        [Authorize]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<VehicleInfo>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicles([FromQuery] string? make = null, [FromQuery] string? registration = null, [FromQuery] string? type = null, [FromQuery] int? offset = 0)
        {
            try
            {
                List<VehicleInfo>? vehicleInfo = await _vehicleInfoService.GetVehicles(make, registration, type, offset);
                if (vehicleInfo == null || vehicleInfo.Count == 0)
                {
                    return new ObjectResult(new { message = "No Vehicles found that match the criteria given." }) // Can replace this with actual error model if needed for better logging on requests
                    {
                        StatusCode = StatusCodes.Status404NotFound
                    };
                }

                return Ok(vehicleInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
