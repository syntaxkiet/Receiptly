
using Shared.Interface;
using Shared.Service.ReceiptParser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using ReceiptlyAPI.Data;
using Shared.Models;
using Receiptly;
using ReceiptlyAPI.Services;
using Shared.Service;
using Shared.Service.Ocr.Tesseract;
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
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddDbContext<ReceiptlyDbContext>();
            builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        // Example: Configuring settings
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IOCRService, TesseractService>();
            builder.Services.AddScoped<IReceiptStringParser, ReceiptParser>();
            builder.Services.AddDbContext<ReceiptlyDbContext>();
            builder.Services.AddScoped<DbAccess>();

            //ToDo Set up proper CORS policies before moving to production
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
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseCors("AllowAll");
            app.UseAuthorization();
            
            app.MapControllers();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            // using (var scope = app.Services.CreateScope())
            // {
            //     var context = scope.ServiceProvider.GetRequiredService<ReceiptlyDbContext>();
            //     context.Database.Migrate();
            // }

            app.Run();
        }
    }
}
