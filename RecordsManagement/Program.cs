using Microsoft.EntityFrameworkCore;
using RecordsManagement.Data;
using RecordsManagement.Extension;
using RecordsManagement.Services;
using RecordsManagement.Utility;

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
builder.Services.AddScoped<IRecordService, RecordService>();

builder.Services.AddHttpClient("Prescription", c => c.BaseAddress = new Uri(builder.Configuration["ServiceUrl:PrescriptionApi"])).AddHttpMessageHandler<HttpInterceptor>();
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
