using AutoMapper;
using BlogAPI.Application.DTOs.Menu;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Mappings
{
    public class MenuProfile : Profile
    {
        public MenuProfile()
        {
            CreateMap<Menu, MenuRequestDto>().ReverseMap();
            CreateMap<Menu, MenuResponseDto>().ReverseMap();
        }
    }
}
