using System.ComponentModel.DataAnnotations;

namespace CommentsManagement.Models
{
    /// <summary>
    /// DTO: Comment
    /// </summary>
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string? Text { get; set; }
        public DateTime CommentCreated { get; set; }
        public DateTime? CommentUpdated { get; set; }
    }
}
