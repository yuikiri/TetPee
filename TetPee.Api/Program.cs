using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TetPee.Api.Extention;
using TetPee.Api.Middlewares;
using TetPee.Repositories;
using TetPee.Repository;
using TetPee.Services.JwtService;
using UserService = TetPee.Services.User;
using CategoryService = TetPee.Services.Category;
using SellerService = TetPee.Services.Seller;
using IdentityService = TetPee.Services.Identity;
using JwtService = TetPee.Services.JwtService;
using ProductService = TetPee.Services.Product;
using MediaService = TetPee.Services.MediaService;
using CloudinaryService = TetPee.Services.CloudinaryService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerServices();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<UserService.IService, UserService.Service>();
builder.Services.AddScoped<CategoryService.IService, CategoryService.Service>();
builder.Services.AddScoped<SellerService.IService, SellerService.Service>();
builder.Services.AddScoped<IdentityService.IService, IdentityService.Service>();
builder.Services.AddScoped<ProductService.IService, ProductService.Service>();
builder.Services.AddScoped<MediaService.IService, CloudinaryService.Service>();

builder.Services.AddScoped<JwtService.IService, JwtService.Service>();

builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerAPI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();