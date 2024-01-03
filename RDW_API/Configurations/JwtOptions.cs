namespace RDW_API.Configurations
{
    /// <summary>
    /// Jwt configuration
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Jwt configuration section name
        /// </summary>
        public const string JWT = "JWT_Options";
        /// <summary>
        /// Jwt token Issuer
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// Jwt token Audience
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Jwt token secret Key used to generate
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Token expiration time in minutes
        /// </summary>
        public int ExpirationInMinutes { get; set; }
    }
}
