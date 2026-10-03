using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sello.Application.DTO;
using Sello.Application.ServiceContracts;
using Sello.Domain.Enums;

namespace Sello.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IReviewService _reviewService;
        private readonly IUserService _userService;
        private readonly IDashboardService _dashboardService;

        public AdminController(IOrderService orderService, IReviewService reviewService, IUserService userService, IDashboardService dashboardService)
        {
            _orderService = orderService;
            _reviewService = reviewService;
            _userService = userService;
            _dashboardService = dashboardService;
        }

        [HttpGet("orders")]
        public async Task<ActionResult<PagedResultResponseDto<OrderHistoryResponseDto>>> GetAllOrders([FromQuery] OrderStatus? orderStatus, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            var orders = await _orderService.GetAllOrdersForAdmin(orderStatus, pageNumber, pageSize);

            return Ok(orders);
        }

        [HttpPut("orders/{id}/status")]
        public async Task<ActionResult<OrderDetailsResponseDto>> UpdateOrderStatus(Guid id, UpdateOrderStatusRequestDto update)
        {
            var order = await _orderService.UpdateOrderStatus(id, update);
            if (order == null)
                return NotFound("Order with this id does not exist");

            return Ok(order);
        }

        [HttpGet("reviews")]
        public async Task<ActionResult<IEnumerable<ReviewResponseDto>>> GetAllReviews()
        {
            var reviews = await _reviewService.GetAllReviews();
            return Ok(reviews);
        }

        [HttpDelete("reviews/{id}")]
        public async Task<ActionResult> DeleteReview(Guid id)
        {
            var deleted = await _reviewService.DeleteReview(id);
            if (deleted == false)
                return NotFound("Couldn't find the review!");

            return NoContent();
        }

        [HttpGet("users")]
        public async Task<ActionResult<PagedResultResponseDto<UserResponseDto>>> GetAllUsers([FromQuery] UserQueryParamsDto paramsDto)
        {
            var users = await _userService.GetAllUsers(paramsDto);

            return Ok(users);
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardResponseDto>> GetDashboardData()
        {
            var dashboardData = await _dashboardService.GetDashboardStatistics();
            return Ok(dashboardData);
        }
    }
}
