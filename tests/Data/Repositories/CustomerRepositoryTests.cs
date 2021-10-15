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

                var customer = await customerRepository.GetAsync(expectedId);

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

                var customer = await customerRepository.GetAsync(expectedId);

                Assert.Null(customer);
            }
        }



        [Fact]
        public async void GetCustomerAsync_NameExists_ReturnsCustomer()
        {
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customer = await customerRepository.GetAsync(customerName);

                Assert.Equal(customerName, customer.Name);
            }
        }


        [Fact]
        public async void GetCustomerAsync_NameExistsWithDifferentCase_ReturnsCustomer()
        {
            const string customerName = "Testy McPerson";
            const string testedName = "testy mcperson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = customerName });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var customer = await customerRepository.GetAsync(testedName);

                Assert.Equal(customerName, customer.Name);
            }
        }

        [Fact]
        public async void GetCustomerAsync_NameDoesNotExist_ReturnsNull()
        {
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                var customer = await customerRepository.GetAsync(customerName);

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

                var customers = await customerRepository.GetAllAsync();

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

                var customers = await customerRepository.GetAllAsync();

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

                await customerRepository.CreateAsync(customerName);

                Assert.Equal(customerName, context.Customers.Find(expectedId).Name);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_HasCustomerId_UpdatesCustomerName()
        {
            const int expectedId = 1;
            const string originalCustomName = "Testy McPerson";
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Name = originalCustomName });
                context.SaveChanges();


                var customerRepository = new CustomerRepository(context);

                await customerRepository.UpdateAsync(new Customer { Id = expectedId, Name = customerName});

                Assert.Equal(customerName, context.Customers.Find(expectedId).Name);
            }
        }

        [Fact]
        public async void UpdateCustomerAsync_IdDoesNotExist_ReturnsFalse()
        {
            const int customerId = 1;
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var customerRepository = new CustomerRepository(context);

                var result = await customerRepository.UpdateAsync(new Customer { Id = customerId, Name = customerName});

                Assert.Null(result);
            }
        }

        [Fact]
        public async void DeleteCustomerAsync_IdExists_DeletesCustomer()
        {
            const int expectedCount = 0;
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Customers.Add(new Customer { Id = expectedId, Name = customerName });
                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);
                
                var result = await customerRepository.DeleteAsync(expectedId);

                Assert.True(result);
                Assert.Equal(expectedCount, context.Customers.Count());
            }
        }

        [Fact]
        public async void DeleteCustomerAsync_IdDoesNotExist_ReturnsFalse()
        {
            const int expectedId = 1;

            using (var context = new CustomerContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.SaveChanges();

                var customerRepository = new CustomerRepository(context);

                var result = await customerRepository.DeleteAsync(expectedId);

                Assert.False(result);
            }
        }
    }
}