using TrucksWebApi.Infrastructure.Interfaces;
using TrucksWebApi.Infrastructure;
using TrucksWebApi.Services.Interfaces;
using TrucksWebApi.Services;
using System.Text.Json.Serialization;
using FluentValidation;
using TrucksWebApi.Models;
using TrucksWebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddSingleton<ITruckRepository, TruckRepository>();
builder.Services.AddScoped<ITruckService, TruckService>();
builder.Services.AddScoped<IValidator<TruckDTO>, TruckValidator>();

builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); 
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
