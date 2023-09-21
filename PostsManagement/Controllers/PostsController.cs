using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostsManagement.DTOs;
using PostsManagement.Models;
using PostsManagement.Services;

#pragma warning disable

namespace PostsManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        /// <summary>
        /// Constructor for the PostsController.
        /// </summary>
        /// <param name="postService">The post service for managing posts.</param>
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        /// <summary>
        /// Retrieves all posts.
        /// </summary>
        /// <returns>HTTP response containing a list of posts.</returns>
        [HttpGet("get-posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetPostsAsync()
        {
            var posts = await _postService.GetPostsAsync();
            return Ok(posts);
        }

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>HTTP response containing the requested post or a not found response.</returns>
        [HttpGet("get-post-by-ID/{id}")]
        public async Task<ActionResult<Post>> GetPostByIdAsync(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
                return NotFound();

            return Ok(post);
        }

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="postDto">The DTO containing post data to create.</param>
        /// <returns>HTTP response containing the created post or an error message.</returns>
        [HttpPost("create-post")]
        public async Task<ActionResult<Post>> CreatePostAsync(PostDTO postDto)
        {
            var createdPost = await _postService.CreatePostAsync(postDto);
            return Ok(new { data = createdPost, message = "Created Entity Successfully." });
        }

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="postDto">The DTO containing updated post data.</param>
        /// <returns>HTTP response indicating success or failure of the update operation.</returns>
        [HttpPut("update-post/{id}")]
        public async Task<IActionResult> UpdatePostAsync(Guid id, PostDTO postDto)
        {
            if (id != id)
            {
                return BadRequest();
            }

            var updatedPost = await _postService.UpdatePostAsync(id, postDto);
            if (updatedPost == null)
                return NotFound();

            return Ok("Updated Entity Successfully.");
        }

        /// <summary>
        /// Deletes a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>HTTP response indicating success or failure of the delete operation.</returns>
        [HttpDelete("delete-post/{id}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            var deleted = await _postService.DeletePostAsync(id);
            if (!deleted)
                return NotFound();

            return Ok("Deleted Entity Successfully.");
        }
    }
}
