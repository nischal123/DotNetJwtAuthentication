using JwtAuthentication.Data;
using JwtAuthentication.Service;
using JwtAuthentication.ServiceExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJWTAuthentication();

//By default, we can’t send an HTTP request to servers of a different origin due to browser security concerns.
builder.Services.AddCorsSupport();

builder.Services.AddControllers();


builder.Services.AddDbContext<UserContext>(options =>
{
    // sql lite database
    options.UseSqlite(
        builder.Configuration["ConnectionString:UserDBConnectionString"]);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ITokenService, TokenService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("EnableCORS");

//for jwt authentication middleware live in action
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
