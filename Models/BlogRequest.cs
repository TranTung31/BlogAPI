using BlogAPI.Data;

namespace BlogAPI.Models
{
    public class BlogRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Thumbnail { get; set; }
        public string Content { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int? AuthorId { get; set; }
        public int? CategoryId { get; set; }
    }
}
