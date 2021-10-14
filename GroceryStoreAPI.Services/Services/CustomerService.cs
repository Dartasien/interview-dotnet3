using GroceryStoreAPI.Data.Repositories;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public Task<bool> CreateAsync(string customerName)
        {
            throw new System.NotImplementedException();
        }
    }
}