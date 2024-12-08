using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Options;
using Shared.Models;
using System.Security.Policy;

namespace ReceiptlyAPI.Data;

public class ReceiptlyDbContext : DbContext
{
    public DbSet<Item> Users { get; set; }
    public DbSet<Receipt> Receipts { get; set; }

    public ReceiptlyDbContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = path;
        
    }
    public string DbPath { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //optionsBuilder.UseSqlite($"Data Source={DbPath}");
        optionsBuilder.UseSqlite("Data Source=Receipts.db");
    }
}