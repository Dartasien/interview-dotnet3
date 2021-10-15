using GroceryStoreAPI.Services.Models;
using GroceryStoreAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/customers")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ILogger _logger;

        public CustomersController(ICustomerService customerService, ILogger logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        /// <summary>
        /// Get Customer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        [Produces("application/json", Type = typeof(CustomerResponse))]
        public async Task<IActionResult> GetAsync(int id)
        {
            if (id == 0)
                return BadRequest(nameof(id));
            try
            {
                var response = await _customerService.GetAsync(new CustomerRequest { Id = id });
                if (response.Id == 0)
                {
                    return NotFound();
                }
                return Ok(response);

            } catch (Exception ex)
            {
                _logger.Error(ex.Message); 
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Get Customer by Name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("name")]
        [Produces("application/json", Type = typeof(CustomerResponse))]
        public async Task<IActionResult> GetAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(nameof(name));
            try
            {
                var response = await _customerService.GetAsync(new CustomerRequest { Name = name });
                if (response.Id == 0)
                {
                    return NotFound();
                }
                return Ok(response);

            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500);
            }
        }


        /// <summary>
        /// GetAllCustomers
        /// </summary>
        /// <returns>IEnumerable<Customer></returns>
        [HttpGet]
        [Produces("application/json", Type = typeof(IEnumerable<CustomerResponse>))]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var response = await _customerService.GetAllAsync();
                return Ok(response);
            } catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Creates a New Customer
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", Type = typeof(CustomerResponse))]
        public async Task<IActionResult> PostAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(nameof(name));

            try
            {
                var response = await _customerService.CreateAsync(name);
                return response.Id > 0 ? Ok(response) : BadRequest(nameof(name));

            } catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Updates a Customer Name
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Produces("application/json", Type = typeof(CustomerResponse))]
        public async Task<IActionResult> PutAsync(CustomerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(nameof(request));
            }

            try
            {
                var response = await _customerService.UpdateAsync(request);
                return response.Id > 0 ? Ok(response) : BadRequest(nameof(request));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Deletes a Customer. This is not reversible.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest(nameof(id));
            }

            try
            {
                var response = await _customerService.DeleteAsync(id);
                return response ? Ok() : BadRequest(nameof(id));
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500);
            }
        }
    }
}