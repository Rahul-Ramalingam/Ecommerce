using Ecommerce.Shared;

namespace Ecommerce.Client.Shared
{
    partial class CartCounter
    {
        private int GetCartItemsCount()
        {
            var cart = localStorage.GetItem<List<CartItem>>("cart");
            return cart == null ? 0 : cart.Count;
        }

        protected override void OnInitialized()
        {
            CartService.OnChange += StateHasChanged;
        }

        public void Dispose()
        {
            CartService.OnChange -= StateHasChanged;
        }
    }
}
