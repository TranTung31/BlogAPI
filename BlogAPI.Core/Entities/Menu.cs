using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Core.Entities
{
    public class Menu
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Path { get; set; }

        public int? ParentId { get; set; }
        public Menu? Parent { get; set; }

        public string? Icon { get; set; }
        public int SortOrder { get; set; }
        public bool IsShow { get; set; } = true;

        [Required]
        public required string Code { get; set; }

        public ICollection<Menu> Children { get; set; } = new List<Menu>();

        public ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
