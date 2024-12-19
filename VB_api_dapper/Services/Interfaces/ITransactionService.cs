using VB_api.Models;

namespace VB_api.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetAllTransactionsAsync();
        Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(long accountID);
        Task<IEnumerable<Transaction>> GetCustomerTransactionsAsync(long customerID);
        Task<decimal> GetAccountBalanceAsync(long customerID, long accountID);
        Task<dynamic> DepositAsync(TransactionRequest request, long customerID);
        Task<dynamic> WithdrawAsync(TransactionRequest request, long customerID);
        Task<dynamic> TransferMoneyAsync(TransferRequest request, long customerID);
    }
}
