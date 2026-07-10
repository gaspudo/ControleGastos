using ControleGastos.Infrastructure.Data;
using ControleGastos.Api.Middlewares; 
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// 1. REGISTRO DE SERVIÇOS (CONTAINER DI)

builder.Services.AddDbContext<Context>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    ));

builder.Services.AddCors(options =>
{
    options.AddPolicy("meuReact", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// 2. CONFIGURAÇÃO DO PIPELINE DE MIDDLEWARES

// O Middleware de erro deve ser o primeiro para conseguir capturar as falhas 
// que acontecerem em qualquer componente depois dele 

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("meuReact");

app.MapControllers();

app.Run();