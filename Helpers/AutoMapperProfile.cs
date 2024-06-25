using AutoMapper;
using BlogAPI.Data;
using BlogAPI.Models;

namespace BlogAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
            CreateMap<Author, AuthorModel>().ReverseMap();
            CreateMap<Blog, BlogModel>().ReverseMap();
            CreateMap<Blog, BlogRequest>().ReverseMap();
        }
    }
}
