using VB_api.Models;

namespace VB_api.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetTransactionsByAccountIDAsync(long accountID);
        Task<IEnumerable<Transaction>> GetTransactionsByCustomerIDAsync(long customerID);
        Task<decimal> GetAccountBalanceAsync(long customerID, long accountID);
        Task<dynamic> DepositAsync(long customerID, long accountID, decimal amount, string explanation);
        Task<dynamic> WithdrawAsync(long customerID, long accountID, decimal amount, string explanation);
        Task<dynamic> TransferMoneyAsync(long sourceCustomerID, long sourceAccountID, long targetAccountID, decimal amount, string explanation);
        Task<bool> AccountExistsAsync(long accountID);
        Task<bool> CustomerAccountExistsAsync(long accountID, long customerID);        
    }
}
