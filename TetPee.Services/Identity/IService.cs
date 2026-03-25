namespace TetPee.Services.Identity;

public interface IService
{
    public Task<Response.IdentityResponse> Login(string email, string password);
}