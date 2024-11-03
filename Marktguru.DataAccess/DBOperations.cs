using Marktguru.DataAccess.DataBaseComponents;
using Marktguru.DataAccess.Transactions;
using System.Data.SqlClient;
using System.Data;
using System.Collections;

namespace Marktguru.DataAccess
{
    /// <summary>
    /// Performs DB Operations - CRUD operations
    /// </summary>
    public sealed class DBOperations
    {

        private readonly String Connection;

        private AdoComponents _component;

        private ICustomSqlTransactions transactions;

        private AdoComponents Component
        {
            get
            {
                if (_component == null)
                {
                    transactions = new CustomSqlTransactions(Connection);
                    _component = new AdoComponents(transactions);
                }
                return _component;
            }
        }

        public DBOperations(String connection)
        {
            Connection = connection;
        }


        private SqlCommand _CreateCommand(string commandText, Dictionary<string,dynamic> ? parameters)
        {
            SqlCommand command = new SqlCommand(commandText, transactions.sqlConnection);
            command.CommandType = CommandType.StoredProcedure;
            foreach(var item in parameters)
            {
                command.Parameters.AddWithValue(item.Key, item.Value);
            }

            return command;
        }

        /// <summary>
        /// Get the datase for the the command text. Command Text should only be "stored procedure name" to get the dataset.
        /// </summary>
        /// <param name="commandText">String</param>
        /// <param name="parameters">Dictionary<see cref="Dictionary{TKey, TValue}"/></param>
        /// <returns>DataSet</returns>
        public DataSet GetDataSet(string commandText, Dictionary<string,dynamic> ? parameters)
        {
            return Component.ExecuteCommandToGetDataSet(_CreateCommand(commandText, parameters));
        }

        /// <summary>
        /// Inserts or updates the tables.
        /// </summary>
        /// <param name="commandText">String</param>
        /// <param name="parameters"><see cref="Dictionary{TKey, TValue}"/></param>
        /// <returns>Boolean</returns>
        public bool InsertOrUpdateRecord(string commandText, Dictionary<string,dynamic> parameters)
        {
            return Component.ExecuteSqlInsertOrUpdate(_CreateCommand(commandText, parameters));
        }
    }
}
