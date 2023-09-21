using PostsManagement.DTOs;
using PostsManagement.Models;

namespace PostsManagement.Services
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
