using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TetPee.Repositories;
using TetPee.Repositories.Entity;

namespace TetPee.Services.Cart;

public class Service : IService
{
    private AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext, IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }
    
    public async Task CreateCart()
    {
        var userId = _httpContext.HttpContext.User.Claims
            .FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userIdGuid = Guid.Parse(userId!);
        var query = _dbContext.Carts
            .Where(x => x.UserId == userIdGuid);
        var isExist = await query.AnyAsync();
        if (isExist)
        {
            throw new Exception("cart is exist");
        }
        
        var cart = new Repositories.Entity.Cart()
        {
            UserId = userIdGuid
        };
        _dbContext.Add(cart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task AddProductToCart(Request.AddProductToCartRequest request)
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userIdGuid = Guid.Parse(userId!);
        var query = _dbContext.Carts.Where(x => x.UserId == userIdGuid);
        var cart = await query.FirstOrDefaultAsync();
        if (cart == null)
        {
            cart = new Repositories.Entity.Cart()
            {
                Id = Guid.NewGuid(),
                UserId = userIdGuid,
            };
            _dbContext.Add(cart);
            await _dbContext.SaveChangesAsync();
        }
        
        var productQuery = _dbContext.CartDetails
            .Where(x => x.CartId == cart.Id && x.ProductId == request.ProductId);
        var cartExist = await productQuery.FirstOrDefaultAsync();
        if (cartExist != null)
        {
            cartExist.Quantity += request.Quantity;
            _dbContext.Update(cartExist);
            await _dbContext.SaveChangesAsync();
            return;
        }
        var cartDetail = new CartDetail()
        {
            CartId = cart.Id,
            ProductId = request.ProductId,
            Quantity = request.Quantity,
        };
        _dbContext.Add(cartDetail);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveProductFromCart(Request.RemoveProductFromCartRequest request)
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userIdGuid = Guid.Parse(userId!);
        var cart = await _dbContext.CartDetails.FirstOrDefaultAsync(x => x.Cart.UserId == userIdGuid && x.ProductId == request.ProductId);
        if (cart == null)
        {
            throw new Exception("cart is not exist");
        }
        _dbContext.Remove(cart);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Response.ProductResponse>> GetCart()
    {
        var userId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
        var userIdGuid = Guid.Parse(userId!);
        var query =  _dbContext.CartDetails.Where(x => x.Cart.UserId == userIdGuid)
            .Select(x => new Response.ProductResponse
            {
                Name = x.Product.Name,
                Price = x.Product.Price,
                Description = x.Product.Description,
                Url =  x.Product.UrlImage,
                Quantity =  x.Quantity,
            });
        var result = await query.ToListAsync();
        return result;
    }
}