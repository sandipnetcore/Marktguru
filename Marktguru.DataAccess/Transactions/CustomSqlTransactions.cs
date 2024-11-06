using System.Data;
using System.Data.SqlClient;


namespace Marktguru.DataAccess.Transactions
{
    internal class CustomSqlTransactions : ICustomSqlTransactions
    {
        /// <summary>
        /// Sql Connection Object
        /// </summary>
        public SqlConnection sqlConnection
        {
            get;
        }

        /// <summary>
        /// Sql Transaction
        /// </summary>
        public SqlTransaction sqlTransaction
        {
            get; private set;
        }


        internal CustomSqlTransactions(string connection)
        {
            sqlConnection = new SqlConnection(connection);
        }


        /// <summary>
        /// Opens the transaction.
        /// </summary>
        public void BeginTransaction()
        {
            if (sqlConnection.State == ConnectionState.Closed)
                sqlConnection.Open();
            sqlTransaction = sqlConnection.BeginTransaction();
        }

        /// <summary>
        /// Rolls back the transaction.
        /// </summary>
        public void RollBackTransaction()
        {
            sqlTransaction.Rollback();
            sqlConnection.Close();
        }

        /// <summary>
        /// Commits the transaction.
        /// </summary>
        public void CommitTransaction()
        {
            sqlTransaction.Commit();
            sqlConnection.Close();
        }
    }
}
