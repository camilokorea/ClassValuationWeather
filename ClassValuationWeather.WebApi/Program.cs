using ClassValuationWeather.Application.Interfaces;
using ClassValuationWeather.Infrastructure.Repositories;
using ClassValuationWeather.Application.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

var dataRepository = new DataRepository(connectionString);
var openMeteoRepository = new OpenMeteoRepository();

builder.Services.AddScoped<IDataRepository>(provider => dataRepository);
builder.Services.AddScoped<IOpenMeteoRepository>(provider => openMeteoRepository);
builder.Services.AddScoped<IMeteoService>(provider => new MeteoService(dataRepository, openMeteoRepository));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
