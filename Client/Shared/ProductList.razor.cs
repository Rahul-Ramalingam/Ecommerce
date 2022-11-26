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

        private string GetPriceText(Product product)
        {
            var variants = product.Variants;
            switch (variants.Count)
            {
                case 0:
                    return string.Empty;
                case >1:
                    var ordered = variants.OrderBy(x => x.Price).ToList();
                    return $"Price varies from ₹{ordered[0].Price} to ₹{ordered.Last().Price}";
                default:
                    return $"₹{variants[0].Price}";
            }
        }
    }
}
