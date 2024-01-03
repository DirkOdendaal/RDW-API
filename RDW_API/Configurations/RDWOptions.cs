namespace RDW_API.Configurations
{
    /// <summary>
    /// RDW Dataset configuration
    /// </summary>
    public class RDWOptions
    {
        /// <summary>
        /// RDW Dataset configuration section name
        /// </summary>
        public const string RDW = "RDW_Options";

        /// <summary>
        /// RDW Base url
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// RDW App Token
        /// </summary>
        public string AppToken { get; set; }
        /// <summary>
        /// RDW App Secret
        /// </summary>
        public string AppSecret { get; set; }
    }
}
