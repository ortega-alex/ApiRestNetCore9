using FluentValidation;
using FluentValidation.AspNetCore;
using GestionProductos.DataAccess;
using GestionProductos.Interfaces;
using GestionProductos.Mapping;
using GestionProductos.Middlewares;
using GestionProductos.Repositories;
using GestionProductos.Services;
using GestionProductos.Validators.Product;
using GestionProductos.Validators.User;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) =>
    loggerConfig.ReadFrom.Configuration(context.Configuration));

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddJwtTokenService(builder.Configuration);

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddAutoMapper(typeof(ProductProfile));
builder.Services.AddAutoMapper(typeof(UserProfile));

builder.Services.AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProductDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RefreshTokenDtoValidator>();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
} 

app.UseAuthentication();
app.UseAuthorization();
// middleware to handle JWT token validation
// app.UseMiddleware<JwtMiddleware>();
app.UseMiddleware<ExceptionHandLingMiddleware>();

app.MapControllers();

app.Run();
