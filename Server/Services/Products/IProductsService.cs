namespace Ecommerce.Server.Services.Products
{
    public interface IProductsService
    {
        public Task<ServiceResponse<List<Product>>> GetProductsAsync();

        public Task<ServiceResponse<Product>> GetProductsAsync(int productId);

        public Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl);

        public Task<ServiceResponse<ProductSearchResultDto>> SearchProducts(string searchText, int page);

        public Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

        public Task<ServiceResponse<List<Product>>> GetFeaturedProductsAsync();
    }
}
