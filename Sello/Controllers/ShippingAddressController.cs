using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;

namespace Sello.Api.Controllers
{
    [Route("api/shipping-addresses")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class ShippingAddressController : ControllerBase
    {
        private readonly IShippingAddressService _shippingAddressService;

        public ShippingAddressController(IShippingAddressService shippingAddressService)
        {
            _shippingAddressService = shippingAddressService;
        }

        [HttpPost]
        public async Task<ActionResult<ShippingAddressResponseDto>> AddShippingAddress(CreateShippingAddressDto addressDto)
        {
            var address = await _shippingAddressService.AddShippingAddress(addressDto);
            return CreatedAtAction(nameof(GetShippingAddressById), new { id = address.Id }, address);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShippingAddressResponseDto>>> GetAllShippingAddresses()
        {
            var addresses = await _shippingAddressService.GetAllShippingAddresses();
            return Ok(addresses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ShippingAddressResponseDto>> GetShippingAddressById(Guid id)
        {
            var address = await _shippingAddressService.GetShippingAddressById(id);
            if (address == null)
                return NotFound("Address with this id does not exist.");

            return Ok(address);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditShippingAddress(Guid id, UpdateShippingAddressDto addressDto)
        {
            var edited = await _shippingAddressService.EditShippingAddress(id, addressDto);
            if (edited == false)
                return NotFound("Address with this id does not exist.");

            return Ok("Address updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShippingAddress(Guid id)
        {
            var deleted = await _shippingAddressService.DeleteShippingAddress(id);
            if (deleted == false)
                return NotFound("Address with this id does not exist.");

            return Ok("Address deleted successfully!");
        }
    }
}
