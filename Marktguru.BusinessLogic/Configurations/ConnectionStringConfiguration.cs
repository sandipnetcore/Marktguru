namespace Marktguru.BusinessLogic.Configurations
{
    /// <summary>
    /// Configuration Settings. This only contains settings that are connection string specific.
    /// It may so happen that we are accessing multiple database connections.
    /// Hence, all database connection has to be in this file.
    /// </summary>
    public sealed class ConnectionStringConfiguration
    {
        /// <summary>
        /// Connection String of the DB
        /// </summary>
        public string ConnectionString
        {
            get;
            set;
        }
    }
}
