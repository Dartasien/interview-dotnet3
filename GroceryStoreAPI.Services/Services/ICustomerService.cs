using GroceryStoreAPI.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Services
{
    public interface ICustomerService
    {
        Task<CustomerResponse> GetAsync(CustomerRequest request);
        Task<IEnumerable<CustomerResponse>> GetAllAsync();
        Task<CustomerResponse> CreateAsync(string customerName);
        Task<CustomerResponse> UpdateAsync(CustomerRequest request);
        Task<bool> DeleteAsync(int customerId);
    }
}