using Ecommerce.Shared;
using System.Collections.Generic;
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
        public string Meassage { get; set; } = "Loading...";

        public int CurrentPage { get; set; } = 1;

        public int PageCount { get; set; } = 0;

        public string LastSearchText { get; set; } = string.Empty;

        public event Action ProductsChanged;

        public async Task GetProductsAsync(string? categoryUrl = null)
        {
            var result = (categoryUrl == null) ? await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/product/featured")
                                             : await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/category/{categoryUrl}");
            if (result != null && result.Data != null)
                Products = result.Data;

            CurrentPage = 1;
            PageCount = 0;

            if (Products.Count == 0)
            {
                Meassage = "No products found";
            }

            ProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProductsAsync(int productId)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
            return result;
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/GetProductSearchSuggestions/{searchText}");
            return result.Data;
        }

        public async Task SearchProducts(string searchText, int page)
        {
            LastSearchText = searchText;
            var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResultDto>>($"api/product/search/{searchText}/{page}");
            if (result != null && result.Data != null)
            {
                Products = result.Data.Products;
                CurrentPage = result.Data.CurrentPage;
                PageCount = result.Data.Pages;
            }
            if (Products.Count == 0)
            {
                Meassage = "No Products Found";
            }
            ProductsChanged?.Invoke();
        }
    }
}
