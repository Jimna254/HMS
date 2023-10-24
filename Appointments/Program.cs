using Appointments.Data;
using Appointments.Extensions;
using Appointments.Services;
using Appointments.Services.Interface;
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

// Register Services
builder.Services.AddScoped<IAppointmentService, AppointmentService>();


//Add Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


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
