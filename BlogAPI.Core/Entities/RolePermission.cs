using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Core.Entities
{
    public class RolePermission
    {
        public int RoleId { get; set; }
        public AspNetRole? Role { get; set; }
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
