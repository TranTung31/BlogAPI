using AutoMapper;
using BlogAPI.Application.Common.Responses;
using BlogAPI.Application.Interfaces.Repositories;
using BlogAPI.Application.Interfaces.Services;
using BlogAPI.Core.Entities;

namespace BlogAPI.Application.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IMapper _mapper;
        private readonly IRolePermissionRepository _rolePermissionRepository;

        public RolePermissionService(IMapper mapper, IRolePermissionRepository rolePermissionRepository)
        {
            _mapper = mapper;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public async Task<BaseResponse<bool>> AssignRolePermission(int roleId, List<int> permissionIds)
        {
            try
            {
                var lstRolePermission = await _rolePermissionRepository.GetLstRolePermissionAsync(roleId);
                var lstRolePermissionToRemove = lstRolePermission
                    .Where(x => !permissionIds.Contains(x.PermissionId))
                    .ToList();

                if (lstRolePermissionToRemove.Any())
                {
                    // Xóa role permission không có
                    await _rolePermissionRepository.DeleteManyRolePermissionAsync(lstRolePermissionToRemove);
                }

                var existingLstRolePermission = lstRolePermission.Select(x => x.PermissionId).ToList();
                var lstRolePermissionToAdd = permissionIds.Where(x => !existingLstRolePermission.Contains(x)).Select(x => new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = x
                    }).ToList();

                if (lstRolePermissionToAdd.Any())
                {
                    // Thêm role permission
                    await _rolePermissionRepository.CreateManyRolePermissionAsync(lstRolePermissionToAdd);
                }

                return true.ToResponse("Gán permission thành công!");
            }
            catch (Exception ex)
            {
                return ($"Gán permission thất bại! (Lỗi {ex.Message})").ToErrorResponse<bool>();
            }
        }
    }
}
