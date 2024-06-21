using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogAPI.Data
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string? Thumbnail { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public Status Status { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateUpdated { get; set; }

        public int? AuthorId { get; set; }

        [ForeignKey(nameof(AuthorId))]
        public Author Author { get; set; }

        public int? CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
    }
}
