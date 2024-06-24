using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class CategoryService : ICategory
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateCategory(CategoryModel model)
        {
            var category = _mapper.Map<Category>(model);
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.Id;
        }

        public async Task DeleteCategory(int id)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<List<CategoryModel>>(categories);
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            return _mapper.Map<CategoryModel>(category);
        }

        public async Task UpdateCategory(int id, CategoryModel model)
        {
            if (id == model.Id)
            {
                var category = _mapper.Map<Category>(model);
                _context.Categories.Update(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
