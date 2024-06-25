using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Helpers;
using BlogAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class BlogService : IBlog
    {
        private readonly BlogDbContext _context;
        private readonly IMapper _mapper;

        public BlogService(BlogDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateBlog(BlogRequest model)
        {
            var blog = _mapper.Map<Blog>(model);
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.Id;
        }

        public async Task DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            } else
            {
                throw new AppException("Blog with Id {0} not found!", id);
            }
        }

        public async Task<List<BlogModel>> GetAllBlogs()
        {
            var query = from blog in _context.Blogs
                        join author in _context.Authors on blog.AuthorId equals author.Id
                        join category in _context.Categories on blog.CategoryId equals category.Id
                        select new BlogModel
                        {
                            Id = blog.Id,
                            Title = blog.Title,
                            Thumbnail = blog.Thumbnail,
                            Content = blog.Content,
                            Status = blog.Status,
                            DateCreated = blog.DateCreated,
                            DateUpdated = blog.DateUpdated,
                            AuthorName = author.Name,
                            CategoryName = category.Name,
                        };
            return await query.ToListAsync();
        }

        public async Task<BlogModel> GetBlog(int id)
        {
            var query = from blog in _context.Blogs
                        join author in _context.Authors on blog.AuthorId equals author.Id
                        join category in _context.Categories on blog.CategoryId equals category.Id
                        select new BlogModel
                        {
                            Id = blog.Id,
                            Title = blog.Title,
                            Thumbnail = blog.Thumbnail,
                            Content = blog.Content,
                            Status = blog.Status,
                            DateCreated = blog.DateCreated,
                            DateUpdated = blog.DateUpdated,
                            AuthorName = author.Name,
                            CategoryName = category.Name,
                        };
            var result = await query.FirstOrDefaultAsync(x => x.Id == id);
            if (result != null)
            {
                return result;
            } else
            {
                throw new AppException("Blog with Id {0} not found!", id);
            }
        }

        public async Task UpdateBlog(int id, BlogRequest model)
        {
            if (id == model.Id)
            {
                var blog = await _context.Blogs.FindAsync(id);
                if (blog != null)
                {
                    blog.Title = model.Title;
                    blog.Thumbnail = model.Thumbnail;
                    blog.Content = model.Content;
                    blog.Status = model.Status;
                    blog.DateCreated = model.DateCreated;
                    blog.DateUpdated = model.DateUpdated;
                    blog.AuthorId = model.AuthorId;
                    blog.CategoryId = model.CategoryId;
                    _context.Blogs.Update(blog);
                    await _context.SaveChangesAsync();
                } else
                {
                    throw new AppException("Blog with Id {0} not found!", id);
                }
            } else
            {
                throw new AppException("Id doesn't match!");
            }
        }
    }
}
