using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.DTOs.Permission
{
    public class PermissionRequestDto
    {
        public string Name { get; set; } = string.Empty;
        // View, Add, Update, Delete
        public string Code { get; set; } = string.Empty;
        public int MenuId { get; set; }
    }
}
