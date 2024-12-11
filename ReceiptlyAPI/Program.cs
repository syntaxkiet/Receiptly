
using ReceiptlyAPI.Controllers;
using Shared.Interface;
using Shared.Models;
using Shared.Service;

namespace ReceiptlyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            
            builder.Services.AddScoped<IReceiptService, ReceiptMockingService>();
            builder.Services.AddScoped<List<Receipt>>(provider =>
            {
                var receiptService = provider.GetRequiredService<IReceiptService>();
                return receiptService.GetAllReceipts() ?? new List<Receipt>();
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IOCRService, TesseractService>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            
            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            
            app.MapControllers();

            app.Run();
        }
    }
}
