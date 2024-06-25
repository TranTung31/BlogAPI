using BlogAPI.Data;

namespace BlogAPI.Models
{
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Thumbnail { get; set; }
        public string Content { get; set; }
        public Status Status { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public string AuthorName { get; set; }
        public string CategoryName { get; set; }
    }
}
