using VB_api.Models;

namespace VB_api.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<dynamic>> GetAllCustomersAsync();
        Task<dynamic> GetCustomerByIdAsync(long customerId);
        Task RegisterCustomerAsync(RegisterRequest request);
        Task<Customer> LoginAsync(string email, string password);
        Task UpdateCustomerAsync(long customerId, RegisterRequest request);
        Task DeleteCustomerAsync(long customerId);
    }
}
