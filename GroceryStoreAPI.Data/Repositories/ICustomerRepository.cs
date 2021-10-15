using GroceryStoreAPI.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer> GetAsync(int customerId);
        Task<Customer> GetAsync(string customerName);
        Task<IEnumerable<Customer>> GetAllAsync();
        Task<Customer> CreateAsync(string customerName);
        Task<Customer> UpdateAsync(Customer customer);
        Task<bool> DeleteAsync(int customerId);
    }
}