using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TetPee.Repositories;
using TetPee.Repositories.Entity;

namespace TetPee.Services.Product;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    public readonly IHttpContextAccessor _httpContext;

    public Service(AppDbContext dbContext,  IHttpContextAccessor httpContext)
    {
        _dbContext = dbContext;
        _httpContext = httpContext;
    }
    public async Task<string> CreateProduct(Request.CreateProductRequest request)
    {
        var sellerId = _httpContext.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "SellerId")?.Value;
        var sellerIdGuid = Guid.Parse(sellerId!);
        var existingProduct = _dbContext.Products
            .Where(x => x.Name.ToLower().Trim() == request.Name.ToLower().Trim());

        bool isExistProduct = await existingProduct.AnyAsync();
        if (isExistProduct)
        {
            throw new Exception("Product already exists");
        }
            
        var existingSeller = _dbContext.Sellers.Where(x => x.Id == sellerIdGuid);
        
        bool isExistSeller = await existingSeller.AnyAsync();
        
        if (!isExistSeller)
        {
            throw new Exception("Sellser not already exists");
        }

        var product = new Repositories.Entity.Product()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            SellerId =  sellerIdGuid
        };
        
        _dbContext.Add(product);
        var sellerResult = await _dbContext.SaveChangesAsync();
        if(request.CategoryIds != null && request.CategoryIds.Count > 0)
        {
            var productCateList = request.CategoryIds.Select(id => new ProductCategory()
            {
                CategoryId = id,
                ProductId = product.Id
            });
            
            // Same with above but using foreach loop
            // var productCateList1 = new List<ProductCategory>();
            // foreach (var id in request.CategoryIds)
            // {
            //     var productCate = new ProductCategory()
            //     {
            //         CategoryId = id,
            //         ProductId = product.Id
            //     };
            //     productCateList1.Add(productCate);
            // }
            
            _dbContext.AddRange(productCateList);
            await _dbContext.SaveChangesAsync();
        }

        if (sellerResult > 0) return "Add Product successfully";
            
        return "Add Product failed";
    
    }
}