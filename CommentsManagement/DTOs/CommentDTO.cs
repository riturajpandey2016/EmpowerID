namespace CommentsManagement.DTOs
{
    /// <summary>
    /// DTO: CommentDTO
    /// </summary>
    public class CommentDTO
    {
        public Guid PostId { get; set; }
        public string? Text { get; set; }
    }
}
