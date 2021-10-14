using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace tests.Data.Repositories
{
    public class CustomerRepositoryTests : BaseRepositoryTests
    {
        public CustomerRepositoryTests()
        : base(
            new DbContextOptionsBuilder<CustomerContext>()
                .UseSqlite("Filename=Test.db")
                .Options)
        {
        }

        [Fact]
        public void CreateCustomerAsync_HasName_CreatesNewCustomer()
        {
            var customerName = "Testy McPerson";
            
            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                var result = customerRepository.CreateCustomerAsync(customerName);

                Assert.Equal(customerName, context.Customers.Find(1).Name);
            }
        }
    }
}