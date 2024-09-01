
using ApplicationLayer;
using DomainLayer;
using InfrastructureLayer;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace dotNet_Cms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOptions();
            builder.Services.Configure<SqlDatabaseConnectionModel>(configuration.GetSection("DatabaseSetting"));
            builder.Services.AddSingleton<IDatabaseConnection, SqlDatabaseConnection>();
            builder.Services.AddSingleton<IBlogRepository, BlogRepository>();
            builder.Services.AddSingleton<IBlogService, BlogService>();

            WebApplication app = builder.Build();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }



            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
