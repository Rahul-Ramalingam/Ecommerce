using Ecommerce.Shared;
using System.Net.Http.Json;

namespace Ecommerce.Client.Services.ProductsService
{
    public class ProductsService : IProductsService
    {
        private readonly HttpClient _httpClient;
        public ProductsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Product> Products { get; set; } = new List<Product>();

        public async Task GetProductsAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product");
            if (result != null && result.Data != null)
                Products = result.Data;
        }

        public async Task<ServiceResponse<Product>> GetProductsAsync(int productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
            return result;
        }
    }
}
