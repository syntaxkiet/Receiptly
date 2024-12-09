using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Options;
using Shared.Models;
using System.Security.Policy;

namespace ReceiptlyAPI.Data;

public class ReceiptlyDbContext : DbContext
{
    public DbSet<Item> ReceiptItems { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    public ReceiptlyDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Combine(path, "ReceiptlyDatabase.sqlite");      
    }
    
    public string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure one-to-many relationship
        modelBuilder.Entity<Receipt>()
            .HasMany(r => r.Items)
            .WithOne(i => i.Receipt)
            .HasForeignKey(i => i.ReceiptId);
    }
}

// Db browser for SQL-lite https://sqlitebrowser.org/dl/
//DB file found at C:\Users\YOURCURRENTUSER\AppData\Local\ReceiptlyDatabase.sqlite
// If migrations result in PRAGMA error you can delete db files and run project to apply last migration. Any content will be deleted though