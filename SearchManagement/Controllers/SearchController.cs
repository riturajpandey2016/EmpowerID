using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SearchManagement.Models;
using SearchManagement.Services;

#pragma warning disable

namespace SearchManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly SearchService _searchService;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="searchService"></param>
        public SearchController(SearchService searchService)
        {
            _searchService = searchService;
        }

        /// <summary>
        /// TODO: This method implements full-text search 
        /// </summary>
        /// <param name="searchText">Takes searchText as parameter and search the result</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> Search([FromQuery] string searchText)
        {
            var results = await _searchService.SearchAsync(searchText);
            return Ok(results);
        }

        /// <summary>
        /// TODO: This method fetches data using the endpoint and Index the document for searching posts
        /// </summary>
        /// <param name="token">token - Generate this token from UserManagement MicroService</param>
        /// <returns></returns>
        [HttpPost("index")]
        public async Task<IActionResult> IndexPosts(string token)
        {
            try
            {
                var postData = await _searchService.FetchPostsFromEndpointAsync(token);
                var posts = JsonConvert.DeserializeObject<List<PostDto>>(postData);
                await _searchService.IndexPostsAsync(posts);

                return Ok("Data indexed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to index data: {ex.Message}");
            }
        }

    }
}
