using Microsoft.EntityFrameworkCore;
using ReceiptlyAPI.Data;
using Shared.Models;

namespace ReceiptlyAPI.Services;

public class DbAccess

{
    private readonly ReceiptlyDbContext _context;

    public DbAccess(ReceiptlyDbContext context)
    {
        _context = context;
    }

    public async Task<List<Receipt>?> GetReceiptsAsync()
    {
        return await _context.Receipts.ToListAsync();
    }

    public async Task<Receipt?> GetReceiptByIdAsync(int id)
    {
        return await _context.Receipts.FindAsync(id);
    }

    public async Task AddOrUpdateReceiptAsync(List<Receipt> receipts)
    {
        foreach (var receipt in receipts)
        {
            var existingReceipt = await _context.Receipts.FindAsync(receipt.Id);
            if(existingReceipt == null)
            {
                _context.Receipts.Add(receipt);
            }
            else
            {
                existingReceipt.PurchaseDate = receipt.PurchaseDate;
                existingReceipt.StoreName = receipt.StoreName;
                existingReceipt.Items = receipt.Items;
                _context.Receipts.Update(existingReceipt);
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task DeleteReceiptAsync(int id)
    {
        var receipt = await _context.Receipts.FindAsync(id);
        if (receipt != null)
        {
            _context.Receipts.Remove(receipt);
            await _context.SaveChangesAsync();
        }
    }
}