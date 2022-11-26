namespace Ecommerce.Server.Services.Categories
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();
    }
}
