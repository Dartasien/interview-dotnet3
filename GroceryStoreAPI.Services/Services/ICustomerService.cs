using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Services
{
    public interface ICustomerService
    {
        Task<bool> CreateAsync(string customerName);
    }
}