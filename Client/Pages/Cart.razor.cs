using Ecommerce.Shared;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Client.Pages
{
    partial class Cart
    {
        List<CartProductResponseDto> cartProducts = null;
        string message = "Loading cart...";

        protected override async Task OnInitializedAsync()
        {
            await LoadCart();
        }

        private async Task RemoveProductFromCart(int productID, int productTypeId)
        {
            await CartService.RemoveProductFromCart(productID, productTypeId);
            await LoadCart();
        }

        private async Task LoadCart()
        {
            if ((await CartService.GetCartItems()).Count == 0)
            {
                message = "Your Cart is Empty...";
                cartProducts = new List<CartProductResponseDto>();
            }
            else
            {
                cartProducts = await CartService.GetCartProducts();
            }
        }

        private async Task UpdateQuantity(ChangeEventArgs e, CartProductResponseDto product)
        {
            product.Quantity = int.Parse(e.Value.ToString());
            if(product.Quantity < 1) {
                product.Quantity = 1;
            }
            await CartService.UpdateQuantity(product);
        }
    }
}
