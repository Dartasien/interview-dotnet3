using AutoMapper;
using GroceryStoreAPI.Data.Entities;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Services.MappingProfiles;
using GroceryStoreAPI.Services.Models;
using GroceryStoreAPI.Services.Services;
using Moq;
using Xunit;

namespace tests.Services
{
    public class CustomerServiceTests
    {

        [Fact]
        public async void CreateAsync_CustomerDoestNotExist_ReturnsCustomerWithId()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            var mockCustomerRepository = new Mock<ICustomerRepository>();

            
            mockCustomerRepository.Setup(x => x.CreateAsync(customerName))
                .ReturnsAsync(new Customer { Id = expectedId, Name = customerName})
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.CreateAsync(customerName);

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.CreateAsync(customerName), Times.Once);
        }

        [Fact]
        public async void CreateAsync_CustomerDoesExist_ReturnsCustomerWithId()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.CreateAsync(customerName))
                .ReturnsAsync(new Customer { Id = expectedId, Name = customerName })
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.CreateAsync(customerName);

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.CreateAsync(customerName), Times.Once);
        }

        [Fact]
        public async void GetAsync_IdDoesNotExist_ReturnsCustomerWithNoId()
        {
            const int expectedId = 0;
            const int customerId = 1;

            var mockCustomerRepository = new Mock<ICustomerRepository>();

            mockCustomerRepository.Setup(x => x.GetAsync(customerId))
                .ReturnsAsync((Customer)null)
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.GetAsync(new CustomerRequest { Id = customerId });

            Assert.Equal(expectedId, result.Id);
        }

        [Fact]
        public async void GetAsync_HasIdAndCustomerExists_ReturnsCustomer()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.GetAsync(expectedId))
                .ReturnsAsync(new Customer { Id = expectedId, Name = customerName })
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.GetAsync(new CustomerRequest { Id = expectedId });

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.GetAsync(expectedId), Times.Once);
        }

        [Fact]
        public async void GetAsync_NameAndCustomerDoesNotExist_ReturnsCustomerWithEmptyId()
        {
            const int expectedId = 0;
            const string customerName = "Testy McPerson";

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.GetAsync(customerName))
                .ReturnsAsync((Customer)null)
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.GetAsync(new CustomerRequest { Name = customerName });

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.GetAsync(customerName), Times.Once);
        }

        [Fact]
        public async void GetAsync_HasNameAndCustomerExists_ReturnsCustomer()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.GetAsync(customerName))
                .ReturnsAsync(new Customer { Id = expectedId, Name = customerName })
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.GetAsync(new CustomerRequest { Name = customerName });

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.GetAsync(customerName), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_CustomerExists_ReturnsCustomerWithUpdatedName()
        {
            const int expectedId = 1;
            const string customerName = "Testy McPerson";

            var customer = new Customer
            {
                Id = expectedId,
                Name = customerName
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();
            mockCustomerRepository.Setup(x => x.UpdateAsync(It.IsAny<Customer>()))
                .ReturnsAsync(new Customer { Id = expectedId, Name = customerName })
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.UpdateAsync(new CustomerRequest { Id = expectedId, Name = customerName });

            Assert.Equal(expectedId, result.Id);
            Assert.Equal(customerName, result.Name);
            mockCustomerRepository.Verify(mcr => mcr.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async void UpdateAsync_CustomerDoesNotExist_ReturnsCustomerEmptyId()
        {
            const int expectedId = 0;
            const int customerId = 1;
            const string customerName = "Testy McPerson";
            
            var customer = new Customer
            {
                Id = customerId,
                Name = customerName
            };

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.UpdateAsync(customer))
                .ReturnsAsync((Customer)null)
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);

            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.UpdateAsync(new CustomerRequest { Id = expectedId, Name = customerName });

            Assert.Equal(expectedId, result.Id);
            mockCustomerRepository.Verify(mcr => mcr.UpdateAsync(It.IsAny<Customer>()), Times.Once);
        }

        [Fact]
        public async void DeleteAsync_CustomerDoesNotExist_ReturnsFalse()
        {
            const int customerId = 1;
            
            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(false)
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);
            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.DeleteAsync(customerId);

            Assert.False(result);
            mockCustomerRepository.Verify(mcr => mcr.DeleteAsync(It.IsAny<int>()), Times.Once);
        }


        [Fact]
        public async void DeleteAsync_CustomerDoesExist_ReturnsTrue()
        {
            const int customerId = 1;

            var mockCustomerRepository = new Mock<ICustomerRepository>();


            mockCustomerRepository.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(true)
                .Verifiable();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(CustomerProfile));
            });

            var automapper = new Mapper(config);
            var service = new CustomerService(mockCustomerRepository.Object, automapper);

            var result = await service.DeleteAsync(customerId);

            Assert.True(result);
            mockCustomerRepository.Verify(mcr => mcr.DeleteAsync(It.IsAny<int>()), Times.Once);
        }
    }
}