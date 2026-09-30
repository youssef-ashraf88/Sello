using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Customer")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutResponseDto>> Checkout(CheckoutRequestDto request)
        {
            var checkout = await _orderService.Checkout(request);

            return Ok(checkout);
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultResponseDto<OrderHistoryResponseDto>>> GetAllOrders([FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var orders = await _orderService.GetAllOrders(pageNumber, pageSize);
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailsResponseDto>> GetOrderDetails(Guid id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
                return NotFound("Order with this id does not exist");

            return Ok(order);
        }
    }
}
