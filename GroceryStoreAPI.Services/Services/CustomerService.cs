using AutoMapper;
using GroceryStoreAPI.Data.Entities;
using GroceryStoreAPI.Data.Repositories;
using GroceryStoreAPI.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Services.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository; 
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<CustomerResponse> GetAsync(CustomerRequest request)
        {
            var customer = request.Id > 0 
                ? await _customerRepository.GetAsync(request.Id) 
                : await _customerRepository.GetAsync(request.Name);

            if (customer == null)
            { 
                return new CustomerResponse { Id = 0 }; 
            }

            var response = _mapper.Map<CustomerResponse>(customer);
            return response;    
        }

        public async Task<IEnumerable<CustomerResponse>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            var response = _mapper.Map<IEnumerable<CustomerResponse>>(customers);

            return response;
        }

        public async Task<CustomerResponse> CreateAsync(string customerName)
        {
            var customer = await _customerRepository.GetAsync(customerName);
            

            if (customer == null)
            {
                customer = await _customerRepository.CreateAsync(customerName);
            }

            var response = _mapper.Map<CustomerResponse>(customer);
            return response;
        }

        public async Task<CustomerResponse> UpdateAsync(CustomerRequest request)
        {
            var customer = _mapper.Map<Customer>(request);

            var updatedCustomer = await _customerRepository.UpdateAsync(customer);

            if (updatedCustomer == null)
            {
                return new CustomerResponse {  Id = 0 };
            }
            var response = _mapper.Map<CustomerResponse>(updatedCustomer);
            return response;
        }

        public async Task<bool> DeleteAsync(int customerId)
        {
            var result = await _customerRepository.DeleteAsync(customerId);

            return result;
        }
    }
}