using Microsoft.EntityFrameworkCore;
using SistemaPedidos.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// --- AJUSTE DE PRODUÇÃO ---
// Tenta pegar a conexão da nuvem (DATABASE_URL), se não existir, usa a local
var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL") 
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (string.IsNullOrEmpty(connectionString))
    {
        // Fallback para memória se você estiver sem banco nenhum configurado
        options.UseInMemoryDatabase("PedidosDB");
    }
    else
    {
        options.UseNpgsql(connectionString);
    }
});

// Configurar o Redis (Apenas se houver conexão configurada)
var redisConfig = builder.Configuration.GetConnectionString("RedisConnection");
if (!string.IsNullOrEmpty(redisConfig))
{
    builder.Services.AddStackExchangeRedisCache(options =>
    {
        options.Configuration = redisConfig;
    });
}
else
{
    builder.Services.AddDistributedMemoryCache(); // Cache em memória caso o Redis esteja offline
}
// ---------------------------

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();

// 3. Configurar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Ativar Swagger (Independente de ser Development ou não, para facilitar seu teste no ngrok)
app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema Pedidos API v1");
    c.RoutePrefix = string.Empty; // Isso faz o Swagger ser a página inicial!
});