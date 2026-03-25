using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TetPee.Repositories;
using TetPee.Repository;
using TetPee.Services.JwtService;

namespace TetPee.Services.Identity;

public class Service : IService
{
    public readonly AppDbContext _dbContext;
    public readonly JwtService.IService _jwtService;
    public readonly JwtOptions _jwtOption = new();
    
    public Service(AppDbContext _dbContext, JwtService.IService _jwtService, IConfiguration configuration)
    {
        this._dbContext = _dbContext;
        this._jwtService = _jwtService;
        configuration.GetSection(nameof(JwtOptions)).Bind(_jwtOption);
    }
    
    public async Task<Response.IdentityResponse> Login(string email, string password)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.HashedPassword != password)
        {
            throw new Exception("Wrong password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim(ClaimTypes.Expired, DateTimeOffset.UtcNow.AddMinutes(_jwtOption.ExpireMinutes).ToString())
        };
        
        var token = _jwtService.GenerateAccessToken(claims);
        var resutl = new Response.IdentityResponse()
        {
            AccessToken = token,
        };
         return resutl;
    }
}