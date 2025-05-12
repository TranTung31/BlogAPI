using AutoMapper;
using BlogAPI.Application.DTOs.Permission;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Mappings
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<Permission, PermissionRequestDto>().ReverseMap();
            CreateMap<Permission, PermissionResponseDto>().ReverseMap();
        }
    }
}
