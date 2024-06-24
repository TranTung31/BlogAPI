using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface ICategory
    {
        public Task<List<CategoryModel>> GetAllCategories();
        public Task<CategoryModel> GetCategory(int id);
        public Task<int> CreateCategory(CategoryModel model);
        public Task UpdateCategory(int id, CategoryModel model);
        public Task DeleteCategory(int id);
    }
}
