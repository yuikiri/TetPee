using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Quartz;
using TetPee.Api.Extention;
using TetPee.Api.Middlewares;
using TetPee.Repositories;

using UserService = TetPee.Services.User;
using CategoryService = TetPee.Services.Category;
using SellerService = TetPee.Services.Seller;
using IdentityService = TetPee.Services.Identity;
using JwtService = TetPee.Services.JwtService;
using ProductService = TetPee.Services.Product;
using MediaService = TetPee.Services.MediaService;
using CloudinaryService = TetPee.Services.CloudinaryService;
using MailService = TetPee.Services.MailService;
using CartService = TetPee.Services.Cart;
using OrderService = TetPee.Services.Order;
using BackgroundJobService = TetPee.Services.BackgroundJobService;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();   

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddJwtServices(builder.Configuration);
builder.Services.AddSwaggerServices();

builder.Services.AddScoped<UserService.IService, UserService.Service>();
builder.Services.AddScoped<CategoryService.IService, CategoryService.Service>();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();
builder.Services.AddScoped<JwtService.IService, JwtService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<ProductService.IService, ProductService.Service>();
builder.Services.AddScoped<MediaService.IService, CloudinaryService.Service>();
builder.Services.AddScoped<MailService.IService, MailService.Service>();
builder.Services.AddScoped<CartService.IService, CartService.Service>();
builder.Services.AddScoped<OrderService.IService, OrderService.Service>();

builder.Services.AddQuartz(options =>
{
    var jobKey = new JobKey(nameof(BackgroundJobService.ProcessTransactionPendingJob));

    options
        .AddJob<BackgroundJobService.ProcessTransactionPendingJob>(jobKey)
        .AddTrigger(trigger =>
            trigger
                .ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInMinutes(2)
                    //thời gian lặp lại
                    .RepeatForever()
                    //lặp lại qài lun
                )
        );
});
builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
    //làm cái job thứ 1 xong thì mới lặp cái job 2
});

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAPI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();