
using Shared.Interface;
using Shared.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using ReceiptlyAPI.Data;
using Shared.Models;
using Receiptly;
namespace ReceiptlyAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();
            builder.Services.AddDbContext<ReceiptlyDbContext>(options => options.UseSqlite("Data Source=:memory:"));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IOCRService, TesseractService>();
            var app = builder.Build();
        
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var _context = services.GetRequiredService<ReceiptlyDbContext>();
    
                _context.Database.OpenConnection();
                _context.Database.EnsureCreated();
    
                _context.Users.Add(new Item { Id = 1, Name = "Mjölk", Quantity = 2, BestBeforeDate = DateTime.Now.AddDays(1)});
                _context.Receipts.Add(new Receipt { Id = 4, StoreName = "Hemköp", PurchaseDate = DateTime.Now.AddDays(5)});
                _context.Receipts.Add(new Receipt {Id = 2, StoreName = "Hemköp", PurchaseDate = DateTime.Now.AddDays(4)});
                _context.Receipts.Add(new Receipt { Id = 3,StoreName = "Hemköp", PurchaseDate = DateTime.Now.AddDays(2)});
                _context.Receipts.Add(new Receipt {Id = 5,  StoreName = "Hemköp", PurchaseDate = DateTime.Now.AddDays(3)});
                _context.SaveChanges();
            }
            app.Run();
        }
    }
}
