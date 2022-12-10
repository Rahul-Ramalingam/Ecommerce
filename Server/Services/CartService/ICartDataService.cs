namespace Ecommerce.Server.Services.CartService
{
    public interface ICartDataService
    {
        public Task<ServiceResponse<List<CartProductResponseDto>>> GetCartProducts(List<CartItem> cartItems);
    }
}
