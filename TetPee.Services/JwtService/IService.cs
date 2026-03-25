using System.Security.Claims;

namespace TetPee.Services.JwtService;

public interface IService
{
    public string GenerateAccessToken(IEnumerable<Claim> claims);
    
    ClaimsPrincipal ValidateToken(string token); 

}