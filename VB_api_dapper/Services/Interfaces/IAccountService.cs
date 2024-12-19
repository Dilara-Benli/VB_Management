using VB_api.Models;

namespace VB_api.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<IEnumerable<Account>> GetAccountsByCustomerAsync(long customerID);
        Task<dynamic> CreateAccountAsync(long customerID, AccountRequest request);
        Task<int> UpdateAccountAsync(long accountID, AccountRequest request);
        Task<int> DeleteAccountAsync(long accountID);
    }
}
