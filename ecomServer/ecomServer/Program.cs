using ecomServer.Data;
using ecomServer.Middleware;
using ecomServer.Repositories;
using ecomServer.Repositories.Contracts;
using ecomServer.Services;
using ecomServer.Services.Contracts;
using ecomServer.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Controllers
builder.Services.AddControllers(); // Add support for controllers to handle API requests


// ✅ DbContext 
builder.Services.AddDbContext<EcomDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ecomdb"))); // Configure Entity Framework to use SQL Server with the connection string from configuration

// Swagger
builder.Services.AddEndpointsApiExplorer(); // Enable API endpoint exploration for Swagger
builder.Services.AddSwaggerGen();          // Enable Swagger generation for API documentation

// CORS 
builder.Services.AddCors(options =>
{
   options.AddDefaultPolicy(policy => // Allow any origin, header, and method for CORS
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// JWT
var jwtSettings = builder.Configuration.GetSection("Jwt");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) 
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"]!)
            )
        };
    });

// DI
builder.Services.AddScoped<JwtUtil>();
builder.Services.AddScoped<PasswordHashUtil>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductServices, ProductServices>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartServices, CartServices>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentServices, PaymentServices>();
builder.Services.AddScoped<ErrorLogService>();


var app = builder.Build();                     // Build the application
app.UseMiddleware<RequestTimingMiddleware>(); // custom request timing middleware
app.UseMiddleware<ExceptionMiddleware>();    // custom exception handling middleware
if (app.Environment.IsDevelopment())        // Enable Swagger only in development environment
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();           // Redirect HTTP to HTTPS
app.UseStaticFiles();               // Added to serve static files
app.UseCors();                     // Enable CORS
app.UseAuthentication();          // Enable authentication
app.UseAuthorization();          // Enable authorization

app.MapControllers();          // Map controller routes
app.Run();                    // Start the application
