using VB_api.Models;

namespace VB_api.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<dynamic>> GetAllCustomersAsync();
        Task<dynamic> GetCustomerByIdAsync(long customerId);
        Task<Customer> GetCustomerByEmailAsync(string email);
        Task RegisterCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(long customerId);
        Task<bool> IsIdentityNumberTakenAsync(long identityNumber);
        Task<bool> IsEmailTakenAsync(string email);
    }
}

