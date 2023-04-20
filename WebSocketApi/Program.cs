using WebSocketApi.Service.Impl;
using WebSocketApi.Service;
using WebSocketApi.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region  业务service的注入


builder.Services.AddSingleton<IWebSocketService, WebSocketServiceImpl>();  //单例注入
var provider = builder.Services.BuildServiceProvider();
IWebSocketService ws = provider.GetService<IWebSocketService>();
ws.Start("0.0.0.0", 18001); // 初始化websocket

#endregion

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
