using LIB.DAL;
using LIB.Managers;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<SpidermanContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB_SPIDERMAN")));

builder.Services.AddScoped<CrimeManager>();
builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<HeroCrimeManager>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Spiderman");
app.Run();
