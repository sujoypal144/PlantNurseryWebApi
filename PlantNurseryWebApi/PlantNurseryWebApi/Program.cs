using Microsoft.EntityFrameworkCore;
using PlantNurseryWebApi.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PlantNurseryDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection"))); //GetConnectionString is mapped to "ConnectionStrings" in appsettings.Development.json


// Register PlantsData and PurchasesData services
builder.Services.AddScoped<PlantsData>();
builder.Services.AddScoped<PurchasesData>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapGet("/", () => "Welcome to the PlantNurseryWebApi !!!");

app.MapControllers();

app.Run();
