using GroceryStoreAPI.Data.Entities;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Data.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _customerContext;

        public CustomerRepository(CustomerContext customerContext)
        {
            _customerContext = customerContext;
        }

        public async Task<bool> CreateCustomerAsync(string customerName)
        {
            var customer = new Customer
            {
                Name = customerName
            };

            _customerContext.Customers.Add(customer);

            var saved = await _customerContext.SaveChangesAsync();

            return saved == 1 ;
        }
    }
}   