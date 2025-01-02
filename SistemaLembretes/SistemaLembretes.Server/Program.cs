using Backend.Configs;
using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configurar os serviços
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=lembretes.db"));

builder.Services.AddScoped<IRepositorioLembretes, RepositorioLembretes>();
builder.Services.AddScoped<IGerenciadorLembretes, GerenciadorLembretes>();
builder.Services.AddControllers();

// Configurar o pipeline de requisição
var app = builder.Build();

app.MapControllers();

app.Run();
