using Newtonsoft.Json;

namespace RDW_API.Models
{
    /// <summary>
    /// Vehicle Model
    /// </summary>
    public class VehicleInfo
    {
        /// <summary>
        /// Vehicle registration number
        /// </summary>
        [JsonProperty("kenteken")]
        public string RegistrationNumber { get; set; }
        /// <summary>
        /// Vehicl Make/Brand
        /// </summary>
        [JsonProperty("merk")]
        public string Make { get; set; }

        /// <summary>
        /// Vehicle Model
        /// </summary>
        [JsonProperty("handelsbenaming")]
        public string Model { get; set; }

        /// <summary>
        /// Vehicle type
        /// </summary>
        [JsonProperty("voertuigsoort")]
        public string VehicleType { get; set; }

        /// <summary>
        /// Date of first admission
        /// </summary>
        [JsonProperty("datum_eerste_toelating_dt")]
        public DateTime DateFirstAdmission { get; set; }

        /// <summary>
        /// Date of first registration
        /// </summary>
        [JsonProperty("datum_eerste_tenaamstelling_in_nederland_dt")]
        public DateTime DateFirstRegistrationNetherlands { get; set; }


    }
}
