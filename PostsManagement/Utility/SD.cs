#pragma warning disable

namespace PostsManagement.Utility
{
    /// <summary>
    /// DTO: specifying ApiType and URL
    /// </summary>
    public static class SD
    {
        public enum ApiType
        { 
           GET,
           POST,
           PUT,
           DELETE
        }
        public static string AuthAPIBase { get; set; }
    }
}
