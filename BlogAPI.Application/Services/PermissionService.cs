using AutoMapper;
using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.DTOs.Permission;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IMapper _mapper;
        private readonly IMenuRepository _menuRepository;
        private readonly IPermissionRepository _permissionRepository;

        public PermissionService(IMapper mapper, IMenuRepository menuRepository, IPermissionRepository permissionRepository)
        {
            _mapper = mapper;
            _menuRepository = menuRepository;
            _permissionRepository = permissionRepository;
        }

        public async Task<BaseResponse<bool>> CreatePermission(PermissionRequestDto permissionRequestDto)
        {
            try
            {
                var menu = await _menuRepository.GetMenuByIdAsync(permissionRequestDto.MenuId);
                var lstPermission = await _permissionRepository.GetLstPermissionsByMenuIdAsync(permissionRequestDto.MenuId);

                if (menu == null) return ("Menu không tồn tại").ToErrorResponse<bool>();

                switch (permissionRequestDto.Name)
                {
                    case "View":
                        permissionRequestDto.Code = $"{menu.Code}.View";
                        break;
                    case "Add":
                        permissionRequestDto.Code = $"{menu.Code}.Add";
                        break;
                    case "Update":
                        permissionRequestDto.Code = $"{menu.Code}.Update";
                        break;
                    case "Delete":
                        permissionRequestDto.Code = $"{menu.Code}.Delete";
                        break;
                    default:
                        permissionRequestDto.Code = "";
                        break;
                }

                if (string.IsNullOrEmpty(permissionRequestDto.Code))
                {
                    return ("Có lỗi xảy ra!").ToErrorResponse<bool>();
                }

                if (lstPermission.Any())
                {
                    var lstCheck = lstPermission.Select(x => x.Code).ToList();
                    if (lstCheck.Contains(permissionRequestDto.Code)) return ($"{permissionRequestDto.Code} đã tồn tại!").ToErrorResponse<bool>();
                }

                var payload = _mapper.Map<Permission>(permissionRequestDto);

                await _permissionRepository.CreatePermissionAsync(payload);

                return true.ToResponse("Tạo permission thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lỗi {ex.Message}").ToErrorResponse<bool>();
            }
        }

        public async Task<BaseResponse<bool>> DeletePermission(int permissionId)
        {
            try
            {
                var existingPermission = await _permissionRepository.GetPermissionByIdAsync(permissionId);

                if (existingPermission == null)
                {
                    return ("Permission không tồn tại!").ToErrorResponse<bool>();
                }

                await _permissionRepository.DeletePermissionAsync(permissionId);
                return true.ToResponse("Xóa permission thành công!");
            }
            catch (Exception ex)
            {
                return ($"Lỗi {ex.Message}").ToErrorResponse<bool>();
            }
        }

        public Task<BaseResponse<bool>> UpdatePermission(int permissionId, PermissionRequestDto permissionRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
