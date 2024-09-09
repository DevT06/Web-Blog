using DataAccess.EFCore;
using DataAccess.EFCore.Repositories;
using Shared.Interfaces;
using Shared.IServices;
using Shared.Services;

namespace WebBlogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<WebBlogDbContext>();

            builder.Services.AddScoped<IBlogRepository, BlogRepository>();
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

            builder.Services.AddScoped<IBlogService, BlogService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            builder.Services.AddControllers();

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("frontendApp", builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:5500")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.UseCors("frontendApp");

            app.Run();
        }
    }
}
