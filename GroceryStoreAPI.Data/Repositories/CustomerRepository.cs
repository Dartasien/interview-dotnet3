using GroceryStoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

            return saved == 1;
        }

        public async Task<bool> UpdateCustomerAsync(int customerId, string customerName)
        {
            var customer = await _customerContext.Customers.FindAsync(customerId);

            if (customer == null)
                return false;

            customer.Name = customerName;

            var saved = await _customerContext.SaveChangesAsync();

            return saved == 1;
        }

        public async Task<bool> UpdateCustomerAsync(string originalCustomerName, string newCustomerName)
        {
            var customer = await _customerContext.Customers.FirstOrDefaultAsync(c => c.Name == originalCustomerName);

            if (customer == null)
                return false;

            customer.Name = newCustomerName;

            var saved = await _customerContext.SaveChangesAsync();

            return saved == 1;
        }
    }
}   