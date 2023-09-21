using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using SearchManagement.Models;
using System.Net.Http.Headers;

#pragma warning disable

namespace SearchManagement.Services
{
    /// <summary>
    /// This Service implements the search related methods.
    /// </summary>
    public class SearchService
    {
        private readonly SearchClient _searchClient;
        private readonly HttpClient _httpClient;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="httpClientFactory"></param>
        public SearchService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            // Initialize the Search Service with configuration settings.
            var searchConfig = configuration.GetSection("AzureSearch");
            string serviceName = searchConfig["ServiceName"];
            string indexName = searchConfig["IndexName"];
            string apiKey = searchConfig["AdminApiKey"];

            var searchUri = new Uri($"https://{serviceName}.search.windows.net");
            _searchClient = new SearchClient(searchUri, indexName, new AzureKeyCredential(apiKey));
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        /// <summary>
        /// TODO: Search for posts matching a given search text.
        /// </summary>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public async Task<IEnumerable<PostDto>> SearchAsync(string searchText)
        {
            var searchOptions = new SearchOptions
            {
                IncludeTotalCount = true,
                OrderBy = { "Title" }
            };

            var response = await _searchClient.SearchAsync<PostDto>(searchText, searchOptions);

            if (response.Value != null)
            {
                return response.Value.GetResults().Select(result => result.Document);
            }
            else
            {
                return Enumerable.Empty<PostDto>();
            }
        }

        /// <summary>
        /// TODO: Fetch posts from an external endpoint using a bearer token for authorization.
        /// </summary>
        /// <param name="bearerToken"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<string> FetchPostsFromEndpointAsync(string bearerToken)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            var apiUrl = _configuration["PostServiceApiUrl"];
            var response = await httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Failed to fetch data from the endpoint. Status code: {response.StatusCode}");
            }
        }

        /// <summary>
        /// TODO: Index a collection of PostDto objects in Azure Cognitive Search.
        /// </summary>
        /// <param name="dataToIndex"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task IndexPostsAsync(IEnumerable<PostDto> dataToIndex)
        {
            try
            {
                foreach (var item in dataToIndex)
                {
                    var batch = IndexDocumentsBatch.Create(IndexDocumentsAction.Upload(item));
                    await _searchClient.IndexDocumentsAsync(batch);
                }
            }
            catch (RequestFailedException ex)
            {
                throw new Exception($"Failed to index documents: {ex.Message}", ex);
            }
        }
    }
}
