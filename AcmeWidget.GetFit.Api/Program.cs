using System.Text.Json.Serialization;
using AcmeWidget.GetFit.Api;
using AcmeWidget.GetFit.Api.ServiceCollectionExtensions;
using AcmeWidget.GetFit.Application;
using AcmeWidget.GetFit.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddApplicationDependencies();
builder.Services.AddDataDependencies(builder.Configuration.GetConnectionString("GetFit"));

builder.Services.AddSingleton<ErrorResponseBuilder>();

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

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<GetFitDbContext>();
dbContext?.Database.Migrate();

app.Run();