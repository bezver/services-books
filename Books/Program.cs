using Microsoft.EntityFrameworkCore;
using Books.DAL.Contexts;
using Books.DAL.Repositories;
using Books.DAL.Services;
using Books.Domain.Models;
using Books.Middleware;

namespace Books
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddControllers();

			builder.Services.AddScoped<IBookService, BookService>();
			builder.Services.AddScoped<IRepository<Book, Guid>, Repository<Book, Guid>>();

			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddDbContext<Context>(options => options.UseSqlServer(connectionString));

			var app = builder.Build();

			using (var scope = app.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider.GetRequiredService<Context>();
				//serviceProvider.Database.EnsureDeleted();
				serviceProvider.Database.EnsureCreated();
			}

			app.UseMiddleware<ExceptionHandler>();
			app.UseHttpsRedirection();
			app.MapControllers();
			app.UseRouting();
			app.Run();
		}
	}
}
