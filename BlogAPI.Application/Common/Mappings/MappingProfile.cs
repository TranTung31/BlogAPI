using AutoMapper;
using BlogAPI.Application.DTOs.Authentication;
using BlogAPI.Application.DTOs.User;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AspNetUser, UserDto>().ReverseMap();
        }
    }
}
