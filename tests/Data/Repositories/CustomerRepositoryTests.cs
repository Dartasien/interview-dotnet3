using GroceryStoreAPI.Data;
using GroceryStoreAPI.Data.Entities;
using GroceryStoreAPI.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        public async void GetCustomerAsync_IdExists_ReturnsCustomer()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customer = await customerRepository.GetCustomerAsync(expectedId);

                Assert.Equal(customerName, customer.Name);
            }
        }

        [Fact]
        public async void GetCustomerAsync_IdDoesNotExist_ReturnsNull()
        {
            const int expectedId = 2;
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customer = await customerRepository.GetCustomerAsync(expectedId);

                Assert.Null(customer);
            }
        }

        [Fact]
        public async void GetAllCustomersAsync_HasTwoCustomers_ReturnsTwoCustomers()
        {
            const int expectedCount = 2;
            const string customerName = "Testy McPerson";
            const string customerNameTwo = "Second Person";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.Customers.Add(new Customer { Name = customerNameTwo });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customers = await customerRepository.GetAllCustomersAsync();

                Assert.Equal(expectedCount, customers.Count());
            }
        }

        [Fact]
        public async void GetAllCustomersAsync_HasThreeCustomers_ReturnsAllCustomers()
        {
            const int expectedCount = 3;
            const string customerName = "Testy McPerson";
            const string customerNameTwo = "Second Person";
            const string customerNameThree = "Third Person";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.Customers.Add(new Customer { Name = customerNameTwo });
                context.Customers.Add(new Customer { Name = customerNameThree });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customers = await customerRepository.GetAllCustomersAsync();

                Assert.Equal(expectedCount, customers.Count());
            }
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