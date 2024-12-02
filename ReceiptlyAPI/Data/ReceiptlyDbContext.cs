using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

using Shared.Models;

namespace ReceiptlyAPI.Data;

public class ReceiptlyDbContext : DbContext
{
    public DbSet<Item> Users { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    public ReceiptlyDbContext(DbContextOptions<ReceiptlyDbContext> options) :base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Item>().HasKey(x => x.Id);
        modelBuilder.Entity<Receipt>().HasKey(i => i.Id);
        // modelBuilder.Entity<Receipt>().HasData(new Receipt
        // {  
        //     Id = 1,
        //     Amount = 100.0m, DueDate = DateTime.Now.AddDays(5),
        // });
        
        // Seed data
        // Users.Add(new User { Name = "John Doe", Email = "john@example.com" });
        // Receipts.Add(new Receipt { Amount = 100.0m, DueDate = DateTime.Now.AddDays(5)});
        // Receipts.Add(new Receipt { Amount = 150.0m, DueDate = DateTime.Now.AddDays(4)});
        // Receipts.Add(new Receipt { Amount = 200.0m, DueDate = DateTime.Now.AddDays(2)});
        // Receipts.Add(new Receipt { Amount = 300.0m, DueDate = DateTime.Now.AddDays(3)});
    }
}