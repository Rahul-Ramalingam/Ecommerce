using Ecommerce.Server.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Tracing;

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

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await SearchProduct(searchText);
            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if(product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split().Select(s => s.Trim(punctuation));

                    result.AddRange(words.Where(w => w.Contains(searchText, StringComparison.OrdinalIgnoreCase) && !result.Contains(w)));
                }
            }
            return new ServiceResponse<List<string>> { Data = result};
        }

        public async Task<ServiceResponse<List<Product>>> SearchProducts(string searchText)
        {
            var response = new ServiceResponse<List<Product>>
            {
                Data = await SearchProduct(searchText)
            };
            return response;
        }

        private Task<List<Product>> SearchProduct(string searchText)
        {
            return _dbContext.Products.Where(p => p.Title.ToLower().Contains(searchText.ToLower())
                                                                  || p.Description.ToLower().Contains(searchText.ToLower()))
                                                            .Include(p => p.Variants)
                                                            .ToListAsync();
        }
    }
}
