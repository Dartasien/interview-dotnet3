using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Entities;
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
        public async void CreateCustomerAsync_HasName_CreatesNewCustomer()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";
            
            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                await customerRepository.CreateCustomerAsync(customerName);

                Assert.Equal(customerName, context.Customers.Find(expectedId).Name);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_HasCustomerId_UpdatesCustomerName()
        {
            const int expectedId = 1;
            const string originalCustomerName = "Testy McPerson";
            const string expectedCustomerName = "Testy McPherson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = originalCustomerName });
                context.SaveChanges();


                var customerRepository = new CustomerRepository(context);

                await customerRepository.UpdateCustomerAsync(expectedId, expectedCustomerName);

                Assert.Equal(expectedCustomerName, context.Customers.Find(expectedId).Name);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_HasNames_UpdatesCustomerNames()
        {
            const int expectedId = 1;
            const string originalCustomerName = "Testy McPerson";
            const string expectedCustomerName = "Testy McPherson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = originalCustomerName });
                context.SaveChanges();


                var customerRepository = new CustomerRepository(context);

                await customerRepository.UpdateCustomerAsync(originalCustomerName, expectedCustomerName);

                Assert.Equal(expectedCustomerName, context.Customers.Find(expectedId).Name);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_IdDoesNotExist_ReturnsFalse()
        {
            var customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                var result = await customerRepository.UpdateCustomerAsync(1, customerName);

                Assert.False(result);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_NameDoesNotExist_ReturnsFalse()
        {
            const string originalCustomerName = "Testy McPerson";
            const string expectedCustomerName = "Testy McPherson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                var result = await customerRepository.UpdateCustomerAsync(originalCustomerName, expectedCustomerName);

                Assert.False(result);
            }
        }
    }
}