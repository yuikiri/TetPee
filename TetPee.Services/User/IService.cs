namespace TetPee.Services.User;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetUserResponse>> GetUsers(string? searchTerm, int pageSize = 10, int pageIndex = 1);
    public Task<Response.GetUserResponse> GetUserById(Guid id);
}