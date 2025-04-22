using BlogAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Application.DTOs.Menu
{
    public class MenuRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string? Icon { get; set; }
        public int SortOrder { get; set; }
        public bool IsShow { get; set; } = true;
        public string Code { get; set; } = string.Empty;
    }
}
