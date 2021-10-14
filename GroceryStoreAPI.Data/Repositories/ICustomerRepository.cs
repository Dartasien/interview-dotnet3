using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public interface ICustomerRepository
    {
        Task<bool> CreateCustomerAsync(string customerName);
    }
}