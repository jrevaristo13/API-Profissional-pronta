using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- 1. CONFIGURAÇÃO DE BANCO DE DADOS ---
// Ele vai tentar pegar do Ambiente (Nuvem) ou do appsettings (Local)
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    // UseNpgsql é necessário para conectar ao Neon.tech
    options.UseNpgsql(connectionString);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- 2. CONFIGURAÇÃO DO SWAGGER ---
// Removi o 'if' para o Swagger funcionar sempre, inclusive no ngrok
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Pedidos API v1");
    c.RoutePrefix = string.Empty; // Swagger será a página principal
});

app.UseAuthorization();
app.MapControllers();

app.Run();