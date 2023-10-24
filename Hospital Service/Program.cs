using Hospital_Service.Data;
using Hospital_Service.Extensions;
using Hospital_Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connection to DB
builder.Services.AddDbContext<AppDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//AddAutomapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Services
builder.Services.AddScoped<IHospitalService, HospitalService>();

//custom builders
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseMigration();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
