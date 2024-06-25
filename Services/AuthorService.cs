using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class AuthorService : IAuthor
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public AuthorService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateAuthor(AuthorModel model)
        {
            var author = _mapper.Map<Author>(model);
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return author.Id;
        }

        public async Task DeleteAuthor(int id)
        {
            var author = _context.Authors.FirstOrDefault(x => x.Id == id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<AuthorModel>> GetAllAuthors()
        {
            var authors = await _context.Authors.ToListAsync();
            return _mapper.Map<List<AuthorModel>>(authors);
        }

        public async Task<AuthorModel> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            return _mapper.Map<AuthorModel>(author);
        }

        public async Task UpdateAuthor(int id, AuthorModel model)
        {
            if (id == model.Id)
            {
                var author = await _context.Authors.FindAsync(id);
                if (author != null)
                {
                    author.Name = model.Name;
                    author.Description = model.Description;
                    _context.Authors.Update(author);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
