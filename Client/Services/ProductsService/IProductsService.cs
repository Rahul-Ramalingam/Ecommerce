using Ecommerce.Shared;

namespace Ecommerce.Client.Services.ProductsService
{
    public interface IProductsService
    {
        public List<Product> Products { get; set; }

        public Task GetProductsAsync();

        public Task<ServiceResponse<Product>> GetProductsAsync(int productId);
    }
}
