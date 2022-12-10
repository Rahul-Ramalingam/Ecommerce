using Ecommerce.Shared;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Client.Pages
{
    partial class ProductDetails
    {
        private Product? product = null;
        private string message = string.Empty;
        private int currentTypeId = 1;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            message = "Loading Product..";

            var result = await ProductService.GetProductsAsync(Id);
            if (!result.Success)
            {
                message = result.Message;
            }
            else
            {
                currentTypeId = result.Data.Variants.FirstOrDefault().ProductTypeId;
                product = result.Data;
            }
        }

        private ProductVariant? GetSelectedVariant()
        {
            return product?.Variants?.FirstOrDefault(v => v.ProductTypeId == currentTypeId);
        }

        private async Task AddToCart()
        {
            var productVariant = GetSelectedVariant();
            var cartItem = new CartItem
            {
                ProductId = productVariant.ProductId,
                ProductTypeId = productVariant.ProductTypeId
            };
            await CartService.AddToCart(cartItem);
        }
    }
}
