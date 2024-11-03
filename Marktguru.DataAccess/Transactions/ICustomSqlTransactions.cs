using System.Data.SqlClient;

namespace Marktguru.DataAccess.Transactions
{
    internal interface ICustomSqlTransactions
    {
        SqlConnection sqlConnection { get; }

        SqlTransaction sqlTransaction { get; }
        void BeginTransaction();

        void RollBackTransaction();

        void CommitTransaction();
    }
}
