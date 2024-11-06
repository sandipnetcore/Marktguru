namespace Marktguru.BusinessLogic.Configurations
{
    /// <summary>
    /// A class that holds only the configuration that is needed for creation and validation of JWT.
    /// </summary>
    public class JWTConfigurationSettings
    {
        /// <summary>
        /// Issue URL
        /// </summary>
        public string IssuerUrl { get; set; }
        
        /// <summary>
        /// Audience URL. 
        /// </summary>
        public string AudienceUrl { get; set; }

        /// <summary>
        /// Expirty Time for Token. We will keep it minutes.
        /// </summary>
        public double ExpiryInSeconds { get; set; }

        /// <summary>
        /// Private key
        /// </summary>
        public string PrivateKey { get; set; }
    }
}
