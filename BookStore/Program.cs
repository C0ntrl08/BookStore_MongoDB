using BookStore.Models;
using BookStore.Services;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.Configure<BookStoreDatabaseSettings>
                (builder.Configuration.GetRequiredSection("BookStoreDatabase"));

            // Registering BooksService with DI as a Singleton
            builder.Services.AddSingleton<BooksService>();

            // Property names in the web API's serialized JSON response match their corresponding property names: Author not author
            builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            // Line below - required only for minimal APIs
            //builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Enable - Swagger Middleware
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
