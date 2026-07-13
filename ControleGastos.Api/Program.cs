using ControleGastos.Infrastructure.Data;
using ControleGastos.Api.Middlewares; 
using Microsoft.EntityFrameworkCore;
using ControleGastos.Domain.Interfaces;
using ControleGastos.Infrastructure.Repositories;
using ControleGastos.Infrastructure.UnitOfWork;
using ControleGastos.Api.Application.Interface;
using ControleGastos.Api.Services;
using ControleGastos.Api.Converters;

var builder = WebApplication.CreateBuilder(args);


// REGISTRO DE SERVIÇOS (CONTAINER DI)

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


// Repositórios da Aplicação
builder.Services.AddScoped<IPessoaRepository, PessoaRepository>();
builder.Services.AddScoped<ITransacaoRepository, TransacaoRepository>();
builder.Services.AddScoped<IRelatorioRepository, RelatorioRepository>();

// Unit of Work
// Deve obrigatoriamente ser registrado como Scoped para compartilhar a mesma 
// instância do DbContext e dos Repositórios dentro da mesma requisição HTTP.
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Services
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<ITransacaoService, TransacaoService>();
builder.Services.AddScoped<IRelatorioService, RelatorioService>();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //aplica o nosso formatador de 2 casas decimais para TODOS os decimais da API
        options.JsonSerializerOptions.Converters.Add(new DecimalJsonConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

var app = builder.Build();

// CONFIGURAÇÃO DO PIPELINE DE MIDDLEWARES
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