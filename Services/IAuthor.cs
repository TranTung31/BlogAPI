using BlogAPI.Models;

namespace BlogAPI.Services
{
    public interface IAuthor
    {
        public Task<List<AuthorModel>> GetAllAuthors();
        public Task<AuthorModel> GetAuthor(int id);
        public Task<int> CreateAuthor(AuthorModel model);
        public Task UpdateAuthor(int id, AuthorModel model);
        public Task DeleteAuthor(int id);
    }
}
