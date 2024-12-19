using Microsoft.Identity.Client;
using VB_api.Models;
using VB_api.Repositories;
using VB_api.Repositories.Interfaces;
using VB_api.Services.Interfaces;

namespace VB_api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactionsAsync()
        {
            return await _transactionRepository.GetAllTransactionsAsync();
        }

        public async Task<IEnumerable<Transaction>> GetAccountTransactionsAsync(long accountID)
        {
            return await _transactionRepository.GetTransactionsByAccountIDAsync(accountID);
        }

        public async Task<IEnumerable<Transaction>> GetCustomerTransactionsAsync(long customerID)
        {
            return await _transactionRepository.GetTransactionsByCustomerIDAsync(customerID);
        }

        public async Task<decimal> GetAccountBalanceAsync(long customerID, long accountID)
        {
            var accountExists = await _transactionRepository.CustomerAccountExistsAsync(accountID, customerID);
            if (!accountExists)
            {
                throw new Exception("Hesap bulunamadı.");
            }
            return await _transactionRepository.GetAccountBalanceAsync(customerID, accountID);
        }

        public async Task<dynamic> DepositAsync(TransactionRequest request, long customerID)
        {
            var accountExists = await _transactionRepository.CustomerAccountExistsAsync(request.accountID, customerID);
            if (!accountExists)
            {
                throw new Exception("Hesap bulunamadı.");
            }
            return await _transactionRepository.DepositAsync(customerID, request.accountID, request.amount, request.explanation);
        }

        public async Task<dynamic> WithdrawAsync(TransactionRequest request, long customerID)
        {
            var accountExists = await _transactionRepository.CustomerAccountExistsAsync(request.accountID, customerID);
            if (!accountExists)
            {
                throw new Exception("Hesap bulunamadı.");
            }
            var currentBalance = await _transactionRepository.GetAccountBalanceAsync(customerID, request.accountID);
            if (currentBalance < request.amount)
            {
                throw new Exception("Hesapta yeterli bakiye bulunmamaktadır.");
            }
            return await _transactionRepository.WithdrawAsync(customerID, request.accountID, request.amount, request.explanation);
        }

        public async Task<dynamic> TransferMoneyAsync(TransferRequest request, long customerID)
        {
            if (request.SourceAccountID == request.TargetAccountID)
            {
                throw new Exception("Gönderen ve alıcı hesap aynı olamaz.");
            }
            var sourceAccountExists = await _transactionRepository.CustomerAccountExistsAsync(request.SourceAccountID, customerID);
            if (!sourceAccountExists)
            {
                throw new Exception("Gönderen hesap bulunamadı.");
            }
            var targetAccountExists = await _transactionRepository.AccountExistsAsync(request.TargetAccountID);
            if (!targetAccountExists)
            {
                throw new Exception("Alıcı hesap bulunamadı.");
            }
            var currentBalance = await _transactionRepository.GetAccountBalanceAsync(customerID, request.SourceAccountID);
            if (currentBalance < request.Amount)
            {
                throw new Exception("Gönderen hesapta yeterli bakiye bulunmamaktadır.");
            }
            return await _transactionRepository.TransferMoneyAsync(customerID, request.SourceAccountID, request.TargetAccountID, request.Amount, request.Explanation);
        }

    }
}
