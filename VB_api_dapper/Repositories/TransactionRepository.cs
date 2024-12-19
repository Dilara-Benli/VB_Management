using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using VB_api.Models;
using VB_api.Repositories.Interfaces;

namespace VB_api.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IDbConnection _connection;

        public TransactionRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _connection.QueryAsync<Transaction>("sp_GetAllTransactions", commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByAccountIDAsync(long accountID)
        {
            return await _connection.QueryAsync<Transaction>(
                "sp_GetTransactionsByAccountID",
                new { AccountID = accountID },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByCustomerIDAsync(long customerID)
        {
            return await _connection.QueryAsync<Transaction>(
                "sp_GetTransactionsByCustomerID",
                new { CustomerID = customerID },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<decimal> GetAccountBalanceAsync(long customerID, long accountID)
        {
            return await _connection.QuerySingleOrDefaultAsync<decimal>(
                "sp_GetAccountBalance",
                new { CustomerID = customerID, AccountID = accountID },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<dynamic> DepositAsync(long customerID, long accountID, decimal amount, string explanation)
        {
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "sp_Deposit",
                new { CustomerID = customerID, AccountID = accountID, Amount = amount, Explanation = explanation },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<dynamic> WithdrawAsync(long customerID, long accountID, decimal amount, string explanation)
        {
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "sp_Withdraw",
                new { CustomerID = customerID, AccountID = accountID, Amount = amount, Explanation = explanation },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<dynamic> TransferMoneyAsync(long sourceCustomerID, long sourceAccountID, long targetAccountID, decimal amount, string explanation)
        {
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "sp_TransferMoney",
                new
                {
                    SourceCustomerID = sourceCustomerID,
                    SourceAccountID = sourceAccountID,
                    TargetAccountID = targetAccountID,
                    Amount = amount,
                    Explanation = explanation
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> AccountExistsAsync(long accountID)
        {
            var result = await _connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Account WHERE AccountID = @AccountID",
                new { AccountID = accountID });
            return result > 0;
        }

        public async Task<bool> CustomerAccountExistsAsync(long accountID, long customerID)
        {
            var result = await _connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Account WHERE AccountID = @AccountID AND CustomerID = @CustomerID",
                new { AccountID = accountID, CustomerID = customerID });
            return result > 0;
        }
    }
}
