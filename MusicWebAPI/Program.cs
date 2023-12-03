
using DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MusicWebAPI
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddDbContext<SongContext>();

			//public void ConfigureServices(IServiceCollection services)
			//{
			//	// DbContext'i ekleyerek Scoped olarak ayarla


			//	// Diðer servisleri eklemeye devam et...
			//}

			//var connectionString = builder.Configuration.GetConnectionString("SqLite_Connection");
			//builder.Services.AddDbContext<ProductsContext>(options => options.UseSqlite(connectionString));

			//builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

			//builder.Services.AddDbContext<SongContext>(options =>
			//		options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=songsDbApi;Trusted_Connection=True;"));

			builder.Services.AddDbContext<SongContext>();


			//builder.Services.AddDbContext<SongContext>(options =>
			//		options.UseSqlServer("(localdb)\\MSSQLLocalDB; Initial Catalog=songsDbApi; Integrated Security=SSPI;"));


			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

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
		}
	}
}
