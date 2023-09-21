using CommentsManagement.DTOs;
using CommentsManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

#pragma warning disable

namespace CommentsManagement.Services
{
    public class CommentService : ICommentService
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbCommentContext _context;

        public CommentService(ApplicationDbCommentContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;
        }

        /// <summary>
        /// Get comments for a specific post asynchronously.
        /// </summary>
        /// <param name="postId">The unique identifier of the post.</param>
        /// <returns>The comments associated with the post.</returns>
        public async Task<IEnumerable<Comment>> GetCommentsAsync(Guid postId)
        {
            // Generate a cache key based on the postId
            var cacheKey = $"CommentsCacheKey:{postId}";

            // Try to get data from cache
            var cachedData = await _cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                // Data found in cache; deserialize and return it
                var comments = JsonSerializer.Deserialize<IEnumerable<Comment>>(cachedData);
                return comments;
            }

            // Data not found in cache; fetch from repository
            var commentsFromDB = await _context.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();

            // Cache the fetched data
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Set cache expiration time
            };
            await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(commentsFromDB), cacheOptions);

            return commentsFromDB;
        }

        /// <summary>
        /// Get a comment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the comment.</param>
        /// <returns>The comment with the specified ID.</returns>
        public async Task<Comment> GetCommentByIdAsync(Guid id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        }

        /// <summary>
        /// Create a new comment asynchronously.
        /// </summary>
        /// <param name="commentDto">The DTO containing comment information.</param>
        /// <returns>The newly created comment.</returns>
        public async Task<Comment> CreateCommentAsync(CommentDTO commentDto)
        {
            Comment comment = new()
            {
                Id = Guid.NewGuid(),
                PostId = commentDto.PostId,
                Text = commentDto.Text,
                CommentCreated = DateTime.UtcNow,
                CommentUpdated = null // Initially, no update
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();
            var cacheKey = $"CommentsCacheKey:{commentDto.PostId}";
            await _cache.RemoveAsync(cacheKey);
            return comment;
        }

        /// <summary>
        /// Update an existing comment asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the comment to update.</param>
        /// <param name="commentDto">The DTO containing updated comment information.</param>
        /// <returns>The updated comment.</returns>
        public async Task<Comment> UpdateCommentAsync(Guid id, CommentDTO commentDto)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (existingComment == null)
                return null;

            existingComment.Text = commentDto.Text;
            existingComment.CommentUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingComment;
        }

        /// <summary>
        /// Delete a comment by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the comment to delete.</param>
        /// <returns>True if the comment was deleted successfully; otherwise, false.</returns>
        public async Task<bool> DeleteCommentAsync(Guid id)
        {
            var existingComment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
            if (existingComment == null)
                return false;

            _context.Comments.Remove(existingComment);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
