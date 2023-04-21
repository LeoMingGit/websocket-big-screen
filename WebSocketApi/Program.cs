using WebSocketApi.Service.Impl;
using WebSocketApi.Service;
using WebSocketApi.Common;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ≥ı ºªØwebsocket
var ws =WebSocketServiceImpl.Instance;
ws.Start("0.0.0.0", 18001); 

builder.Services.AddHttpContextAccessor();
WebAppContext.Services = builder.Services;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
