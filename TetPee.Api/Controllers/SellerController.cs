using Microsoft.AspNetCore.Mvc;
using TetPee.Services.Seller;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SellerController: ControllerBase
{
    private readonly IService _sellerService;

    public SellerController(IService sellerService)
    {
        _sellerService = sellerService;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetSellers(int pageSize = 10, int pageIndex = 1, string? searchTerm = null)
    {
        var result = await _sellerService.GetSellers(searchTerm, pageSize, pageIndex);
        return Ok(result);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetSellerById(Guid id)
    {
        var result = await _sellerService.GetSellerById(id);
        return Ok(result);
    }
    
    [HttpPost("")]
    public async Task<IActionResult> CreateSeller(Request.CreateSellerRequest request)
    {
        var result = await _sellerService.CreateSeller(request);
        return Ok(result);
    }
}