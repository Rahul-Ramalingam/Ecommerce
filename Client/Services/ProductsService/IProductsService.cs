using Ecommerce.Shared;

namespace Ecommerce.Client.Services.ProductsService
{
    public interface IProductsService
    {
        //to notify the UI when we change the category
        event Action ProductsChanged;

        public List<Product> Products { get; set; }

        public string Meassage { get; set; }

        public Task GetProductsAsync(string? categoryUrl = null);

        public Task<ServiceResponse<Product>> GetProductsAsync(int productId);
        
        public Task SearchProducts(string SearchText);

        public Task<List<string>> GetProductSearchSuggestions(string SearchText);
    }
}
