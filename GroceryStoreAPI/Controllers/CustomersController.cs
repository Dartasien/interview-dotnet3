using GroceryStoreAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPut]
        public async Task<IActionResult> PutAsync(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
                return BadRequest(nameof(customerName));

            var result = false;

            try
            {
                result = await _customerService.CreateAsync(customerName);

            } catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            

            return result ? Ok() : BadRequest(nameof(customerName));
        }
    }
}