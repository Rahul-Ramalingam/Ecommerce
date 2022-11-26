using Ecommerce.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services.Products
{
    public class ProductsService : IProductsService
    {
        private readonly DataContext _dbContext;
        public ProductsService(DataContext context)
        {
            _dbContext = context;
        }
        public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        {
            return new ServiceResponse<List<Product>>()
            {
                Data = await _dbContext.Products
                .Include(v => v.Variants)
                .ThenInclude(p => p.ProductType)
                .ToListAsync()
            };
        }

        public async Task<ServiceResponse<Product>> GetProductsAsync(int productId)
        {
            var response = new ServiceResponse<Product>();
            var product = await _dbContext.Products
                .Include(v=>v.Variants)
                .ThenInclude(p => p.ProductType)
                .FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                response.Success = false;
                response.Message = "Product does not exist";
            }
            else
            {
                response.Success = true;
                response.Data = product;
            }
            return response;
        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryUrl)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await _dbContext.Products
                        .Where(p => p.Category.Url.ToLower() == categoryUrl.ToLower())
                        .Include(v => v.Variants)
                        .ThenInclude(p => p.ProductType)
                        .ToListAsync()
            };

            return response;
        }
    }
}
