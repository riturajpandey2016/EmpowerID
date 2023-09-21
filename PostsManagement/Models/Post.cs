using System.ComponentModel.DataAnnotations;

#pragma warning disable

namespace PostsManagement.Models
{
    /// <summary>
    /// DTO: Post
    /// </summary>
    public class Post
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime PostCreated { get; set; }
        public DateTime? PostUpdated { get; set; }
    }
}
