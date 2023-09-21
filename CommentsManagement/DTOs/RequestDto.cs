using static CommentsManagement.Utility.SD;

#pragma warning disable

namespace CommentsManagement.DTOs
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
