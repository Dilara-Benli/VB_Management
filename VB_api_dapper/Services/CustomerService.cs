using VB_api.Models;
using VB_api.Repositories.Interfaces;
using VB_api.Services.Interfaces;

namespace VB_api.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<dynamic>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        public async Task<dynamic> GetCustomerByIdAsync(long customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (customer == null)
            {
                throw new Exception("Müşteri bulunamadı.");
            }

            return customer;
        }

        public async Task RegisterCustomerAsync(RegisterRequest request)
        {
            if (await _customerRepository.IsIdentityNumberTakenAsync(request.identityNumber))
            {
                throw new Exception("TC Kimlik Numarası zaten alınmış.");
            }

            if (await _customerRepository.IsEmailTakenAsync(request.email))
            {
                throw new Exception("E-posta adresi zaten alınmış.");
            }

            var customer = new Customer
            {
                CustomerName = request.name,
                CustomerLastName = request.lastName,
                CustomerBirthDate = request.birthDate,
                CustomerIdentityNumber = request.identityNumber,
                CustomerEmail = request.email,
                CustomerPasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password)
            };

            await _customerRepository.RegisterCustomerAsync(customer);
        }

        public async Task<Customer> LoginAsync(string email, string password)
        {
            var customer = await _customerRepository.GetCustomerByEmailAsync(email);

            if (customer == null)
            {
                throw new Exception("Geçersiz mail veya şifre.");
            }

            if (!BCrypt.Net.BCrypt.Verify(password, customer.CustomerPasswordHash))
            {
                throw new Exception("Geçersiz mail veya şifre.");
            }

            return customer;
        }

        public async Task UpdateCustomerAsync(long customerId, RegisterRequest request)
        {
            var existingCustomer = await _customerRepository.GetCustomerByIdAsync(customerId);

            if (existingCustomer.CustomerIdentityNumber != request.identityNumber &&
                await _customerRepository.IsIdentityNumberTakenAsync(request.identityNumber))
            {
                throw new Exception("TC Kimlik Numarası zaten alınmış.");
            }

            if (existingCustomer.CustomerEmail != request.email &&
                await _customerRepository.IsEmailTakenAsync(request.email))
            {
                throw new Exception("E-posta adresi zaten alınmış.");
            }

            var customer = new Customer
            {
                CustomerID = customerId,
                CustomerName = request.name,
                CustomerLastName = request.lastName,
                CustomerBirthDate = request.birthDate,
                CustomerIdentityNumber = request.identityNumber,
                CustomerEmail = request.email,
                CustomerPasswordHash = string.IsNullOrEmpty(request.password) ? null : BCrypt.Net.BCrypt.HashPassword(request.password)
            };

            await _customerRepository.UpdateCustomerAsync(customer);
        }

        public async Task DeleteCustomerAsync(long customerId)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(customerId);
            if (customer == null)
            {
                throw new Exception("Müşteri bulunamadı.");
            }
           
            await _customerRepository.DeleteCustomerAsync(customerId);
        }
    }
}
