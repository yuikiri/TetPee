using Microsoft.EntityFrameworkCore;
using TetPee.Repositories;

namespace TetPee.Services.User;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Base.Response.PageResult<Response.GetUserResponse>> GetUssers(string? searchTerm, int pageSize, int pageIndex)
    {
        var query = _dbContext.Users.Where(x => true);
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm) || 
                x.LastName.Contains(searchTerm) || 
                x.Email.Contains(searchTerm));
        }

        query  = query.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        
        var selectedQuery = query.Select(x => new Response.GetUserResponse()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Role = x.Role,
            ImageUrl = x.ImageUrl,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
        });

        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetUserResponse>()
        {
            Item = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };

        return result;
    }

    public async Task<Base.Response.PageResult<Response.GetUserResponse>> GetUsers(string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var query = _dbContext.Users.Where(x => true);
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm) || 
                x.LastName.Contains(searchTerm) || 
                x.Email.Contains(searchTerm));
        }

        query  = query.Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        
        var selectedQuery = query.Select(x => new Response.GetUserResponse()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Role = x.Role,
            ImageUrl = x.ImageUrl,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
        });
        
        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetUserResponse>()
        {
            Item = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        
        return result;
    }
    

    public async Task<Response.GetUserResponse?> GetUserById(Guid id)
    {
        var query = _dbContext.Users.Where(x => x.Id == id);

        var selectedQuery = query.Select(x => new Response.GetUserResponse()
        {
            Id = x.Id,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Role = x.Role,
            ImageUrl = x.ImageUrl,
            PhoneNumber = x.PhoneNumber,
            Address = x.Address,
        });
        var result = await selectedQuery.FirstOrDefaultAsync();
        return result;
    }
}