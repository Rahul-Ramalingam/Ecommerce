using Ecommerce.Shared;
using System.Net.Http.Json;

namespace Ecommerce.Client.Shared
{
    partial class ProductList
    {
        public static List<Product> products = new();

        protected override async Task OnInitializedAsync()
        {
            var result = await Http.GetFromJsonAsync<List<Product>>("api/Product");
            if(result != null)
                products = result;
        }
    }
}
