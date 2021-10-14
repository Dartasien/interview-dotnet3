using GroceryStoreAPI.Services.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GroceryStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        [HttpPut]
        public async Task<IActionResult> PutAsync(string customerName)
        {
            if (string.IsNullOrEmpty(customerName))
                return BadRequest(nameof(customerName));

            var result = await _customerService.CreateAsync(customerName);

            return result ? Ok() : BadRequest(nameof(customerName));
        }
    }
}