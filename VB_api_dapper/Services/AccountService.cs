using VB_api.Models;
using VB_api.Repositories.Interfaces;
using VB_api.Services.Interfaces;

namespace VB_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _accountRepository.GetAllAccountsAsync();
        }

        public async Task<IEnumerable<Account>> GetAccountsByCustomerAsync(long customerID)
        {
            var customerExists = await _accountRepository.CustomerExistsAsync(customerID);
            if (!customerExists)
            {
                throw new Exception("Müşteri bulunamadı.");
            }

            return await _accountRepository.GetAccountsByCustomerIDAsync(customerID);
        }

        public async Task<dynamic> CreateAccountAsync(long customerID, AccountRequest request)
        {
            return await _accountRepository.CreateAccountAsync(
                customerID,
                request.accountName,
                request.currencyType ?? "TL"
            );
        }

        public async Task<int> UpdateAccountAsync(long accountID, AccountRequest request)
        {
            return await _accountRepository.UpdateAccountAsync(
                accountID,
                request.accountName,
                request.currencyType
            );
        }

        public async Task<int> DeleteAccountAsync(long accountID)
        {
            return await _accountRepository.DeleteAccountAsync(accountID);
        }
    }
}
