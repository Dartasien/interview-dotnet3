using GroceryStoreAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

        public async Task<Customer> GetAsync(int customerId)
        {
            var customer = await _customerContext.Customers.FindAsync(customerId);

            return customer;
        }

        public async Task<Customer> GetAsync(string customerName)
        {
            var customer = await _customerContext.Customers
                .FirstOrDefaultAsync(c => c.Name.ToLower() == customerName.ToLower());

            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync() => await _customerContext.Customers.ToListAsync();

        public async Task<Customer> CreateAsync(string customerName)
        {
            var customer = new Customer
            {
                Name = customerName
            };

            _customerContext.Customers.Add(customer);

            await _customerContext.SaveChangesAsync();

            return customer;
        }

        public async Task<Customer> UpdateAsync(Customer customer)
        {
            var existingCustomer = await _customerContext.Customers.FindAsync(customer.Id);

            if (existingCustomer == null)
                return existingCustomer;

            existingCustomer.Name = customer.Name;

            await _customerContext.SaveChangesAsync();

            return existingCustomer;
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            var customer = await _customerContext.Customers.FindAsync(customerId);

            if (customer != null)
            {
                _customerContext.Customers.Remove(customer);
                var saved = await _customerContext.SaveChangesAsync();

                return saved == 1;
            }
            return false;
        }
    }
}   