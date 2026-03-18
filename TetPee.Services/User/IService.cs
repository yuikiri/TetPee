using TetPee.Services.User;
using TetPee.Services.User;

namespace TetPee.Services.User;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetUsersResponse>> GetUsers(
        string? searchTerm,
        int pageSize,
        int pageIndex);
    
    public Task<Response.GetUsersResponse?> GetUserById(Guid id);
}