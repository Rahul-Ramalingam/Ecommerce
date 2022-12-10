using Ecommerce.Server.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartDataService _cartService;

        public CartController(ICartDataService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems)
        {
            var result = _cartService.GetCartProducts(cartItems);
            return result.Result;
        }
    }
}
