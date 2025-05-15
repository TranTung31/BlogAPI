using AutoMapper;
using BlogAPI.Application.DTOs.Role;
using BlogAPI.Core.Entities;

namespace BlogAPI.Application.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<AspNetRole, RoleRequestDto>().ReverseMap();
            CreateMap<AspNetRole, RoleResponseDto>().ReverseMap();
        }
    }
}
