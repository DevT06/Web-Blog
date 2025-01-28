using Amazon.Runtime;
using Amazon.S3;
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

			// Configure AWS options
			var awsOptions = builder.Configuration.GetSection("AWS");
			var awsCredentials = new BasicAWSCredentials(awsOptions["AccessKey"], awsOptions["SecretKey"]);
			var awsConfig = new AmazonS3Config
			{
				RegionEndpoint = Amazon.RegionEndpoint.GetBySystemName(awsOptions["Region"])
			};

			builder.Services.AddSingleton<IAmazonS3>(sp => new AmazonS3Client(awsCredentials, awsConfig));
			builder.Services.AddScoped<IS3Service, S3Service>();

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
