using Ecommerce.Shared;
using System.Net.Http.Json;

namespace Ecommerce.Client.Shared
{
    partial class ProductList
    {
        public static List<Product> products = new();

        protected override void OnInitialized()
        {
            ProductsService.ProductsChanged += StateHasChanged;
        }

        public void Dispose()
        {
            ProductsService.ProductsChanged -= StateHasChanged;
        }
    }
}
