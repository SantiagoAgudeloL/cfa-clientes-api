using CFA.Clientes.Api.Infrastructure.Configuration;
using CFA.Clientes.Api.Infrastructure.Persistence;
using CFA.Clientes.Api.Infrastructure.Repositories;
using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);


EnvLoader.Load();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddSingleton<DbConnectionFactory>();


builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<ITelefonoRepository, TelefonoRepository>();
builder.Services.AddScoped<IDireccionRepository, DireccionRepository>();




builder.Services.AddScoped<ClienteUseCase>();
builder.Services.AddScoped<TelefonoUseCase>();
builder.Services.AddScoped<DireccionUseCase>();

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();