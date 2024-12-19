using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using VB_api.Models;
using VB_api.Repositories.Interfaces;

namespace VB_api.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _connection;

        public CustomerRepository(IConfiguration configuration)
        {
            _connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<IEnumerable<dynamic>> GetAllCustomersAsync()
        {
            return await _connection.QueryAsync<dynamic>("sp_GetAllCustomers", commandType: CommandType.StoredProcedure);
        }

        public async Task<dynamic> GetCustomerByIdAsync(long customerId)
        {
            return await _connection.QuerySingleOrDefaultAsync<dynamic>(
                "sp_GetCustomerByID", new { CustomerID = customerId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<Customer> GetCustomerByEmailAsync(string email)
        {
            //QuerySingleOrDefaultAsync
            return await _connection.QuerySingleOrDefaultAsync<Customer>(
                "sp_GetCustomerByEmail", new { Email = email }, commandType: CommandType.StoredProcedure);
        }

        public async Task RegisterCustomerAsync(Customer customer)
        {
            var parameters = new
            {
                customer.CustomerName,
                customer.CustomerLastName,
                customer.CustomerBirthDate,
                customer.CustomerIdentityNumber,
                customer.CustomerEmail,
                customer.CustomerPasswordHash
            };
            await _connection.ExecuteAsync("sp_RegisterCustomer", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            var parameters = new
            {
                customer.CustomerID,
                customer.CustomerName,
                customer.CustomerLastName,
                customer.CustomerBirthDate,
                customer.CustomerIdentityNumber,
                customer.CustomerEmail,
                customer.CustomerPasswordHash
            };
            await _connection.ExecuteAsync("sp_UpdateCustomer", parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task DeleteCustomerAsync(long customerId)
        {
            await _connection.ExecuteAsync("sp_DeleteCustomer", new { CustomerID = customerId }, commandType: CommandType.StoredProcedure);
        }

        public async Task<bool> IsIdentityNumberTakenAsync(long identityNumber)
        {
            var result = await _connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Customer WHERE CustomerIdentityNumber = @IdentityNumber",
                new { IdentityNumber = identityNumber });
            return result > 0;
        }

        public async Task<bool> IsEmailTakenAsync(string email)
        {
            var result = await _connection.ExecuteScalarAsync<int>(
                "SELECT COUNT(1) FROM Customer WHERE CustomerEmail = @Email",
                new { Email = email });
            return result > 0;
        }
    }
}
