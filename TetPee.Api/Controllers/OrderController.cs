using Microsoft.AspNetCore.Mvc;
using TetPee.Services.Models;
using TetPee.Services.Order;

namespace TetPee.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IService _orderService;
    public OrderController(IService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpPost("create order")]
    public async Task<IActionResult> CreateOrder(Request.CreateOrderRequest request)
    {
        var result = await _orderService.CreateOrder(request);
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(result, "Order created", HttpContext.TraceIdentifier));
    }
    
    [HttpPost("sepay/webhook")]
    public async Task<IActionResult> SepayWebhook(Request.SepayWebhookRequest request)
    {
        await _orderService.SepayWebhookHandler(request);
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse("", "Webhook response", HttpContext.TraceIdentifier));
    }
    
    [HttpGet("History")]
    public async Task<IActionResult> GetOrder()
    {
        var result = await _orderService.GetOrder();
        return Ok(ApiResponse.ApiResponseFactory.SuccessResponse(result,"Webhook response", HttpContext.TraceIdentifier));
    }
}