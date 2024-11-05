namespace MarktguruAssignment.API.Logging
{
    /// <summary>
    /// Configuration for Custon Logging which will be used by the provider.
    /// </summary>
    public class FileLoggingOptions
    {
        /// <summary>
        /// Folder Path
        /// </summary>
        public string LogFolderPath { get; set; }
        
        /// <summary>
        /// File Path
        /// </summary>
        public string LogFilePath { get; set; }

        /// <summary>
        /// The replace string in the LogFilePath should be replaced by DATETIME.UTC
        /// </summary>
        public string ReplaceString { get; set; }
    }
}
