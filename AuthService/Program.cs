using AuthService.Data;
using AuthService.Models;
using AuthService.Services;
using AuthService.Services.IServices;
using AuthService.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//connect to database
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Register IdentityFramework
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

//Register Services
builder.Services.AddScoped<IJWTSevices, JWTService>();
builder.Services.AddScoped<IUserService, UserServices>();

//AM
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure JWtOptions 
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

var app = builder.Build();

// add all pending migrations
app.UseMigration();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
