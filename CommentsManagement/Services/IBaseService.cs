
using CommentsManagement.DTOs;
using CommentsManagement.Models;

namespace CommentsManagement.Services
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
