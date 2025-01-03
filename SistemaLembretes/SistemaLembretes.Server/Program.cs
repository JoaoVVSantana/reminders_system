using Backend.Configs;
using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlite("Data Source=lembretes.db"));
builder.WebHost.UseUrls("http://localhost:5000");
builder.Services.AddScoped<IRepositorioLembretes, RepositorioLembretes>();
builder.Services.AddScoped<IGerenciadorLembretes, GerenciadorLembretes>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
