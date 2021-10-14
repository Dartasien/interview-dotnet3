using GroceryStoreAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerAsync(int customerId);
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<bool> CreateCustomerAsync(string customerName);
        Task<bool> UpdateCustomerAsync(int customerId, string customerName);
        Task<bool> UpdateCustomerAsync(string originalCustomerName, string newCustomerName);
    }
}