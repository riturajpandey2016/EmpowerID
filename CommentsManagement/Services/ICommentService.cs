using CommentsManagement.DTOs;
using CommentsManagement.Models;

namespace CommentsManagement.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId);
        Task<Comment> GetCommentByIdAsync(Guid id);
        Task<Comment> CreateCommentAsync(CommentDTO commentDto);
        Task<Comment> UpdateCommentAsync(Guid id, CommentDTO commentDto);
        Task<bool> DeleteCommentAsync(Guid id);
    }
}
