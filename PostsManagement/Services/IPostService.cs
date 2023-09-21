using PostsManagement.DTOs;
using PostsManagement.Models;

namespace PostsManagement.Services
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetPostsAsync();
        Task<Post> GetPostByIdAsync(Guid id);
        Task<Post> CreatePostAsync(PostDTO postDto);
        Task<Post> UpdatePostAsync(Guid id, PostDTO post);
        Task<bool> DeletePostAsync(Guid id);
    }
}
