using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using PostsManagement.DTOs;
using PostsManagement.Models;
using System.Text.Json;

#pragma warning disable

namespace PostsManagement.Services
{
    public class PostService : IPostService
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbPostContext _context;

        /// <summary>
        /// Constructor for the PostService.
        /// </summary>
        /// <param name="cache">The distributed cache service used for caching.</param>
        /// <param name="context">The database context for working with posts.</param>
        public PostService(IDistributedCache cache, ApplicationDbPostContext context)
        {
            _cache = cache;
            _context = context;
        }

        /// <summary>
        /// Gets a list of posts asynchronously, either from cache or the database.
        /// </summary>
        /// <returns>An enumerable collection of posts.</returns>
        public async Task<IEnumerable<Post>> GetPostsAsync()
        {
            // Get data from cache
            var cachedData = await _cache.GetStringAsync("PostsCacheKey");
            if (cachedData != null)
            {
                // Data found in cache - deserialize and return it
                var posts = JsonSerializer.Deserialize<IEnumerable<Post>>(cachedData);
                return posts;
            }
            // Data not found in cache - fetch from database
            var postsFromRepository = await _context.Posts.ToListAsync();

            // Cache the fetched data
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Set cache expiration time
            };
            await _cache.SetStringAsync("PostsCacheKey", JsonSerializer.Serialize(postsFromRepository), cacheOptions);

            return postsFromRepository;
        }

        /// <summary>
        /// Gets a post by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>The post with the specified ID, or null if not found.</returns>
        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// Creates a new post asynchronously.
        /// </summary>
        /// <param name="postDto">The DTO containing post data.</param>
        /// <returns>The created post entity.</returns>
        public async Task<Post> CreatePostAsync(PostDTO postDto)
        {
            if (postDto is null)
            {
                throw new ArgumentNullException(nameof(postDto));
            } 
            var isDataDuplicated = _context.Posts.FirstOrDefault(x => x.Title == postDto.Title || x.Content == postDto.Content);

            if (isDataDuplicated != null)
            {
                throw new InvalidOperationException("Duplicated Data");
            }
            Post post = new()
            {
                Id = Guid.NewGuid(),
                Title = postDto.Title,
                Content = postDto.Content,
                PostCreated = DateTime.UtcNow,
                PostUpdated = DateTime.UtcNow
            };
            _context.Posts.Add(post);
            await _context.SaveChangesAsync();
            await _cache.RemoveAsync("PostsCacheKey");
            return post;
        }

        /// <summary>
        /// Updates an existing post asynchronously.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="postDto">The DTO containing updated post data.</param>
        /// <returns>The updated post entity, or null if the post does not exist.</returns>
        public async Task<Post> UpdatePostAsync(Guid id, PostDTO postDto)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPost == null)
                return null;

            existingPost.Title = postDto.Title;
            existingPost.Content = postDto.Content;
            existingPost.PostUpdated = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return existingPost;
        }

        /// <summary>
        /// Deletes a post asynchronously.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>True if the post was successfully deleted, false if not found.</returns>
        public async Task<bool> DeletePostAsync(Guid id)
        {
            var existingPost = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (existingPost == null)
                return false;

            _context.Posts.Remove(existingPost);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
