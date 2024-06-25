using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface IBlog
    {
        public Task<List<BlogModel>> GetAllBlogs();
        public Task<BlogModel> GetBlog(int id);
        public Task<int> CreateBlog(BlogRequest model);
        public Task UpdateBlog(int id, BlogRequest model);
        public Task DeleteBlog(int id);
    }
}
