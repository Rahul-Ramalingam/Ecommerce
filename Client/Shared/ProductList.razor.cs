using Ecommerce.Shared;
using System.Net.Http.Json;

namespace Ecommerce.Client.Shared
{
    partial class ProductList
    {
        public static List<Product> products = new();

        protected override async Task OnInitializedAsync()
        {
            await ProductsService.GetProductsAsync();
        }
    }
}
