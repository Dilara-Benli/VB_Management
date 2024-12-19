using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using VB_api.Models;
using VB_api.Repositories.Interfaces;

namespace VB_api.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IDbConnection _connection;

        public AccountRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _connection.QueryAsync<Account>(
                "sp_GetAllAccounts",
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerIDAsync(long customerID)
        {
            return await _connection.QueryAsync<Account>(
                "sp_GetAccountsByCustomerID",
                new { CustomerID = customerID },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<dynamic> CreateAccountAsync(long customerID, string accountName, string currencyType)
        {
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "sp_CreateAccount",
                new
                {
                    CustomerID = customerID,
                    AccountName = accountName,
                    CurrencyType = currencyType,
                    AccountBalance = 0m
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> UpdateAccountAsync(long accountID, string accountName, string currencyType)
        {
            return await _connection.ExecuteAsync(
                "sp_UpdateAccount",
                new
                {
                    AccountID = accountID,
                    AccountName = accountName,
                    CurrencyType = currencyType
                },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<int> DeleteAccountAsync(long accountID)
        {
            return await _connection.ExecuteAsync(
                "sp_DeleteAccount",
                new { AccountID = accountID },
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<bool> CustomerExistsAsync(long customerId)
        {
            var result = await _connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Customer WHERE CustomerID = @CustomerID",
                new { CustomerID = customerId });
            return result > 0;
        }
    }
}
