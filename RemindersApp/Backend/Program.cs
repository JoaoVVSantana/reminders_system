
using Backend.Configs;
using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            
            builder.Services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlite("Data Source=lembretes.db"));

            builder.Services.AddScoped<IRepositorioLembretes, RepositorioLembretes>();
            builder.Services.AddScoped<IGerenciadorLembretes, GerenciadorLembretes>();
            builder.Services.AddControllers();

            var app = builder.Build();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();

        }
    }
}
