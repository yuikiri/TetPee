using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TetPee.Api.Extention;
using TetPee.Repositories;
using TetPee.Services.Models;
using TetPee.Services.Product;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IService _productService;
    public ProductController(AppDbContext dbContext, IService userService)
    {
        _productService = userService;
    }
    
    [Authorize(Policy = JwtExtentions.SellerPolicy)]
    [HttpPost("")]
    public async Task<IActionResult> CreateProduct(Request.CreateProductRequest request)
    {
        var result = await _productService.CreateProduct(request);
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(result, "Product created", HttpContext.TraceIdentifier));
    }
}