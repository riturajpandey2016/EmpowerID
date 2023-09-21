using CommentsManagement.DTOs;
using CommentsManagement.Models;
using CommentsManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

#pragma warning disable

namespace CommentsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>
        /// Constructor for the CommentsController.
        /// </summary>
        /// <param name="commentService">The service responsible for managing comments.</param>
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Retrieves comments for a specific post asynchronously.
        /// </summary>
        /// <param name="postId">The ID of the post for which to retrieve comments.</param>
        /// <returns>An enumerable collection of comments for the specified post.</returns>
        [HttpGet("{postId}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsAsync(Guid postId)
        {
            var comments = await _commentService.GetCommentsAsync(postId);
            return Ok(comments);
        }

        /// <summary>
        /// Retrieves a comment by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to retrieve.</param>
        /// <returns>The comment with the specified ID, or null if not found.</returns>
        [HttpGet("get-comment-by-ID/{id}")]
        public async Task<ActionResult<Comment>> GetCommentByIdAsync(Guid id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound();

            return Ok(comment);
        }

        /// <summary>
        /// Creates a new comment asynchronously.
        /// </summary>
        /// <param name="commentDto">The DTO containing comment data.</param>
        /// <returns>The created comment entity.</returns>
        [HttpPost("create-comment")]
        public async Task<ActionResult<Comment>> CreateCommentAsync(CommentDTO commentDto)
        {
            var createdComment = await _commentService.CreateCommentAsync(commentDto);
            return Ok(new { data = createdComment, message = "Created Entity Successfully." });
        }

        /// <summary>
        /// Updates an existing comment asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to update.</param>
        /// <param name="commentDto">The DTO containing updated comment data.</param>
        /// <returns>The updated comment entity, or null if the comment does not exist.</returns>
        [HttpPut("update-comment/{id}")]
        public async Task<IActionResult> UpdateCommentAsync(Guid id, CommentDTO commentDto)
        {
            if (id != id)
                return BadRequest();

            var updatedComment = await _commentService.UpdateCommentAsync(id, commentDto);
            if (updatedComment == null)
                return NotFound();

            return Ok("Updated Entity Successfully.");
        }

        /// <summary>
        /// Deletes a comment asynchronously.
        /// </summary>
        /// <param name="id">The ID of the comment to delete.</param>
        /// <returns>True if the comment was successfully deleted, false if not found.</returns>
        [HttpDelete("delete-comment/{id}")]
        public async Task<IActionResult> DeleteCommentAsync(Guid id)
        {
            var deleted = await _commentService.DeleteCommentAsync(id);
            if (!deleted)
                return NotFound();

            return Ok("Deleted Entity Successfully.");
        }
    }
}
