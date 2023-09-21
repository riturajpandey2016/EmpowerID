using static PostsManagement.Utility.SD;

#pragma warning disable

namespace PostsManagement.DTOs
{
    /// <summary>
    /// DTO: RequestDto
    /// </summary>
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } 
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
