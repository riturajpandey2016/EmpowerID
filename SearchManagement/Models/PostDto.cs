#pragma warning disable

namespace SearchManagement.Models
{
    /// <summary>
    /// DTO: This data transfer object returns post as response 
    /// </summary>
    public class PostDto
    {
        public string Id { get; set; }  
        public string Title { get; set; }
        public string? Content { get; set; }
        public DateTime PostCreated { get; set; }
        public DateTime? PostUpdated { get; set; }
    }
}
