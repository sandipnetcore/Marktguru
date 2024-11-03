using Marktguru.DataAccess.Transactions;
using System.Data;
using System.Data.SqlClient;

namespace Marktguru.DataAccess.DataBaseComponents
{
    internal class AdoComponents
    {
        private readonly ICustomSqlTransactions _Transactions;
        internal AdoComponents(ICustomSqlTransactions transactions)
        {
            _Transactions = transactions;
        }

        internal DataSet ExecuteCommandToGetDataSet(SqlCommand sqlCommand)
        {
            DataSet ds = new DataSet();
            try
            {
                OpenConnection();
                SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
                adapter.Fill(ds);
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }


        internal bool ExecuteSqlInsertOrUpdate(SqlCommand sqlCommand)
        {
            bool result = false;

            try
            {
                _Transactions.BeginTransaction();
                sqlCommand.Transaction = _Transactions.sqlTransaction;
                sqlCommand.ExecuteNonQuery();
                _Transactions.CommitTransaction();
                result = true;
            }
            catch (Exception ex)
            {
                _Transactions.RollBackTransaction();
            }
            finally
            {
                CloseConnection();
            }
            return result;
        }

        private void OpenConnection()
        {
            if (_Transactions.sqlConnection != null && _Transactions.sqlConnection.State == ConnectionState.Closed)
            {
                _Transactions.sqlConnection.Open();
            }
        }

        private void CloseConnection()
        {
            if (_Transactions.sqlConnection != null && _Transactions.sqlConnection.State != ConnectionState.Closed)
            {
                _Transactions.sqlConnection.Close();
            }
        }
    }
}
