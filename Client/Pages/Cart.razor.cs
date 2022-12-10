using Ecommerce.Shared;

namespace Ecommerce.Client.Pages
{
    partial class Cart
    {
        List<CartProductResponseDto> cartProducts = null;
        string message = "Loading cart...";

        protected override async Task OnInitializedAsync()
        {
            if((await CartService.GetCartItems()).Count == 0)
            {
                message = "Your Cart is Empty...";
                cartProducts = new List<CartProductResponseDto>();
            }
            else
            {
                cartProducts = await CartService.GetCartProducts();
            }
        }
    }
}
