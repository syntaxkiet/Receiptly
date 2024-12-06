
using Shared.Interface;
using Shared.Service;

namespace ReceiptlyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IOCRService, TesseractService>();
            builder.Services.AddControllers()
                            .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            //ToDo Add Cors for production environment
#if DEBUG
            builder.Services.AddCors(options =>
                            {
                                options.AddDefaultPolicy(policy =>
                                {
                                    policy.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader();
                                });
                            });
#endif
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //Enable CORS
            app.UseCors();

            // Other middleware
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}
