using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TetPee.Api.Extention;
using TetPee.Services.Cart;
using TetPee.Services.Models;

namespace TetPee.Api.Controllers;

[Authorize(Policy = JwtExtentions.UserPolicy)]
[ApiController]
[Route("api/[controller]")]
public class CartController : ControllerBase
{
    public readonly IService _cartService;
    public CartController(IService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCart()
    {
        await _cartService.CreateCart();
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(null, "Product created", HttpContext.TraceIdentifier));
    }
    
    [HttpPost("Product")]
    public async Task<IActionResult> AddProductToCart(Request.AddProductToCartRequest request)
    {
        await _cartService.AddProductToCart(request);
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(null, "Product created", HttpContext.TraceIdentifier));
    }
    
    [HttpDelete("delete product")]
    public async Task<IActionResult> DeleteProduct(Request.RemoveProductFromCartRequest request)
    {
        await _cartService.RemoveProductFromCart(request);
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(null, "Product deleted", HttpContext.TraceIdentifier));
    }
    
    [HttpGet("get cart items")]
    public async Task<IActionResult> GetCart()
    {
        var result = await _cartService.GetCart();
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(result, "Product list", HttpContext.TraceIdentifier));
    }
}