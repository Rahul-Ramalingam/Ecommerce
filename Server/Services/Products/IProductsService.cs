namespace Ecommerce.Server.Services.Products
{
    public interface IProductsService
    {
        public Task<ServiceResponse<List<Product>>> GetProductsAsync();

        public Task<ServiceResponse<Product>> GetProductsAsync(int productId);
    }
}
