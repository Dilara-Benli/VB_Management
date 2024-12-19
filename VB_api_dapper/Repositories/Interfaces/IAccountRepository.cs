using VB_api.Models;

namespace VB_api.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<IEnumerable<Account>> GetAccountsByCustomerIDAsync(long customerID);
        Task<dynamic> CreateAccountAsync(long customerID, string accountName, string currencyType);
        Task<int> UpdateAccountAsync(long accountID, string accountName, string currencyType);
        Task<int> DeleteAccountAsync(long accountID);
        Task<bool> CustomerExistsAsync(long customerId);
    }
}
