namespace Ecommerce.Server.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dbContext;

        public CategoryService(DataContext context)
        {
            _dbContext = context;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategoriesAsync()
        {
            return new ServiceResponse<List<Category>>
            {
                Data = await _dbContext.Categories.ToListAsync()
            };
        }
    }
}
