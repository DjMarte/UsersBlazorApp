using Microsoft.EntityFrameworkCore;
using UserBlazorApp.API.Services;
using UsersBlazorApp.Data.Context;
using UsersBlazorApp.Data.Interfaces;
using UsersBlazorApp.Data.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<UsersDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("ConStr")));

builder.Services.AddScoped<IApiService<AspNetUsers>, UserService>();
builder.Services.AddScoped<IApiService<AspNetRoleClaims>, RoleClaimService>();
builder.Services.AddScoped<IApiService<AspNetUserClaims>, UserClaimService>();
builder.Services.AddScoped<IApiService<AspNetRoles>, RoleService>();

builder.Services.AddCors(op => {
    op.AddPolicy("AllowAnyOrigin", 
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAnyOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
