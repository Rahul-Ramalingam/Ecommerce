using Ecommerce.Shared;
using Microsoft.AspNetCore.Components;

namespace Ecommerce.Client.Pages
{
    partial class ProductDetails
    {
        private Product? product = null;
        private string message = string.Empty;

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
                product = result.Data;
            }
        }
    }
}
