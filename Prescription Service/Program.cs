using Microsoft.EntityFrameworkCore;
using Prescription_Service.Data;
using Prescription_Service.Extension;
using Prescription_Service.Service;

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

// Register Service
builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();

//custom builders 
builder.AddSwaggenGenExtension();
builder.AddAppAuthentication();
var app = builder.Build();

//Register for Stripe
Stripe.StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:Key").Get<string>();
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
