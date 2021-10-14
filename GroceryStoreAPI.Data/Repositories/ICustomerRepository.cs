using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomerAsync(string customerName);
        Task<bool> UpdateCustomerAsync(int customerId, string customerName);
        Task<bool> UpdateCustomerAsync(string originalCustomerName, string newCustomerName);
    }
}