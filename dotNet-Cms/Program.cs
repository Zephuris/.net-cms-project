using ApplicationLayer.Blog;
using ApplicationLayer.User;
using DomainLayer.Database;
using DomainLayer.Users;
using InfrastructureLayer.BlogRepository;
using InfrastructureLayer.Database;
using InfrastructureLayer.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

namespace dotNet_Cms
{
    public class Program
    {
        public static void Main(string[] args)
      {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                           options =>
                           {
                               options.TokenValidationParameters = new TokenValidationParameters()
                               {
                                   ValidateIssuer = false,
                                   ValidateAudience = false,
                                   ValidateLifetime = true,
                                   ValidateIssuerSigningKey = true,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("JWT:Key").Value)),
                                   ClockSkew = TimeSpan.Zero

                               };
                           });
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddOptions();
            builder.Services.Configure<SqlDatabaseConnectionModel>(configuration.GetSection("DatabaseSetting"));
            builder.Services.Configure<JwtConfigModel>(configuration.GetSection("JWT"));

            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IUserService, UserService>();

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
